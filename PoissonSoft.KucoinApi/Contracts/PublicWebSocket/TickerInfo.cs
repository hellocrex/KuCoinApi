using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KuCoinApi.Contracts.PublicWebSocket
{
    public class TickerInfo
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("topic")]
        public string Topic { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("data")]
        public MarketData.MarketData Data { get; set; }
        
    }
}
