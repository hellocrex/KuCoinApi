using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KuCoinApi.Contracts.User.Request
{
    public class HistoricalWithdrawList
    {
        /// <summary>
        /// [Optional] The current page.
        /// </summary>
        [JsonProperty("currentPage")]
        public int CurrentPage { get; set; }

        /// <summary>
        /// [Optional] Number of entries per page.
        /// </summary>
        [JsonProperty("pageSize")]
        public int PageSize { get; set; }

        /// <summary>
        /// [Optional] Currency.
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// [Optional] Start time (milisecond)
        /// </summary>
        [JsonProperty("startAt")]
        public long StartAt { get; set; }

        /// <summary>
        /// [Optional] End time (milisecond)
        /// </summary>
        [JsonProperty("endAt")]
        public long EndAt { get; set; }

        /// <summary>
        /// [Optional] Status. Available value: PROCESSING, SUCCESS, and FAILURE
        /// </summary>
        [JsonProperty("status")]
        public WithdrawStatus Status { get; set; }
    }

    public enum WithdrawStatus
    {
        /// <summary>
        /// PROCESSING
        /// </summary>
        [EnumMember(Value = "PROCESSING")]
        Processing,

        /// <summary>
        /// SUCCESS
        /// </summary>
        [EnumMember(Value = "SUCCESS")]
        Success,

        /// <summary>
        /// FAILURE
        /// </summary>
        [EnumMember(Value = "FAILURE")]
        Failure
    }
}
