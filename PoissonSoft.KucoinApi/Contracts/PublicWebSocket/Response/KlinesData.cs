using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KuCoinApi.Contracts.PublicWebSocket.Response
{
    public class KlinesData
    {
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("candles")]
        public decimal[] candles { get; set; }

        [JsonProperty("time")]
        public long Time { get; set; }
    }
}
