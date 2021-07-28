using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using PoissonSoft.CommonUtils.ConsoleUtils;
using PoissonSoft.KuСoinApi.Contracts;
using PoissonSoft.KuСoinApi.Contracts.Enums;
using PoissonSoft.KuСoinApi.Contracts.MarketData.Request;
using PoissonSoft.KuСoinApi.Contracts.Trade;
using PoissonSoft.KuСoinApi.Contracts.Trade.Request;
using PoissonSoft.KuСoinApi.Contracts.Trade.Response;

namespace KuСoinApi.Example
{
    internal partial class ActionManager
    {
        private bool ShowTradeApiPage()
        {
            var actions = new Dictionary<ConsoleKey, string>()
            {
                [ConsoleKey.A] = "Orders | Place a new order",
                [ConsoleKey.B] = "Orders | Place a margin order",
                [ConsoleKey.C] = "Orders | Place Bulk Orders",
                [ConsoleKey.D] = "Orders | Cancel an order",
                [ConsoleKey.E] = "Orders | Cancel Single Order by clientOid",
                [ConsoleKey.F] = "Orders | Cancel all orders",
                [ConsoleKey.G] = "Orders | List Orders",
                [ConsoleKey.H] = "Orders | Get V1 Historical Orders List",
                [ConsoleKey.I] = "Orders | Recent Orders",
                [ConsoleKey.J] = "Orders | Get an order",
                [ConsoleKey.K] = "Orders | Get Single Active Order by clientOid",
                [ConsoleKey.L] = "List Fills",
                [ConsoleKey.M] = "Recent Fills",
                [ConsoleKey.N] = "Stop Order | Place a new order",
                [ConsoleKey.O] = "Stop Order | Cancel an Order",
                [ConsoleKey.P] = "Stop Order | Cancel Orders",
                [ConsoleKey.Q] = "Stop Order | Get Single Order Info",
                [ConsoleKey.R] = "Stop Order | List Stop Orders",
                [ConsoleKey.S] = "Stop Order | Get Single Order by clientOid",
                [ConsoleKey.T] = "Stop Order | Cancel Single Order by clientOid",


                [ConsoleKey.Escape] = "Go back"
            };

            var selectedAction = InputHelper.GetUserAction("Select action:", actions);

            switch (selectedAction)
            {
                case ConsoleKey.A: // Orders | Place a new order
                    SafeCall(() =>
                    {
                        var order = apiClient.TradeApi.NewOrder(
                            new NewOrderRequest
                            {
                                ClientId = Guid.NewGuid().ToString(),
                                Symbol = InputHelper.GetString("Trade instrument symbol: "),
                                Side = InputHelper.GetEnum<OrderSide>("Side"),
                                Type = InputHelper.GetEnum<OrderType>("Type"),
                                TradeType = InputHelper.GetEnum<TradeType>("Trade type"),
                                STP = InputHelper.GetEnum<STP>("STP: "),
                                Price = InputHelper.GetString("Price per base currency: "),
                                size = InputHelper.GetString("Amount of base currency to buy or sell: ")

                            },
                            true
                        );
                        Console.WriteLine(JsonConvert.SerializeObject(order, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.B: // Orders | Place a new order
                    SafeCall(() =>
                    {
                        var order = apiClient.TradeApi.NewMarginOrder(
                            new NewMargin
                            {
                                ClientOid = Guid.NewGuid().ToString(),
                                Symbol = InputHelper.GetString("Trade instrument symbol: "),
                                Side = InputHelper.GetEnum<OrderSide>("Side"),
                                Type = InputHelper.GetEnum<OrderType>("Type"),
                                STP = InputHelper.GetEnum<STP>("STP: "),
                                Price = InputHelper.GetString("Price per base currency: "),
                                size = InputHelper.GetString("Amount of base currency to buy or sell: ")
                            }
                        );
                        Console.WriteLine(JsonConvert.SerializeObject(order, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.C: // Orders | Place a new order
                    SafeCall(() =>
                    {
                        var order = apiClient.TradeApi.PlaceBulkOrders(
                            new BulkOrder
                            {
                                ClientOid = Guid.NewGuid().ToString(),
                                Symbol = InputHelper.GetString("Trade instrument symbol: "),
                                Side = InputHelper.GetEnum<OrderSide>("Side"),
                                Type = InputHelper.GetEnum<OrderType>("Type"),
                                Price = InputHelper.GetString("Price"),
                                Size = InputHelper.GetString("Size"),
                                STP = InputHelper.GetEnum<STP>("STP: ")
                            }
                        );
                        Console.WriteLine(JsonConvert.SerializeObject(order, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.D: // Orders | Place a new order
                    SafeCall(() =>
                    {
                        var order = apiClient.TradeApi.CancelAnOrder(
                            new Url
                            {
                                UrlString = Guid.NewGuid().ToString()
                            }
                        );
                        Console.WriteLine(JsonConvert.SerializeObject(order, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.E: // Orders | Place a new order
                    SafeCall(() =>
                    {
                        var order = apiClient.TradeApi.CancelSingleOrderByClientOid(
                            new Url
                            {
                                UrlString = Guid.NewGuid().ToString()
                            }
                        );
                        Console.WriteLine(JsonConvert.SerializeObject(order, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.F: // Orders | Place a new order
                    SafeCall(() =>
                    {
                        var order = apiClient.TradeApi.CancelAllOrders(
                            new CancelOrders
                            {
                                Symbol = InputHelper.GetString("Trade instrument symbol: "),
                                TradeType = InputHelper.GetEnum<TradeType>("Type")
                            }
                        );
                        Console.WriteLine(JsonConvert.SerializeObject(order, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.G: // Orders | List Orders
                    SafeCall(() =>
                    {
                        var data = apiClient.TradeApi.ListOrders(
                            new OrderReq
                            {
                                StatusOrder = InputHelper.GetEnum<Status>("active or done: "),
                                //Symbol = InputHelper.GetString("Symbol: "),
                                Side = InputHelper.GetEnum<OrderSide>("")
                            });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.H: // Orders | List Orders
                    SafeCall(() =>
                    {
                        var data = apiClient.TradeApi.GetHistoricalOrdersList(
                            new HistoricalOrderReq()
                            {
                                Symbol = InputHelper.GetString("Symbol: "),
                                Side = InputHelper.GetEnum<OrderSide>(""),
                                PageSize = Convert.ToInt32(InputHelper.GetString("PageSize: ")),
                                StartTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - DateTimeOffset.UtcNow.AddDays(-5).ToUnixTimeMilliseconds()
                            });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.L: // List Fills
                    SafeCall(() =>
                    {
                        var data = apiClient.TradeApi.ListFills(
                            new FillsReq
                            {
                                TradeType = InputHelper.GetEnum<TradeType>("The type of trading : TRADE（Spot Trading）, MARGIN_TRADE (Margin Trading): ")
                            });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.M: // Recent Fills
                    SafeCall(() =>
                    {
                        var data = apiClient.TradeApi.RecentFills();
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.I: // Orders | Recent Orders
                    SafeCall(() =>
                    {
                        var data = apiClient.TradeApi.RecentOrders();
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.J: // Orders | Get an order
                    SafeCall(() =>
                    {
                        var data = apiClient.TradeApi.GetOrder(
                            new Url
                            {
                                UrlString = InputHelper.GetString("Order ID: ")
                            });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.K: // Orders | Get Single Active Order by clientOid
                    SafeCall(() =>
                    {
                        var data = apiClient.TradeApi.GetSingleActiveOrderByClientOid(
                            new Url
                            {
                                UrlString = InputHelper.GetString("clientOid: ")
                            });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.N: // Orders | Get Single Active Order by clientOid
                    SafeCall(() =>
                    {
                        var data = apiClient.TradeApi.PlaceNewStopOrder(
                            new NewStopOrder
                            {
                                ClientOid = Guid.NewGuid().ToString(),
                                Side = InputHelper.GetEnum<OrderSide>(""),
                                Symbol = InputHelper.GetString("A valid trading symbol code. e.g. ETH-BTC: "),
                                StopPrice = InputHelper.GetString("Stop price: ")
                            });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.O: // Orders | Get Single Active Order by clientOid
                    SafeCall(() =>
                    {
                        var data = apiClient.TradeApi.CancelStopOrder(
                            new Url
                            {
                                UrlString = InputHelper.GetString("unique ID of the order: ")
                            });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.P: // Orders | Get Single Active Order by clientOid
                    SafeCall(() =>
                    {
                        var data = apiClient.TradeApi.CancelStopOrders(
                            new CancelStopOrder
                            {
                                Symbol = InputHelper.GetString("[Optional] symbol: "),
                                TradeType = InputHelper.GetEnum<TradeType>(""),
                                OrderIds = InputHelper.GetString("Unique ID of the order: ")
                            });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.Q: // Orders | Get Single Active Order by clientOid
                    SafeCall(() =>
                    {
                        var data = apiClient.TradeApi.GetStopSingleOrderInfo(
                            new Url
                            {
                                UrlString = InputHelper.GetString("Order ID: ")
                            });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.R: // Stop Orders | List Stop Orders
                    SafeCall(() =>
                    {
                        var data = apiClient.TradeApi.ListStopOrders(
                            new ListStopOrder
                            {
                                StatusOrder = InputHelper.GetEnum<Status>("active or done: "),
                                Symbol = InputHelper.GetString("Symbol: "),
                                Side = InputHelper.GetEnum<OrderSide>(""),
                                CurrentPage = Convert.ToInt32(InputHelper.GetString("Current page: "))
                            });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.S: // Stop Orders | List Stop Orders
                    SafeCall(() =>
                    {
                        var data = apiClient.TradeApi.GetStopSingleOrderByClientOId(
                            new SingleOrderByClientOId
                            {
                                ClientOid = InputHelper.GetString("ClientOid: "),
                                Symbol = InputHelper.GetString("Symbol: ")
                                
                            });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.T: // Stop Orders | List Stop Orders
                    SafeCall(() =>
                    {
                        var data = apiClient.TradeApi.CancelStopSingleOrderByClientOId(
                            new SingleOrderByClientOId
                            {
                                ClientOid = InputHelper.GetString("ClientOid: "),
                                Symbol = InputHelper.GetString("Symbol: ")

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