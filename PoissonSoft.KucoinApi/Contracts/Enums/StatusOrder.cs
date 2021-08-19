using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PoissonSoft.KuCoinApi.Contracts.Enums
{
    public enum StatusOrder
    {
        /// <summary>
        /// Unknown
        /// </summary>
        Unknown,

        /// <summary>
        /// Active
        /// </summary>
        [EnumMember(Value = "active")]
        Active,

        /// <summary>
        /// Done
        /// </summary>
        [EnumMember(Value = "done")]
        Done,

        /// <summary>
        /// Open
        /// </summary>
        [EnumMember(Value = "open")]
        Open,

        /// <summary>
        /// Close
        /// </summary>
        [EnumMember(Value = "close")]
        Close
    }
}
