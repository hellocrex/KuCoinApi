using Newtonsoft.Json;
using PoissonSoft.KuСoinApi.Contracts.Enums;

namespace PoissonSoft.KuСoinApi.Contracts.Trade.Request
{
    public class NewOrder
    {
        /// <summary>
        /// Unique order id created by users to identify their orders, e.g. UUID.
        /// </summary>
        [JsonProperty("clientOid")]
        public string ClientOid { get; set; }

        /// <summary>
        /// [Optional] remark for the order, length cannot exceed 100 utf8 characters
        /// </summary>
        [JsonProperty("remark")]
        public string Remark { get; set; }

        /// <summary>
        /// [Optional] self trade prevention , CN, CO, CB or DC
        /// </summary>
        [JsonProperty("stp")]
        public STP STP { get; set; }

        /// <summary>
        /// [Optional] Only list orders for a specific symbol
        /// </summary>
        [JsonProperty("symbol")]
        public string Ticker { get; set; }

        /// <summary>
        /// [Optional] buy or sell
        /// </summary>
        [JsonProperty("side")]
        public OrderSide Side { get; set; }

        /// <summary>
        /// [Optional] limit, market, limit_stop or market_stop
        /// </summary>
        [JsonProperty("type")]
        public OrderType Type { get; set; }

        /// <summary>
        /// The type of trading : TRADE（Spot Trading）, MARGIN_TRADE (Margin Trading)
        /// </summary>
        [JsonProperty("tradeType")]
        public TradeType TypeTrade { get; set; }
    }
}
