using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.KuCoinApi.Contracts.Enums;

namespace PoissonSoft.KuCoinApi.Contracts.Trade.Request
{
    public class HistoricalOrderReq
    {
        /// <summary>
        /// [Optional] The current page
        /// </summary>
        [JsonProperty("currentPage")]
        public int CurrentPage { get; set; }

        /// <summary>
        /// [Optional] Number of entries per page
        /// </summary>
        [JsonProperty("pageSize")]
        public int PageSize { get; set; }

        /// <summary>
        /// [Optional] Only list orders for a specific symbol
        /// </summary>
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        /// <summary>
        /// ‎[Optional] Start time (milisecond)
        /// </summary>
        [JsonProperty("startAt")]
        public long StartTime { get; set; }

        /// <summary>
        /// [Optional] End time (milisecond)
        /// </summary>
        [JsonProperty("endAt")]
        public long EndTime { get; set; }

        /// <summary>
        /// [Optional] buy or sell
        /// </summary>
        [JsonProperty("side")]
        public OrderSide Side { get; set; }

    }
}
