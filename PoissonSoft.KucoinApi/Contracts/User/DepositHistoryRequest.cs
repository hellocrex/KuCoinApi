﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.KuCoinApi.Contracts.Enums;

namespace PoissonSoft.KuCoinApi.Contracts.User
{
    /// <summary>
    /// Deposit history request
    /// </summary>
    public class DepositHistoryRequest
    {
        /// <summary>
        /// Coin (optional)
        /// </summary>
        [JsonProperty("coin")]
        public string Coin { get; set; }

        /// <summary>
        /// Deposit status (optional)
        /// </summary>
        [JsonProperty("status")]
        public DepositAndWithdrawStatus? Status { get; set; }

        /// <summary>
        /// Start time timestamp in ms (optional)
        /// Default: 90 days from current timestamp
        /// Please notice the default startTime and endTime to make sure that time interval is within 0-90 days.
        /// If both startTime and endTime are sent, time between startTime and endTime must be less than 90 days.
        /// </summary>
        [JsonProperty("startTime")]
        public long? StartTimeMs { get; set; }

        /// <summary>
        /// End time timestamp in ms (optional)
        /// Default: present timestamp
        /// Please notice the default startTime and endTime to make sure that time interval is within 0-90 days.
        /// If both startTime and endTime are sent, time between startTime and endTime must be less than 90 days.
        /// </summary>
        [JsonProperty("endTime")]
        public long? EndTimeMs { get; set; }

        /// <summary>
        /// Default: 0 (optional)
        /// </summary>
        [JsonProperty("offset")]
        public int? Offset { get; set; }

        /// <summary>
        /// Limit. (optional)
        /// </summary>
        [JsonProperty("limit")]
        public int? Limit { get; set; }
    }

}