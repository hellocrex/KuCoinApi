using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KuCoinApi.Contracts.MarketData.Request
{
    public class ReqCurrencyDetail
    {
        /// <summary>
        /// Ticker symbol of a base currency,eg.USD,EUR.
        /// </summary>
        [JsonProperty("currency")]
        public string Coin { get; set; }

        /// <summary>
        /// [Optional] Support for querying the chain of currency, e.g. The available value for USDT are OMNI, ERC20, TRC20. This only apply for multi-chain currency, and there is no need for single chain currency.
        /// </summary>
        [JsonProperty("chain", NullValueHandling = NullValueHandling.Ignore)]
        public string Network { get; set; }
    }
}
