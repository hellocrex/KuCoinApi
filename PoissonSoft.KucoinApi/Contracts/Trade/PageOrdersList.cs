using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.KuСoinApi.Contracts.Trade.Response;

namespace PoissonSoft.KuСoinApi.Contracts.Trade
{
    public class PageOrdersList
    {
        /// <summary>
        /// System error codes
        /// </summary>
        [JsonProperty("code")]
        public int SystemCode { get; set; }

        /// <summary>
        /// data
        /// </summary>
        [JsonProperty("data")]
        public Page Data { get; set; }
    }
}
