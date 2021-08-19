using Newtonsoft.Json;
using PoissonSoft.KuCoinApi.Contracts.MarketData.Response;

namespace PoissonSoft.KuCoinApi.Contracts.MarketData
{
    public class TradeHistory
    {
        /// <summary>
        /// Код http ответа
        /// </summary>
        [JsonProperty("code")]
        public int SystemCode { get; set; }

        /// <summary>
        /// TradeHistoryCoin
        /// </summary>
        [JsonProperty("data")]
        public Histories[] TradeHistoryCoin { get; set; }
    }
}
