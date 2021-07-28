using System;
using NLog;
using PoissonSoft.KuСoinApi;

namespace KuСoinApi.Example
{
    class Program
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            ICredentialsProvider credentialsProvider = new NppCryptProvider();
            KuСoinApiClientCredentials credentials;
            try
            {
                credentials = credentialsProvider.GetCredentials();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
            
            var apiClient = new KuСoinApiClient(credentials, logger) { IsDebug = true };

            new ActionManager(apiClient).Run();
        }
    }
}
