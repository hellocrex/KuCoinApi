using System;
using System.Collections.Generic;
using System.Text;
using NLog;
using PoissonSoft.KuСoinApi.User;
using PoissonSoft.KuСoinApi;
using PoissonSoft.KuСoinApi.MarkerData;
using PoissonSoft.KuСoinApi.Trade;
using PoissonSoft.KuСoinApi.Transport;

namespace PoissonSoft.KuСoinApi
{
    public sealed class KuСoinApiClient
    {
        private readonly KuСoinApiClientCredentials credentials;

        internal ILogger Logger { get; }

        /// <summary>
        /// Создание экземпляра
        /// </summary>
        /// <param name="credentials"></param>
        /// <param name="logger"></param>
        public KuСoinApiClient(KuСoinApiClientCredentials credentials, ILogger logger)
        {
            Logger = logger;
            this.credentials = credentials;

            userApi = new UserApi(this, credentials, logger);
            marketDataApi = new MarketDataApi(this, credentials, logger);
            tradeApi = new TradeApi(this, credentials, logger);
        }

        public bool IsDebug { get; set; } = false;

        internal Throttler Throttler { get; }

        public IUserApi UserApi => userApi;
        private readonly UserApi userApi;

        /// <summary>
        /// Rest-API для получение рыночных данных
        /// </summary>
        public IMarketDataApi MarketDataApi => marketDataApi;
        private readonly MarketDataApi marketDataApi;

        /// <summary>
        /// Rest-API для получение рыночных данных
        /// </summary>
        public ITradeApi TradeApi => tradeApi;
        private readonly TradeApi tradeApi;
    }

}
