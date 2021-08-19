using Newtonsoft.Json;

namespace PoissonSoft.KuCoinApi.Contracts.User.Response
{
    public class WithdrawalsListResp
    {
        /// <summary>
        /// Unique identity
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
        /// <summary>
        /// Withdrawal address
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; set; }

        /// <summary>
        /// Address remark. If there’s no remark, it is empty
        /// </summary>
        [JsonProperty("memo")]
        public string Memo { get; set; }

        /// <summary>
        /// Withdrawal amount
        /// </summary>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Withdrawal fee
        /// </summary>
        [JsonProperty("fee")]
        public float Fee { get; set; }

        /// <summary>
        /// Currency
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// Internal withdrawal or not
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
        public string Status { get; set; }

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
