using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PoissonSoft.KuCoinApi.Contracts.Enums
{
    public enum OrderType
    {
        /// <summary>
        /// Неизвестный (ошибочный) тип
        /// </summary>
        Unknown,

        /// <summary>
        /// Limit Order
        /// </summary>
        [EnumMember(Value = "limit")]
        Limit,

        /// <summary>
        /// Market Order
        /// </summary>
        [EnumMember(Value = "market")]
        Market,

        /// <summary>
        /// Market Order
        /// </summary>
        [EnumMember(Value = "stop ")]
        Stop,

        /// <summary>
        /// Market Order
        /// </summary>
        [EnumMember(Value = "limit_stop ")]
        Limit_stop,

        /// <summary>
        /// Market Order
        /// </summary>
        [EnumMember(Value = "market_stop")]
        Market_stop
    }
}
