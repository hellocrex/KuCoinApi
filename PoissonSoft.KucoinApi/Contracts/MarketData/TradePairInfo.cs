using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KuСoinApi.Contracts.MarketData
{
    /// <summary>
    /// Общие данные по бирже
    /// </summary>
    public class TradePairInfo
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
        public MarketData Data { get; set; }
    }
}
