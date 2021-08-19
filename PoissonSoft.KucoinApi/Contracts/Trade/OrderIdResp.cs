using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.KuCoinApi.Contracts.Trade.Response;

namespace PoissonSoft.KuCoinApi.Contracts.Trade
{
    public class OrderIdResp
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
        public NewOrder Data { get; set; }
    }
}
