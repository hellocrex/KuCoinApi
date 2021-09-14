using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace PoissonSoft.KuCoinApi.Contracts.WebSocketStream
{
    /// <summary>
    /// Command Request Methods
    /// </summary>
    public enum CommandRequestMethod
    {
        /// <summary>
        /// Unknown (erroneous)
        /// </summary>
        Unknown,

        /// <summary>
        /// Subscribe stream
        /// </summary>
        [EnumMember(Value = "subscribe")]
        Subscribe,

        /// <summary>
        /// Unsubscribe stream
        /// </summary>
        [EnumMember(Value = "unsubscribe")]
        Unsubscribe,

        /// <summary>
        /// Ping
        /// </summary>
        [EnumMember(Value = "ping")]
        Ping
    }
}
