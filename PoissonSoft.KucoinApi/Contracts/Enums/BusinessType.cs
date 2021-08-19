using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PoissonSoft.KuCoinApi.Contracts.Enums
{
    public enum BusinessType
    {
        /// <summary>
        /// DEPOSIT
        /// </summary>
        [EnumMember(Value = "Deposit")]
        Deposit,

        /// <summary>
        /// WITHDRAW
        /// </summary>
        [EnumMember(Value = "Withdraw")]
        Withdraw,

        /// <summary>
        /// TRANSFER
        /// </summary>
        [EnumMember(Value = "Transfer")]
        Transfer,

        /// <summary>
        /// SUB_TRANSFER
        /// </summary>
        [EnumMember(Value = "Sub transfer")]
        Sub_transfer,

        /// <summary>
        /// TRADE_EXCHANGE
        /// </summary>
        [EnumMember(Value = "Trade exchange")]
        Trade_exchange,

        /// <summary>
        /// "MARGIN_EXCHANGE
        /// </summary>
        [EnumMember(Value = "margin exchange")]
        Margin_exchange,

        /// <summary>
        /// KUCOIN_BONUS
        /// </summary>
        [EnumMember(Value = "Kukoin bonus")]
        Kukoin_bonus
    }
}
