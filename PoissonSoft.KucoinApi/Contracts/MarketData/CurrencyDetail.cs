using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.KuСoinApi.Contracts.MarketData.Response;

namespace PoissonSoft.KuСoinApi.Contracts.MarketData
{
    public class CurrencyDetail
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
        public CurrencyData Data { get; set; }
    }
}
