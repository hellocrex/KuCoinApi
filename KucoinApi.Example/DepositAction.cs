using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.CommonUtils.ConsoleUtils;
using PoissonSoft.KuСoinApi.Contracts.Enums;
using PoissonSoft.KuСoinApi.Contracts.MarketData.Request;
using PoissonSoft.KuСoinApi.Contracts.User;
using PoissonSoft.KuСoinApi.Contracts.User.Account.Request;
using PoissonSoft.KuСoinApi.Contracts.User.Request;

namespace KuСoinApi.Example
{
    internal partial class ActionManager
    {
        private bool ShowDepositPage()
        {
            var actions = new Dictionary<ConsoleKey, string>()
            {
                [ConsoleKey.C] = "Create Deposit Address V1",
                [ConsoleKey.D] = "Get Deposit Address V2",
                [ConsoleKey.S] = "Get Deposit Address",
                [ConsoleKey.L] = "Get Deposit List",
                [ConsoleKey.H] = "Get V1 Historical Deposits List",

                [ConsoleKey.Escape] = "Go back"
            };

            var selectedAction = InputHelper.GetUserAction("Select action:", actions);


            switch (selectedAction)
            {
                case ConsoleKey.B: // All Coins' Information
                    SafeCall(() =>
                    {
                        var data = apiClient.UserApi.AllCoinsInformation();
                        //Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                        Console.WriteLine("userApi Test");
                    });
                    return true;

                case ConsoleKey.C: // Create an Account
                    SafeCall(() =>
                    {
                        var data = apiClient.UserApi.CreateDepositAddressV1(new CurrencyReq
                        {
                            Symbol = InputHelper.GetString("Ticker: ")

                        });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.D: // Create an Account
                    SafeCall(() =>
                    {
                        var data = apiClient.UserApi.GetDepositAddressV2(new CurrencyReq
                        {
                            Symbol = InputHelper.GetString("Ticker: ")

                        });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.S: // Create an Account
                    SafeCall(() =>
                    {
                        var data = apiClient.UserApi.GetDepositAddress(new CurrencyReq
                        {
                            Symbol = InputHelper.GetString("Ticker: ")

                        });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                //case ConsoleKey.F: // Withdraw [SAPI]
                //    SafeCall(() =>
                //    {
                //        var withdrawRequest = new WithdrawRequest
                //        {
                //            Coin = InputHelper.GetString("Coin: "),
                //            Address = InputHelper.GetString("Address: "),
                //            AddressTag = InputHelper.GetString("AddressTag: "),
                //            Amount = InputHelper.GetDecimal("Amount: ")
                //        };
                //        if (withdrawRequest.AddressTag == string.Empty) withdrawRequest.AddressTag = null;

                //        if (!InputHelper.Confirm($"Warning! Do you really want to withdraw {withdrawRequest.Amount} {withdrawRequest.Coin} " +
                //                                 $"to address {withdrawRequest.Address}|{withdrawRequest.AddressTag}?")) return;

                //        var data = apiClient.WalletApi.Withdraw(withdrawRequest);
                //        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                //    });
                //    return true;

                case ConsoleKey.L: // Get Deposit List
                    SafeCall(() =>
                    {
                        var data = apiClient.UserApi.GetDepositList(
                            new CurrencyReq
                            {
                                Symbol = InputHelper.GetString("Ticker: ")
                            });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.H: // Get Deposit List
                    SafeCall(() =>
                    {
                        var data = apiClient.UserApi.GetV1HistoricalDepositsList(new CurrencyReq());
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

              
                case ConsoleKey.E: // List Accounts
                    SafeCall(() =>
                    {
                        var data = apiClient.UserApi.GetAccountLedgers(new Ledgers
                        {
                            Currency = InputHelper.GetString("Ticker: "),
                            Direction = InputHelper.GetEnum<Direction>("Direction: "),
                            BusinesType = InputHelper.GetEnum<BusinessType>("Business type: "),
                            startAt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - 100000,
                            endAt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
                        });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                //case ConsoleKey.L: // Deposit Address (supporting network)
                //    SafeCall(() =>
                //    {
                //        var data = apiClient.WalletApi.DepositAddress(
                //            InputHelper.GetString("Coin to deposit: "));
                //        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                //    });
                //    return true;

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
