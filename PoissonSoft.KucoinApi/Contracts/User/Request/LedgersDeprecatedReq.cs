using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.KuCoinApi.Contracts.Enums;

namespace PoissonSoft.KuCoinApi.Contracts.User.Request
{
    public class LedgersDeprecatedReq
    {
        /// <summary>
        /// ID of the account
        /// </summary>
        [JsonProperty("accountId")]
        public string AccountId { get; set; }

        /// <summary>
        /// [Optional] Account type: main, trade, margin or pool
        /// </summary>
        [JsonProperty("direction")]
        public Direction Direction { get; set; }

        /// <summary>
        /// [Optional] Account type: main, trade, margin or pool
        /// </summary>
        [JsonProperty("bizType")]
        public BusinessType BusinesType { get; set; }

        /// <summary>
        /// [Optional] Account type: main, trade, margin or pool
        /// </summary>
        [JsonProperty("startAt")]
        public long startAt { get; set; }

        /// <summary>
        /// [Optional] Account type: main, trade, margin or pool
        /// </summary>
        [JsonProperty("endAt")]
        public long endAt { get; set; }
    }
}
