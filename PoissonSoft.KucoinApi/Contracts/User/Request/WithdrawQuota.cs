using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KuCoinApi.Contracts.User.Request
{
    public class WithdrawQuota
    {
        /// <summary>
        /// currency. e.g. BTC
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// [Optional] The chain name of currency, e.g. The available value for USDT are OMNI, ERC20, TRC20, default is ERC20.
        /// </summary>
        [JsonProperty("chain")]
        public string Chain { get; set; }
    }
}
