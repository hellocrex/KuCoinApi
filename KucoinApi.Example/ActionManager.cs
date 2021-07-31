using System;
using System.Collections.Generic;
using System.Text;
using PoissonSoft.CommonUtils.ConsoleUtils;
using PoissonSoft.KuСoinApi;

namespace KuСoinApi.Example
{
    internal partial class ActionManager
    {
        private readonly KuСoinApiClient apiClient;

        public ActionManager(KuСoinApiClient apiClient)
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
                [ConsoleKey.A] = "User API NOT IMPL",

                [ConsoleKey.T] = "Trade API",
                [ConsoleKey.C] = "Market Data",

                [ConsoleKey.D] = "Spot Account API",
                [ConsoleKey.E] = "Spot Data Stream",
                [ConsoleKey.F] = "Spot Data Collector",

                [ConsoleKey.Escape] = "Go back (exit)",
            };

            var selectedAction = InputHelper.GetUserAction("Select action:", actions);

            switch (selectedAction)
            {
                case ConsoleKey.A:
                    while (ShowUserApiPage()) { }
                    return true;

                case ConsoleKey.C:
                    while (ShowMarketDataPage()) { }
                    return true;

                case ConsoleKey.T:
                    while (ShowTradeApiPage()) { }
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
