using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.KuCoinApi.Contracts.MarketData.Response.Get24hrStats;

namespace PoissonSoft.KuCoinApi.Contracts.MarketData
{
    public class StatisticTickerPair
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
        public StatisticInfoTicker Data { get; set; }
    }
}
