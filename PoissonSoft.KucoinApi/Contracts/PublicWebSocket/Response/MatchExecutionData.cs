using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.KuCoinApi.Contracts.Enums;

namespace PoissonSoft.KuCoinApi.Contracts.PublicWebSocket.Response
{
    public class MatchExecutionData
    {
        [JsonProperty("symbol")]
        public string Instrument { get; set; }

        [JsonProperty("side")]
        public OrderSide Side { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("makerOrderId")]
        public string MakerOrderId { get; set; }

        [JsonProperty("sequence")]
        public long Sequence { get; set; }

        [JsonProperty("size")]
        public decimal Size { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("takerOrderId")]
        public string TakerOrderId { get; set; }

        [JsonProperty("tradeId")]
        public string TradeId { get; set; }

        [JsonProperty("time")]
        public long Time { get; set; }
    }
}
