using System;
using System.Linq;
using Newtonsoft.Json;

namespace PoissonSoft.KuCoinApi.Contracts.MarketData
{
    /// <summary>
    /// Общие данные по бирже
    /// </summary>
    public class AllMarketTickers : ICloneable
    {
        /// <summary>
        /// Код http ответа
        /// </summary>
        [JsonProperty("code")]
        public int SystemCode { get; set; }

        /// <summary>
        /// Серверное время
        /// </summary>
        [JsonProperty("data")]
        public MarketTickers Data { get; set; }

        /// <inheritdoc />
        public object Clone()
        {
            return new AllMarketTickers
            {
                SystemCode = SystemCode,
               // Data = Data?.Select(x => (Data)x.Clone()).ToArray()
            };
        }
    }
}