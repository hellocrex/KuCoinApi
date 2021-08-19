using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.KuCoinApi.Contracts.PublicWebSocket.Response;

namespace PoissonSoft.KuCoinApi.Contracts.PublicWebSocket
{
    public class SpotMarketLevel2Depth5
    {
        [JsonProperty("type")]
        public string Message { get; set; }

        [JsonProperty("topic")]
        public string Topic { get; set; }

        [JsonProperty("subject")]
        public string Instrument { get; set; }

        [JsonProperty("data")]
        public SpotMarketLevel2Data Data { get; set; }
    }
}
