﻿using PoissonSoft.KuCoinApi.Contracts;
using PoissonSoft.KuCoinApi.Contracts.MarketData;
using PoissonSoft.KuCoinApi.Contracts.MarketData.Request;
using PoissonSoft.KuCoinApi.Contracts.MarketData.Response;
using PoissonSoft.KuCoinApi.Contracts.MarketData.Response.GetMarketList;

namespace PoissonSoft.KuCoinApi.MarkerData
{
    public interface IMarketDataApi
    {
        /// <summary>
        /// Current exchange trading rules and symbol information
        /// </summary>
        /// <param name="cacheValidityIntervalSec">Допускается возврат кешированных значений,
        /// полученных в течение указанного количества секунд назад. Если кешированные данные отсутствуют
        /// или были получены ранее указанного времени, то будут загружены актуальные данные</param>
        /// <returns></returns>
        ExchangeInfo GetSymbolsList(int cacheValidityIntervalSec = 30 * 60);
        TradePairInfo GetTicker(TradePair request);
        StatisticTickerPair Get24hrStats(TradePair request);
        AllMarketTickers GetAllTicker(int cacheValidityIntervalSec = 30 * 60);
        MarketList GetMarketList();
        TradeHistory GetTradeHistories(TradePair request);
        CandleData GetKlines(Candle request);
        CurrencyList GetCurrencies();
        CurrencyDetail GetCurrencyDetail(SpecialBuildQuery request);
        FiatPriceList GetFiatPrice(FiatPrice request);
        OrderBook GetPartOrderBook(TradePair request, byte count);
        OrderBook GetFullOrderBook(TradePair request);
        OrderBook GetFullOrderBookDeprecated(TradePair request);
    }
}
