using System;
using System.Linq;
using Newtonsoft.Json;

namespace PoissonSoft.KuСoinApi.Contracts.MarketData
{
    /// <summary>
    /// Общие данные по бирже
    /// </summary>
    public class TickerInfo : ICloneable
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
        public Data Data { get; set; }

        /// <inheritdoc />
        public object Clone()
        {
            return new TickerInfo
            {
                SystemCode = SystemCode,
               // Data = Data?.Select(x => (Data)x.Clone()).ToArray()
            };
        }
    }
}