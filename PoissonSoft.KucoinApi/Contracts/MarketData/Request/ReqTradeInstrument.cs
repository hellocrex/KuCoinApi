using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KuCoinApi.Contracts.MarketData.Request
{
    public class ReqTradeInstrument
    {
        /// <summary>
        /// Торговая пара
        /// </summary>
        [JsonProperty("symbol")]
        public string Symbol { get; set; }
    }
}
