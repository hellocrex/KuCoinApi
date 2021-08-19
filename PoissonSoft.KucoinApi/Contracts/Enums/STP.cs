using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PoissonSoft.KuCoinApi.Contracts.Enums
{
    public enum STP
    {
        /// <summary>
        /// Cancel newest
        /// </summary>
        [EnumMember(Value = "")]
        Empty,

        /// <summary>
        /// Cancel newest
        /// </summary>
        [EnumMember(Value = "CN")]
        CN,

        /// <summary>
        /// Cancel oldest
        /// </summary>
        [EnumMember(Value = "CO")]
        CO,

        /// <summary>
        /// Cancel both
        /// </summary>
        [EnumMember(Value = "CB")]
        CB,

        /// <summary>
        /// Decrease and Cancel
        /// </summary>
        [EnumMember(Value = "DC")]
        DC
    }
}
