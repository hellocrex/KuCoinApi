using System;
using System.Collections.Generic;
using System.Text;
using PoissonSoft.CommonUtils.ConsoleUtils;
using PoissonSoft.KuCoinApi;

namespace KuCoinApi.Example
{
    internal partial class ActionManager
    {
        private readonly KuCoinApiClient apiClient;

        public ActionManager(KuCoinApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public void Run()
        {
            while (ShowMainPage())
            {
            }

            Console.WriteLine("> The program stopped. Press any key to exit...");
            Console.ReadKey();
        }

        private bool ShowMainPage()
        {
            Console.Clear();
            var actions = new Dictionary<ConsoleKey, string>()
            {
                [ConsoleKey.A] = "User API",
                [ConsoleKey.B] = "User.Deposit/Withdrawals",

                [ConsoleKey.C] = "Trade API",
                [ConsoleKey.D] = "Market Data",
                [ConsoleKey.E] = "Market Data Stream",

                [ConsoleKey.Escape] = "Go back (exit)",
            };

            var selectedAction = InputHelper.GetUserAction("Select action:", actions);

            switch (selectedAction)
            {
                case ConsoleKey.A:
                    while (ShowUserApiPage()) { }
                    return true;

                case ConsoleKey.B:
                    while (ShowDepositPage()) { }
                    return true;

                case ConsoleKey.C:
                    while (ShowTradeApiPage()) { }
                    return true;

                case ConsoleKey.D:
                    while (ShowMarketDataPage()) { }
                    return true;

                case ConsoleKey.E:
                    while (ShowMarketDataStreamPage()) { }
                    return true;



                case ConsoleKey.Escape:
                    return false;
                default:
                    return true;
            }
        }
        private void SafeCall(Action action)
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
