using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KuСoinApi.Contracts.MarketData.Response
{
    public class CandleResp
    {
        /// <summary>
        /// Start time of the candle cycle
        /// </summary>
        [JsonProperty("time")]
        public long Time { get; set; }

        /// <summary>
        /// Opening price
        /// </summary>
        [JsonProperty("open")]
        public decimal OpenPrice { get; set; }

        /// <summary>
        /// Opening price
        /// </summary>
        [JsonProperty("close")]
        public decimal ClosePrice { get; set; }

        /// <summary>
        /// Highest price
        /// </summary>
        [JsonProperty("high")]
        public decimal HighPrice { get; set; }

        /// <summary>
        /// Lowest price
        /// </summary>
        [JsonProperty("low")]
        public decimal LowPrice { get; set; }

        /// <summary>
        /// Transaction volume
        /// </summary>
        [JsonProperty("volume")]
        public decimal TransactionVolume { get; set; }

        /// <summary>
        /// Transaction amount
        /// </summary>
        [JsonProperty("turnover")]
        public decimal TransactionAmount { get; set; }
    }
}
