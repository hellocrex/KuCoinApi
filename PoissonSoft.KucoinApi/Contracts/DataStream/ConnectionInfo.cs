using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KuCoinApi.Contracts.DataStream
{
    public class ConnectionInfo
    {
        /// <summary>
        /// symbol
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; }

        /// <summary>
        /// symbol
        /// </summary>
        [JsonProperty("instanceServers")]
        public InstanceServersData[] InstanceData { get; set; }
    }
}
