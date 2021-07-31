using System;
using Newtonsoft.Json;
using PoissonSoft.KuСoinApi.Contracts.Serialization;

namespace PoissonSoft.KuСoinApi.Contracts
{
    /// <summary>
    /// Cписок доступных валютных пар для торговли
    /// </summary>
    public class DataCoin : ICloneable
    {
        /// <summary>
        /// ‎Уникальный код символа,
        /// он не изменится после переименования‎
        /// </summary>
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        /// <summary>
        /// ‎Название торговых пар,
        /// оно будет меняться после переименования‎
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// ‎Базовая валюта‎
        /// </summary>
        [JsonProperty("baseCurrency")]
        public string BaseCurrency { get; set; }

        /// <summary>
        /// Валюта котировки
        /// </summary>
        [JsonProperty("quoteCurrency")]
        public string QuoteCurrency { get; set; }

        /// <summary>
        /// ‎
        /// </summary>
        [JsonProperty("market")]
        public string Market { get; set; }

        /// <summary>
        /// ‎Минимальное количество заказа, необходимое для размещения заказа‎
        /// </summary>
        [JsonProperty("baseMinSize")]
        public string BaseMinSize { get; set; }

        /// <summary>
        /// ‎Минимальные средства ордера, необходимые для размещения рыночного ордера‎
        /// </summary>
        [JsonProperty("quoteMinSize")]
        public string QuoteMinSize { get; set; }

        /// <summary>
        /// ‎Максимальный размер ордера, необходимый для размещения ордера‎
        /// </summary>
        [JsonProperty("baseMaxSize")]
        public string BaseMaxSize { get; set; }

        /// <summary>
        /// ‎Максимальные средства ордера, необходимые для размещения рыночного ордера‎
        /// </summary>
        [JsonProperty("quoteMaxSize")]
        public string QuteMaxSize { get; set; }

        /// <summary>
        /// ‎Приращение размера ордера.
        /// Значение должно быть положительным кратным приросту baseIncrement.‎
        /// </summary>
        [JsonProperty("baseIncrement")]
        public string BaseIncrement { get; set; }

        /// <summary>
        /// ‎Приращение средств, необходимых для размещения рыночного ордера.
        /// Значение должно быть положительным кратным приросту quoteIncrement.‎
        /// </summary>
        [JsonProperty("quoteIncrement")]
        public string QuoteIncrement { get; set; }

        /// <summary>
        /// ‎Приращение цены, необходимое для размещения лимитного ордера.
        /// Значение должно быть положительным кратным приращению цены.‎
        /// </summary>
        [JsonProperty("priceLimitRate")]
        public string PriceLimitRate { get; set; }

        /// <summary>
        /// ‎Валюта, в которой взимаются комиссии
        /// </summary>
        [JsonProperty("feeCurrency")]
        public string FeeCurrency { get; set; }

        /// <summary>
        /// Размер временного интервала, на который распространяется лимит, в единицах
        /// </summary>
        [JsonProperty("isMarginEnabled")]
        public bool isMarginEnable { get; set; }

        /// <summary>
        /// Размер лимита (максимальное допустимое количество операций за данный период)
        /// </summary>
        [JsonProperty("enableTrading")]
        public bool EnableTrading { get; set; }

        /// <inheritdoc />
        public object Clone()
        {
            return new DataCoin()
            {
                Symbol = Symbol,
                Name = Name,
                BaseCurrency = BaseCurrency,
                QuoteCurrency = QuoteCurrency,
                Market = Market,
                BaseMinSize = BaseMinSize,
                QuoteMinSize = QuoteMinSize,
                BaseMaxSize = BaseMaxSize,
                QuteMaxSize = QuteMaxSize,
                BaseIncrement = BaseIncrement,
                QuoteIncrement = QuoteIncrement,
                PriceLimitRate = PriceLimitRate,
                FeeCurrency = FeeCurrency,
                isMarginEnable = isMarginEnable,
                EnableTrading = EnableTrading
            };
        }
    }
}