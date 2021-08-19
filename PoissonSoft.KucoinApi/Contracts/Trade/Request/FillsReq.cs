using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.KuCoinApi.Contracts.Enums;

namespace PoissonSoft.KuCoinApi.Contracts.Trade.Request
{
    public class FillsReq
    {
        /// <summary>
        /// [Optional] Limit the list of fills to this orderId（If you specify orderId, ignore other conditions)
        /// </summary>
        [JsonProperty("orderId")]
        public string OrderId { get; set; }

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
        /// The type of trading : TRADE（Spot Trading）, MARGIN_TRADE (Margin Trading)
        /// </summary>
        [JsonProperty("tradeType")]
        public TradeType TradeType { get; set; }

        /// <summary>
        /// ‎[Optional] Start time (milisecond)
        /// </summary>
        [JsonProperty("startAt")]
        public long StartTime { get; set; }

        /// <summary>
        /// [Optional] End time (milisecond)
        /// </summary>
        [JsonProperty("endAt")]
        public long EndTime { get; set; }
    }
}
