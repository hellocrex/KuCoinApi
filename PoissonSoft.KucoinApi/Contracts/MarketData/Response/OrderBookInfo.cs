using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KuCoinApi.Contracts.MarketData.Response
{
    public class OrderBookInfo
    {
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonProperty("time")]
        public long Time { get; set; }

        /// <summary>
        /// Sequence number
        /// </summary>
        [JsonProperty("sequence")]
        public long Sequence { get; set; }

        /// <summary>
        /// Bids
        /// </summary>
        [JsonProperty("bids")]
        public decimal[][] Bids { get; set; }

        /// <summary>
        /// Asks
        /// </summary>
        [JsonProperty("asks")]
        public decimal[][] Asks { get; set; }
    }
}
