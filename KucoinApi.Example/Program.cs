using System;
using NLog;
using PoissonSoft.KuCoinApi;

namespace KuCoinApi.Example
{
    class Program
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            ICredentialsProvider credentialsProvider = new NppCryptProvider();
            KuCoinApiClientCredentials credentials;
            try
            {
                credentials = credentialsProvider.GetCredentials();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
            
            var apiClient = new KuCoinApiClient(credentials, logger) { IsDebug = true };

            new ActionManager(apiClient).Run();
        }
    }
}
