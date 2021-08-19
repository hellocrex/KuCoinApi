using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.KuCoinApi.Contracts.Enums;

namespace PoissonSoft.KuCoinApi.Contracts
{
    /// <summary>
    /// Лимит
    /// </summary>
    public class RateLimit0 : ICloneable
    {

        /// <summary>
        /// Тип лимита
        /// </summary>
        [JsonProperty("rateLimitType")]
        public RateLimitType RateLimitType { get; set; }

        /// <summary>
        /// Единица измерения временного интервала, на который распространяется лимит
        /// (см. константы INTERVAL_*)
        /// </summary>
        [JsonProperty("interval")]
        public long IntervalUnit { get; set; }

        /// <summary>
        /// Размер временного интервала, на который распространяется лимит, в единицах,
        /// заданных в поле <see cref="IntervalUnit"/>
        /// </summary>
        [JsonProperty("intervalNum")]
        public int IntervalNum { get; set; }

        /// <summary>
        /// Размер лимита (максимальное допустимое количество операций за данный период)
        /// </summary>
        [JsonProperty("limit")]
        public int Limit { get; set; }

        /// <inheritdoc />
        public object Clone()
        {
            return new RateLimit0
            {
                RateLimitType = RateLimitType,
                IntervalUnit = IntervalUnit,
                IntervalNum = IntervalNum,
                Limit = Limit
            };
        }
    }
}
