using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PoissonSoft.CommonUtils.ConsoleUtils;
using PoissonSoft.KuCoinApi.Contracts;
using PoissonSoft.KuCoinApi.Contracts.Enums;
using PoissonSoft.KuCoinApi.Contracts.MarketData.Request;
using PoissonSoft.KuCoinApi.Contracts.User;
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

                case ConsoleKey.A: // Create Deposit Address V1
                    SafeCall(() =>
                    {
                        var data = apiClient.UserApi.CreateDepositAddressV1(new ReqDepositAddress
                        {
                            Symbol = InputHelper.GetString("Ticker: ")
                        });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.B: // Create an Account
                    SafeCall(() =>
                    {
                        var data = apiClient.UserApi.GetDepositAddressV2(new ReqDepositAddress
                        {
                            Symbol = InputHelper.GetString("Ticker: ")

                        });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.C: // Create an Account
                    SafeCall(() =>
                    {
                        var Symbol = InputHelper.GetString("Ticker: ");
                        var data = apiClient.UserApi.GetDepositAddress(Symbol, null);
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
                        //DepositList data;
                        //for (int i = 0; i < 20; i++)
                        //{

                         //   Console.WriteLine($"{i}_{DateTimeOffset.UtcNow}");
                            var data = apiClient.UserApi.GetDepositList(
                                new ReqDepositAndWithdrawList
                                {
                                    Coin = InputHelper.GetString("Currency: ")
                                });
                             Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                        //}

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
                        var data = apiClient.UserApi.GetHistoricalDepositsList(new ReqDepositAndWithdrawList
                        {
                            Coin = InputHelper.GetString("Currency: ")
                        });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.F: // Get Withdrawals List
                    SafeCall(() =>
                    {
                        var data = apiClient.UserApi.GetWithdrawalsList(new ReqDepositAndWithdrawList
                        {
                            Coin = InputHelper.GetString("Currency: ")
                        });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.G: // Get V1 Historical Withdrawals List
                    SafeCall(() =>
                    {
                        var data = apiClient.UserApi.GetHistoricalWithdrawList(new ReqDepositAndWithdrawList
                        {
                            Coin = InputHelper.GetString("Currency: "),
                           // CurrentPage = Convert.ToInt32(InputHelper.GetString("The current page: ")),
                           // PageSize= Convert.ToInt32(InputHelper.GetString("Number of entries per page: "))
                        });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.H: // Get Withdrawal Quotas
                    SafeCall(() =>
                    {
                        var data = apiClient.UserApi.GetWithdrawalQuotas(new ReqCurrencyInfo
                        {
                            Currency = InputHelper.GetString("Currency: ")
                        });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.I: // Apply Withdraw
                    SafeCall(() =>
                    {
                        var data = apiClient.UserApi.ApplyWithdraw(new ReqWithdraw
                        {
                            Coin = InputHelper.GetString("Currency: "),
                            Address = InputHelper.GetString("Withdrawal address: "),
                            Amount = InputHelper.GetDecimal("Withdrawal amount: ")
                        });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.J: // Cancel Withdrawal
                    SafeCall(() =>
                    {
                        var data = apiClient.UserApi.CancelWithdrawal(new SpecialBuildQuery
                        {
                            Parameter = InputHelper.GetString("Path parameter, a unique ID for a withdrawal order: ")
                        });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;


                case ConsoleKey.Z: // List Accounts
                    SafeCall(() =>
                    {
                        var data = apiClient.UserApi.GetAccountLedgers(new ReqLedgers
                        {
                            AccountId = InputHelper.GetString("AccountId: "),
                            Direction = InputHelper.GetEnum<Direction>("Direction: "),
                            BusinesType = InputHelper.GetEnum<BusinessType>("Business type: "),
                            StartAt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - 100000,
                            EndAt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
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
