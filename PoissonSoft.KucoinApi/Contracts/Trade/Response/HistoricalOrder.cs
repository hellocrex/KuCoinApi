using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.KuСoinApi.Contracts.Enums;

namespace PoissonSoft.KuСoinApi.Contracts.Trade.Response
{
    public class HistoricalOrder
    {
        /// <summary>
        /// symbol
        /// </summary>
        [JsonProperty("symbol")]
        public string Ticker { get; set; }

        /// <summary>
        /// symbol
        /// </summary>
        [JsonProperty("dealPrice")]
        public string DealPrice { get; set; }

        /// <summary>
        /// symbol
        /// </summary>
        [JsonProperty("dealValue")]
        public string DealValue { get; set; }

        /// <summary>
        /// symbol
        /// </summary>
        [JsonProperty("side")]
        public Direction Side { get; set; }

        /// <summary>
        /// symbol
        /// </summary>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// symbol
        /// </summary>
        [JsonProperty("size")]
        public decimal size { get; set; }

        /// <summary>
        /// symbol
        /// </summary>
        [JsonProperty("fee")]
        public string Fee { get; set; }

        /// <summary>
        /// symbol
        /// </summary>
        [JsonProperty("createdAt")]
        public long CreatedAt { get; set; }
    }
}
