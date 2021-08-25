using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KuCoinApi.Contracts.Trade.Response
{
    /// <summary>
    ///
    /// </summary>
    public class CancelOrderByClientOid
    {
        /// <summary>
        /// Order ID of cancelled order
        /// </summary>
        [JsonProperty("cancelledOrderId")]
        public string CancelledOrderId { get; set; }

        /// <summary>
        /// Unique order id created by users to identify their orders
        /// </summary>
        [JsonProperty("clientOid")]
        public string ClientOid { get; set; }

    }
}
