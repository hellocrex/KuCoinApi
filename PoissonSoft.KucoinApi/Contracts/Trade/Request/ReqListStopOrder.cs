using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.KuCoinApi.Contracts.Enums;

namespace PoissonSoft.KuCoinApi.Contracts.Trade.Request
{
    public class ReqListStopOrder : ReqOrderList
    {

        /// <summary>
        /// [Optional] The current page
        /// </summary>
        [JsonProperty("currentPage", NullValueHandling = NullValueHandling.Ignore)]
        public int? CurrentPage { get; set; }

        /// <summary>
        /// [Optional] comma seperated order ID list
        /// </summary>
        [JsonProperty("orderIds", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderIds { get; set; }

        /// <summary>
        /// [Optional] page size
        /// </summary>
        [JsonProperty("pageSize", NullValueHandling = NullValueHandling.Ignore)]
        public string PageSize { get; set; }
    }
}
