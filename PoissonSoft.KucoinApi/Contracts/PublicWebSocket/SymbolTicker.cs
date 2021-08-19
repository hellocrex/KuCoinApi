using Newtonsoft.Json;
using PoissonSoft.KuCoinApi.Contracts.PublicWebSocket.Responce;

namespace PoissonSoft.KuCoinApi.Contracts.PublicWebSocket
{
    public class SymbolTicker
    {
        [JsonProperty("type")]
        public string Message { get; set; }

        [JsonProperty("topic")]
        public string Topic { get; set; }

        [JsonProperty("subject")]
        public string Instrument { get; set; }

        [JsonProperty("data")]
        public SymbolTickerData Data { get; set; }
    }
}
