using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Newtonsoft.Json;
using PoissonSoft.BinanceApi.Contracts.Serialization;
using PoissonSoft.KuCoinApi.Contracts.DataStream;
using PoissonSoft.KuCoinApi.Contracts.Exceptions;
using PoissonSoft.KuCoinApi.Contracts.PublicWebSocket;
using PoissonSoft.KuCoinApi.Contracts.Serialization;
using PoissonSoft.KuCoinApi.Transport;
using PoissonSoft.KuCoinApi.Transport.Rest;
using PoissonSoft.KuCoinApi.Transport.Ws;
using Timer = System.Timers.Timer;

namespace PoissonSoft.KuCoinApi.DataStream
{
    public class UserDataStream : IUserDataStream
    {
        //private const string WS_BASE_ENDPOINT = "wss://stream.binance.com:9443";

        private readonly KuCoinApiClient apiClient;
        private readonly KuCoinApiClientCredentials credentials;
        private readonly string userFriendlyName = nameof(KuCoinApi.DataStream);
        private readonly RestClient RestClient;

        private string listenKey;
        private Timer pingTimer;
        private string token;
        private string WsBaseEndpoint { get; set; }
        private WebSocketStreamListener streamListener;
        private TimeSpan reconnectTimeout = TimeSpan.Zero;
        private readonly JsonSerializerSettings serializerSettings;

        /// <summary>
        /// Создание экземпляра
        /// </summary>
        public UserDataStream(KuCoinApiClient apiClient, KuCoinApiClientCredentials credentials)
        {
            this.apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            this.credentials = credentials ?? throw new ArgumentNullException(nameof(credentials));
            Status = DataStreamStatus.Closed;

            RestClient = new RestClient(apiClient.Logger, "https://openapi-sandbox.kucoin.com/api",
            //RestClient = new RestClient(apiClient.Logger, "https://api.kucoin.com/api/",
                new[] { EndpointSecurityType.UserStream }, credentials,
                apiClient.Throttler);


            serializerSettings = new JsonSerializerSettings
            {
                Context = new StreamingContext(StreamingContextStates.All,
                    new SerializationContext { Logger = apiClient.Logger }),
                ContractResolver = new CaseSensitiveContractResolver(),
            };
        }


        /// <inheritdoc />
        //public UserDataStreamType StreamType { get; protected set; }

        /// <inheritdoc />
        public string Symbol { get; protected set; }

        /// <inheritdoc />
        public DataStreamStatus Status { get; protected set; }

        /// <inheritdoc />
        public bool NeedCloseListenKeyOnClosing { get; set; } = true;

        /// <inheritdoc />
        public event EventHandler<AccountBalanceUpdatePayload> OnAccountUpdate;

        /// <inheritdoc />
        public event EventHandler<BalanceUpdatePayload> OnBalanceUpdate;

        /// <inheritdoc />
        public event EventHandler<OrderExecutionReportPayload> OnOrderExecuteEvent;

        /// <inheritdoc />
        public event EventHandler<OrderListStatusPayload> OnOrderListStatusEvent;

        /// <inheritdoc />
        private string GetToken()
        {
            var response =
                RestClient.MakeRequest<CreateListenKeyResponse>(
                    new RequestParameters(HttpMethod.Post, "v1/bullet-public", 0));
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

            pingTimer = new Timer(TimeSpan.FromMinutes(1).TotalMilliseconds) { AutoReset = true };
            pingTimer.Elapsed += OnPingTimer;
            pingTimer.Enabled = true;
            streamListener = new WebSocketStreamListener(apiClient, credentials);
            streamListener.OnConnected += OnConnectToStream;
            //OnConnectionClosed += OnDisconnect;
            streamListener.OnMessage += OnStreamMessage;
            TryConnectToWebSocket(token);
        }

        /// <inheritdoc />
        public void Close()
        {
            if (Status == DataStreamStatus.Closed) return;

            Status = DataStreamStatus.Closing;

            if (streamListener != null)
            {
                try
                {
                    streamListener.OnConnected -= OnConnectToStream;
                   // streamListener.OnConnectionClosed -= OnDisconnect;
                    streamListener.OnMessage -= OnStreamMessage;
                }
                catch { /*ignore*/ }
                streamListener.Dispose();
                streamListener = null;
            }

            if (pingTimer != null)
            {
                pingTimer.Enabled = false;
                try
                {
                    pingTimer.Elapsed -= OnPingTimer;
                }
                catch { /*ignore*/ }
                pingTimer.Dispose();
                pingTimer = null;
            }

            try
            {
               // if (NeedCloseListenKeyOnClosing) CloseListenKey(listenKey);
            }
            catch (Exception e)
            {
                apiClient.Logger.Error($"{userFriendlyName}. Exception when closing Listen Key:\n{e}");
            }

            apiClient.Logger.Info($"{userFriendlyName}. Connection closed");
            Status = DataStreamStatus.Closed;
        }

        private void OnPingTimer(object sender, ElapsedEventArgs e)
        {
            try
            {
                KeepAliveToken(token);
            }
            catch (EndpointCommunicationException ece) when (ece.Message.ToUpperInvariant().Contains("This listenKey does not exist".ToUpperInvariant()))
            {
                apiClient.Logger.Error($"{userFriendlyName}. An obsolete listenKey has been detected. It will be reconnected with a new listenKey. Exception details:\n{ece}");
                Task.Run(ReconnectWithNewToken);
            }
            catch (Exception ex)
            {
                apiClient.Logger.Error($"{userFriendlyName}. Exception when send ping to Listen Key:\n{ex}");
            }
        }


        /// <inheritdoc />
        protected void KeepAliveToken(string key)
        {
            RestClient.MakeRequest<EmptyResponse>(
                new RequestParameters(HttpMethod.Post, "v1/bullet-public", 0)
                {
                    Parameters = new Dictionary<string, string>
                    {
                        ["id"] = GenerateUniqueId().ToString(),
                        ["type"] = "ping"
                    },
                    PassAllParametersInQueryString = true
                });
        }

        /// <summary>
        /// Reconnection with new token
        /// </summary>
        protected void ReconnectWithNewToken()
        {
            if (disposed) return;
            try
            {
                Close();
            }
            catch (Exception e)
            {
                apiClient.Logger.Error($"{userFriendlyName}. {nameof(ReconnectWithNewToken)}. Exception when calling {nameof(Close)} method:\n{e}");
            }
            Status = DataStreamStatus.Closed;

            while (Status == DataStreamStatus.Closed)
            {
                if (disposed) return;
                try
                {
                    Open();
                }
                catch (Exception e)
                {
                    apiClient.Logger.Error($"{userFriendlyName}. {nameof(ReconnectWithNewToken)}. Exception when calling {nameof(Open)} method:\n{e}");
                    Thread.Sleep(TimeSpan.FromSeconds(10));
                }
            }
        }


        private long lastId;
        private long GenerateUniqueId()
        {
            return Interlocked.Increment(ref lastId);
        }

        
        private void TryConnectToWebSocket(string token)
        {
            while (true)
            {
                if (disposed) return;
                try
                {
                    streamListener.Connect($"{WsBaseEndpoint}?token={token}&[connectId={GenerateUniqueId()}]");
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

        #region [Payload processing]

        private void OnStreamMessage(object sender, string message)
        {
            Task.Run(() =>
            {
                try
                {
                    ProcessStreamMessage(message);
                }
                catch (Exception e)
                {
                    apiClient.Logger.Error($"{userFriendlyName}. Exception when processing payload.\n" +
                                 $"Message: {message}\n" +
                                 $"Exception: {e}");
                }
            });
        }

        private void ProcessStreamMessage(string message)
        {
            if (apiClient.IsDebug)
            {
                apiClient.Logger.Debug($"{userFriendlyName}. New payload received:\n{message}");
            }

            var baseMsg = JsonConvert.DeserializeObject<PayloadBase>(message, serializerSettings);
            switch (baseMsg?.EventType)
            {
                // outboundAccountPosition is sent any time an account balance has changed and contains the assets
                // that were possibly changed by the event that generated the balance change.
                case "account.balance":
                    OnAccountUpdate?.Invoke(this, JsonConvert.DeserializeObject<AccountBalanceUpdatePayload>(message, serializerSettings));
                    break;

                // outboundAccountInfo has been deprecated and will be removed in the future.
                // It is recommended to use outboundAccountPosition instead. 
                case "outboundAccountInfo":
                    // Ignore because this event is deprecated
                    break;

                // Balance Update occurs during the following:
                //   - Deposits or withdrawals from the account
                //   - Transfer of funds between accounts(e.g.Spot to Margin)
                case "balanceUpdate":
                    OnBalanceUpdate?.Invoke(this, JsonConvert.DeserializeObject<BalanceUpdatePayload>(message, serializerSettings));
                    break;

                // Событие, информирующее об изменении ордера
                case "executionReport":
                    OnOrderExecuteEvent?.Invoke(this, JsonConvert.DeserializeObject<OrderExecutionReportPayload>(message, serializerSettings));
                    break;

                // If the order is an OCO, an event will be displayed named ListStatus in addition to the executionReport event.
                case "listStatus":
                    OnOrderListStatusEvent?.Invoke(this, JsonConvert.DeserializeObject<OrderListStatusPayload>(message, serializerSettings));
                    break;

                default:
                    apiClient.Logger.Error($"{userFriendlyName}. Unknown payload Event Type '{baseMsg?.EventType}'\n" +
                                 $"Message: {message}");
                    break;

            }
        }

        #endregion

        /// <inheritdoc />
        //public void Close()
        //{
        //    if (Status == DataStreamStatus.Closed) return;

        //    Status = DataStreamStatus.Closing;

        //    if (streamListener != null)
        //    {
        //        try
        //        {
        //            streamListener.OnConnected -= OnConnectToStream;
        //            streamListener.OnConnectionClosed -= OnDisconnect;
        //            streamListener.OnMessage -= OnStreamMessage;
        //        }
        //        catch { /*ignore*/ }
        //        streamListener.Dispose();
        //        streamListener = null;
        //    }

        //    if (pingTimer != null)
        //    {
        //        pingTimer.Enabled = false;
        //        try
        //        {
        //            pingTimer.Elapsed -= OnPingTimer;
        //        }
        //        catch { /*ignore*/ }
        //        pingTimer.Dispose();
        //        pingTimer = null;
        //    }

        //    try
        //    {
        //        if (NeedCloseListenKeyOnClosing) CloseListenKey(listenKey);
        //    }
        //    catch (Exception e)
        //    {
        //        apiClient.Logger.Error($"{userFriendlyName}. Exception when closing Listen Key:\n{e}");
        //    }

        //    apiClient.Logger.Info($"{userFriendlyName}. Connection closed");
        //    Status = DataStreamStatus.Closed;
        //}

        private void OnConnectToStream(object sender, EventArgs e)
        {
            Status = DataStreamStatus.Active;
            reconnectTimeout = TimeSpan.Zero;
            apiClient.Logger.Info($"{userFriendlyName}. Successfully connected!");
        }

        #region [Dispose pattern]
        private bool disposed;
        /// <summary/>
        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;

            if (disposing)
            {
                pingTimer?.Dispose();
                streamListener?.Dispose();
            }

            disposed = true;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
