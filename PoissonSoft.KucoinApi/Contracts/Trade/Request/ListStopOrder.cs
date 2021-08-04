using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.KuСoinApi.Contracts.Enums;

namespace PoissonSoft.KuСoinApi.Contracts.Trade.Request
{
    public class ListStopOrder : OrderReq
    {

        /// <summary>
        /// [Optional] The current page
        /// </summary>
        [JsonProperty("currentPage")]
        public int CurrentPage { get; set; }

        /// <summary>
        /// [Optional] comma seperated order ID list
        /// </summary>
        [JsonProperty("orderIds")]
        public string OrderIds { get; set; }

        /// <summary>
        /// [Optional] page size
        /// </summary>
        [JsonProperty("pageSize")]
        public string PageSize { get; set; }
    }
}
