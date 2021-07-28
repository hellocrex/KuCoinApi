using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KuСoinApi.Contracts.MarketData.Response
{
    public class CurrencyList
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
        public CurrencyData[] Data { get; set; }
    }
}
