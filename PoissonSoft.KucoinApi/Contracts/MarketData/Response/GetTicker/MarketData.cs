using Newtonsoft.Json;

namespace PoissonSoft.KuСoinApi.Contracts.MarketData
{
    public class MarketData
    {
        /// <summary>
        /// Последовательность‎
        /// </summary>
        [JsonProperty("sequence")]
        public long Sequence { get; set; }

        /// <summary>
        /// ‎Лучшая цена ask‎
        /// </summary>
        [JsonProperty("bestAsk")]
        public float BestAsk { get; set; }

        /// <summary>
        /// Размер последней сделки‎
        /// </summary>
        [JsonProperty("size")]
        public decimal Size { get; set; }

        /// <summary>
        /// Цена последней торговли‎
        /// </summary>
        [JsonProperty("price")]
        public decimal Price { get; set; }

        /// <summary>
        /// ‎Лучший размер ставки‎
        /// </summary>
        [JsonProperty("bestBidSize")]
        public float BestBidSize { get; set; }

        /// <summary>
        /// Лучшая цена предложения‎
        /// </summary>
        [JsonProperty("bestBid")]
        public float BestBid { get; set; }

        /// <summary>
        /// Лучший размер ask‎
        /// </summary>
        [JsonProperty("bestAskSize")]
        public decimal BestAskSize { get; set; }

        /// <summary>
        /// timestamp
        /// </summary>
        [JsonProperty("time")]
        public long Time { get; set; }
    }
}