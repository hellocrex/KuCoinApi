using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.KuСoinApi.Contracts.MarketData.Response.Get24hrStats;

namespace PoissonSoft.KuСoinApi.Contracts.MarketData
{
    public class FiatPriceList
    {
        /// <summary>
        /// Код http ответа
        /// </summary>
        [JsonProperty("code")]
        public int SystemCode { get; set; }

        /// <summary>
        /// Код http ответа
        /// </summary>
        [JsonProperty("data")]
        public Dictionary<string, decimal> Data { get; set; }
    }
}
