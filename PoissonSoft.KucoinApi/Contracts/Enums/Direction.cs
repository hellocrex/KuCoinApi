using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PoissonSoft.KuСoinApi.Contracts.Enums
{
    public enum Direction
    {
        /// <summary>
        /// Receive
        /// </summary>
        [EnumMember(Value = "in")]
        In,

        /// <summary>
        /// Send
        /// </summary>
        [EnumMember(Value = "out")]
        Out
    }
}
