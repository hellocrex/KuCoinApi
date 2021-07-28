using System;
using System.Collections.Generic;
using System.Text;
using NLog;

namespace PoissonSoft.KucoinApi
{
    public sealed class KucoinApiClient
    {
        private readonly KucoinApiClientCredentials credentials;

        internal ILogger Logger { get; }

        /// <summary>
        /// Создание экземпляра
        /// </summary>
        /// <param name="credentials"></param>
        /// <param name="logger"></param>
        public KucoinApiClient(KucoinApiClientCredentials credentials, ILogger logger)
        {
            Logger = logger;
            this.credentials = credentials;
        }

        public bool IsDebug { get; set; } = false;
    }

}
