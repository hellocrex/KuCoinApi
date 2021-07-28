using Newtonsoft.Json;
using PoissonSoft.KuСoinApi.Contracts.MarketData.Response;

namespace PoissonSoft.KuСoinApi.Contracts.MarketData
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
