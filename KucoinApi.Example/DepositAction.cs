using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PoissonSoft.CommonUtils.ConsoleUtils;
using PoissonSoft.KuCoinApi.Contracts.Enums;
using PoissonSoft.KuCoinApi.Contracts.MarketData.Request;
using PoissonSoft.KuCoinApi.Contracts.User;
using PoissonSoft.KuCoinApi.Contracts.User.Account.Request;
using PoissonSoft.KuCoinApi.Contracts.User.Request;

namespace KuCoinApi.Example
{
    internal partial class ActionManager
    {
        private bool ShowDepositPage()
        {
            var actions = new Dictionary<ConsoleKey, string>()
            {
                [ConsoleKey.A] = "Create Deposit Address V1",
                [ConsoleKey.B] = "Get Deposit Address V2",
                [ConsoleKey.C] = "Get Deposit Address",
                [ConsoleKey.D] = "Get Deposit List",
                [ConsoleKey.E] = "Get V1 Historical Deposits List",
                [ConsoleKey.F] = "Get Withdrawals List",
                [ConsoleKey.G] = "Get V1 Historical Withdrawals List",
                [ConsoleKey.H] = "Get Withdrawal Quotas",
                [ConsoleKey.I] = "Apply Withdraw",
                [ConsoleKey.J] = "Cancel Withdrawal",

                [ConsoleKey.Escape] = "Go back"
            };

            var selectedAction = InputHelper.GetUserAction("Select action:", actions);


            switch (selectedAction)
            {
                case ConsoleKey.X: // All Coins' Information
                    SafeCall(() =>
                    {
                        var data = apiClient.UserApi.AllCoinsInformation();
                        //Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                        Console.WriteLine("userApi Test");
                    });
                    return true;

                case ConsoleKey.A: // Create an Account
                    SafeCall(() =>
                    {
                        var data = apiClient.UserApi.CreateDepositAddressV1(new CurrencyReq
                        {
                            Symbol = InputHelper.GetString("Ticker: ")

                        });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.B: // Create an Account
                    SafeCall(() =>
                    {
                        var data = apiClient.UserApi.GetDepositAddressV2(new CurrencyReq
                        {
                            Symbol = InputHelper.GetString("Ticker: ")

                        });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.C: // Create an Account
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

                case ConsoleKey.D: // Get Deposit List
                    SafeCall(() =>
                    {
                        DepositList data;
                        for (int i = 0; i < 20; i++)
                        {

                            Console.WriteLine($"{i}_{DateTimeOffset.UtcNow}");
                            data = apiClient.UserApi.GetDepositList(
                                new CurrencyReq
                                {
                                    Symbol = "BTC"//InputHelper.GetString("Currency: ")
                                });
                            // Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                        }

                        // нормальный вариант
                        //var data1 = apiClient.UserApi.GetDepositList(
                        //    new CurrencyReq
                        //    {
                        //        Symbol = "BTC"//InputHelper.GetString("Currency: ")
                        //    });
                        //Console.WriteLine(JsonConvert.SerializeObject(data1, Formatting.Indented));


                    });
                    return true;

                case ConsoleKey.E: // Get V1 Historical Deposits List
                    SafeCall(() =>
                    {
                        var data = apiClient.UserApi.GetV1HistoricalDepositsList(new DepositReq
                        {
                            Currency = InputHelper.GetString("Currency: ")
                        });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.F: // Get Withdrawals List
                    SafeCall(() =>
                    {
                        var data = apiClient.UserApi.GetWithdrawalsList(new DepositReq
                        {
                            Currency = InputHelper.GetString("Currency: ")
                        });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.G: // Get V1 Historical Withdrawals List
                    SafeCall(() =>
                    {
                        var data = apiClient.UserApi.GetV1HistoricalWithdrawList(new HistoricalWithdrawList
                        {
                            Currency = InputHelper.GetString("Currency: "),
                            CurrentPage = Convert.ToInt32(InputHelper.GetString("The current page: ")),
                            PageSize= Convert.ToInt32(InputHelper.GetString("Number of entries per page: "))
                        });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.H: // Get Withdrawal Quotas
                    SafeCall(() =>
                    {
                        var data = apiClient.UserApi.GetWithdrawalQuotas(new WithdrawQuota
                        {
                            Currency = InputHelper.GetString("Currency: ")
                        });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.I: // Apply Withdraw
                    SafeCall(() =>
                    {
                        var data = apiClient.UserApi.ApplyWithdraw(new WithdrawReq
                        {
                            Currency = InputHelper.GetString("Currency: "),
                            Address = InputHelper.GetString("Withdrawal address: "),
                            Amount = InputHelper.GetString("Withdrawal amount: ")
                        });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.J: // Cancel Withdrawal
                    SafeCall(() =>
                    {
                        var data = apiClient.UserApi.CancelWithdrawal(new Url
                        {
                            UrlString = InputHelper.GetString("Path parameter, a unique ID for a withdrawal order: ")
                        });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;


                case ConsoleKey.Z: // List Accounts
                    SafeCall(() =>
                    {
                        var data = apiClient.UserApi.GetAccountLedgers(new LedgersReq
                        {
                            Currency = InputHelper.GetString("Ticker: "),
                            Direction = InputHelper.GetEnum<Direction>("Direction: "),
                            BusinesType = InputHelper.GetEnum<BusinessType>("Business type: "),
                            StartAt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - 100000,
                            EndAt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
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
