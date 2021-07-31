using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.CommonUtils.ConsoleUtils;
using PoissonSoft.KuСoinApi.Contracts.MarketData;

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
                case ConsoleKey.T: // Get All Tickers' Information
                    SafeCall(() =>
                    {
                        var data = apiClient.MarketDataApi.GetTicker(new TradePair());
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
