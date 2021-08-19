using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KuCoinApi.Contracts.User.Response
{
    public class WithdrawQuotasResp
    {
        /// <summary>
        /// Currency
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// Current available withdrawal amount
        /// </summary>
        [JsonProperty("availableAmount")]
        public decimal AvailableAmount { get; set; }

        /// <summary>
        /// Remaining amount available to withdraw the current day
        /// </summary>
        [JsonProperty("remainAmount")]
        public decimal RemainAmount { get; set; }

        /// <summary>
        /// Minimum withdrawal amount
        /// </summary>
        [JsonProperty("withdrawMinSize")]
        public decimal WithdrawMinSize { get; set; }

        /// <summary>
        /// Total BTC amount available to withdraw the current day
        /// </summary>
        [JsonProperty("limitBTCAmount")]
        public decimal LimitBTCAmount { get; set; }

        /// <summary>
        /// Fees for internal withdrawal
        /// </summary>
        [JsonProperty("innerWithdrawMinFee")]
        public float InnerWithdrawMinFee { get; set; }

        /// <summary>
        /// The estimated BTC amount (based on the daily fiat limit) that can be withdrawn within the current day
        /// </summary>
        [JsonProperty("usedBTCAmount")]
        public decimal UsedBTCAmount { get; set; }

        /// <summary>
        /// Is the withdraw function enabled or not
        /// </summary>
        [JsonProperty("isWithdrawEnabled")]
        public bool IsWithdrawEnabled { get; set; }

        /// <summary>
        /// Minimum withdrawal fee
        /// </summary>
        [JsonProperty("withdrawMinFee")]
        public float WithdrawMinFee { get; set; }

        /// <summary>
        /// Floating point precision.
        /// </summary>
        [JsonProperty("Precision")]
        public float Precision { get; set; }

        /// <summary>
        /// The chain name of currency, e.g. The available value for USDT are OMNI, ERC20, TRC20, default is ERC20.
        /// </summary>
        [JsonProperty("chain")]
        public string Chain { get; set; }
    }
}
