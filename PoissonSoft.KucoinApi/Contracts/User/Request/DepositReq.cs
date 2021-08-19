using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.KuCoinApi.Contracts.Enums;

namespace PoissonSoft.KuCoinApi.Contracts.User.Request
{
    public class DepositReq
    {
        /// <summary>
        /// [Optional] Account type: main, trade, margin or pool
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// [Optional] Account type: main, trade, margin or pool
        /// </summary>
        [JsonProperty("startAt")]
        public long StartTime { get; set; }

        /// <summary>
        /// [Optional] Account type: main, trade, margin or pool
        /// </summary>
        [JsonProperty("endAt")]
        public long EndTime { get; set; }

        /// <summary>
        /// [Optional] Account type: main, trade, margin or pool
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
