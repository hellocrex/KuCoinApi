﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.KuСoinApi.Contracts.Enums;

namespace PoissonSoft.KuСoinApi.Contracts.User.Request
{
    public class LedgersReq
    {
        /// <summary>
        /// [Optional] Account type: main, trade, margin or pool
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }

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
        public long StartAt { get; set; }

        /// <summary>
        /// [Optional] Account type: main, trade, margin or pool
        /// </summary>
        [JsonProperty("endAt")]
        public long EndAt { get; set; }
    }
}