using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KuСoinApi.Contracts.MarketData
{
    public class MarketData
    {
        /// <summary>
        /// Последовательность‎
        /// </summary>
        [JsonProperty("sequence")]
        public string Sequence { get; set; }

        /// <summary>
        /// ‎Лучшая цена ask‎
        /// </summary>
        [JsonProperty("bestAsk")]
        public string BestAsk { get; set; }

        /// <summary>
        /// Размер последней сделки‎
        /// </summary>
        [JsonProperty("size")]
        public string Size { get; set; }

        /// <summary>
        /// Цена последней торговли‎
        /// </summary>
        [JsonProperty("price")]
        public decimal Price { get; set; }

        /// <summary>
        /// ‎Лучший размер ставки‎
        /// </summary>
        [JsonProperty("bestBidSize")]
        public string BestBidSize { get; set; }

        /// <summary>
        /// Лучшая цена предложения‎
        /// </summary>
        [JsonProperty("bestBid")]
        public string BestBid { get; set; }

        /// <summary>
        /// Лучший размер ask‎
        /// </summary>
        [JsonProperty("bestAskSize")]
        public string BestAskSize { get; set; }

        /// <summary>
        /// timestamp
        /// </summary>
        [JsonProperty("time")]
        public string Time { get; set; }
    }
}
