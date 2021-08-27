using System;
using Newtonsoft.Json;

namespace PoissonSoft.KuCoinApi.Contracts.MarketData.Response
{
    public class MarketTickers
    {
        /// <summary>
        /// timestamp
        /// </summary>
        [JsonProperty("time")]
        public long Time { get; set; }

        /// <summary>
        /// Информация по ticker'у за 24ч
        /// </summary>
        [JsonProperty("ticker")]
        public Ticker[] Ticker { get; set; }
    }
}
