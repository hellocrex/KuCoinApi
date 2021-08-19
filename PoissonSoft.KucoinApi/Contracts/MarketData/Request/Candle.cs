using Newtonsoft.Json;
using PoissonSoft.KuCoinApi.Contracts.Enums;

namespace PoissonSoft.KuCoinApi.Contracts.MarketData.Request
{
    public class Candle
    {
        /// <summary>
        /// Filled side. The filled side is set to the taker by default
        /// </summary>
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        /// <summary>
        /// Filled side. The filled side is set to the taker by default
        /// </summary>
        [JsonProperty("startAt")]
        public long StartTime { get; set; }

        /// <summary>
        /// Filled side. The filled side is set to the taker by default
        /// </summary>
        [JsonProperty("endAt")]
        public long EndTime { get; set; }

        /// <summary>
        /// Filled side. The filled side is set to the taker by default
        /// </summary>
        [JsonProperty("type")]
        public CandlestickPattern CandlestickPattern { get; set; }
    }
}
