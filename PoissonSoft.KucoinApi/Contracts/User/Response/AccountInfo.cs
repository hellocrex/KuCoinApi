using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.KuCoinApi.Contracts.Enums;

namespace PoissonSoft.KuCoinApi.Contracts.User.Account.Response
{
    public class AccountInfo
    {
        /// <summary>
        /// The ID of the account
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Currency
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// Account type: main, trade, margin or pool
        /// </summary>
        [JsonProperty("type")]
        public AccountType AccountType { get; set; }

        /// <summary>
        /// Total funds in the account
        /// </summary>
        [JsonProperty("balance")]
        public decimal balance { get; set; }

        /// <summary>
        /// Funds available to withdraw or trade
        /// </summary>
        [JsonProperty("available")]
        public decimal Available { get; set; }

        /// <summary>
        /// Funds on hold (not available for use)
        /// </summary>
        [JsonProperty("holds")]
        public decimal Holds { get; set; }
    }
}
