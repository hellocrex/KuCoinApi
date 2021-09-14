using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.KuCoinApi.Contracts.Enums;

namespace PoissonSoft.KuCoinApi.Contracts.Trade.Response
{
    public class HistoricalPage
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("currentPage")]
        public int CurrentPage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("pageSize")]
        public int PageSize { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("totalNum")]
        public int TotalNum { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("totalPage")]
        public int TotalPage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("items")]
        public HistoricalOrder[] Items { get; set; }

    }

    public class HistoricalOrder
    {
        /// <summary>
        /// symbol
        /// </summary>
        [JsonProperty("symbol")]
        public string Ticker { get; set; }

        /// <summary>
        /// Filled price
        /// </summary>
        [JsonProperty("dealPrice")]
        public string DealPrice { get; set; }

        /// <summary>
        /// Executed size of funds
        /// </summary>
        [JsonProperty("dealValue")]
        public string DealValue { get; set; }

        /// <summary>
        /// transaction direction,include buy and sell
        /// </summary>
        [JsonProperty("side")]
        public Direction Side { get; set; }

        /// <summary>
        /// Executed quantity
        /// </summary>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Order quantity
        /// </summary>
        [JsonProperty("size")]
        public decimal size { get; set; }

        /// <summary>
        /// Fee
        /// </summary>
        [JsonProperty("fee")]
        public decimal Fee { get; set; }

        /// <summary>
        /// Create time
        /// </summary>
        [JsonProperty("createdAt")]
        public long CreatedAt { get; set; }
    }
}
