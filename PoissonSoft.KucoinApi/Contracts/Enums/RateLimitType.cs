﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PoissonSoft.KuCoinApi.Contracts.Enums
{
    /// <summary>
    /// Тип лимита
    /// </summary>
    public enum RateLimitType
    {
        /// <summary>
        /// Неизвестный (ошибочный) тип
        /// </summary>
        Unknown,

        /// <summary>
        /// Лимит на суммарный вес запросов с одного IP
        /// </summary>
        [EnumMember(Value = "REQUEST_WEIGHT")]
        RequestWeight,

        /// <summary>
        /// Лимит на количество ордеров
        /// </summary>
        [EnumMember(Value = "ORDERS")]
        Orders,

        /// <summary>
        /// RAW_REQUESTS
        /// </summary>
        [EnumMember(Value = "RAW_REQUESTS")]
        RawRequests,
    }
}
