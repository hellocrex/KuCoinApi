using System;
using NLog;
using PoissonSoft.KucoinApi;

namespace KucoinApi.Example
{
    class Program
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            ICredentialsProvider credentialsProvider = new NppCryptProvider();
            KucoinApiClientCredentials credentials;
            try
            {
                credentials = credentialsProvider.GetCredentials();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            var apiClient = new KucoinApiClient(credentials, logger) { IsDebug = true };

            new ActionManager(apiClient).Run();
        }
    }
}
