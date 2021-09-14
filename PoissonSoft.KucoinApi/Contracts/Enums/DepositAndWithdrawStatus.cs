using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PoissonSoft.KuCoinApi.Contracts.Enums
{
    public enum DepositAndWithdrawStatus
    {
        /// <summary>
        /// Unknown (erroneous) type
        /// </summary>
        Unknown,

        /// <summary>
        /// PROCESSING
        /// </summary>
        [EnumMember(Value = "PROCESSING")]
        Processing,

        /// <summary>
        /// WALLET_PROCESSING
        /// </summary>
        [EnumMember(Value = "WALLET_PROCESSING")]
        Wallet_processing,

        /// <summary>
        /// SUCCESS
        /// </summary>
        [EnumMember(Value = "SUCCESS")]
        Success,

        /// <summary>
        /// FAILURE
        /// </summary>
        [EnumMember(Value = "FAILURE")]
        Failure,
    }
}
