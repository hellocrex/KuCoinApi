using System;
using System.Collections.Generic;
using System.Text;
using NLog;
using PoissonSoft.KuCoinApi.User;
using PoissonSoft.KuCoinApi;
using PoissonSoft.KuCoinApi.MarkerData;
using PoissonSoft.KuCoinApi.PublicWebSocket;
using PoissonSoft.KuCoinApi.Trade;
using PoissonSoft.KuCoinApi.Transport;
using PoissonSoft.KuCoinApi.UserDataStream;

namespace PoissonSoft.KuCoinApi
{
    public sealed class KuCoinApiClient
    {
        private readonly KuCoinApiClientCredentials credentials;

        internal ILogger Logger { get; }

        /// <summary>
        /// Создание экземпляра
        /// </summary>
        /// <param name="credentials"></param>
        /// <param name="logger"></param>
        public KuCoinApiClient(KuCoinApiClientCredentials credentials, ILogger logger)
        {
            Logger = logger;
            this.credentials = credentials;
            Throttler = new Throttler(this);

            userApi = new UserApi(this, credentials, logger);
            marketDataApi = new MarketDataApi(this, credentials, logger);
            tradeApi = new TradeApi(this, credentials, logger);
            //dataStream = new UserDataStream.UserDataStream(this, credentials);
            webSocketPublicChannel = new PublicChannel(this, credentials);
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

        /// <summary>
        /// User Data Stream
        /// </summary>
        //public IUserDataStream UserDataStream => dataStream;
        //private readonly UserDataStream.UserDataStream dataStream;

        /// <summary>
        /// WebSocketFeed
        /// </summary>
        public IPublicChannel PublicChannel => webSocketPublicChannel;
        private readonly PublicChannel webSocketPublicChannel;
    }

}
