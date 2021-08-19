using System;
using System.Net.Http;
using System.Net.WebSockets;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Newtonsoft.Json;
using NLog;
using PoissonSoft.BinanceApi.Contracts.Serialization;
using PoissonSoft.BinanceApi.Contracts.UserDataStream;
using PoissonSoft.BinanceApi.Transport;
using PoissonSoft.KuCoinApi.Contracts.DataStream;
using PoissonSoft.KuCoinApi.Contracts.Exceptions;
using PoissonSoft.KuCoinApi.Contracts.Serialization;
using PoissonSoft.KuCoinApi.Contracts.UserDataStream;
using PoissonSoft.KuCoinApi.Transport.Rest;
using PoissonSoft.KuCoinApi.Utils;
using Timer = System.Timers.Timer;

namespace PoissonSoft.KuCoinApi.Transport.Ws
{
    internal sealed class WebSocketStreamListener: IDisposable
    {
        private readonly KuCoinApiClient apiClient;
        private readonly ILogger logger;
        private readonly KuCoinApiClientCredentials credentials;

        private ClientWebSocket client;
        private readonly object clientCreationSync = new object();
        private CancellationTokenSource cancellationTokenSource;
        private TaskCompletionSource<object> socketFinishedTcs;

        private readonly RestClient Restclient;

        public string Endpoint { get; private set; }
        public WebSocketState? State => client?.State;

        public event EventHandler OnConnected; 
        public event EventHandler<(WebSocketCloseStatus? CloseStatus, string CloseStatusDescription)> OnConnectionClosed;
        public event EventHandler<string> OnMessage;

        private readonly JsonSerializerSettings serializerSettings;
        public DataStreamStatus Status { get; protected set; }
        private string token;
        private string WsBaseEndpoint { get; set; }
        private Timer pingTimer;
        private TimeSpan reconnectTimeout = TimeSpan.Zero;
        private readonly string userFriendlyName = nameof(WebSocketStreamListener);

        public DataStreamStatus WsConnectionStatus { get; private set; } = DataStreamStatus.Closed;


        public WebSocketStreamListener(KuCoinApiClient apiClient, KuCoinApiClientCredentials credentials)
        {
            this.apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            logger = apiClient.Logger;
            this.credentials = credentials ?? throw new ArgumentNullException(nameof(credentials));

            Restclient = new RestClient(apiClient.Logger, "https://api.kucoin.com/api/v1",
                new[] { EndpointSecurityType.UserStream }, credentials,
                apiClient.Throttler);

            this.apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            this.credentials = credentials ?? throw new ArgumentNullException(nameof(credentials));
            Status = DataStreamStatus.Closed;


            serializerSettings = new JsonSerializerSettings
            {
                Context = new StreamingContext(StreamingContextStates.All,
                    new SerializationContext { Logger = apiClient.Logger }),
                ContractResolver = new CaseSensitiveContractResolver(),
            };
        }
        
        public void Close()
        {
            lock (clientCreationSync)
            {
                if (client == null) return;

                Endpoint = null;
                cancellationTokenSource.Cancel();
                client.Abort();
                socketFinishedTcs.Task.Wait();
                client.Dispose();
                cancellationTokenSource?.Dispose();
            }

        }

        public async Task StartListening()
        {
            var buffer = new byte[1024 * 4];
            var closeStatus = WebSocketCloseStatus.NormalClosure;
            var closeStatusDescription = string.Empty;

            while (!cancellationTokenSource.IsCancellationRequested)
            {
                try
                {
                    var msg = await client.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationTokenSource.Token);
                    if (msg.CloseStatus.HasValue)
                    {
                        closeStatus = msg.CloseStatus.Value;
                        closeStatusDescription = msg.CloseStatusDescription;
                        break;
                    }

                    if (msg.MessageType == WebSocketMessageType.Close)
                    {
                        closeStatus = WebSocketCloseStatus.NormalClosure;
                        closeStatusDescription = "Connection closed by client";
                        break;
                    }

                    if (!msg.EndOfMessage)
                    {
                        closeStatus = WebSocketCloseStatus.MessageTooBig;
                        closeStatusDescription = "Message too big";
                        break;
                    }

                    if (msg.MessageType != WebSocketMessageType.Text)
                    {
                        logger.Warn($"Unsupported type of client message ({msg.MessageType}) was ignored");
                        continue;
                    }

                    // Обработка сообщений от сервера
                    try
                    {
                        var msgContent = Encoding.UTF8.GetString(buffer, 0, msg.Count);
                        ProcessServerMessage(msgContent);
                    }
                    catch (Exception e)
                    {
                        logger.Error($"Исключение при обработке сообщения от клиента\n{e}");
                    }

                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception e)
                {
                    closeStatus = WebSocketCloseStatus.InternalServerError;
                    logger.Error($"Разрыв WebSocket-соединения в связи с исключение\n{e}");
                    break;
                }

            }

            if (client.State != WebSocketState.Aborted)
            {
                try
                {
                    await client.CloseAsync(closeStatus, closeStatusDescription, CancellationToken.None);
                }
                catch
                {
                    // ignore
                }
            }

            OnConnectionClosed?.Invoke(this, (client.CloseStatus, client.CloseStatusDescription));

            socketFinishedTcs.SetResult(null);
        }

        public void SendMessage(string msg)
        {
            apiClient.Throttler.ThrottleWs(1);

            var data = new ArraySegment<byte>(Encoding.UTF8.GetBytes(msg));
            client.SendAsync(data, WebSocketMessageType.Text, true, CancellationToken.None);
        }

        private void ProcessServerMessage(string msg)
        {
            OnMessage?.Invoke(this, msg);
        }


        /// <inheritdoc />
        private string GetToken()
        {
            var response =
                Restclient.MakeRequest<CreateListenKeyResponse>(
                    new RequestParameters(HttpMethod.Post, "bullet-public", 0));
            WsBaseEndpoint = response.Data.InstanceData[0].Endpoint;
            if (string.IsNullOrWhiteSpace(response.Data.Token))
                throw new Exception("Server returned empty token");

            return response.Data.Token;
        }

        public void Open()
        {
            if (Status != DataStreamStatus.Closed) return;
            Status = DataStreamStatus.Connecting;

            try
            {
                token = GetToken();
            }
            catch (Exception e)
            {
                apiClient.Logger.Error($"{userFriendlyName}. Can not create Listen Key. Exception:\n{e}");
                Status = DataStreamStatus.Closed;
                return;
            }

            //pingTimer = new Timer(TimeSpan.FromMinutes(30).TotalMilliseconds) { AutoReset = true };
            //pingTimer.Elapsed += OnPingTimer;
            //pingTimer.Enabled = true;

            //OnConnected += OnConnectToStream;
            //OnConnectionClosed += OnDisconnect;
            //OnMessage += OnStreamMessage;
            TryConnectToWebSocket(token);
        }

        //private void OnConnectToWs(object sender, EventArgs e)
        //{
        //    WsConnectionStatus = DataStreamStatus.Active;
        //    reconnectTimeout = TimeSpan.Zero;
        //    apiClient.Logger.Info($"{userFriendlyName}. Successfully connected to stream!");
        //}

        //private void OnConnectToStream(object sender, EventArgs e)
        //{
        //    Status = DataStreamStatus.Active;
        //    reconnectTimeout = TimeSpan.Zero;
        //    //apiClient.Logger.Info($"{userFriendlyName}. Successfully connected!");
        //}


        //private void OnDisconnect(object sender, (WebSocketCloseStatus? CloseStatus, string CloseStatusDescription) e)
        //{
        //    if (disposed || Status == DataStreamStatus.Closing) return;
        //    Status = DataStreamStatus.Reconnecting;
        //    if (reconnectTimeout.TotalSeconds < 15) reconnectTimeout += TimeSpan.FromSeconds(1);
        //    //apiClient.Logger.Error($"{userFriendlyName}. WebSocket was disconnected. Try reconnect again after {reconnectTimeout}.");
        //    Task.Run(() =>
        //    {
        //        Task.Delay(reconnectTimeout);
        //        TryConnectToWebSocket(token);
        //    });
        //}

        private bool disposed;
        private void TryConnectToWebSocket(string token)
        {
            while (true)
            {
                if (disposed) return;
                try
                {
                    Connect($"{WsBaseEndpoint}?token={token}&[connectId={GenerateUniqueId()}]");
                    return;
                }
                catch (Exception e)
                {
                    if (reconnectTimeout.TotalSeconds < 15) reconnectTimeout += TimeSpan.FromSeconds(1);
                    apiClient.Logger.Error($"{userFriendlyName}. WebSocket connection failed. Try again after {reconnectTimeout}. Exception:\n{e}");
                    Thread.Sleep(reconnectTimeout);
                }
            }
        }

         public void Connect(string url)
         {
            Close();

            lock (clientCreationSync)
            {
                client = new ClientWebSocket();
                client.Options.Proxy = ProxyHelper.CreateProxy(credentials);
                cancellationTokenSource = new CancellationTokenSource();
                socketFinishedTcs = new TaskCompletionSource<object>();
                try
                {
                    var t = client.ConnectAsync(new Uri(url), cancellationTokenSource.Token);
                    t.Wait();
                }
                catch
                {
                    socketFinishedTcs.SetResult(null);
                    throw;
                }
                OnConnected?.Invoke(this, EventArgs.Empty);
                _ = StartListening();
            }
        }

        //private void OnStreamMessage(object sender, string message)
        //{
        //    Task.Run(() =>
        //    {
        //        try
        //        {
        //            ProcessStreamMessage(message);
        //        }
        //        catch (Exception e)
        //        {
        //            //apiClient.Logger.Error($"{userFriendlyName}. Exception when processing payload.\n" +
        //            //                       $"Message: {message}\n" +
        //            //                       $"Exception: {e}");
        //        }
        //    });
        //}

        //private void ProcessStreamMessage(string message)
        //{
        //    if (apiClient.IsDebug)
        //    {
        //        //apiClient.Logger.Debug($"{userFriendlyName}. New payload received:\n{message}");
        //    }

        //    var baseMsg = JsonConvert.DeserializeObject<PayloadBase>(message, serializerSettings);
        //    switch (baseMsg?.EventType)
        //    {
        //        // outboundAccountPosition is sent any time an account balance has changed and contains the assets
        //        // that were possibly changed by the event that generated the balance change.
        //        case "outboundAccountPosition":
        //            OnAccountUpdate?.Invoke(this, JsonConvert.DeserializeObject<AccountUpdatePayload>(message, serializerSettings));
        //            break;

        //        // outboundAccountInfo has been deprecated and will be removed in the future.
        //        // It is recommended to use outboundAccountPosition instead. 
        //        case "outboundAccountInfo":
        //            // Ignore because this event is deprecated
        //            break;

        //        // Balance Update occurs during the following:
        //        //   - Deposits or withdrawals from the account
        //        //   - Transfer of funds between accounts(e.g.Spot to Margin)
        //        case "balanceUpdate":
        //            OnBalanceUpdate?.Invoke(this, JsonConvert.DeserializeObject<BalanceUpdatePayload>(message, serializerSettings));
        //            break;

        //        // Событие, информирующее об изменении ордера
        //        case "executionReport":
        //            OnOrderExecuteEvent?.Invoke(this, JsonConvert.DeserializeObject<OrderExecutionReportPayload>(message, serializerSettings));
        //            break;

        //        // If the order is an OCO, an event will be displayed named ListStatus in addition to the executionReport event.
        //        case "listStatus":
        //            OnOrderListStatusEvent?.Invoke(this, JsonConvert.DeserializeObject<OrderListStatusPayload>(message, serializerSettings));
        //            break;

        //        default:
        //            //apiClient.Logger.Error($"{userFriendlyName}. Unknown payload Event Type '{baseMsg?.EventType}'\n" +
        //            //  $"Message: {message}");
        //            break;

        //    }
        //}
        private long lastId;
        private long GenerateUniqueId()
        {
            return Interlocked.Increment(ref lastId);
        }

        //private void OnPingTimer(object sender, ElapsedEventArgs e)
        //{
        //    try
        //    {
        //        // KeepAliveListenKey(listenKey);
        //    }
        //    catch (EndpointCommunicationException ece) when (ece.Message.ToUpperInvariant().Contains("This listenKey does not exist".ToUpperInvariant()))
        //    {
        //        //apiClient.Logger.Error($"{userFriendlyName}. An obsolete listenKey has been detected. It will be reconnected with a new listenKey. Exception details:\n{ece}");
        //        Task.Run(ReconnectWithNewListenKey);
        //    }
        //    catch (Exception ex)
        //    {
        //        //apiClient.Logger.Error($"{userFriendlyName}. Exception when send ping to Listen Key:\n{ex}");
        //    }
        //}

        /// <summary>
        /// Reconnection with new listenKey
        /// </summary>
        //protected void ReconnectWithNewListenKey()
        //{
        //    if (disposed) return;
        //    try
        //    {
        //        Close();
        //    }
        //    catch (Exception e)
        //    {
        //        //apiClient.Logger.Error($"{userFriendlyName}. {nameof(ReconnectWithNewListenKey)}. Exception when calling {nameof(Close)} method:\n{e}");
        //    }
        //    Status = DataStreamStatus.Closed;

        //    while (Status == DataStreamStatus.Closed)
        //    {
        //        if (disposed) return;
        //        try
        //        {
        //            Open();
        //        }
        //        catch (Exception e)
        //        {
        //            //apiClient.Logger.Error($"{userFriendlyName}. {nameof(ReconnectWithNewListenKey)}. Exception when calling {nameof(Open)} method:\n{e}");
        //            Thread.Sleep(TimeSpan.FromSeconds(10));
        //        }
        //    }
        //}

        public void Dispose()
        {
            Close();
        }


    }
}
