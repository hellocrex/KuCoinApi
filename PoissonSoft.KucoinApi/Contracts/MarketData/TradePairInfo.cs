using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.KuCoinApi.Contracts.MarketData.Response;

namespace PoissonSoft.KuCoinApi.Contracts.MarketData
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
        /// 
        /// </summary>
        [JsonProperty("data")]
        public TickerMarketData Data { get; set; }
    }
}
