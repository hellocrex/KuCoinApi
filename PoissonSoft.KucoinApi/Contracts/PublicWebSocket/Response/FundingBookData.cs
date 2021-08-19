using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KuCoinApi.Contracts.PublicWebSocket.Response
{
    public class FundingBookData
    {
        /// <summary>
        /// Currency
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// Daily interest rate. e.g. 0.002 is 0.2%
        /// </summary>
        [JsonProperty("dailyIntRate")]
        public float DailyIntRate { get; set; }

        /// <summary>
        /// Annual interest rate. e.g. 0.12 is 12%
        /// </summary>
        [JsonProperty("annualIntRate")]
        public float AnnualIntRate { get; set; }

        /// <summary>
        /// Term (Unit: Day)    
        /// </summary>
        [JsonProperty("term")]
        public int Term { get; set; }

        /// <summary>
        /// Lend or borrow. Currently, only "Lend" is available
        /// </summary>
        [JsonProperty("side")]
        public string Side { get; set; }

        /// <summary>
        /// Sequence number
        /// </summary>
        [JsonProperty("sequence")]
        public long Sequence { get; set; }

        /// <summary>
        /// Current total size. When this value is 0, remove this record from the order book.
        /// </summary>
        [JsonProperty("size")]
        public decimal Size { get; set; }

        /// <summary>
        /// Timestamp (nanosecond)
        /// </summary>
        [JsonProperty("ts")]
        public long Time { get; set; }
    }
}
