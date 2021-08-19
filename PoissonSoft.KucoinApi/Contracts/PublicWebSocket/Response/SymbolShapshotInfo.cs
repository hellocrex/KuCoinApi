using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KuCoinApi.Contracts.PublicWebSocket.Response
{
    public class SymbolShapshotInfo
    {
        [JsonProperty("averagePrice")]
        public decimal AveragePrice { get; set; }

        [JsonProperty("close")]
        public decimal Close { get; set; }

        [JsonProperty("trading")]
        public bool Trading { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("buy")]
        public decimal Buy { get; set; }

        [JsonProperty("sell")]
        public decimal Sell { get; set; }

        [JsonProperty("sort")]
        public int Sort { get; set; }

        [JsonProperty("volValue")]
        public decimal VolValue { get; set; }

        [JsonProperty("baseCurrency")]
        public string BaseCurrency { get; set; }

        [JsonProperty("market")]
        public string Market { get; set; }

        [JsonProperty("markets")]
        public string[] Markets { get; set; }

        [JsonProperty("open")]
        public decimal Open { get; set; }

        [JsonProperty("quoteCurrency")]
        public string QuoteCurrency { get; set; }

        [JsonProperty("symbolCode")] 
        public string SymbolCode { get; set; }

        [JsonProperty("datetime")]
        public long Datetime { get; set; }

        [JsonProperty("high")]
        public decimal High { get; set; }

        [JsonProperty("vol")]
        public decimal Vol { get; set; }

        [JsonProperty("low")]
        public decimal Low { get; set; }

        [JsonProperty("makerCoefficient")]
        public float MakerCoefficient { get; set; }

        [JsonProperty("makerFeeRate")]
        public float MakerFeeRate { get; set; }

        [JsonProperty("takerCoefficient")]
        public float TakerCoefficient { get; set; }

        [JsonProperty("takerFeeRate")]
        public float TakerFeeRate { get; set; }

        [JsonProperty("marginTrade")]
        public bool MarginTrade { get; set; }

        [JsonProperty("changePrice")]
        public decimal ChangePrice { get; set; }

        [JsonProperty("changeRate")]
        public decimal ChangeRate { get; set; }

        [JsonProperty("lastTradedPrice")]
        public decimal LastTradedPrice { get; set; }

        [JsonProperty("board")]
        public int Board { get; set; }

        [JsonProperty("mark")]
        public int Mark { get; set; }
    }
}
