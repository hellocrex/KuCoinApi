using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KuCoinApi.Contracts.Trade.Response
{
    /// <summary>
    ///
    /// </summary>
    public class CancelOrder
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("cancelledOrderIds")]
        public string[] CancelledOrderId { get; set; }

    }
}
