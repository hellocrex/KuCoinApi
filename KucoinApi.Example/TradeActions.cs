using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using PoissonSoft.CommonUtils.ConsoleUtils;
using PoissonSoft.KuCoinApi.Contracts;
using PoissonSoft.KuCoinApi.Contracts.Enums;
using PoissonSoft.KuCoinApi.Contracts.MarketData.Request;
using PoissonSoft.KuCoinApi.Contracts.Trade;
using PoissonSoft.KuCoinApi.Contracts.Trade.Request;
using PoissonSoft.KuCoinApi.Contracts.Trade.Response;

namespace KuCoinApi.Example
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
                            new ReqNewOrder
                            {
                                ClientOid = Guid.NewGuid().ToString(),
                                Instrument = InputHelper.GetString("Trade instrument symbol: "),
                                Side = InputHelper.GetEnum<OrderSide>("Side"),
                                Type = InputHelper.GetEnum<OrderType>("Type"),
                               // TradeType = InputHelper.GetEnum<TradeType>("Trade type"),
                               // STP = InputHelper.GetEnum<STP>("STP: "),
                                Price = InputHelper.GetDecimal("Price per base currency: "),
                               // Funds = InputHelper.GetString("The desired amount of quote currency to use: "),
                                Size = InputHelper.GetDecimal("Amount of base currency to buy or sell: ")

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
                            new ReqNewMarginOrder
                            {
                                ClientOid = Guid.NewGuid().ToString(),
                                Symbol = InputHelper.GetString("Trade instrument symbol: "),
                                Side = InputHelper.GetEnum<OrderSide>("Side"),
                                Type = InputHelper.GetEnum<OrderType>("Type"), 
                                //STP = InputHelper.GetEnum<STP>("STP: "),
                                Price = InputHelper.GetString("Price per base currency: "),
                                Size = InputHelper.GetString("Amount of base currency to buy or sell: ")
                            }
                        );
                        Console.WriteLine(JsonConvert.SerializeObject(order, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.C: // Orders | Place a new order
                    SafeCall(() =>
                    {
                        var order = apiClient.TradeApi.PlaceBulkOrders(
                            new ReqBulkOrder
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
                        var order = apiClient.TradeApi.CancelOrder(
                            new SpecialBuildQuery
                            {
                                Parameter = InputHelper.GetString("Order id: ")//Guid.NewGuid().ToString())
                            }
                        );
                        Console.WriteLine(JsonConvert.SerializeObject(order, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.E: // Orders | Place a new order
                    SafeCall(() =>
                    {
                        var order = apiClient.TradeApi.CancelSingleOrderByClientOid(
                            new SpecialBuildQuery
                            {
                                Parameter = InputHelper.GetString("Order by clientOid: ")
                            }
                        );
                        Console.WriteLine(JsonConvert.SerializeObject(order, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.F: // Orders | Place a new order
                    SafeCall(() =>
                    {
                        var order = apiClient.TradeApi.CancelAllOrders(
                            new ReqCancelOrders
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
                            new ReqOrderList
                            {
                                Instrument = InputHelper.GetString("Instruments: "),
                                //StatusOrder = InputHelper.GetEnum<StatusOrder>("active or done: "),
                                StartTime = 1630847139978,
                               // Side = InputHelper.GetEnum<OrderSide>(""),
                                TypeTrade = TradeType.Trade//InputHelper.GetEnum<TradeType>("The type of trading: ")
                            });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.H: // Orders | List Orders
                    SafeCall(() =>
                    {
                        var data = apiClient.TradeApi.GetHistoricalOrdersList(
                            new ReqHistoricalOrder()
                            {
                                Symbol = InputHelper.GetString("Symbol: "),
                                Side = InputHelper.GetEnum<OrderSide>("Side: ")
                               // StartTime = 1628154187000
                                // PageSize = Convert.ToInt32(InputHelper.GetString("PageSize: "))
                            });
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
                            new SpecialBuildQuery
                            {
                                Parameter = InputHelper.GetString("Order ID: ")
                            });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.K: // Orders | Get Single Active Order by clientOid
                    SafeCall(() =>
                    {
                        var data = apiClient.TradeApi.GetSingleActiveOrderByClientOid(
                            new SpecialBuildQuery
                            {
                                Parameter = InputHelper.GetString("clientOid: ")
                            });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.L: // List Fills
                    SafeCall(() =>
                    {
                        var data = apiClient.TradeApi.ListFills(
                            new ReqFills
                            {
                                OrderId = InputHelper.GetString("OrderId: ")
                              //  TradeType = InputHelper.GetEnum<TradeType>("The type of trading : TRADE（Spot Trading）, MARGIN_TRADE (Margin Trading): "),
                              //  Symbol = InputHelper.GetString("Symbol: "),
                               // Type = InputHelper.GetEnum<OrderType>(""),
                               // Side = InputHelper.GetEnum<OrderSide>(""),
                               // StartTime = 1630851083730
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

                case ConsoleKey.N: // Orders | Get Single Active Order by clientOid
                    SafeCall(() =>
                    {
                        var data = apiClient.TradeApi.PlaceNewStopOrder(
                            new ReqNewOrder
                            {
                                ClientOid = Guid.NewGuid().ToString(),
                                Side = InputHelper.GetEnum<OrderSide>(""),
                                Instrument = InputHelper.GetString("A valid trading symbol code. e.g. ETH-BTC: "),
                                StopPrice = InputHelper.GetDecimal("Stop price: "),
                                Price = InputHelper.GetDecimal("Price: "),
                                Size = InputHelper.GetDecimal("Size: ")
                                
                            });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.O: // Orders | Get Single Active Order by clientOid
                    SafeCall(() =>
                    {
                        var data = apiClient.TradeApi.CancelStopOrder(
                            new SpecialBuildQuery
                            {
                                Parameter = InputHelper.GetString("unique ID of the order: ")
                            });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.P: // Orders | Get Single Active Order by clientOid
                    SafeCall(() =>
                    {
                        var data = apiClient.TradeApi.CancelStopOrders(
                            new ReqCancelOrders
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
                            new SpecialBuildQuery
                            {
                                Parameter = InputHelper.GetString("Order ID: ")
                            });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.R: // Stop Orders | List Stop Orders
                    SafeCall(() =>
                    {
                        var data = apiClient.TradeApi.ListStopOrders(
                            new ReqListStopOrder
                            {
                                //StatusOrder = InputHelper.GetEnum<StatusOrder>("active or done: "),
                                Instrument = InputHelper.GetString("Symbol: "),
                                TypeTrade = InputHelper.GetEnum<TradeType>("Type trade: "),
                                //Side = InputHelper.GetEnum<OrderSide>(""),
                            });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.S: // Stop Orders | List Stop Orders
                    SafeCall(() =>
                    {
                        var data = apiClient.TradeApi.GetStopSingleOrderByClientOId(
                            new ReqOrderByClientOId
                            {
                                ClientOid = InputHelper.GetString("ClientOid: "),
                                //Symbol = InputHelper.GetString("Symbol: ")
                                
                            });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.T: // Stop Orders | List Stop Orders
                    SafeCall(() =>
                    {
                        var data = apiClient.TradeApi.CancelStopSingleOrderByClientOId(
                            new ReqOrderByClientOId
                            {
                                ClientOid = InputHelper.GetString("ClientOid: "),
                               // Symbol = InputHelper.GetString("Symbol: ")

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