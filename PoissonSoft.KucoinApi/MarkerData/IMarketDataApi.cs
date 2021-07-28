using PoissonSoft.KuСoinApi.Contracts;
using PoissonSoft.KuСoinApi.Contracts.MarketData;
using PoissonSoft.KuСoinApi.Contracts.MarketData.Request;
using PoissonSoft.KuСoinApi.Contracts.MarketData.Response;
using PoissonSoft.KuСoinApi.Contracts.MarketData.Response.GetMarketList;

namespace PoissonSoft.KuСoinApi.MarkerData
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
        CurrencyDetail GetCurrencyDetail(Url request);
        FiatPriceList GetFiatPrice(FiatPrice request);
        FiatPriceList GetPartOrderBook(TradePair request, byte count);
        FiatPriceList GetFullOrderBook(TradePair request);
    }
}
