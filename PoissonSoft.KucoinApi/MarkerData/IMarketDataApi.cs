using PoissonSoft.KuСoinApi.Contracts;
using PoissonSoft.KuСoinApi.Contracts.MarketData;

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

        MarketData GetTicker(TradePair request);
        TickerInfo GetAllTicker(int cacheValidityIntervalSec = 30 * 60);
    }
}
