using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.KuCoinApi.Contracts.Enums;
using PoissonSoft.KuCoinApi.Contracts.Trade.Request;

namespace PoissonSoft.KuCoinApi.Contracts.Trade.Request
{
    public class ReqNewOrder
    {
        /// <summary>
        /// ‎Уникальный идентификатор заказа, созданный пользователями для идентификации заказов, например, UUID‎
        /// </summary>
        [JsonProperty("clientOid")]
        public string ClientOid { get; set; }

        /// <summary>
        /// buy or sell
        /// </summary>
        [JsonProperty("side")]
        public OrderSide Side { get; set; }

        /// <summary>
        /// ‎действительный код торгового символа. например, ETH-BTC‎
        /// </summary>
        [JsonProperty("symbol")]
        public string Instrument { get; set; }

        /// <summary>
        /// ‎[Необязательно]‎‎ ‎‎лимит‎‎ или ‎‎рынок‎‎ (по умолчанию ‎‎— лимит‎)
        /// </summary>
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public OrderType? Type { get; set; }

        /// <summary>
        /// ‎[Необязательно]‎‎ замечание для заказа, длина не может превышать 100 символов utf8‎
        /// </summary>
        [JsonProperty("remark", NullValueHandling = NullValueHandling.Ignore)]
        public string Comment { get; set; }

        /// <summary>
        /// [Optional] Either loss or entry, the default is loss. Requires stopPrice to be defined.
        /// </summary>
        [JsonProperty("stop", NullValueHandling = NullValueHandling.Ignore)]
        public StopOrderType? Stop { get; set; }

        /// <summary>
        /// Need to be defined if stop is specified.
        /// </summary>
        [JsonProperty("stopPrice", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? StopPrice { get; set; }

        /// <summary>
        /// ‎[Необязательно]‎‎ предотвращение самоторговли, ‎‎CN,‎‎ ‎‎CO,‎‎ ‎‎CB‎‎ или ‎‎DC‎
        /// </summary>
        [JsonProperty("stp", NullValueHandling = NullValueHandling.Ignore)]
        public STP? STP { get; set; }

        /// <summary>
        /// [Необязательно]‎‎ Вид торговли: ‎‎TRADE (Spot‎‎Trade), ‎‎MARGIN_TRADE‎‎ (Margin Trade). По умолчанию используется ‎‎значение TRADE.‎‎
        /// </summary>
        [JsonProperty("tradeType", NullValueHandling = NullValueHandling.Ignore)]
        public TradeType? TradeType { get; set; }
        
        // дополнительные параметры, зависящие от типа ордера
        //LIMIT ORDER PARAMETERS
        /// <summary>
        /// price per base currency
        /// </summary>
        [JsonProperty("price",NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Price { get; set; }

        /// <summary>
        /// amount of base currency to buy or sell
        /// </summary>
        [JsonProperty("size")]
        public decimal Size { get; set; }

        /// <summary>
        /// [Optional] GTC, GTT, IOC, or FOK (default is GTC)
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

        // MARKET ORDER PARAMETER
        /// <summary>
        /// [Optional] The maximum visible size of an iceberg order
        /// </summary>
        [JsonProperty("funds", NullValueHandling = NullValueHandling.Ignore)]
        public string Funds { get; set; }
    }
}
