using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PoissonSoft.KuСoinApi.Contracts.Enums
{
    public enum OrderType
    {
        /// <summary>
        /// Unknown (erroneous) type
        /// </summary>
        Unknown,

        /// <summary>
        /// Limit Order
        /// </summary>
        [EnumMember(Value = "LIMIT")]
        Limit,

        /// <summary>
        /// Market Order
        /// </summary>
        [EnumMember(Value = "MARKET")]
        Market
    }
}
