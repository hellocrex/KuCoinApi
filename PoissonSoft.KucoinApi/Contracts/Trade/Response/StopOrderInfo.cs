using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.KuCoinApi.Contracts.Enums;

namespace PoissonSoft.KuCoinApi.Contracts.Trade.Response
{
    public class StopOrderInfo
    {
        /// <summary>
        /// Order ID, the ID of an order.
        /// </summary>
        [JsonProperty("id")]
        public string OrderId { get; set; }

        /// <summary>
        /// ‎Symbol
        /// </summary>
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        /// <summary>
        /// User ID
        /// </summary>
        [JsonProperty("userId")]
        public string UserId { get; set; }

        /// <summary>
        /// Order status, include NEW, TRIGGERED
        /// </summary>
        [JsonProperty("status")]
        public OrderStatus Status { get; set; }

        /// <summary>
        ///transaction direction,include buy and sell
        /// </summary>
        [JsonProperty("side")]
        public OrderSide Side { get; set; }

        /// <summary>
        /// ‎‎лимит‎‎ или ‎‎рынок‎‎ (по умолчанию ‎‎— лимит‎)
        /// </summary>
        [JsonProperty("type")]
        public OrderType Type { get; set; }

        /// <summary>
        /// ‎[Необязательно]‎‎ замечание для заказа, длина не может превышать 100 символов utf8‎
        /// </summary>
        [JsonProperty("remark")]
        public string Comment { get; set; }

        /// <summary>
        /// ‎[Необязательно]‎‎ предотвращение самоторговли, ‎‎CN,‎‎ ‎‎CO,‎‎ ‎‎CB‎‎ или ‎‎DC‎
        /// </summary>
        [JsonProperty("stp", NullValueHandling = NullValueHandling.Ignore)]
        public STP STP { get; set; }

        /// <summary>
        /// ‎‎TRADE (Spot‎‎Trade), ‎‎MARGIN_TRADE‎‎ (Margin Trade)
        /// </summary>
        [JsonProperty("tradeType")]
        public TradeType TradeType { get; set; }

        /// <summary>
        /// price per base currency
        /// </summary>
        [JsonProperty("price")]
        public decimal Price { get; set; }

        /// <summary>
        /// amount of base currency to buy or sell
        /// </summary>
        [JsonProperty("size")]
        public decimal size { get; set; }

        /// <summary>
        ///  GTC, GTT, IOC, or FOK (default is GTC)
        /// </summary>
        [JsonProperty("timeInForce")]
        public TimeInForce TimeInForce { get; set; }

        /// <summary>
        /// cancel after n seconds, requires timeInForce to be GTT
        /// </summary>
        [JsonProperty("cancelAfter")]
        public long CancelAfter { get; set; }

        /// <summary>
        /// Post only flag, invalid when timeInForce is IOC or FOK
        /// </summary>
        [JsonProperty("postOnly")]
        public bool PostOnly { get; set; }

        /// <summary>
        /// Order will not be displayed in the order book
        /// </summary>
        [JsonProperty("hidden")]
        public bool Hidden { get; set; }

        /// <summary>
        /// Only aportion of the order is displayed in the order book
        /// </summary>
        [JsonProperty("iceberg")]
        public bool Iceberg { get; set; }

        /// <summary>
        /// The maximum visible size of an iceberg order
        /// </summary>
        [JsonProperty("visibleSize")]
        public string VisibleSize { get; set; }
        
        /// <summary>
        /// The maximum visible size of an iceberg order
        /// </summary>
        [JsonProperty("funds")]
        public string Funds { get; set; }

        /// <summary>
        /// user-entered order unique mark
        /// </summary>
        [JsonProperty("clientOid")]
        public string ClientOid { get; set; }
        
        /// <summary>
        /// tag order source
        /// </summary>
        [JsonProperty("tags")]
        public string Tags { get; set; }

        /// <summary>
        /// Time of place a stop order, accurate to nanoseconds
        /// </summary>
        [JsonProperty("orderTime")]
        public long OrderTime { get; set; }

        /// <summary>
        /// domainId, e.g: kucoin
        /// </summary>
        [JsonProperty("domainId")]
        public string DomainId { get; set; }

        /// <summary>
        /// trade source: USER（Order by user）, MARGIN_SYSTEM（Order by margin system）
        /// </summary>
        [JsonProperty("tradeSource")]
        public string TradeSource { get; set; }

        /// <summary>
        /// The currency of the fee
        /// </summary>
        [JsonProperty("feeCurrency")]
        public string FeeCurrency { get; set; }

        /// <summary>
        /// Fee Rate of taker
        /// </summary>
        [JsonProperty("takerFeeRate")]
        public decimal TakerFeeRate { get; set; }

        /// <summary>
        /// Fee Rate of maker
        /// </summary>
        [JsonProperty("makerFeeRate")]
        public decimal MakerFeeRate { get; set; }

        /// <summary>
        /// order creation time
        /// </summary>
        [JsonProperty("createdAt")]
        public long CreatedAt { get; set; }

        /// <summary>
        /// Stop order type, include loss and entry
        /// </summary>
        [JsonProperty("stop")]
        public StopOrderType Stop { get; set; }

        /// <summary>
        /// The trigger time of the stop order
        /// </summary>
        [JsonProperty("stopTriggerTime", NullValueHandling = NullValueHandling.Ignore)]
        public long StopTriggerTime { get; set; }

        /// <summary>
        /// stop price
        /// </summary>
        [JsonProperty("stopPrice")]
        public decimal StopPrice { get; set; }
    }
}
