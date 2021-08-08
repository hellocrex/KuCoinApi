using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.KuСoinApi.Contracts.Trade.Response;
using PoissonSoft.KuСoinApi.Contracts.User.Response;

namespace PoissonSoft.KuСoinApi.Contracts.User
{
    public class DepositList
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
        public Deposit[] Items { get; set; }
    }
}
