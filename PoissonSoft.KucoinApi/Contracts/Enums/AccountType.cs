using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PoissonSoft.KuCoinApi.Contracts.Enums
{
    /// <summary>
    /// Account type
    /// </summary>
    public enum AccountType
    {
        /// <summary>
        /// Main
        /// </summary>
        [EnumMember(Value = "main")]
        Main,

        /// <summary>
        /// Trade
        /// </summary>
        [EnumMember(Value = "trade")]
        Trade,

        /// <summary>
        /// Margin
        /// </summary>
        [EnumMember(Value = "margin")]
        Margin,

        /// <summary>
        /// Margin
        /// </summary>
        [EnumMember(Value = "pool")]
        Pool
    }
}
