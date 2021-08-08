using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.KuСoinApi.Contracts.Trade.Response;

namespace PoissonSoft.KuСoinApi.Contracts.Trade
{
    public class Page
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
        public Order[] Items { get; set; }


    }
}
