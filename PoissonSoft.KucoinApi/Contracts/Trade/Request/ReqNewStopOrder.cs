using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.KuCoinApi.Contracts.Enums;

namespace PoissonSoft.KuCoinApi.Contracts.Trade.Request
{
    public class ReqNewStopOrder
    {
        /// <summary>
        /// Unique order id created by users to identify their orders, e.g. UUID.
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
        public string Symbol { get; set; }

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
        public string Stop { get; set; }

        /// <summary>
        /// Need to be defined if stop is specified.
        /// </summary>
        [JsonProperty("stopPrice")]
        public string StopPrice { get; set; }

        /// <summary>
        /// ‎[Optional] self trade prevention , CN, CO, CB , DC (limit order does not support DC)
        /// </summary>
        [JsonProperty("stp", NullValueHandling = NullValueHandling.Ignore)]
        public STP? STP { get; set; }

        /// <summary>
        /// [Необязательно]‎‎ Вид торговли: ‎‎TRADE (Spot‎‎Trade), ‎‎MARGIN_TRADE‎‎ (Margin Trade). По умолчанию используется ‎‎значение TRADE.‎‎
        /// </summary>
        [JsonProperty("tradeType", NullValueHandling = NullValueHandling.Ignore)]
        public TradeType? TradeType { get; set; }
    }
}
