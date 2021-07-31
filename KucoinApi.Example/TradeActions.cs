using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.CommonUtils.ConsoleUtils;
using PoissonSoft.KuСoinApi.Contracts.Enums;
using PoissonSoft.KuСoinApi.Contracts.Trade;

namespace KuСoinApi.Example
{
    internal partial class ActionManager
    {
        private bool ShowTradeApiPage()
        {
            var actions = new Dictionary<ConsoleKey, string>()
            {
                [ConsoleKey.N] = "Place a new order",
                [ConsoleKey.M] = "Place a margin order",
                [ConsoleKey.B] = "Place Bulk Orders",
                [ConsoleKey.C] = "Cancel an order",
                [ConsoleKey.S] = "Cancel Single Order by clientOid",
                [ConsoleKey.A] = "Cancel all orders",
                [ConsoleKey.L] = "List Orders",
                [ConsoleKey.G] = "Get V1 Historical Orders List",
                [ConsoleKey.R] = "Recent Orders",
                [ConsoleKey.O] = "Get an order",
                [ConsoleKey.F] = "Get Single Active Order by clientOid",

                [ConsoleKey.Escape] = "Go back"
            };

            var selectedAction = InputHelper.GetUserAction("Select action:", actions);

            switch (selectedAction)
            {
                case ConsoleKey.N: // All Coins' Information
                    SafeCall(() =>
                    {
                        var order = apiClient.TradeApi.NewOrder(
                            new NewOrderRequest
                            {
                                Symbol = InputHelper.GetString("Trade instrument symbol: "),
                                Side = InputHelper.GetEnum<OrderSide>("Side"),
                                Type = InputHelper.GetEnum<OrderType>("Type"),
                                TradeType = InputHelper.GetEnum<TradeType>("Trade type")
                            },
                            true
                        );
                        Console.WriteLine(JsonConvert.SerializeObject(order, Formatting.Indented));
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