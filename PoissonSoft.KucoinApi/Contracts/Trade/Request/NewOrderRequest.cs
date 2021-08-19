using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.KuCoinApi.Contracts.Enums;
using PoissonSoft.KuCoinApi.Contracts.Trade.Request;

namespace PoissonSoft.KuCoinApi.Contracts.Trade.Request
{
    public class NewOrderRequest : OrderParameters
    {
        /// <summary>
        /// ‎Уникальный идентификатор заказа, созданный пользователями для идентификации заказов, например, UUID‎
        /// </summary>
        [JsonProperty("clientOid")]
        public string ClientId { get; set; }

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
        [JsonProperty("stp")]
        public STP STP { get; set; }

        /// <summary>
        /// [Необязательно]‎‎ Вид торговли: ‎‎TRADE (Spot‎‎Trade), ‎‎MARGIN_TRADE‎‎ (Margin Trade). По умолчанию используется ‎‎значение TRADE.‎‎
        /// </summary>
        [JsonProperty("tradeType")]
        public TradeType TradeType { get; set; }
    }
}
