using System;
using Newtonsoft.Json;

namespace PoissonSoft.KuСoinApi.Contracts.MarketData
{
    public class Data
    {
        /// <summary>
        /// timestamp
        /// </summary>
        [JsonProperty("time")]
        public string Time { get; set; }

        /// <summary>
        /// Информация по ticker'у за 24ч
        /// </summary>
        [JsonProperty("ticker")]
        public Ticker[] Ticker { get; set; }
    }
}
