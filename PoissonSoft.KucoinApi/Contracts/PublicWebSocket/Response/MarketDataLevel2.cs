using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KuCoinApi.Contracts.PublicWebSocket.Response
{
    public class MarketDataLevel2
    {
        [JsonProperty("sequenceStart")]
        public long SequenceStart { get; set; }

        [JsonProperty("symbol")]
        public string Instrument { get; set; }

        [JsonProperty("changes")]
        public MarketDataLevel2Changes Changes { get; set; }

        [JsonProperty("sequenceEnd")]
        public long SequenceEnd { get; set; }
    }

    public class MarketDataLevel2Changes
    {
        [JsonProperty("asks")]
        public long[] Asks { get; set; }

        [JsonProperty("bids")]
        public long[][] Bids { get; set; }
    }
}
