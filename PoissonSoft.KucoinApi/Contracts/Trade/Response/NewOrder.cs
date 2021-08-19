using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KuCoinApi.Contracts.Trade.Response
{
    public class NewOrder
    {
        /// <summary>
        /// The ID of the order
        /// </summary>
        [JsonProperty("orderId")]
        public string OrderId { get; set; }
    }
}
