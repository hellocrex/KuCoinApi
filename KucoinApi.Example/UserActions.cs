using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.CommonUtils.ConsoleUtils;
using PoissonSoft.KuCoinApi.Contracts.Enums;
using PoissonSoft.KuCoinApi.Contracts.MarketData;
using PoissonSoft.KuCoinApi.Contracts.MarketData.Request;
using PoissonSoft.KuCoinApi.Contracts.Trade.Request;
using PoissonSoft.KuCoinApi.Contracts.User;
using PoissonSoft.KuCoinApi.Contracts.User.Account.Request;
using PoissonSoft.KuCoinApi.Contracts.User.Request;

namespace KuCoinApi.Example
{
    internal partial class ActionManager
    {
        private bool ShowUserApiPage()
        {
            var actions = new Dictionary<ConsoleKey, string>()
            {
                [ConsoleKey.A] = "Get User Info of all Sub-Accounts",
                [ConsoleKey.B] = "Create an Account",
                [ConsoleKey.C] = "List Accounts",
                [ConsoleKey.D] = "Get an Account",
                [ConsoleKey.E] = "Get Account Ledgers(deprecated)",
                [ConsoleKey.F] = "Get Account Ledgers",
                [ConsoleKey.G] = "Get Account Balance of a Sub-Account",
                [ConsoleKey.H] = "Get the Aggregated Balance of all Sub-Accounts",
                [ConsoleKey.I] = "Get the Transferable",
                [ConsoleKey.J] = "Transfer between Master user and Sub-user",
                [ConsoleKey.K] = "Inner Transfer",
                [ConsoleKey.L] = "Basic user fee",
                [ConsoleKey.M] = "Actual fee rate of the trading pair",

                [ConsoleKey.Escape] = "Go back"
            };

            var selectedAction = InputHelper.GetUserAction("Select action:", actions);

            switch (selectedAction)
            {
                case ConsoleKey.X: // All Coins' Information
                    SafeCall(() =>
                    {
                        var data = apiClient.UserApi.AllCoinsInformation();
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                        Console.WriteLine("userApi Test");
                    });
                    return true;

                case ConsoleKey.B: // Create an Account
                    SafeCall(() =>
                    {
                        var data = apiClient.UserApi.CreateAccount(new AccountC
                        {
                            AccountType = InputHelper.GetEnum<AccountType>("AccountType"),
                            Currency = InputHelper.GetString("Ticker: ")
                            
                        });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.C: // List Accounts
                    SafeCall(() =>
                    {
                        var data = apiClient.UserApi.GetListAccounts(new AccountC
                        {
                            Currency = InputHelper.GetString("Currency: "),
                            AccountType = InputHelper.GetEnum<AccountType>("AccountType")
                        });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.D: // Create an Account
                    SafeCall(() =>
                    {
                        var data = apiClient.UserApi.GetAccount(new Url
                        {
                            UrlString = InputHelper.GetString("ID of the account: ")
                        });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.Z: // Deposit History (supporting network)
                    SafeCall(() =>
                    {
                        var data = apiClient.UserApi.DepositHistory(new DepositHistoryRequest
                        {
                            Coin = InputHelper.GetString("Coin: "),
                        });
                        //Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;


                case ConsoleKey.A: // "Get User Info of all Sub-Accounts
                    SafeCall(() =>
                    {
                        var data = apiClient.UserApi.UserInfo();
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.Y: // Withdraw History (supporting network)
                    SafeCall(() =>
                    {
                        var data = apiClient.UserApi.GetBasicUserFee();
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.E: // List Accounts
                    SafeCall(() =>
                    {
                        var data = apiClient.UserApi.GetAccountLedgersDeprecated(new LedgersDeprecatedReq
                        {
                            AccountId = InputHelper.GetString("AccountId: "),
                            //Direction = InputHelper.GetEnum<Direction>("Direction: "),
                            //BusinesType = InputHelper.GetEnum<BusinessType>("Business type: ")
                        });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.F: // List Accounts
                    SafeCall(() =>
                    {
                        var data = apiClient.UserApi.GetAccountLedgers(new LedgersReq
                        {
                            Currency = InputHelper.GetString("Currency: "),
                            StartAt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - DateTimeOffset.UtcNow.AddDays(-1).ToUnixTimeMilliseconds()
                            //Direction = InputHelper.GetEnum<Direction>("Direction: "),
                            //BusinesType = InputHelper.GetEnum<BusinessType>("Business type: ")
                        });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.G: // Get Account Balance of a Sub-Account
                    SafeCall(() =>
                    {
                        var data = apiClient.UserApi.GetAccountBalanceOfSubAccount(new Url
                        {
                            UrlString = InputHelper.GetString("The user ID of a sub-account: "),
                        });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.H: // Get the Aggregated Balance of all Sub-Accounts
                    SafeCall(() =>
                    {
                        var data = apiClient.UserApi.GetAggregatedBalanceOfAllSubAccounts();
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.I: // Get the Aggregated Balance of all Sub-Accounts
                    SafeCall(() =>
                    {
                        var data = apiClient.UserApi.GetTransferable(new AccountC
                        {
                            Currency = InputHelper.GetString("The user ID of a sub-account: "),
                            AccountType = InputHelper.GetEnum<AccountType>("")
                        });
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.L: // Deposit Address (supporting network)
                    SafeCall(() =>
                    {
                        var data = apiClient.UserApi.GetBasicUserFee();
                        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                case ConsoleKey.M: // Get the Aggregated Balance of all Sub-Accounts
                    SafeCall(() =>
                    {
                        var data = apiClient.UserApi.ActualFeeRateTradingPair(new TradePairs
                        {
                            Symbols = InputHelper.GetString("Trading pair (optional, you can inquire fee rates of 10 trading pairs each time at most): ")
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
