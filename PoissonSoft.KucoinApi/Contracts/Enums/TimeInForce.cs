using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PoissonSoft.KuСoinApi.Contracts.Enums
{
    public enum TimeInForce
    {
        /// <summary>
        /// Good Till Canceled orders remain open on the book until canceled. This is the default behavior if no policy is specified.
        /// </summary>
        [EnumMember(Value = "GTC")]
        GTC,

        /// <summary>
        /// Good Till Time orders remain open on the book until canceled or the allotted cancelAfter is depleted on the matching engine
        /// </summary>
        [EnumMember(Value = "GTT")]
        GTT,

        /// <summary>
        /// Immediate Or Cancel orders instantly cancel the remaining size of the limit order instead of opening it on the book.
        /// </summary>
        [EnumMember(Value = "IOC")]
        IOC,

        /// <summary>
        /// Fill Or Kill orders are rejected if the entire size cannot be matched.
        /// </summary>
        [EnumMember(Value = "FOK")]
        FOK
    }
}
