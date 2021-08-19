using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.BinanceApi.Contracts.UserDataStream;
using PoissonSoft.CommonUtils.ConsoleUtils;
using PoissonSoft.KuCoinApi.Contracts.Enums;
using PoissonSoft.KuCoinApi.Contracts.MarketData;
using PoissonSoft.KuCoinApi.Contracts.MarketData.Request;
using PoissonSoft.KuCoinApi.Contracts.UserDataStream;

namespace KuCoinApi.Example
{
    internal partial class ActionManager
    {
        private bool ShowMarketDataPage()
        {
            var actions = new Dictionary<ConsoleKey, string>()
            {
                [ConsoleKey.A] = "Get Symbols List",
                [ConsoleKey.B] = "Get Ticker",
                [ConsoleKey.C] = "Get All Tickers",
                [ConsoleKey.D] = "Get 24hr Stats",
                [ConsoleKey.I] = "Get Market List",
                [ConsoleKey.F] = "Get Trade Histories",
                [ConsoleKey.G] = "Get Klines",
                [ConsoleKey.H] = "Get Currencies",
                [ConsoleKey.I] = "Get Currency Detail",
                [ConsoleKey.J] = "Get Fiat Price",
                [ConsoleKey.K] = "Get Part Order Book",
                [ConsoleKey.L] = "Get Full Order Book [Deprecated]",
                [ConsoleKey.M] = "Get Full Order Book",

                [ConsoleKey.Escape] = "Go back"
            };

            var selectedAction = InputHelper.GetUserAction("Select action:", actions);

            switch (selectedAction)
            {
                case ConsoleKey.A: // Get Symbols List' Information
                    SafeCall(() =>
                    {
                        var data = apiClient.MarketDataApi.GetSymbolsList();
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.B: // Get Ticker Information
                    SafeCall(() =>
                    {
                        var data = apiClient.MarketDataApi.GetTicker(
                            new TradePair
                            {
                                Symbol = InputHelper.GetString("Trade instrument symbol: ")
                            });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.C: // Get Ticker Information
                    SafeCall(() =>
                    {
                        var data = apiClient.MarketDataApi.Get24hrStats(
                            new TradePair
                            {
                                Symbol = InputHelper.GetString("Trade instrument symbol: ")
                            });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.D: // Get All Tickers' Information
                    SafeCall(() =>
                    {
                        var data = apiClient.MarketDataApi.GetAllTicker();
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.E: // Get Symbols List' Information
                    SafeCall(() =>
                    {
                        var data = apiClient.MarketDataApi.GetMarketList();
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.F:
                    SafeCall(() =>
                    {
                        var data = apiClient.MarketDataApi.GetTradeHistories(
                            new TradePair
                            {
                                Symbol = InputHelper.GetString("Trade instrument symbol: ")
                            });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.G:
                    SafeCall(() =>
                    {
                        var data = apiClient.MarketDataApi.GetKlines(
                            new Candle
                            {
                                Symbol = InputHelper.GetString("Trade instrument symbol: "),
                                StartTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                                EndTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds() - 1000000,
                                CandlestickPattern = InputHelper.GetEnum<CandlestickPattern>("Candle Pattern")
                            });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.H: // Get Currencies Information
                    SafeCall(() =>
                    {
                        var data = apiClient.MarketDataApi.GetCurrencies();
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.I: // Get Ticker Information
                    SafeCall(() =>
                    {
                        var data = apiClient.MarketDataApi.GetCurrencyDetail(
                            new Url
                            {
                                UrlString = InputHelper.GetString("Currency: ")
                            });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.J: // Get Currencies Information
                    SafeCall(() =>
                    {
                        var data = apiClient.MarketDataApi.GetFiatPrice(
                            new FiatPrice
                            {
                                BaseCurrency = InputHelper.GetString("Currency: "),
                                Currencies = InputHelper.GetString("Currencies, eg.:BTC,ETH: ")
                            });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.K: // Get Part Order Book
                    SafeCall(() =>
                    {
                        var data = apiClient.MarketDataApi.GetPartOrderBook(
                            new TradePair
                            {
                                Symbol = InputHelper.GetString("Trade instrument symbol: ")
                            }, 100);
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.L: // Get Full Order Book Deprecated
                    SafeCall(() =>
                    {
                        var data = apiClient.MarketDataApi.GetFullOrderBookDeprecated(
                            new TradePair
                            {
                                Symbol = InputHelper.GetString("Trade instrument symbol: ")
                            });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.M: // Get Full Order Book
                    SafeCall(() =>
                    {
                        var data = apiClient.MarketDataApi.GetFullOrderBook(
                            new TradePair
                            {
                                Symbol = InputHelper.GetString("Trade instrument symbol: ")
                            });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.Escape:
                    return false;

                default:
                    if (actions.ContainsKey(selectedAction))
                    {
                        Console.WriteLine($"Method '{actions[selectedAction]}' is not implemented");
                    }

                    return true;
            }
        }

        private bool ShowMarketDataStreamPage()
        {
            var actions = new Dictionary<ConsoleKey, string>()
            {
                [ConsoleKey.A] = "Symbol Ticker",
                [ConsoleKey.B] = "All Symbols Ticker",
                [ConsoleKey.C] = "Symbol Snapshot",
                [ConsoleKey.D] = "Market Snapshot",
                [ConsoleKey.E] = "Level-2 Market Data",
                [ConsoleKey.F] = "Level2 - 5/50 best ask/bid orders",
                [ConsoleKey.G] = "Klines",
                [ConsoleKey.H] = "Match Execution Data",
                [ConsoleKey.I] = "Index Price",
                [ConsoleKey.J] = "Mark Price",
                [ConsoleKey.K] = "Order Book Change",
                [ConsoleKey.L] = "Private Order Change Events",
                [ConsoleKey.M] = "Account Balance Notice",
                [ConsoleKey.N] = "Debt Ratio Change",
                [ConsoleKey.O] = "Position Status Change Event",
                [ConsoleKey.P] = "Margin Trade Order Enters Event",
                [ConsoleKey.Q] = "Margin Order Update Event",
                [ConsoleKey.R] = "Margin Order Done Event",
                [ConsoleKey.S] = "Stop Order Event",
                // можно вынести в отдельный метод и тогда ключи можно назначить сначала
                [ConsoleKey.U] = "Open stream",
                [ConsoleKey.V] = "Close stream",
                [ConsoleKey.W] = "Subscribe",
                [ConsoleKey.X] = "Unsubscribe",




                [ConsoleKey.Escape] = "Go back"
            };

            var selectedAction = InputHelper.GetUserAction("Select action:", actions);

            switch (selectedAction)
            {
                case ConsoleKey.A: // Symbol Ticker
                    SubscribeOnSymbolTicker();
                    return true;

                case ConsoleKey.B: // All Symbols Ticker
                    SubscribeOnAllSymbolTicker();
                    return true;

                case ConsoleKey.C: // Symbol Snapshot
                    SubscribeOnSymbolSnapshot();
                    return true;

                case ConsoleKey.D: // Market Snapshot
                    SubscribeOnMarketSnapshot();
                    return true;

                case ConsoleKey.E: // Level-2 Market Data
                    SubscribeOnLevel2MarketData();
                    return true;

                case ConsoleKey.F: // Level-2 Market Data
                    SubscribeOnSpotMarketLevel2Depth5();
                    return true;

                case ConsoleKey.G: // Level-2 Market Data
                    SubscribeOnKlines();
                    return true;

                case ConsoleKey.H: // Match Execution Data
                    SubscribeOnMatchExecutionData();
                    return true;

                case ConsoleKey.I:
                    SubscribeOnIndexPrice();
                    return true;

                case ConsoleKey.J:
                    SubscribeOnMarkPrice();
                    return true;

                case ConsoleKey.K:
                    SubscribeOnOrderBookChange();
                    return true;

                case ConsoleKey.L:
                    SubscribeOnPrivateOrderChangeEvents();
                    return true;

                case ConsoleKey.M:
                    SubscribeOnAccountBalance();
                    return true;

                case ConsoleKey.N:
                    SubscribeOnDebtRatioChange();
                    return true;

                case ConsoleKey.O:
                    SubscribeOnPositionStatusChangeEvent();
                    return true;

                case ConsoleKey.P:
                    SubscribeOnMarginTradeOrderEntersEvent();
                    return true;

                case ConsoleKey.Q:
                    SubscribeOnMarginOrderUpdateEvent();
                    return true;
                case ConsoleKey.R:
                    SubscribeOnMarginOrderDoneEvent();
                    return true;
                case ConsoleKey.S:
                    SubscribeOnStopOrderEvent();
                    return true;

                case ConsoleKey.X:
                    SafeCall(() =>
                    {
                        var subscriptionId = InputHelper.GetLong("Subscription id to unsubscribe:");
                        var result = apiClient.PublicChannel.Unsubscribe(subscriptionId);
                        Console.WriteLine($"Unsubscribe result = {result}");
                    });
                    return true;
                    

                case ConsoleKey.Escape:
                    return false;

                default:
                    if (actions.ContainsKey(selectedAction))
                    {
                        Console.WriteLine($"Method '{actions[selectedAction]}' is not implemented");
                    }

                    return true;
            }
        }

        private void OpenSpotUserDataStream()
        {
            try
            {
               
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void CloseSpotUserDataStream()
        {
            try
            {
               
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void SubscribeOnSymbolTicker()
        {
            var instrument = InputHelper.GetString("Trade instrument: ");
            try
            {
                var subscriptionInfo = apiClient.PublicChannel.SubscribeSymbolTicker(instrument, OnPayloadReceived);

                Console.WriteLine($"Subscription Info:\n{JsonConvert.SerializeObject(subscriptionInfo, Formatting.Indented)}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void SubscribeOnAllSymbolTicker()
        {
            try
            {
                var subscriptionInfo = apiClient.PublicChannel.SubscribeAllSymbolTicker(OnPayloadReceived);

                Console.WriteLine($"Subscription Info:\n{JsonConvert.SerializeObject(subscriptionInfo, Formatting.Indented)}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void SubscribeOnSymbolSnapshot()
        {
            var instrument = InputHelper.GetString("Trade instrument: ");
            try
            {
                var subscriptionInfo = apiClient.PublicChannel.SubscribeSymbolSnapshot(instrument, OnPayloadReceived);
                Console.WriteLine($"Subscription Info:\n{JsonConvert.SerializeObject(subscriptionInfo, Formatting.Indented)}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void SubscribeOnMarketSnapshot()
        {
            var ticker = InputHelper.GetString("Ticker: ");
            try
            {
                var subscriptionInfo = apiClient.PublicChannel.SubscribeMarketSnapshot(ticker, OnPayloadReceived);
                Console.WriteLine($"Subscription Info:\n{JsonConvert.SerializeObject(subscriptionInfo, Formatting.Indented)}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void SubscribeOnLevel2MarketData()
        {
            var instrument = InputHelper.GetString("Trade instrument: ");
            try
            {
                var subscriptionInfo = apiClient.PublicChannel.SubscribeLevel2MarketData(instrument, OnPayloadReceived);
                Console.WriteLine($"Subscription Info:\n{JsonConvert.SerializeObject(subscriptionInfo, Formatting.Indented)}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        private void SubscribeOnSpotMarketLevel2Depth5()
        {
            var instrument = InputHelper.GetString("Trade instrument: ");
            var depth = InputHelper.GetInt("Count best ask/bid orders data (5 or 50):");
            try
            {
                var subscriptionInfo = apiClient.PublicChannel.SubscribeSpotMarketLevel2Depth(instrument, depth, OnPayloadReceived);
                Console.WriteLine($"Subscription Info:\n{JsonConvert.SerializeObject(subscriptionInfo, Formatting.Indented)}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void SubscribeOnKlines()
        {
            var instrument = InputHelper.GetString("Trade instrument: ");
            var interval = InputHelper.GetEnum<CandlestickPattern>("Interval: ");
            try
            {
                var subscriptionInfo = apiClient.PublicChannel.SubscribeKlines(instrument, interval, OnPayloadReceived);
                Console.WriteLine($"Subscription Info:\n{JsonConvert.SerializeObject(subscriptionInfo, Formatting.Indented)}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void SubscribeOnMatchExecutionData()
        {
            var instrument = InputHelper.GetString("Trade instrument: ");
            try
            {
                var subscriptionInfo = apiClient.PublicChannel.SubscribeMatchExecutionData(instrument, OnPayloadReceived);
                Console.WriteLine($"Subscription Info:\n{JsonConvert.SerializeObject(subscriptionInfo, Formatting.Indented)}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void SubscribeOnIndexPrice()
        {
            var instrument = InputHelper.GetString("Trade instrument: ");
            try
            {
                var subscriptionInfo = apiClient.PublicChannel.SubscribeIndexPrice(instrument, OnPayloadReceived);
                Console.WriteLine($"Subscription Info:\n{JsonConvert.SerializeObject(subscriptionInfo, Formatting.Indented)}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void SubscribeOnMarkPrice()
        {
            var ticker = InputHelper.GetString("Ticker: ");
            try
            {
                var subscriptionInfo = apiClient.PublicChannel.SubscribeMarkPrice(ticker, OnPayloadReceived);
                Console.WriteLine($"Subscription Info:\n{JsonConvert.SerializeObject(subscriptionInfo, Formatting.Indented)}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void SubscribeOnOrderBookChange()
        {
            var ticker = InputHelper.GetString("Currency: ");
            try
            {
                var subscriptionInfo = apiClient.PublicChannel.SubscribeOrderBookChange(ticker, OnPayloadReceived);
                Console.WriteLine($"Subscription Info:\n{JsonConvert.SerializeObject(subscriptionInfo, Formatting.Indented)}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        #region Private Channels

        private void SubscribeOnPrivateOrderChangeEvents()
        {
            try
            {
                var subscriptionInfo = apiClient.PublicChannel.SubscribePrivateOrderChangeEvents(OnPayloadReceived);

                Console.WriteLine($"Subscription Info:\n{JsonConvert.SerializeObject(subscriptionInfo, Formatting.Indented)}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void SubscribeOnAccountBalance()
        {
            try
            {
                var subscriptionInfo = apiClient.PublicChannel.SubscribeAccountBalance(OnPayloadReceived);

                Console.WriteLine($"Subscription Info:\n{JsonConvert.SerializeObject(subscriptionInfo, Formatting.Indented)}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void SubscribeOnDebtRatioChange()
        {
            try
            {
                var subscriptionInfo = apiClient.PublicChannel.SubscribeDebtRatioChange(OnPayloadReceived);

                Console.WriteLine($"Subscription Info:\n{JsonConvert.SerializeObject(subscriptionInfo, Formatting.Indented)}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        private void SubscribeOnPositionStatusChangeEvent()
        {
            try
            {
                var subscriptionInfo = apiClient.PublicChannel.SubscribePositionStatusChangeEvent(OnPayloadReceived);

                Console.WriteLine($"Subscription Info:\n{JsonConvert.SerializeObject(subscriptionInfo, Formatting.Indented)}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void SubscribeOnMarginTradeOrderEntersEvent()
        {
            var ticker = InputHelper.GetString("Currency: ");
            try
            {
                var subscriptionInfo = apiClient.PublicChannel.SubscribeMarginTradeOrderEntersEvent(ticker, OnPayloadReceived);

                Console.WriteLine($"Subscription Info:\n{JsonConvert.SerializeObject(subscriptionInfo, Formatting.Indented)}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void SubscribeOnMarginOrderUpdateEvent()
        {
            var ticker = InputHelper.GetString("Currency: ");
            try
            {
                var subscriptionInfo = apiClient.PublicChannel.SubscribeMarginOrderUpdateEvent(ticker, OnPayloadReceived);

                Console.WriteLine($"Subscription Info:\n{JsonConvert.SerializeObject(subscriptionInfo, Formatting.Indented)}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void SubscribeOnMarginOrderDoneEvent()
        {
            var ticker = InputHelper.GetString("Currency: ");
            try
            {
                var subscriptionInfo = apiClient.PublicChannel.SubscribeMarginOrderDoneEvent(ticker, OnPayloadReceived);

                Console.WriteLine($"Subscription Info:\n{JsonConvert.SerializeObject(subscriptionInfo, Formatting.Indented)}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void SubscribeOnStopOrderEvent()
        {
            try
            {
                var subscriptionInfo = apiClient.PublicChannel.SubscribeStopOrderEvent(OnPayloadReceived);

                Console.WriteLine($"Subscription Info:\n{JsonConvert.SerializeObject(subscriptionInfo, Formatting.Indented)}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        

        #endregion


        private void OnPayloadReceived<TPayload>(TPayload payload)
        {
            Console.WriteLine(JsonConvert.SerializeObject(payload, Formatting.Indented));
        }

        private void SpotDataStreamOnAccountUpdate(object sender, AccountUpdatePayload e)
        {
            Console.WriteLine($"OnAccountUpdate event\n{JsonConvert.SerializeObject(e, Formatting.Indented)}");
        }

        private void SpotDataStreamOnBalanceUpdate(object sender, BalanceUpdatePayload e)
        {
            Console.WriteLine($"OnBalanceUpdate event\n{JsonConvert.SerializeObject(e, Formatting.Indented)}");
        }

        private void SpotDataStreamOnOrderExecuteEvent(object sender, OrderExecutionReportPayload e)
        {
            Console.WriteLine($"OnOrderExecuteEvent event\n{JsonConvert.SerializeObject(e, Formatting.Indented)}");
        }

        private void SpotDataStreamOnOrderListStatusEvent(object sender, OrderListStatusPayload e)
        {
            Console.WriteLine($"OnOrderListStatusEvent event\n{JsonConvert.SerializeObject(e, Formatting.Indented)}");
        }
    }
}
