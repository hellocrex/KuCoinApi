using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KuCoinApi.Contracts.User.Response
{
    public class Transferable
    {
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
        public string Available { get; set; }

        /// <summary>
        /// Funds on hold (not available for use)
        /// </summary>
        [JsonProperty("holds")]
        public string Holds { get; set; }

        /// <summary>
        /// Funds available to transfer
        /// </summary>
        [JsonProperty("transferable")]
        public string Able { get; set; }
    }
}
