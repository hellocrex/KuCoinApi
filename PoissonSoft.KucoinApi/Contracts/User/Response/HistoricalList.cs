using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KuCoinApi.Contracts.User.Response
{
    public class HistoricalList
    {
       /// <summary>
        /// Deposit amount
        /// </summary>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
       
        /// <summary>
        /// Currency
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }

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
        /// Creation time of the database record
        /// </summary>
        [JsonProperty("createAt")]
        public long CreateAt { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
