using System;
using System.Net.Http;
using NLog;
using PoissonSoft.KuCoinApi.Contracts;
using PoissonSoft.KuCoinApi.Contracts.MarketData;
using PoissonSoft.KuCoinApi.Contracts.MarketData.Request;
using PoissonSoft.KuCoinApi.Contracts.MarketData.Response;
using PoissonSoft.KuCoinApi.Contracts.User.Request;
using PoissonSoft.KuCoinApi.Transport;
using PoissonSoft.KuCoinApi.Transport.Rest;
using PoissonSoft.KuCoinApi.Utils;

namespace PoissonSoft.KuCoinApi.MarkerData
{
    internal sealed class MarketDataApi : IMarketDataApi
    {
        private readonly KuCoinApiClient apiClient;
        private readonly RestClient client;

        public MarketDataApi(KuCoinApiClient apiClient, KuCoinApiClientCredentials credentials, ILogger logger)
        {

            this.apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            client = new RestClient(logger, "https://api.kucoin.com/api/v1",
                new[] { EndpointSecurityType.Trade }, credentials, this.apiClient.Throttler);

            exchangeInfoCache = new SimpleCache<ExchangeInfo>(LoadExchangeInfo, logger);
        }

        private readonly SimpleCache<ExchangeInfo> exchangeInfoCache;

        #region Symbols & Ticker
        public ExchangeInfo GetSymbolsList(int cacheValidityIntervalSec = 1800)
        {
            return exchangeInfoCache.GetValue(TimeSpan.FromSeconds(cacheValidityIntervalSec));
        }

        public ExchangeInfo GetSymbolsList(ReqSymbolList request)
        {
            return client.MakeRequest<ExchangeInfo>(
                new RequestParameters(HttpMethod.Get, "symbols", 0)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public TradePairInfo GetTicker(ReqTradeInstrument request)
        {
            return client.MakeRequest<TradePairInfo>(
                new RequestParameters(HttpMethod.Get, "market/orderbook/level1", 0)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public StatisticTickerPair Get24hrStats(ReqTradeInstrument request)
        {
            return client.MakeRequest<StatisticTickerPair>(
                new RequestParameters(HttpMethod.Get, "market/stats", 0)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public AllMarketTickers GetAllTicker(int cacheValidityIntervalSec = 1800)
        {
            return client.MakeRequest<AllMarketTickers>(new RequestParameters(HttpMethod.Get, "market/allTickers", 0));
        }
        private ExchangeInfo LoadExchangeInfo()
        {
            return client.MakeRequest<ExchangeInfo>(new RequestParameters(HttpMethod.Get, "symbols", 0));
        }

        public MarketList GetMarketList()
        {
            return client.MakeRequest<MarketList>(new RequestParameters(HttpMethod.Get, "markets", 0));
        }

        #endregion

        #region Order Book
        public OrderBook GetPartOrderBook(ReqTradeInstrument request, byte count)
        {
            return client.MakeRequest<OrderBook>(
                new RequestParameters(HttpMethod.Get, $"market/orderbook/level2_{count}", 0)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public OrderBook GetFullOrderBookDeprecated(ReqTradeInstrument request)
        {
            return client.MakeRequest<OrderBook>(
                new RequestParameters(HttpMethod.Get, "market/orderbook/level2", 0)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public OrderBook GetFullOrderBook(ReqTradeInstrument request)
        {
            return client.MakeRequest<OrderBook>(
                new RequestParameters(HttpMethod.Get, "market/orderbook/level2", 30 / 3)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        #endregion
        
        #region Histories

        public TradeHistory GetTradeHistories(ReqTradeInstrument request)
        {
            return client.MakeRequest<TradeHistory>(
                new RequestParameters(HttpMethod.Get, "market/histories", 0)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public CandleData GetKlines(ReqCandles request)
        {
            return client.MakeRequest<CandleData>(
                new RequestParameters(HttpMethod.Get, "market/candles", 0)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        #endregion

        #region Currencies

        public CurrencyList GetCurrencies()
        {
            return client.MakeRequest<CurrencyList>(new RequestParameters(HttpMethod.Get, "currencies", 0));
        }
        //SpecialBuildQuery
        //ReqCurrencyInfo
        public CurrencyDetail GetCurrencyDetail(SpecialBuildQuery request)
        {
            return client.MakeRequest<CurrencyDetail>(
                new RequestParameters(HttpMethod.Get, "currencies", 0)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public FiatPriceList GetFiatPrice(ReqFiatPrices request)
        {
            return client.MakeRequest<FiatPriceList>(
                new RequestParameters(HttpMethod.Get, "prices", 0)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        #endregion

    }
}
