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
    public class RateLimit
    {

        /// <summary>
        /// Тип лимита
        /// </summary>
        public RateLimitType IdRequest{ get; set; }

        /// <summary>
        /// Единица измерения временного интервала, на который распространяется лимит
        /// (см. константы INTERVAL_*)
        /// </summary>
        public RateLimitUnit IntervalUnit { get; set; }

        /// <summary>
        /// Размер временного интервала, на который распространяется лимит, в единицах,
        /// заданных в поле <see cref="IntervalUnit"/>
        /// </summary>
        public int IntervalNum { get; set; }

        /// <summary>
        /// Размер лимита (максимальное допустимое количество операций за данный период)
        /// </summary>
        public int Limit { get; set; }
       
    }
}
