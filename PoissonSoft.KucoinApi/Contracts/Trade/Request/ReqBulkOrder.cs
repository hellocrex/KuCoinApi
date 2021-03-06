using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.KuCoinApi.Contracts.Enums;

namespace PoissonSoft.KuCoinApi.Contracts.Trade.Request
{
    public class ReqBulkOrder
    {
        /// <summary>
        /// Unique order id created by users to identify their orders, e.g. UUID.
        /// </summary>
        [JsonProperty("clientOid")]
        public string ClientOid { get; set; }

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
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public OrderType? Type { get; set; }

        /// <summary>
        /// [Optional] remark for the order, length cannot exceed 100 utf8 characters
        /// </summary>
        [JsonProperty("remark", NullValueHandling = NullValueHandling.Ignore)]
        public string Remark { get; set; }

        /// <summary>
        /// The type of trading : TRADE（Spot Trading）, MARGIN_TRADE (Margin Trading)
        /// </summary>
        [JsonProperty("tradeType", NullValueHandling = NullValueHandling.Ignore)]
        public TradeType? TradeType { get; set; }

        /// <summary>
        /// [Optional] self trade prevention , CN, CO, CB or DC
        /// </summary>
        [JsonProperty("stp", NullValueHandling = NullValueHandling.Ignore)]
        public STP? STP { get; set; }

        /// <summary>
        /// [Optional] Either loss or entry. Requires stopPrice to be defined
        /// </summary>
        [JsonProperty("stop", NullValueHandling = NullValueHandling.Ignore)]
        public string Stop { get; set; }

        /// <summary>
        /// [Optional] Need to be defined if stop is specified.
        /// </summary>
        [JsonProperty("stopPrice", NullValueHandling = NullValueHandling.Ignore)]
        public string StopPrice { get; set; }

        /// <summary>
        /// Price per base currency
        /// </summary>
        [JsonProperty("price")]
        public string Price { get; set; }

        /// <summary>
        /// Amount of base currency to buy or sell
        /// </summary>
        [JsonProperty("size")]
        public string Size { get; set; }

        /// <summary>
        /// [Optional] active or done(done as default), Only list orders with a specific status
        /// </summary>
        [JsonProperty("timeInForce", NullValueHandling = NullValueHandling.Ignore)]
        public TimeInForce? TimeInForce { get; set; }

        /// <summary>
        /// [Optional] cancel after n seconds, requires timeInForce to be GTT
        /// </summary>
        [JsonProperty("cancelAfter", NullValueHandling = NullValueHandling.Ignore)]
        public long? CancelAfter { get; set; }

        /// <summary>
        /// [Optional] Post only flag, invalid when timeInForce is IOC or FOK
        /// </summary>
        [JsonProperty("postOnly", NullValueHandling = NullValueHandling.Ignore)]
        public bool? PostOnly { get; set; }

        /// <summary>
        /// [Optional] Order will not be displayed in the order book
        /// </summary>
        [JsonProperty("hidden", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Hidden { get; set; }

        /// <summary>
        /// [Optional] Only aportion of the order is displayed in the order book
        /// </summary>
        [JsonProperty("iceberg", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Iceberg { get; set; }

        /// <summary>
        /// [Optional] The maximum visible size of an iceberg order
        /// </summary>
        [JsonProperty("visibleSize", NullValueHandling = NullValueHandling.Ignore)]
        public string VisibleSize { get; set; }
    }
}
