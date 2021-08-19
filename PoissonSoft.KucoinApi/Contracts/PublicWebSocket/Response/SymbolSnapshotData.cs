using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KuCoinApi.Contracts.PublicWebSocket.Response
{
    public class SymbolSnapshotData
    {
        [JsonProperty("sequence")]
        public long Sequence { get; set; }

        [JsonProperty("data")]
        public SymbolShapshotInfo Data { get; set; }
    }
}
