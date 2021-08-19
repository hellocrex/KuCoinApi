using Newtonsoft.Json;

namespace PoissonSoft.KuCoinApi.Contracts.PublicWebSocket.Response
{
    public class AccountBalanceData
    {
        /// <summary>
        /// total balance
        /// </summary>
        [JsonProperty("total")]
        public decimal Total { get; set; }

        /// <summary>
        /// available balance
        /// </summary>
        [JsonProperty("available")]
        public decimal Available { get; set; }

        /// <summary>
        /// the change of available balance
        /// </summary>
        [JsonProperty("availableChange")]
        public decimal AvailableChange { get; set; }

        /// <summary>
        /// Currency
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// hold amount
        /// </summary>
        [JsonProperty("hold")]
        public decimal Hold { get; set; }

        /// <summary>
        /// the change of hold balance
        /// </summary>
        [JsonProperty("holdChange")]
        public decimal holdChange { get; set; }

        /// <summary>
        /// relation event
        /// </summary>
        [JsonProperty("relationEvent")]
        public string RelationEvent { get; set; }

        /// <summary>
        /// relation event id
        /// </summary>
        [JsonProperty("relationEventId")]
        public string RelationEventId { get; set; }

        /// <summary>
        /// the context of trade event
        /// </summary>
        [JsonProperty("relationContext")]
        public RelationContext RelationContext { get; set; }

        /// <summary>
        /// timestamp
        /// </summary>
        [JsonProperty("time")]
        public long Time { get; set; }
    }

    public class RelationContext
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("symbol")]
        public string Instrument { get; set; }

        /// <summary>
        /// the trade Id when order is executed
        /// </summary>
        [JsonProperty("tradeId")]
        public string TradeId { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("orderId")]
        public string orderId { get; set; }
    }
}
