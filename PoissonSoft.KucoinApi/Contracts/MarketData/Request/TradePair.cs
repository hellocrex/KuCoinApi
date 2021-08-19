using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KuCoinApi.Contracts.MarketData
{
    public class TradePair
    {
        /// <summary>
        /// Торговая пара
        /// </summary>
        [JsonProperty("symbol")]
        public string Symbol { get; set; }
    }
}
