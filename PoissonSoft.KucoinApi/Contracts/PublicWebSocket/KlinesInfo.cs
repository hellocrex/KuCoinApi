using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.KuCoinApi.Contracts.PublicWebSocket.Response;

namespace PoissonSoft.KuCoinApi.Contracts.PublicWebSocket
{
    public class KlinesInfo
    {
        [JsonProperty("type")]
        public string Message { get; set; }

        [JsonProperty("topic")]
        public string Topic { get; set; }

        [JsonProperty("subject")]
        public string Instrument { get; set; }

        [JsonProperty("data")]
        public KlinesData Data { get; set; }
    }
}
