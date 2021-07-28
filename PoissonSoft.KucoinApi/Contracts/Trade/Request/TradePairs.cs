using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KuСoinApi.Contracts.Trade.Request
{
    public class TradePairs
    {
        /// <summary>
        /// Trading pair (optional, you can inquire fee rates of 10 trading pairs each time at most)
        /// </summary>
        [JsonProperty("symbols")]
        public string Symbols { get; set; }
    }
}
