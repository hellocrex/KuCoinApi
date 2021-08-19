using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KuCoinApi.Contracts.PublicWebSocket.Response
{
    public class DeptRatioData
    {
        /// <summary>
        /// Debt ratio
        /// </summary>
        [JsonProperty("debtRatio")]
        public decimal DebtRatio { get; set; }

        /// <summary>
        /// Total debt in BTC (interest included)
        /// </summary>
        [JsonProperty("totalDebt")]
        public decimal TotalDebt { get; set; }

        /// <summary>
        /// Debt list (interest included)
        /// </summary>
        [JsonProperty("debtList")]
        public string DebtList { get; set; }

        /// <summary>
        /// Timestamp (millisecond)
        /// </summary>
        [JsonProperty("timestamp")]
        public long Time { get; set; }
    }
}
