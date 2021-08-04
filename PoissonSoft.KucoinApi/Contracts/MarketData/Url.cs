using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KuСoinApi.Contracts.MarketData.Request
{
    public class Url
    {
        /// <summary>
        /// Торговая пара
        /// </summary>
        //[JsonProperty("symbol")]
        public string UrlString { get; set; }
    }
}
