using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.BinanceApi.Contracts.Serialization;

namespace PoissonSoft.KuCoinApi.Contracts.WebSocketStream
{

    /// <summary>
    /// Request to manage Market Data Streams
    /// </summary>
    public class CommandRequest
    {
        /// <summary>
        /// Id to identify a response to the request
        /// </summary>
        [JsonProperty("id")]
        public long RequestId { get; set; }

        /// <summary>
        /// Method
        /// </summary>
        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumExConverter), CommandRequestMethod.Unknown)]
        public CommandRequestMethod Type { get; set; }

        /// <summary>
        /// Id to identify a response to the request
        /// </summary>
        [JsonProperty("topic")]
        public string Topic { get; set; }

        /// <summary>
        /// Id to identify a response to the request
        /// </summary>
        [JsonProperty("privatechanel")]
        public bool Privatechanel { get; set; }

        /// <summary>
        /// Id to identify a response to the request
        /// </summary>
        [JsonProperty("response")]
        public bool Responce { get; set; }
    }
}
