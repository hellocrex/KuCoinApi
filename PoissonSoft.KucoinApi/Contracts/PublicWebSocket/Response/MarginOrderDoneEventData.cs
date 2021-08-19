using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KuCoinApi.Contracts.PublicWebSocket.Response
{
    public class MarginOrderDoneEventData
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
        /// Done reason (filled or canceled)
        /// </summary>
        [JsonProperty("reason")]
        public string Reason { get; set; }

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
