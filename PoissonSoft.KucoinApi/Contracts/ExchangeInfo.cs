using System;
using System.Linq;
using Newtonsoft.Json;
using PoissonSoft.KuCoinApi.Contracts;

namespace PoissonSoft.KuCoinApi.Contracts
{
    /// <summary>
    /// Общие данные по бирже
    /// </summary>
    public class ExchangeInfo : ICloneable
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
        public DataCoin[] Data { get; set; }

        /// <inheritdoc />
        public object Clone()
        {
            return new ExchangeInfo
            {
                SystemCode = SystemCode,
                Data = Data?.Select(x =>(DataCoin)x.Clone()).ToArray()
            };
        }
    }
}