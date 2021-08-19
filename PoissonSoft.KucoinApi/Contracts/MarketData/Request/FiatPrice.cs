using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KuCoinApi.Contracts.MarketData.Request
{
    public class FiatPrice
    {
        /// <summary>
        /// [Optional] Ticker symbol of a base currency,eg.USD,EUR.
        /// </summary>
        [JsonProperty("base")]
        public string BaseCurrency { get; set; }

        /// <summary>
        /// [Optional] Comma-separated cryptocurrencies to be converted into fiat, e.g.: BTC,ETH, etc.
        /// </summary>
        [JsonProperty("currencies")]
        public string Currencies { get; set; }
    }
}
