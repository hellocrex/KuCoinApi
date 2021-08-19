using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.KuCoinApi.Contracts.PublicWebSocket.Responce;
using PoissonSoft.KuCoinApi.Contracts.PublicWebSocket.Response;

namespace PoissonSoft.KuCoinApi.Contracts.PublicWebSocket
{
    public class MarketLevel2
    {
        [JsonProperty("type")]
        public string Message { get; set; }

        [JsonProperty("topic")]
        public string Topic { get; set; }

        [JsonProperty("subject")]
        public string Instrument { get; set; }

        [JsonProperty("data")]
        public MarketDataLevel2 Data { get; set; }
    }
}
