using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.KuСoinApi.Contracts.Enums;

namespace PoissonSoft.KuСoinApi.Contracts.Trade.Request
{
    public class SingleOrderByClientOId
    {
        /// <summary>
        /// Unique order id created by users to identify their orders
        /// </summary>
        [JsonProperty("clientOid")]
        public string ClientOid { get; set; }

        /// <summary>
        /// [Optional] Unique order id created by users to identify their orders
        /// </summary>
        [JsonProperty("symbol")]
        public string Symbol { get; set; }
    }
}
