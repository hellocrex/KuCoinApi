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
using PoissonSoft.KuCoinApi.DataStream;

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


            //var credential = new KuCoinApiClientCredentials
            //{
            //    ApiKey = "610ab077bc85c200065a674a",
            //    SecretKey = "b628fabb-8834-460f-bbc4-82cf3df4a418",
            //    PassPhrase = "mivit9g9AZ$"
            //};

            userApi = new UserApi(this, credentials, logger);
            marketDataApi = new MarketDataApi(this, credentials, logger);
            tradeApi = new TradeApi(this, credentials, logger);
            dataStream = new UserDataStream(this, credentials);
            userDataCollector = new UserDataCollector(this);
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
        /// Data Stream
        /// </summary>
        public IUserDataStream UserDataStream => dataStream;
        private readonly UserDataStream dataStream;

        /// <summary>
        /// Сборщик актуальных данных по аккаунту
        /// (балансы, ордеры, трейды)
        /// </summary>
        public IUserDataCollector UserDataCollector => userDataCollector;
        private readonly UserDataCollector userDataCollector;

        /// <summary>
        /// WebSocketFeed
        /// </summary>
        public IPublicChannel PublicChannel => webSocketPublicChannel;
        private readonly PublicChannel webSocketPublicChannel;

        /// <inheritdoc />
        public void Dispose()
        {

            //if (userDataCollector?.IsStarted == true) userDataCollector.Stop();
            //userDataCollector?.Dispose();

            //if (dataStream?.Status == DataStreamStatus.Active) dataStream.Close();
            //dataStream?.Dispose();

            //marketStreamsManager?.Dispose();

            //marketDataApi?.Dispose();
            //spotAccountApi?.Dispose();
            //walletApi?.Dispose();

            Throttler?.Dispose();
        }
    }


}
