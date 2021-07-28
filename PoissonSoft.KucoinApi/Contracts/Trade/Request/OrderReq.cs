using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.KuСoinApi.Contracts.Enums;

namespace PoissonSoft.KuСoinApi.Contracts.Trade.Request
{
    /// <summary>
    /// Параметры необходимые для запроса позволяющего получить текущий список ордеров.
    /// Example
    /// GET /api/v1/orders? status = active
    /// </summary>
    public class OrderReq
    {
        /// <summary>
        /// [Optional] active or done(done as default), Only list orders with a specific status
        /// </summary>
        [JsonProperty("status")]
        public Status StatusOrder { get; set; }

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
        public TradeType TypeTrade { get; set; }

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
