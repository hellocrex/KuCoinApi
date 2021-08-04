using Newtonsoft.Json;
using PoissonSoft.KuСoinApi.Contracts.Enums;

namespace PoissonSoft.KuСoinApi.Contracts.Trade.Request
{
    public class MarginReq
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
        public string Symbol { get; set; }

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
        /// [Optional] The type of trading, including cross (cross mode) and isolated (isolated mode).
        /// It is set at cross by default. The isolated mode will be released soon, so stay tuned!
        /// </summary>
        [JsonProperty("marginMode")]
        public string MarginMode { get; set; }

        /// <summary>
        /// [Optional] Auto-borrow to place order. The system will first borrow you funds at the optimal
        /// interest rate and then place an order for you.
        /// </summary>
        [JsonProperty("autoBorrow")]
        public bool AutoBorrow { get; set; }
    }
}
