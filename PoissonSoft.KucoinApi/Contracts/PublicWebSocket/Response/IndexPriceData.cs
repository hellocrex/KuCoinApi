using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KuCoinApi.Contracts.PublicWebSocket.Response
{
    public class IndexPriceData
    {
        [JsonProperty("symbol")]
        public string Instrument { get; set; }

        [JsonProperty("granularity")]
        public int Granularity { get; set; }

        [JsonProperty("value")]
        public decimal Value { get; set; }

        [JsonProperty("timestamp")]
        public long Time { get; set; }
    }
}
