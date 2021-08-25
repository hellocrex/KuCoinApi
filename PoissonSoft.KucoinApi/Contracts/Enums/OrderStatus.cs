using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PoissonSoft.KuCoinApi.Contracts.Enums
{
    public enum OrderStatus
    {
        /// <summary>
        /// Неизвестный (ошибочный) тип
        /// </summary>
        Unknown,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember(Value = "NEW")]
        New,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember(Value = "TRIGGERED")]
        Triggered,
    }
}
