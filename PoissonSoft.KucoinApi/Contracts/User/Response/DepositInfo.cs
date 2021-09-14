using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.KuCoinApi.Contracts.Enums;

namespace PoissonSoft.KuCoinApi.Contracts.User.Response
{
    public class DepositInfo
    {
        /// <summary>
        /// Deposit address
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; set; }

        /// <summary>
        /// Address remark. If there’s no remark, it is empty
        /// </summary>
        [JsonProperty("memo")]
        public string Memo { get; set; }

        /// <summary>
        /// Deposit amount
        /// </summary>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Fees charged for deposit
        /// </summary>
        [JsonProperty("fee")]
        public float Fee { get; set; }

        /// <summary>
        /// Currency
        /// </summary>
        [JsonProperty("currency")]
        public string Coin { get; set; }

        /// <summary>
        /// Internal deposit or not
        /// </summary>
        [JsonProperty("isInner")]
        public bool isInner { get; set; }

        /// <summary>
        /// Wallet Txid
        /// </summary>
        [JsonProperty("walletTxId")]
        public string walletTxId { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        [JsonProperty("status")]
        public DepositAndWithdrawStatus Status { get; set; }

        /// <summary>
        /// Remark
        /// </summary>
        [JsonProperty("remark")]
        public string Remark { get; set; }

        /// <summary>
        /// Creation time of the database record
        /// </summary>
        [JsonProperty("createdAt")]
        public long CreatedAt { get; set; }

        /// <summary>
        /// Update time of the database record
        /// </summary>
        [JsonProperty("updatedAt")]
        public long UpdatedAt { get; set; }
    }
}
