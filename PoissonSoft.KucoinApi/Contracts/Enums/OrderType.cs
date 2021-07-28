using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PoissonSoft.KuСoinApi.Contracts.Enums
{
    public enum OrderType
    {
        /// <summary>
        /// Limit Order
        /// </summary>
        [EnumMember(Value = "limit")]
        Limit,

        /// <summary>
        /// Market Order
        /// </summary>
        [EnumMember(Value = "market")]
        Market,

        /// <summary>
        /// Market Order
        /// </summary>
        [EnumMember(Value = "limit_stop ")]
        Limit_stop,

        /// <summary>
        /// Market Order
        /// </summary>
        [EnumMember(Value = "market_stop")]
        Market_stop
    }
}
