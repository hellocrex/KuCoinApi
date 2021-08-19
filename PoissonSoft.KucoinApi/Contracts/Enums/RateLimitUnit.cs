﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PoissonSoft.KuCoinApi.Contracts.Enums
{
    /// <summary>
    /// Единицы измерения временного интервала для лимита
    /// </summary>
    public enum RateLimitUnit
    {
        /// <summary>
        /// Неизвестный (ошибочный) тип
        /// </summary>
        Unknown = 0,

        /// <summary/>
        [EnumMember(Value = "SECOND")]
        Second = 1,

        /// <summary/>
        [EnumMember(Value = "MINUTE")]
        Minute = Second * 60,

        /// <summary/>
        [EnumMember(Value = "HOUR")]
        Hour = Minute * 60,

        /// <summary/>
        [EnumMember(Value = "DAY")]
        Day = Hour * 24,

    }
}
