using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.KuCoinApi.Contracts.Enums;
using PoissonSoft.KuCoinApi.Contracts.Trade.Request;

namespace PoissonSoft.KuCoinApi.Contracts.Trade.Response
{
    public class BulkOrderData : ReqBulkOrder
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("channel")]
        public string Channel { get; set; }

        /// <summary>
        /// OrderId
        /// </summary>
        [JsonProperty("id")]
        public string OrderId { get; set; }

        /// <summary>
        /// status (success, fail)
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("failMsg")]
        public string FailMsg { get; set; }
        
    }
}
