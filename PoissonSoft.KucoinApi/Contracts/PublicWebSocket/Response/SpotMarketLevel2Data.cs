using Newtonsoft.Json;

namespace PoissonSoft.KuCoinApi.Contracts.PublicWebSocket.Response
{
    public class SpotMarketLevel2Data
    {
        [JsonProperty("asks")]
        public decimal[][] Asks { get; set; }

        [JsonProperty("bids")]
        public decimal[][] Bids { get; set; }

        [JsonProperty("timestamp")]
        public long Time { get; set; }
    }
}
