using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KuCoinApi.Contracts.PublicWebSocket.Response
{
    public class PositionStatusEventData
    {
        /// <summary>
        /// Event type
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Timestamp (millisecond)
        /// </summary>
        [JsonProperty("timestamp")]
        public long Time { get; set; }
    }
}
