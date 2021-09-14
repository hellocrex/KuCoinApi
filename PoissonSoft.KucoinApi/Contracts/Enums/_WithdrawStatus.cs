using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PoissonSoft.KuCoinApi.Contracts.Enums
{
    public enum _WithdrawStatus
    {
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
        Failure
    }

    //public enum WithdrawStatus
    //{
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    PROCESSING = 0,

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    WALLET_PROCESSING = 1,

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    SUCCESS = 2,

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    FAILURE = 3,
    //}
}
