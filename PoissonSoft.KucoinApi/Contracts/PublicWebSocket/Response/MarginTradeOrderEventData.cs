using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KuCoinApi.Contracts.PublicWebSocket.Response
{
    public class MarginTradeOrderEventData
    {
        /// <summary>
        /// Currency
        /// </summary>
        [JsonProperty("currency")]
        public string Ticker { get; set; }

        /// <summary>
        /// Trade ID
        /// </summary>
        [JsonProperty("orderId")]
        public string OrderId { get; set; }

        /// <summary>
        /// Daily interest rate.
        /// </summary>
        [JsonProperty("dailyIntRate")]
        public decimal DailyIntRate { get; set; }

        /// <summary>
        /// Term (Unit: Day)  
        /// </summary>
        [JsonProperty("term")]
        public int Term { get; set; }

        /// <summary>
        /// Size
        /// </summary>
        [JsonProperty("size")]
        public int Size { get; set; }

        /// <summary>
        /// Lend or borrow. Currently, only "Lend" is available
        /// </summary>
        [JsonProperty("side")]
        public string Side { get; set; }

        /// <summary>
        /// Timestamp (millisecond)
        /// </summary>
        [JsonProperty("ts")]
        public long Time { get; set; }
    }
}
