using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KuCoinApi.Contracts.User.Request
{
    public class WithdrawReq
    {
        /// <summary>
        /// Currency
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// Withdrawal address
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; set; }

        /// <summary>
        /// Withdrawal amount, a positive number which is a multiple of the amount precision (fees excluded)
        /// </summary>
        [JsonProperty("amount")]
        public string Amount { get; set; }

        /// <summary>
        /// [Optional] Address remark. If there’s no remark, it is empty.
        /// </summary>
        [JsonProperty("memo")]
        public string Memo { get; set; }

        /// <summary>
        /// [Optional] Internal withdrawal or not. Default setup: false
        /// </summary>
        [JsonProperty("isInner")]
        public bool isInner { get; set; }

        /// <summary>
        /// [Optional] Remark
        /// </summary>
        [JsonProperty("remark")]
        public string Remark { get; set; }

        /// <summary>
        /// [Optional] The chain name of currency, e.g. The available value for USDT are OMNI, ERC20, TRC20, default is ERC20.
        /// </summary>
        [JsonProperty("chain")]
        public string Chain { get; set; }
    }
}
