using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KuCoinApi.Contracts.User.Request
{
    public class ReqDepositList
    {
        /// <summary>
        /// [Optional] The current page.
        /// </summary>
        [JsonProperty("currentPage", NullValueHandling = NullValueHandling.Ignore)]
        public int? CurrentPage { get; set; }

        /// <summary>
        /// [Optional] Number of entries per page.
        /// </summary>
        [JsonProperty("pageSize, NullValueHandling = NullValueHandling.Ignore")]
        public int? PageSize { get; set; }

        /// <summary>
        /// [Optional] Currency
        /// </summary>
        [JsonProperty("currency", NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; }

        /// <summary>
        /// [Optional] Start time (millisecond)
        /// </summary>
        [JsonProperty("startAt", NullValueHandling = NullValueHandling.Ignore)]
        public long? StartAt { get; set; }

        /// <summary>
        /// [Optional] End time (millisecond)
        /// </summary>
        [JsonProperty("endAt", NullValueHandling = NullValueHandling.Ignore)]
        public long? EndAt { get; set; }

        /// <summary>
        /// [Optional] Status. Available value: PROCESSING, SUCCESS, and FAILURE
        /// </summary>
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public DepositStatus? Status { get; set; }
    }
}
