using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using NLog;
using PoissonSoft.KuСoinApi.Contracts;
using PoissonSoft.KuСoinApi.Contracts;
using PoissonSoft.KuСoinApi.Contracts.MarketData;
using PoissonSoft.KuСoinApi.Contracts.User;
using PoissonSoft.KuСoinApi.Transport;
using PoissonSoft.KuСoinApi.Transport.Rest;
using PoissonSoft.KuСoinApi.Utils;

namespace PoissonSoft.KuСoinApi.MarkerData
{
    internal sealed class MarketDataApi : IMarketDataApi
    {
        private readonly KuСoinApiClient apiClient;
        private readonly RestClient client;

        public MarketDataApi(KuСoinApiClient apiClient, KuСoinApiClientCredentials credentials, ILogger logger)
        {

            this.apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            client = new RestClient(logger, "https://api.kucoin.com/api/v1",
                new[] { EndpointSecurityType.Trade }, credentials);
                //,this.apiClient.Throttler);

            exchangeInfoCache = new SimpleCache<ExchangeInfo>(LoadExchangeInfo, logger);
        }

        private readonly SimpleCache<ExchangeInfo> exchangeInfoCache;
        public ExchangeInfo GetSymbolsList(int cacheValidityIntervalSec = 1800)
        {
            return exchangeInfoCache.GetValue(TimeSpan.FromSeconds(cacheValidityIntervalSec));
        }

        public MarketData GetTicker(TradePair request)
        {
            return client.MakeRequest<MarketData>(
                new RequestParameters(HttpMethod.Get, "market/orderbook/level1", 1)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public TickerInfo GetAllTicker(int cacheValidityIntervalSec = 1800)
        {
            return client.MakeRequest<TickerInfo>(new RequestParameters(HttpMethod.Get, "market/allTickers", 1));
        }
        private ExchangeInfo LoadExchangeInfo()
        {
            return client.MakeRequest<ExchangeInfo>(new RequestParameters(HttpMethod.Get, "symbols", 1));
        }

    }
}
