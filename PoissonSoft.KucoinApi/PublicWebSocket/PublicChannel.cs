using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Humanizer;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PoissonSoft.BinanceApi.MarketDataStreams;
using PoissonSoft.BinanceApi.Utils;
using PoissonSoft.KuCoinApi.Contracts.Enums;
using PoissonSoft.KuCoinApi.Contracts.PublicWebSocket;
using PoissonSoft.KuCoinApi.Contracts.WebSocketStream;
using PoissonSoft.KuCoinApi.Transport;
using PoissonSoft.KuCoinApi.Transport.Ws;
using Timer = System.Timers.Timer;

namespace PoissonSoft.KuCoinApi.PublicWebSocket
{
    public class PublicChannel: IPublicChannel, IDisposable
    {
        //private const string WS_ENDPOINT = "wss://push1-v2.kucoin.com/endpoint";
        private Timer pingTimer;

        private readonly KuCoinApiClient apiClient;
        private readonly WebSocketStreamListener2 streamListener;

        private readonly ConcurrentDictionary<string, ConcurrentDictionary<long, SubscriptionWrap>> subscriptions =
            new ConcurrentDictionary<string, ConcurrentDictionary<long, SubscriptionWrap>>();

        private readonly string userFriendlyName = nameof(PublicChannel);
        private TimeSpan reconnectTimeout = TimeSpan.Zero;

        public DataStreamStatus WsConnectionStatus { get; private set; } = DataStreamStatus.Closed;


        public PublicChannel(KuCoinApiClient apiClient, KuCoinApiClientCredentials credentials)
        {
            this.apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));

            streamListener = new WebSocketStreamListener2(apiClient, credentials);
            streamListener.OnConnected += OnConnectToWs;
            streamListener.OnConnectionClosed += OnDisconnect;
            streamListener.OnMessage += OnStreamMessage;
        }

       
        public void Close()
        {
            if (WsConnectionStatus == DataStreamStatus.Closed) return;

            UnsubscribeAll();

            WsConnectionStatus = DataStreamStatus.Closing;
            try
            {
                streamListener?.Close();
            }
            catch (Exception e)
            {
                apiClient.Logger.Error($"{userFriendlyName}. Exception when closing Websocket connection:\n{e}");
            }

            apiClient.Logger.Info($"{userFriendlyName}. Websocket connection closed");
            WsConnectionStatus = DataStreamStatus.Closed;
        }

        private CommandResponse<string[]> GetAllStreams()
        {
            if (WsConnectionStatus != DataStreamStatus.Active) return new CommandResponse<string[]>
            {
                Success = true,
                Data = Array.Empty<string>()
            };

            var request = new CommandRequest
            {
                RequestId = GenerateUniqueId()
            };

            return ProcessRequest<string[]>(request);
        }


        private void OnConnectToWs(object sender, EventArgs e)
        {
            WsConnectionStatus = DataStreamStatus.Active;
            reconnectTimeout = TimeSpan.Zero;
            apiClient.Logger.Info($"{userFriendlyName}. Successfully connected to stream!");
            // тут просто для теста ставлю таймер и запускаем пинг
            pingTimer = new Timer(TimeSpan.FromMinutes(1).TotalMilliseconds) { AutoReset = true };
            pingTimer.Elapsed += OnPingTimer;
            pingTimer.Enabled = true;
        }

        private void OnPingTimer(object sender, ElapsedEventArgs e)
        {
            Ping();
        }

        private void OnDisconnect(object sender, (WebSocketCloseStatus? CloseStatus, string CloseStatusDescription) e)
        {
            if (disposed || WsConnectionStatus == DataStreamStatus.Closing) return;

            WsConnectionStatus = DataStreamStatus.Reconnecting;
            if (reconnectTimeout.TotalSeconds < 15) reconnectTimeout += TimeSpan.FromSeconds(1);
            apiClient.Logger.Error($"{userFriendlyName}. WebSocket was disconnected. Try reconnect again after {reconnectTimeout}.");
            Task.Run(() =>
            {
                Task.Delay(reconnectTimeout);
                // TryConnectToWebSocket();
                RestoreSubscriptions();
            });
        }

        private CommandResponse<object> SubscribeToStream(string topicString)
        {
            while (!disposed && WsConnectionStatus != DataStreamStatus.Active)
            {
                streamListener.Open();
                if (WsConnectionStatus != DataStreamStatus.Active)
                    Thread.Sleep(500);
            }

            CommandRequest request = new CommandRequest
            {
                RequestId = GenerateUniqueId(),
                Type = CommandRequestMethod.Subscribe,
                Topic = topicString,
                Privatechanel = true,
                Responce = true
            };

            return ProcessRequest<object>(request);

        }

        private CommandResponse<object> Ping()
        {
            while (!disposed && WsConnectionStatus != DataStreamStatus.Active)
            {
                streamListener.Open();
                if (WsConnectionStatus != DataStreamStatus.Active)
                    Thread.Sleep(500);
            }

            CommandRequest request = new CommandRequest
            {
                RequestId = GenerateUniqueId(),
                Type = CommandRequestMethod.Ping
            };

            return ProcessRequest<object>(request);

        }

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

        // данный метод, выполняет функцию фильтра, определяя какой тип ответа пришел и куда направить дальше.
        private void ProcessStreamMessage(string message)
        {
            Console.WriteLine($"{userFriendlyName}. New payload received:\n{message}");

            if (apiClient.IsDebug)
            {
                apiClient.Logger.Debug($"{userFriendlyName}. New payload received:\n{message}");
            }

            var jToken = JToken.Parse(message);

            if (jToken.Type != JTokenType.Object)
            {
                apiClient.Logger.Error($"{userFriendlyName}. An unexpected message was received from the server.\n" +
                                       $"Type: {jToken.Type}, Message: {message}");
                return;
            }
            var jObject = (JObject)jToken;


            // Ответ на запрос
            if (jObject.ContainsKey("type"))
            {
                var tMess = jObject["type"];

                // Ответ на подписку
                if (tMess?.ToString() == "ack")
                {
                    ProcessResponse(jObject);
                }

                // получен снимок с данными
                if (tMess?.ToString() == "message")
                {
                    if (!jObject.ContainsKey("data"))
                    {
                        apiClient.Logger.Error($"{userFriendlyName}. Stream message does not contains 'data' field.\n" +
                                               $"Message: {message}");
                        return;
                    }
                    ProcessPayload(jObject);
                }


                return;
            }

            // Ошибка
            var waitingRequests = responseWaiters.Values.ToArray().Select(x => JsonConvert.SerializeObject(x.Request));

            apiClient.Logger.Error($"{userFriendlyName}. Получена информация об ошибке: {message}\n" +
                                   $"Отправленные запросы, ожидающие ответа:\n{string.Join("\n", waitingRequests)}");

        }

        private void ProcessPayload(JToken streamData)
        {
            string streamName = streamData["topic"]?.ToString();

            string[] words = null;
            foreach (var subscriptionItem in subscriptions)
            {
                words = streamName.Split(':');
                // если есть совпадения с адресом topic, считаем что подписка есть
                if (subscriptionItem.Key.Contains($"{words[0]}"))
                {
                    streamName = subscriptionItem.Key;
                    break;
                }
            }

            if (!subscriptions.TryGetValue(streamName, out var activeSubscriptionsDic))
            {
                apiClient.Logger.Error($"{userFriendlyName}. Получено сообщение из потока '{streamName}', " +
                                       "однако подписок на данный поток не обнаружено");
                return;
            }

            var activeSubscriptions = activeSubscriptionsDic.Values.ToList();
            if (!activeSubscriptions.Any()) return;

            var payloadType = activeSubscriptions.First().Info.Topic;
            switch (payloadType)
            {
                case Topic.Ticker:
                    activeSubscriptions.ForEach(sw =>
                    {
                        if (sw.CallbackAction is Action<SymbolTicker> callback)
                        {
                            callback(streamData?.ToObject<SymbolTicker>());
                        }
                    });
                    break;

                case Topic.TickerAll:
                    activeSubscriptions.ForEach(sw =>
                    {
                        if (sw.CallbackAction is Action<SymbolTicker> callback)
                        {
                            callback(streamData?.ToObject<SymbolTicker>());
                        }
                    });
                    break;

                case Topic.Snapshot:
                    activeSubscriptions.ForEach(sw =>
                    {
                        if (sw.CallbackAction is Action<SymbolSnapshot> callback)
                        {
                            callback(streamData?.ToObject<SymbolSnapshot>());
                        }
                    });
                    break;
                case Topic.MarketLevel2:
                    activeSubscriptions.ForEach(sw =>
                    {
                        if (sw.CallbackAction is Action<MarketLevel2> callback)
                        {
                            callback(streamData?.ToObject<MarketLevel2>());
                        }
                    });
                    break;
                case Topic.SpotMarketLevel2Depth:
                    activeSubscriptions.ForEach(sw =>
                    {
                        if (sw.CallbackAction is Action<SpotMarketLevel2Depth5> callback)
                        {
                            callback(streamData?.ToObject<SpotMarketLevel2Depth5>());
                        }
                    });
                    break;
                case Topic.MarketCandles:
                    activeSubscriptions.ForEach(sw =>
                    {
                        if (sw.CallbackAction is Action<KlinesInfo> callback)
                        {
                            callback(streamData?.ToObject<KlinesInfo>());
                        }
                    });
                    break;
                case Topic.MarketMatch:
                    activeSubscriptions.ForEach(sw =>
                    {
                        if (sw.CallbackAction is Action<MatchExecution> callback)
                        {
                            callback(streamData?.ToObject<MatchExecution>());
                        }
                    });
                    break;
                case Topic.IndicatorIndex:
                    activeSubscriptions.ForEach(sw =>
                    {
                        if (sw.CallbackAction is Action<IndexPrice> callback)
                        {
                            callback(streamData?.ToObject<IndexPrice>());
                        }
                    });
                    break;
                case Topic.IndicatorMarkPrice:
                    activeSubscriptions.ForEach(sw =>
                    {
                        if (sw.CallbackAction is Action<IndexPrice> callback)
                        {
                            callback(streamData?.ToObject<IndexPrice>());
                        }
                    });
                    break;
                case Topic.MarginFundingBook:
                    activeSubscriptions.ForEach(sw =>
                    {
                        if (sw.CallbackAction is Action<FundingBook> callback)
                        {
                            callback(streamData?.ToObject<FundingBook>());
                        }
                    });
                    break;
                case Topic.SpotMarketTradeOrders:
                    activeSubscriptions.ForEach(sw =>
                    {
                        if (sw.CallbackAction is Action<OrderChangeEvent> callback)
                        {
                            callback(streamData?.ToObject<OrderChangeEvent>());
                        }
                    });
                    break;
                case Topic.AccountBalance:
                    activeSubscriptions.ForEach(sw =>
                    {
                        if (sw.CallbackAction is Action<AccountBalanceUpdatePayload> callback)
                        {
                            callback(streamData?.ToObject<AccountBalanceUpdatePayload>());
                        }
                    });
                    break;
                case Topic.DebtRatio:
                    activeSubscriptions.ForEach(sw =>
                    {
                        if (sw.CallbackAction is Action<DeptRatio> callback)
                        {
                            callback(streamData?.ToObject<DeptRatio>());
                        }
                    });
                    break;
                case Topic.PositionStatusEvent:
                    activeSubscriptions.ForEach(sw =>
                    {
                        if (sw.CallbackAction is Action<PositionStatusEvent> callback)
                        {
                            callback(streamData?.ToObject<PositionStatusEvent>());
                        }
                    });
                    break;
                case Topic.MarginTradeOrderEvent:
                    activeSubscriptions.ForEach(sw =>
                    {
                        if (sw.CallbackAction is Action<MarginTradeOrderEvent> callback)
                        {
                            callback(streamData?.ToObject<MarginTradeOrderEvent>());
                        }
                    });
                    break;
                case Topic.MarginOrderUpdateEvent:
                    activeSubscriptions.ForEach(sw =>
                    {
                        if (sw.CallbackAction is Action<MarginOrderUpdateEvent> callback)
                        {
                            callback(streamData?.ToObject<MarginOrderUpdateEvent>());
                        }
                    });
                    break;
                case Topic.MarginOrderDoneEvent:
                    activeSubscriptions.ForEach(sw =>
                    {
                        if (sw.CallbackAction is Action<MarginOrderDoneEvent> callback)
                        {
                            callback(streamData?.ToObject<MarginOrderDoneEvent>());
                        }
                    });
                    break;
                case Topic.StopOrderEvent:
                    activeSubscriptions.ForEach(sw =>
                    {
                        if (sw.CallbackAction is Action<StopOrderEvent> callback)
                        {
                            callback(streamData?.ToObject<StopOrderEvent>());
                        }
                    });
                    break;

                default:
                    apiClient.Logger.Error($"{userFriendlyName}. Unknown Payload type '{payloadType}'");
                    break;
            }
        }

        private void  ProcessResponse(JObject response)
        {
            long requestId;
            try
            {
                requestId = response["id"]?.ToObject<long>()
                           ?? throw new Exception("При конвертации поля id в long получили значение NULL");
            }
            catch (Exception ex)
            {
                apiClient.Logger.Error($"{userFriendlyName}. При обработке ответа на запрос не удалось получить ID запроса. " +
                                       $"Message: {response}\nException {ex}");
                return;
            }

            if (!responseWaiters.TryGetValue(requestId, out var waiter))
            {
                apiClient.Logger.Error($"{userFriendlyName}. Среди ожидающих ответа запросов не удалось найти запрос " +
                                       $"с ID={requestId}");
                return;
            }
            
            var cmdResp = new CommandResponse<object>
            {
                Success = true
            };
            try
            {
                switch (waiter.Request.Type)
                {
                    case CommandRequestMethod.Subscribe:
                    case CommandRequestMethod.Unsubscribe:
                    case CommandRequestMethod.Ping:
                        cmdResp.Data = null;
                        break;
                    default:
                        cmdResp.Success = false;
                        cmdResp.ErrorDescription = $"Unknown Request Method '{waiter.Request.Type}'";
                        break;
                }
            }
            catch (Exception e)
            {
                cmdResp.Success = false;
                cmdResp.ErrorDescription = e.Message;
            }

            waiter.Response = cmdResp;

            //ответ получили, устанавливаем в сигнальное состояние
            waiter.SyncEvent.Set();
        }

        private void RestoreSubscriptions()
        {
            var currentStreams = subscriptions.Keys.ToArray();
            if (!currentStreams.Any()) return;

            foreach (var streamKey in currentStreams)
            {
                var resp = SubscribeToStream(streamKey);
                if (!resp.Success)
                {
                    apiClient.Logger.Error($"{userFriendlyName}. Не удалось возобновить подписку на stream '{streamKey}'. " +
                                           $"Ошибка: {resp.ErrorDescription}");
                }
            }
        }

        public bool Unsubscribe(long subscriptionId)
        {
            var streams = subscriptions.Keys.ToArray();
            foreach (var streamKey in streams)
            {
                if (!subscriptions.TryGetValue(streamKey, out var subscriptionsByKey)
                    || !subscriptionsByKey.ContainsKey(subscriptionId)) continue;

                if (!subscriptionsByKey.TryRemove(subscriptionId, out _)) return true;

                if (!subscriptionsByKey.IsEmpty) return true;

                if (!subscriptions.TryRemove(streamKey, out _)) return true;

                var resp = UnsubscribeStream(streamKey);
                if (!resp.Success)
                {
                    apiClient.Logger.Error($"{userFriendlyName}. Stream unsubscription error: {resp.ErrorDescription}");
                }

                return resp.Success;

            }

            apiClient.Logger.Error($"{userFriendlyName}. Не удалось отписаться от подписки {subscriptionId}");
            return false;
        }

        public void UnsubscribeAll()
        {
            if (WsConnectionStatus == DataStreamStatus.Active)
            {
                var resp = GetAllStreams();
                if (!resp.Success)
                    throw new Exception($"{userFriendlyName}. Не удалось получить список активных подписок");

                if (resp.Data?.Any() == true)
                {
                    foreach (var streamKey in resp.Data)
                    {
                        UnsubscribeStream(streamKey);
                    }
                }
            }

            subscriptions.Clear();
        }

        private CommandResponse<object> UnsubscribeStream(string topicString)
        {
            if (WsConnectionStatus != DataStreamStatus.Active) return new CommandResponse<object>
            {
                Success = true
            };

            var request = new CommandRequest
            {
                RequestId = GenerateUniqueId(),
                Type = CommandRequestMethod.Unsubscribe,
                Topic = topicString,
                Privatechanel = true,
                Responce = true
            };

            return ProcessRequest<object>(request);
        }

        private long lastId;

        private class SubscriptionWrap
        {
            public SubscriptionInfo Info { get; set; }
            public object CallbackAction { get; set; }
        }

        private long GenerateUniqueId()
        {
            return Interlocked.Increment(ref lastId);
        }

        private class ManualResetEventPool : ObjectPool<ManualResetEventSlim>
        {
            public ManualResetEventPool()
                : base(startSize: 20, minSize: 10, sizeIncrement: 5, availableLimit: 200)
            { }

            // Новый экземпляр объекта создаётся в не сигнальном состоянии
            protected override ManualResetEventSlim CreateEntity()
            {
                return new ManualResetEventSlim();
            }
        }

        private readonly ManualResetEventPool manualResetEventPool = new ManualResetEventPool();
        private const int WAIT_FOR_REQUEST_PROCESSING_MS = 5_000;

        private readonly ConcurrentDictionary<long, ResponseWaiter> responseWaiters =
            new ConcurrentDictionary<long, ResponseWaiter>();

        private CommandResponse<T> ProcessRequest<T>(CommandRequest request)
        {
            bool requestWasProcessed;
            ManualResetEventSlim syncEvent = null;
            ResponseWaiter waiter = null;
            try
            {
                if (!manualResetEventPool.TryGetEntity(out syncEvent))
                {
                    apiClient.Logger.Error($"{userFriendlyName}. Couldn't get the synchronization object from the pool");
                }
                syncEvent?.Reset();
                waiter = new ResponseWaiter(request, syncEvent);
                responseWaiters.TryAdd(waiter.Request.RequestId, waiter);
                streamListener.SendMessage(JsonConvert.SerializeObject(request));
                requestWasProcessed = syncEvent != null && syncEvent.Wait(WAIT_FOR_REQUEST_PROCESSING_MS);
            }
            finally
            {
                if (syncEvent != null) manualResetEventPool.ReturnToPool(syncEvent);
                if (waiter?.Request != null) responseWaiters.TryRemove(waiter.Request.RequestId, out _);
            }

            if (!requestWasProcessed)
            {// TODO : и тут ошибка при ping
                return new CommandResponse<T>
                {
                    Success = false,
                    ErrorDescription = "Incorrect data in Response: " +
                                       $"{(waiter.Response?.Data == null ? "NULL" : waiter.Response.Data.GetType().Name)}"
                };
            }

            if (waiter.Response?.Success == null)
            {
                return new CommandResponse<T>
                {
                    Success = false,
                    ErrorDescription = "Incorrect response: " +
                                       $"{(waiter.Response == null ? "NULL" : JsonConvert.SerializeObject(waiter.Response))}"
                };
            }

            if (waiter.Response?.Success == false)
            {
                return new CommandResponse<T>
                {
                    Success = false,
                    ErrorDescription = waiter.Response.ErrorDescription ?? "Error"
                };
            }

            T data;
            try
            {
                data = (T)waiter.Response.Data;
            }
            catch
            {
                return new CommandResponse<T>
                {
                    Success = false,
                    ErrorDescription = "Incorrect data in Response: " +
                                       $"{(waiter.Response.Data == null ? "NULL" : waiter.Response.Data.GetType().Name)}"
                };
            }

            return new CommandResponse<T>
            {
                Success = true,
                ErrorDescription = waiter.Response.ErrorDescription,
                Data = data
            };
        }

        private class ResponseWaiter
        {
            public CommandRequest Request { get; }

            public ManualResetEventSlim SyncEvent { get; }

            public CommandResponse<object> Response { get; set; }

            public ResponseWaiter(CommandRequest request, ManualResetEventSlim syncEvent)
            {
                Request = request;
                SyncEvent = syncEvent;
            }
        }

        private class CommandResponse<T>
        {
            public bool Success { get; set; }

            public T Data { get; set; }

            public string ErrorDescription { get; set; }
        }

        private bool AddSubscription(SubscriptionWrap sw)
        {
            var needSubscribeToChannel = false;
            if (!subscriptions.TryGetValue(sw.Info.TopicString, out var streamSubscriptions))
            {
                streamSubscriptions = new ConcurrentDictionary<long, SubscriptionWrap>();
                if (subscriptions.TryAdd(sw.Info.TopicString, streamSubscriptions))
                {
                    needSubscribeToChannel = true;
                }
                else
                {
                    subscriptions.TryGetValue(sw.Info.TopicString, out streamSubscriptions);
                }
            }

            if (streamSubscriptions == null)
                throw new Exception($"{userFriendlyName}. Unexpected error. Can not add key '{sw.Info.TopicString}' " +
                                    $"to {nameof(subscriptions)} dictionary");

            streamSubscriptions[sw.Info.Id] = sw;

            return needSubscribeToChannel;
        }

        #region WS Channels Public

        public SubscriptionInfo SubscribeSymbolTicker(string instrument, Action<SymbolTicker> callbackAction)
        {
            var subscriptionInfo = new SubscriptionInfo
            {
                Id = GenerateUniqueId(),
                Topic = Topic.Ticker,
                TopicString = $"{Topic.Ticker.Humanize()}:{instrument}",
                Parameters = new Dictionary<string, string>
                {
                    ["instrument"] = instrument
                }
            };
            var subscriptionWrap = new SubscriptionWrap
            {
                Info = subscriptionInfo,
                CallbackAction = callbackAction
            };
            var needSubscribeToStream = AddSubscription(subscriptionWrap);

            if (needSubscribeToStream)
            {
                var resp = SubscribeToStream(subscriptionInfo.TopicString);
                if (!resp.Success)
                    throw new Exception($"{userFriendlyName}. Stream subscription error: {resp.ErrorDescription}");
            }

            return subscriptionInfo;
        }
        public SubscriptionInfo SubscribeAllSymbolTicker(Action<SymbolTicker> callbackAction)
        {
            var subscriptionInfo = new SubscriptionInfo
            {
                Id = GenerateUniqueId(),
                Topic = Topic.TickerAll,
                TopicString = $"{Topic.TickerAll.Humanize()}",
                Parameters = new Dictionary<string, string> { }
            };
            var subscriptionWrap = new SubscriptionWrap
            {
                Info = subscriptionInfo,
                CallbackAction = callbackAction
            };
            var needSubscribeToStream = AddSubscription(subscriptionWrap);

            if (needSubscribeToStream)
            {
                var resp = SubscribeToStream(subscriptionInfo.TopicString);
                if (!resp.Success)
                    throw new Exception($"{userFriendlyName}. Stream subscription error: {resp.ErrorDescription}");
            }

            return subscriptionInfo;
        }

        public SubscriptionInfo SubscribeSymbolSnapshot(string instrument, Action<SymbolSnapshot> callbackAction)
        {
            var subscriptionInfo = new SubscriptionInfo
            {
                Id = GenerateUniqueId(),
                Topic = Topic.Snapshot,
                TopicString = $"{Topic.Snapshot.Humanize()}:{instrument}",
                Parameters = new Dictionary<string, string>
                {
                    ["instrument"] = instrument
                }
            };
            var subscriptionWrap = new SubscriptionWrap
            {
                Info = subscriptionInfo,
                CallbackAction = callbackAction
            };
            var needSubscribeToStream = AddSubscription(subscriptionWrap);

            if (needSubscribeToStream)
            {
                var resp = SubscribeToStream(subscriptionInfo.TopicString);
                if (!resp.Success)
                    throw new Exception($"{userFriendlyName}. Stream subscription error: {resp.ErrorDescription}");
            }

            return subscriptionInfo;
        }

        public SubscriptionInfo SubscribeMarketSnapshot(string ticker, Action<SymbolSnapshot> callbackAction)
        {
            var subscriptionInfo = new SubscriptionInfo
            {
                Id = GenerateUniqueId(),
                Topic = Topic.Snapshot,
                TopicString = $"{Topic.Snapshot.Humanize()}:{ticker}",
                Parameters = new Dictionary<string, string>
                {
                    ["ticker"] = ticker
                }
            };
            var subscriptionWrap = new SubscriptionWrap
            {
                Info = subscriptionInfo,
                CallbackAction = callbackAction
            };
            var needSubscribeToStream = AddSubscription(subscriptionWrap);

            if (needSubscribeToStream)
            {
                var resp = SubscribeToStream(subscriptionInfo.TopicString);
                if (!resp.Success)
                    throw new Exception($"{userFriendlyName}. Stream subscription error: {resp.ErrorDescription}");
            }

            return subscriptionInfo;
        }

        public SubscriptionInfo SubscribeLevel2MarketData(string instrument, Action<MarketLevel2> callbackAction)
        {
            var subscriptionInfo = new SubscriptionInfo
            {
                Id = GenerateUniqueId(),
                Topic = Topic.MarketLevel2,
                TopicString = $"{Topic.MarketLevel2.Humanize()}:{instrument}",
                Parameters = new Dictionary<string, string>
                {
                    ["instrument"] = instrument
                }
            };
            var subscriptionWrap = new SubscriptionWrap
            {
                Info = subscriptionInfo,
                CallbackAction = callbackAction
            };
            var needSubscribeToStream = AddSubscription(subscriptionWrap);

            if (needSubscribeToStream)
            {
                var resp = SubscribeToStream(subscriptionInfo.TopicString);
                if (!resp.Success)
                    throw new Exception($"{userFriendlyName}. Stream subscription error: {resp.ErrorDescription}");
            }

            return subscriptionInfo;
        }

        public SubscriptionInfo SubscribeSpotMarketLevel2Depth(string instrument, int depth, Action<SpotMarketLevel2Depth5> callbackAction)
        {
            var subscriptionInfo = new SubscriptionInfo
            {
                Id = GenerateUniqueId(),
                Topic = Topic.SpotMarketLevel2Depth,
                TopicString = $"{Topic.SpotMarketLevel2Depth.Humanize()}{depth}:{instrument}",
                Parameters = new Dictionary<string, string>
                {
                    ["instrument"] = instrument,
                    ["depth"] = depth.ToString()
                }
            };
            var subscriptionWrap = new SubscriptionWrap
            {
                Info = subscriptionInfo,
                CallbackAction = callbackAction
            };
            var needSubscribeToStream = AddSubscription(subscriptionWrap);

            if (needSubscribeToStream)
            {
                var resp = SubscribeToStream(subscriptionInfo.TopicString);
                if (!resp.Success)
                    throw new Exception($"{userFriendlyName}. Stream subscription error: {resp.ErrorDescription}");
            }

            return subscriptionInfo;
        }

        public SubscriptionInfo SubscribeKlines(string instrument, CandlestickPattern interval, Action<KlinesInfo> callbackAction)
        {
            var subscriptionInfo = new SubscriptionInfo
            {
                Id = GenerateUniqueId(),
                Topic = Topic.MarketCandles,
                TopicString = $"{Topic.MarketCandles.Humanize()}:{instrument}_{((CandlestickPattern)(int)interval).Humanize()}",
                Parameters = new Dictionary<string, string>
                {
                    ["instrument"] = instrument,
                    ["interval"] = ((CandlestickPattern)(int)interval).Humanize()
                }
            };
            var subscriptionWrap = new SubscriptionWrap
            {
                Info = subscriptionInfo,
                CallbackAction = callbackAction
            };
            var needSubscribeToStream = AddSubscription(subscriptionWrap);

            if (needSubscribeToStream)
            {
                var resp = SubscribeToStream(subscriptionInfo.TopicString);
                if (!resp.Success)
                    throw new Exception($"{userFriendlyName}. Stream subscription error: {resp.ErrorDescription}");
            }

            return subscriptionInfo;
        }

        public SubscriptionInfo SubscribeMatchExecutionData(string instrument, Action<MatchExecution> callbackAction)
        {
            var subscriptionInfo = new SubscriptionInfo
            {
                Id = GenerateUniqueId(),
                Topic = Topic.MarketMatch,
                TopicString = $"{Topic.MarketMatch.Humanize()}:{instrument}",
                Parameters = new Dictionary<string, string>
                {
                    ["instrument"] = instrument
                }
            };
            var subscriptionWrap = new SubscriptionWrap
            {
                Info = subscriptionInfo,
                CallbackAction = callbackAction
            };
            var needSubscribeToStream = AddSubscription(subscriptionWrap);

            if (needSubscribeToStream)
            {
                var resp = SubscribeToStream(subscriptionInfo.TopicString);
                if (!resp.Success)
                    throw new Exception($"{userFriendlyName}. Stream subscription error: {resp.ErrorDescription}");
            }

            return subscriptionInfo;
        }

        public SubscriptionInfo SubscribeIndexPrice(string instrument, Action<IndexPrice> callbackAction)
        {
            var subscriptionInfo = new SubscriptionInfo
            {
                Id = GenerateUniqueId(),
                Topic = Topic.IndicatorIndex,
                TopicString = $"{Topic.IndicatorIndex.Humanize()}:{instrument}",
                Parameters = new Dictionary<string, string>
                {
                    ["instrument"] = instrument
                }
            };
            var subscriptionWrap = new SubscriptionWrap
            {
                Info = subscriptionInfo,
                CallbackAction = callbackAction
            };
            var needSubscribeToStream = AddSubscription(subscriptionWrap);

            if (needSubscribeToStream)
            {
                var resp = SubscribeToStream(subscriptionInfo.TopicString);
                if (!resp.Success)
                    throw new Exception($"{userFriendlyName}. Stream subscription error: {resp.ErrorDescription}");
            }

            return subscriptionInfo;
        }

        public SubscriptionInfo SubscribeMarkPrice(string instrument, Action<IndexPrice> callbackAction)
        {
            var subscriptionInfo = new SubscriptionInfo
            {
                Id = GenerateUniqueId(),
                Topic = Topic.IndicatorMarkPrice,
                TopicString = $"{Topic.IndicatorMarkPrice.Humanize()}:{instrument}",
                Parameters = new Dictionary<string, string>
                {
                    ["instrument"] = instrument
                }
            };
            var subscriptionWrap = new SubscriptionWrap
            {
                Info = subscriptionInfo,
                CallbackAction = callbackAction
            };
            var needSubscribeToStream = AddSubscription(subscriptionWrap);

            if (needSubscribeToStream)
            {
                var resp = SubscribeToStream(subscriptionInfo.TopicString);
                if (!resp.Success)
                    throw new Exception($"{userFriendlyName}. Stream subscription error: {resp.ErrorDescription}");
            }

            return subscriptionInfo;
        }

        public SubscriptionInfo SubscribeOrderBookChange(string ticker, Action<FundingBook> callbackAction)
        {
            var subscriptionInfo = new SubscriptionInfo
            {
                Id = GenerateUniqueId(),
                Topic = Topic.MarginFundingBook,
                TopicString = $"{Topic.MarginFundingBook.Humanize()}:{ticker}",
                Parameters = new Dictionary<string, string>
                {
                    ["currency"] = ticker
                }
            };
            var subscriptionWrap = new SubscriptionWrap
            {
                Info = subscriptionInfo,
                CallbackAction = callbackAction
            };
            var needSubscribeToStream = AddSubscription(subscriptionWrap);

            if (needSubscribeToStream)
            {
                var resp = SubscribeToStream(subscriptionInfo.TopicString);
                if (!resp.Success)
                    throw new Exception($"{userFriendlyName}. Stream subscription error: {resp.ErrorDescription}");
            }

            return subscriptionInfo;
        }

        #endregion

        #region WS Private Channels

        public SubscriptionInfo SubscribePrivateOrderChangeEvents(Action<OrderChangeEvent> callbackAction)
        {
            var subscriptionInfo = new SubscriptionInfo
            {
                Id = GenerateUniqueId(),
                Topic = Topic.SpotMarketTradeOrders,
                TopicString = $"{Topic.SpotMarketTradeOrders.Humanize()}",
                Parameters = new Dictionary<string, string> { }
            };
            var subscriptionWrap = new SubscriptionWrap
            {
                Info = subscriptionInfo,
                CallbackAction = callbackAction
            };
            var needSubscribeToStream = AddSubscription(subscriptionWrap);

            if (needSubscribeToStream)
            {
                var resp = SubscribeToStream(subscriptionInfo.TopicString);
                if (!resp.Success)
                    throw new Exception($"{userFriendlyName}. Stream subscription error: {resp.ErrorDescription}");
            }

            return subscriptionInfo;
        }

        public SubscriptionInfo SubscribeAccountBalance(Action<AccountBalanceUpdatePayload> callbackAction)
        {
            var subscriptionInfo = new SubscriptionInfo
            {
                Id = GenerateUniqueId(),
                Topic = Topic.AccountBalance,
                TopicString = $"{Topic.AccountBalance.Humanize()}",
                Parameters = new Dictionary<string, string> { }
            };
            var subscriptionWrap = new SubscriptionWrap
            {
                Info = subscriptionInfo,
                CallbackAction = callbackAction
            };
            var needSubscribeToStream = AddSubscription(subscriptionWrap);

            if (needSubscribeToStream)
            {
                var resp = SubscribeToStream(subscriptionInfo.TopicString);
                if (!resp.Success)
                    throw new Exception($"{userFriendlyName}. Stream subscription error: {resp.ErrorDescription}");
            }

            return subscriptionInfo;
        }

        public SubscriptionInfo SubscribeDebtRatioChange(Action<DeptRatio> callbackAction)
        {
            var subscriptionInfo = new SubscriptionInfo
            {
                Id = GenerateUniqueId(),
                Topic = Topic.DebtRatio,
                TopicString = Topic.DebtRatio.Humanize(),
                Parameters = new Dictionary<string, string> { }
            };
            var subscriptionWrap = new SubscriptionWrap
            {
                Info = subscriptionInfo,
                CallbackAction = callbackAction
            };
            var needSubscribeToStream = AddSubscription(subscriptionWrap);

            if (needSubscribeToStream)
            {
                var resp = SubscribeToStream(subscriptionInfo.TopicString);
                if (!resp.Success)
                    throw new Exception($"{userFriendlyName}. Stream subscription error: {resp.ErrorDescription}");
            }

            return subscriptionInfo;
        }

        public SubscriptionInfo SubscribePositionStatusChangeEvent(Action<PositionStatusEvent> callbackAction)
        {
            var subscriptionInfo = new SubscriptionInfo
            {
                Id = GenerateUniqueId(),
                Topic = Topic.PositionStatusEvent,
                TopicString =Topic.PositionStatusEvent.Humanize(),
                Parameters = new Dictionary<string, string> { }
            };
            var subscriptionWrap = new SubscriptionWrap
            {
                Info = subscriptionInfo,
                CallbackAction = callbackAction
            };
            var needSubscribeToStream = AddSubscription(subscriptionWrap);

            if (needSubscribeToStream)
            {
                var resp = SubscribeToStream(subscriptionInfo.TopicString);
                if (!resp.Success)
                    throw new Exception($"{userFriendlyName}. Stream subscription error: {resp.ErrorDescription}");
            }

            return subscriptionInfo;
        }

        public SubscriptionInfo SubscribeMarginTradeOrderEntersEvent(string ticker, Action<MarginTradeOrderEvent> callbackAction)
        {
            var subscriptionInfo = new SubscriptionInfo
            {
                Id = GenerateUniqueId(),
                Topic = Topic.MarginTradeOrderEvent,
                TopicString = $"{Topic.MarginTradeOrderEvent.Humanize()}:{ticker}",
                Parameters = new Dictionary<string, string>
                {
                    ["symbol"] = ""
                }
            };
            var subscriptionWrap = new SubscriptionWrap
            {
                Info = subscriptionInfo,
                CallbackAction = callbackAction
            };
            var needSubscribeToStream = AddSubscription(subscriptionWrap);

            if (needSubscribeToStream)
            {
                var resp = SubscribeToStream(subscriptionInfo.TopicString);
                if (!resp.Success)
                    throw new Exception($"{userFriendlyName}. Stream subscription error: {resp.ErrorDescription}");
            }

            return subscriptionInfo;
        }

        public SubscriptionInfo SubscribeMarginOrderUpdateEvent(string ticker, Action<MarginOrderUpdateEvent> callbackAction)
        {
            var subscriptionInfo = new SubscriptionInfo
            {
                Id = GenerateUniqueId(),
                Topic = Topic.MarginOrderUpdateEvent,
                TopicString = $"{Topic.MarginOrderUpdateEvent.Humanize()}:{ticker}",
                Parameters = new Dictionary<string, string>
                {
                    ["currency"] = ticker
                }
            };
            var subscriptionWrap = new SubscriptionWrap
            {
                Info = subscriptionInfo,
                CallbackAction = callbackAction
            };
            var needSubscribeToStream = AddSubscription(subscriptionWrap);

            if (needSubscribeToStream)
            {
                var resp = SubscribeToStream(subscriptionInfo.TopicString);
                if (!resp.Success)
                    throw new Exception($"{userFriendlyName}. Stream subscription error: {resp.ErrorDescription}");
            }

            return subscriptionInfo;
        }

        public SubscriptionInfo SubscribeMarginOrderDoneEvent(string ticker, Action<MarginOrderDoneEvent> callbackAction)
        {
            var subscriptionInfo = new SubscriptionInfo
            {
                Id = GenerateUniqueId(),
                Topic = Topic.MarginOrderDoneEvent,
                TopicString = $"{Topic.MarginOrderDoneEvent.Humanize()}:{ticker}",
                Parameters = new Dictionary<string, string>
                {
                    ["currency"] = ticker
                }
            };
            var subscriptionWrap = new SubscriptionWrap
            {
                Info = subscriptionInfo,
                CallbackAction = callbackAction
            };
            var needSubscribeToStream = AddSubscription(subscriptionWrap);

            if (needSubscribeToStream)
            {
                var resp = SubscribeToStream(subscriptionInfo.TopicString);
                if (!resp.Success)
                    throw new Exception($"{userFriendlyName}. Stream subscription error: {resp.ErrorDescription}");
            }

            return subscriptionInfo;
        }

        public SubscriptionInfo SubscribeStopOrderEvent(Action<StopOrderEvent> callbackAction)
        {
            var subscriptionInfo = new SubscriptionInfo
            {
                Id = GenerateUniqueId(),
                Topic = Topic.StopOrderEvent,
                TopicString = Topic.StopOrderEvent.Humanize(),
                Parameters = new Dictionary<string, string>{}
            };
            var subscriptionWrap = new SubscriptionWrap
            {
                Info = subscriptionInfo,
                CallbackAction = callbackAction
            };
            var needSubscribeToStream = AddSubscription(subscriptionWrap);

            if (needSubscribeToStream)
            {
                var resp = SubscribeToStream(subscriptionInfo.TopicString);
                if (!resp.Success)
                    throw new Exception($"{userFriendlyName}. Stream subscription error: {resp.ErrorDescription}");
            }

            return subscriptionInfo;
        }

        #endregion
        

        private bool disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;

            if (disposing)
            {
                if (streamListener != null)
                {
                    Close();
                    streamListener.OnConnected -= OnConnectToWs;
                    streamListener.OnConnectionClosed -= OnDisconnect;
                    streamListener.OnMessage -= OnStreamMessage;
                    streamListener?.Dispose();
                }

                manualResetEventPool?.Dispose();
            }

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
