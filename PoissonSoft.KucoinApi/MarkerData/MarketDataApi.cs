using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using NLog;
using PoissonSoft.KuСoinApi.Contracts;
using PoissonSoft.KuСoinApi.Contracts;
using PoissonSoft.KuСoinApi.Contracts.MarketData;
using PoissonSoft.KuСoinApi.Contracts.MarketData.Request;
using PoissonSoft.KuСoinApi.Contracts.MarketData.Response;
using PoissonSoft.KuСoinApi.Contracts.MarketData.Response.Get24hrStats;
using PoissonSoft.KuСoinApi.Contracts.MarketData.Response.GetMarketList;
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

        public TradePairInfo GetTicker(TradePair request)
        {
            return client.MakeRequest<TradePairInfo>(
                new RequestParameters(HttpMethod.Get, "market/orderbook/level1", 1)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public StatisticTickerPair Get24hrStats(TradePair request)
        {
            return client.MakeRequest<StatisticTickerPair>(
                new RequestParameters(HttpMethod.Get, "market/stats", 1)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public AllMarketTickers GetAllTicker(int cacheValidityIntervalSec = 1800)
        {
            return client.MakeRequest<AllMarketTickers>(new RequestParameters(HttpMethod.Get, "market/allTickers", 1));
        }
        private ExchangeInfo LoadExchangeInfo()
        {
            return client.MakeRequest<ExchangeInfo>(new RequestParameters(HttpMethod.Get, "symbols", 1));
        }

        public MarketList GetMarketList()
        {
            return client.MakeRequest<MarketList>(new RequestParameters(HttpMethod.Get, "markets", 1));
        }


        public FiatPriceList GetPartOrderBook(TradePair request, byte count)
        {
            return client.MakeRequest<FiatPriceList>(
                new RequestParameters(HttpMethod.Get, $"market/orderbook/level2_{count}", 1)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public FiatPriceList GetFullOrderBook(TradePair request)
        {
            return client.MakeRequest<FiatPriceList>(
                new RequestParameters(HttpMethod.Get, "market/orderbook/level2", 1)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public TradeHistory GetTradeHistories(TradePair request)
        {
            return client.MakeRequest<TradeHistory>(
                new RequestParameters(HttpMethod.Get, "market/histories", 1)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public CandleData GetKlines(Candle request)
        {
            return client.MakeRequest<CandleData>(
                new RequestParameters(HttpMethod.Get, "market/candles", 1)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public CurrencyList GetCurrencies()
        {
            return client.MakeRequest<CurrencyList>(new RequestParameters(HttpMethod.Get, "currencies", 1));
        }

        public CurrencyDetail GetCurrencyDetail(Url request)
        {

            // return client.MakeRequest<CurrencyDetail>(new RequestParameters(HttpMethod.Get, "currencies/BTC", 1));
            return client.MakeRequest<CurrencyDetail>(
                new RequestParameters(HttpMethod.Get, "currencies", 1)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public FiatPriceList GetFiatPrice(FiatPrice request)
        {
            return client.MakeRequest<FiatPriceList>(
                new RequestParameters(HttpMethod.Get, "prices", 1)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
            //return client.MakeRequest<FiatPriceList>(new RequestParameters(HttpMethod.Get, "prices", 1));
        }

    }
}
