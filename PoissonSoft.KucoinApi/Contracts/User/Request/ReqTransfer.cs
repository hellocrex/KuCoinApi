using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.KuCoinApi.Contracts.Enums;

namespace PoissonSoft.KuCoinApi.Contracts.User.Request
{
    public class ReqTransfer
    {
        /// <summary>
        /// Unique order id created by users to identify their orders, e.g. UUID.
        /// </summary>
        [JsonProperty("clientOid")]
        public string ClientOid { get; set; }

        /// <summary>
        /// Currency
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// Account type of payer: main, trade, margin or pool
        /// </summary>
        [JsonProperty("from")]
        public AccountType From { get; set; }

        /// <summary>
        /// Account type of payee: main, trade, margin , contract or pool
        /// </summary>
        [JsonProperty("to")]
        public AccountType To { get; set; }

        /// <summary>
        /// Transfer amount, the amount is a positive integer multiple of the currency precision.
        /// </summary>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
    }
}
