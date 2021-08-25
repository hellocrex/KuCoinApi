using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PoissonSoft.KuCoinApi.Contracts.Enums
{
    public enum StopOrderType
    {
        /// <summary>
        /// Неизвестный (ошибочный) тип
        /// </summary>
        Unknown,

        /// <summary>
        /// stop: 'loss': Triggers when the last trade price changes to a value at or below the stopPrice.
        /// </summary>
        [EnumMember(Value = "loss")]
        Loss,

        /// <summary>
        /// stop: 'entry': Triggers when the last trade price changes to a value at or above the stopPrice.
        /// </summary>
        [EnumMember(Value = "entry")]
        Entry
    }
}
