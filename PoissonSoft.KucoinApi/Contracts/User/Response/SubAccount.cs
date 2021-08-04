using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.KuСoinApi.Contracts.Enums;

namespace PoissonSoft.KuСoinApi.Contracts.User.Response
{
    public class SubAccount
    {
        /// <summary>
        /// The user ID of a sub-user
        /// </summary>
        [JsonProperty("subUserId")]
        public string SubUserId { get; set; }

        /// <summary>
        /// The username of a sub-user
        /// </summary>
        [JsonProperty("subName")]
        public string SubName { get; set; }

        /// <summary>
        /// Currency
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// Total funds in an account
        /// </summary>
        [JsonProperty("balance")]
        public decimal Balance { get; set; }

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

        /// <summary>
        /// Calculated on this currency
        /// </summary>
        [JsonProperty("baseCurrency")]
        public string baseCurrency { get; set; }

        /// <summary>
        /// The base currency price
        /// </summary>
        [JsonProperty("baseCurrencyPrice")]
        public string BaseCurrencyPrice { get; set; }

        /// <summary>
        /// The base currency amount
        /// </summary>
        [JsonProperty("baseAmount")]
        public string BaseAmount { get; set; }
    }
}
