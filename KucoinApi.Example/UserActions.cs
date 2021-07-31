using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.CommonUtils.ConsoleUtils;
using PoissonSoft.KuСoinApi.Contracts.User;

namespace KuСoinApi.Example
{
    internal partial class ActionManager
    {
        private bool ShowUserApiPage()
        {
            var actions = new Dictionary<ConsoleKey, string>()
            {
                [ConsoleKey.C] = "Create an Account",
                [ConsoleKey.L] = "List Accounts",
                [ConsoleKey.A] = "Get an Account",
                [ConsoleKey.D] = "Get Account Ledgers(deprecated)",
                [ConsoleKey.E] = "Get Account Ledgers",
                [ConsoleKey.B] = "Get Account Balance of a Sub-Account",
                [ConsoleKey.S] = "Get the Aggregated Balance of all Sub-Accounts",
                [ConsoleKey.T] = "Get the Transferable",
                [ConsoleKey.Y] = "Transfer between Master user and Sub-user",
                [ConsoleKey.I] = "Inner Transfer",

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

                case ConsoleKey.H: // Deposit History (supporting network)
                    SafeCall(() =>
                    {
                        var data = apiClient.UserApi.DepositHistory(new DepositHistoryRequest
                        {
                            Coin = InputHelper.GetString("Coin: "),
                        });
                        //Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                    });
                    return true;

                //case ConsoleKey.J: // Withdraw History (supporting network)
                //    SafeCall(() =>
                //    {
                //        var data = apiClient.WalletApi.WithdrawHistory(new WithdrawHistoryRequest
                //        {
                //            Coin = InputHelper.GetString("Coin: "),
                //        });
                //        Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
                //    });
                //    return true;

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
