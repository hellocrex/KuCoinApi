using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KuCoinApi.Contracts.MarketData.Request
{
    public class ReqSymbolList
    {
        /// <summary>
        /// Filled side. The filled side is set to the taker by default
        /// </summary>
        [JsonProperty("market", NullValueHandling = NullValueHandling.Ignore)]
        public string Market { get; set; }
    }
}
