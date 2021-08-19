using Newtonsoft.Json;
using PoissonSoft.KuCoinApi.Contracts.Enums;

namespace PoissonSoft.KuCoinApi.Contracts.PublicWebSocket.Response
{
    public class OrderChangeEventData
    {
        [JsonProperty("symbol")]
        public string Instrument { get; set; }

        [JsonProperty("orderType")]
        public OrderType OrderType { get; set; }

        [JsonProperty("side")]
        public OrderSide Side { get; set; }

        [JsonProperty("orderId")]
        public string OrderId { get; set; }

        [JsonProperty("type")]
        public StatusOrder StatusOrder { get; set; }

        [JsonProperty("orderTime")]
        public long OrderTime { get; set; }

        [JsonProperty("size")]
        public float Size { get; set; }

        [JsonProperty("filledSize")]
        public float FilledSize { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("clientOid")]
        public long ClientOid { get; set; }

        [JsonProperty("remainSize")]
        public float RemainSize { get; set; }

        [JsonProperty("status")]
        public StatusOrder Status { get; set; }

        [JsonProperty("ts")]
        public long Time { get; set; }
    }
}
