using System;
using Newtonsoft.Json;

namespace PoissonSoft.KuCoinApi.Contracts.MarketData.Response
{
    public class Ticker : ICloneable
    {
        /// <summary>
        /// Символ
        /// </summary>
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        /// <summary>
        /// ‎Название торговых пар,
        /// оно будет меняться после переименования‎
        /// </summary>
        [JsonProperty("symbolName")]
        public string Name { get; set; }

        /// <summary>
        /// Лучшая цена предложения‎
        /// </summary>
        [JsonProperty("buy")]
        public decimal BestPriceBuy { get; set; }

        /// <summary>
        /// Лучшая цена продажи‎
        /// </summary>
        [JsonProperty("sell")]
        public decimal BestPriceSell { get; set; }

        /// <summary>
        /// ‎Скорость изменения за 24ч
        /// </summary>
        [JsonProperty("changeRate")]
        public decimal ChangeRate { get; set; }

        /// <summary>
        /// Изменение цены‎ за 24ч
        /// </summary>
        [JsonProperty("changePrice")]
        public decimal ChangePrice { get; set; }

        /// <summary>
        /// ‎Самая высокая цена за 24ч‎
        /// </summary>
        [JsonProperty("high")]
        public decimal HighPrice { get; set; }

        /// <summary>
        /// ‎Самая низкая цена за 24ч‎
        /// </summary>
        [JsonProperty("low")]
        public decimal LowPrice { get; set; }

        /// <summary>
        /// ‎24-х ч объем, выполненный на основе базовой валюты‎
        /// </summary>
        [JsonProperty("vol")]
        public decimal VolBaseCurrency { get; set; }

        /// <summary>
        /// ‎Торговая сумма за 24 ч‎
        /// </summary>
        [JsonProperty("volValue")]
        public decimal TotalVol { get; set; }

        /// <summary>
        /// Цена последней торговли
        /// </summary>
        [JsonProperty("last")]
        public decimal LastPrice { get; set; }

        /// <summary>
        /// Средняя торговая цена за последние 24 часа‎
        /// </summary>
        [JsonProperty("averagePrice")]
        public decimal AveragePrice { get; set; }

        /// <summary>
        /// ‎‎Базовый сбор‎
        /// </summary>
        [JsonProperty("takerFeeRate")]
        public float TakerFeeRate { get; set; }

        /// <summary>
        /// ‎Базовая плата maker'a‎
        /// </summary>
        [JsonProperty("makerFeeRate")]
        public float MakerFeeRate { get; set; }

        /// <summary>
        /// ‎Коэффициент комиссии такового taker'a‎
        /// </summary>
        [JsonProperty("takerCoefficient")]
        public string TakerCoefficient { get; set; }

        /// <summary>
        /// ‎Коэффициент вознаграждения maker'a‎
        /// </summary>
        [JsonProperty("makerCoefficient")]
        public string MakerCoefficient { get; set; }

        public object Clone()
        {
            return new Ticker()
            {
                Symbol = Symbol,
                Name = Name,
                BestPriceBuy = BestPriceBuy,
                BestPriceSell = BestPriceSell,
                ChangeRate = ChangeRate,
                ChangePrice = ChangePrice,
                HighPrice = HighPrice,
                LowPrice = LowPrice,
                VolBaseCurrency = VolBaseCurrency,
                TotalVol = TotalVol,
                LastPrice = LastPrice,
                AveragePrice = AveragePrice,
                TakerFeeRate = TakerFeeRate,
                MakerFeeRate = MakerFeeRate,
                TakerCoefficient = TakerCoefficient,
                MakerCoefficient = MakerCoefficient
            };
        }

    }
}
