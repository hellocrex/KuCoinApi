using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.CommonUtils.ConsoleUtils;
using PoissonSoft.KuСoinApi.Contracts.Enums;
using PoissonSoft.KuСoinApi.Contracts.MarketData;
using PoissonSoft.KuСoinApi.Contracts.MarketData.Request;

namespace KuСoinApi.Example
{
    internal partial class ActionManager
    {
        private bool ShowMarketDataPage()
        {
            var actions = new Dictionary<ConsoleKey, string>()
            {
                [ConsoleKey.S] = "Get Symbols List",
                [ConsoleKey.T] = "Get Ticker",
                [ConsoleKey.A] = "Get All Tickers",
                [ConsoleKey.C] = "Get 24hr Stats",
                [ConsoleKey.M] = "Get Market List",
                [ConsoleKey.H] = "Get Trade Histories",
                [ConsoleKey.K] = "Get Klines",
                [ConsoleKey.R] = "Get Currencies",
                [ConsoleKey.D] = "Get Currency Detail",
                [ConsoleKey.P] = "Get Fiat Price",
                [ConsoleKey.O] = "Get Part Order Book",
                [ConsoleKey.F] = "Get Full Order Book",

                [ConsoleKey.Escape] = "Go back"
            };

            var selectedAction = InputHelper.GetUserAction("Select action:", actions);

            switch (selectedAction)
            {
                case ConsoleKey.S: // Get Symbols List' Information
                    SafeCall(() =>
                    {
                        var data = apiClient.MarketDataApi.GetSymbolsList();
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.T: // Get Ticker Information
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

                case ConsoleKey.A: // Get All Tickers' Information
                    SafeCall(() =>
                    {
                        var data = apiClient.MarketDataApi.GetAllTicker();
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.M: // Get Symbols List' Information
                    SafeCall(() =>
                    {
                        var data = apiClient.MarketDataApi.GetMarketList();
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.H:
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

                case ConsoleKey.K:
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

                case ConsoleKey.R: // Get Currencies Information
                    SafeCall(() =>
                    {
                        var data = apiClient.MarketDataApi.GetCurrencies();
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.D: // Get Ticker Information
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

                case ConsoleKey.P: // Get Currencies Information
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

                case ConsoleKey.O: // Get Part Order Book
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

                case ConsoleKey.F: // Get Full Order Book
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
    }
}
