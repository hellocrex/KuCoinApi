using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.KuCoinApi.Contracts.Enums;

namespace PoissonSoft.KuCoinApi.Contracts.Trade.Response
{
    public class Fills
    {
        /// <summary>
        /// symbol
        /// </summary>
        [JsonProperty("symbol")]
        public string Ticker { get; set; }

        /// <summary>
        /// ‎trade id, it is generated by Matching engine
        /// </summary>
        [JsonProperty("tradeId")]
        public string TradeId { get; set; }

        /// <summary>
        /// ‎Order ID, unique identifier of an order
        /// </summary>
        [JsonProperty("orderId")]
        public string OrderId { get; set; }

        /// <summary>
        /// counter order id
        /// </summary>
        [JsonProperty("counterOrderId")]
        public string CounterOrderId { get; set; }

        /// <summary>
        /// ‎transaction direction,include buy and sell
        /// </summary>
        [JsonProperty("side")]
        public OrderSide Side { get; set; }

        /// <summary>
        /// order price
        /// </summary>
        [JsonProperty("price")]
        public decimal Price { get; set; }

        /// <summary>
        /// ‎order quantity
        /// </summary>
        [JsonProperty("size")]
        public decimal Size { get; set; }

        /// <summary>
        /// order funds
        /// </summary>
        [JsonProperty("funds")]
        public string Funds { get; set; }

        /// <summary>
        /// order type,e.g. limit,market,stop_limit
        /// </summary>
        [JsonProperty("type")]
        public OrderType Type { get; set; }

        /// <summary>
        /// fee
        /// </summary>
        [JsonProperty("fee")]
        public decimal Commission { get; set; }

        /// <summary>
        /// charge fee currency
        /// </summary>
        [JsonProperty("feeCurrency")]
        public string FeeCurrency { get; set; }

        /// <summary>
        /// ‎stop type, include entry and loss
        /// </summary>
        [JsonProperty("stop")]
        public string StopType { get; set; }

        /// <summary>
        /// include taker and maker
        /// </summary>
        [JsonProperty("liquidity")]
        public string Liquidity { get; set; }

        /// <summary>
        /// forced to become taker, include true and false
        /// </summary>
        [JsonProperty("forceTaker")]
        public string ForceTaker { get; set; }

        /// <summary>
        /// ‎create time
        /// </summary>
        [JsonProperty("createdAt")]
        public long CreatedTime { get; set; }

        /// <summary>
        /// ‎The type of trading : TRADE（Spot Trading）, MARGIN_TRADE (Margin Trading)
        /// </summary>
        [JsonProperty("tradeType")]
        public TradeType TradeType { get; set; }
    }
}
