using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PoissonSoft.KuCoinApi.Contracts.Enums
{
    public enum TradeType
    {
        /// <summary>
        /// Неизвестный (ошибочный) тип
        /// </summary>
        Unknown,

        /// <summary>
        /// Spot ‎‎Trade
        /// </summary>
        [EnumMember(Value = "TRADE")]
        Trade,

        /// <summary>
        /// Margin Trade
        /// </summary>
        [EnumMember(Value = "MARGIN_TRADE")]
        Margin
    }
}
