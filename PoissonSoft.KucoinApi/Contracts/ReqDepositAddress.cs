using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KuCoinApi.Contracts
{
    public class ReqDepositAddress
    {
        /// <summary>
        /// Currency
        /// </summary>
        [JsonProperty("currency")]
        public string Symbol { get; set; }

        /// <summary>
        /// [Optional] The chain name of currency, e.g. The available value for USDT are OMNI, ERC20, TRC20, default is ERC20.
        /// </summary>
        [JsonProperty("chain", NullValueHandling = NullValueHandling.Ignore)]
        public string Chain { get; set; }
        
    }
}
