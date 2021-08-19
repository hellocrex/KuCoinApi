using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KuCoinApi.Contracts.PublicWebSocket.Responce
{
    public class SymbolTickerData
    {
        [JsonProperty("bestAsk")]
        public float BestAsk { get; set; }

        [JsonProperty("bestAskSize")]
        public float BestAskSize { get; set; }

        [JsonProperty("bestBid")]
        public float BestBid { get; set; }

        [JsonProperty("bestBidSize")]
        public double BestBidSize { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("sequence")]
        public long Sequence { get; set; }

        [JsonProperty("size")]
        public decimal Size { get; set; }

        [JsonProperty("time")]
        public long Time { get; set; }
    }
}
