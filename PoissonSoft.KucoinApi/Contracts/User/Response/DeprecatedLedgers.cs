using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.KuСoinApi.Contracts.Enums;

namespace PoissonSoft.KuСoinApi.Contracts.User.Response
{
    public class DeprecatedLedgers
    {
        /// <summary>
        /// Coin (optional)
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Coin (optional)
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// Coin (optional)
        /// </summary>
        [JsonProperty("amount")]
        public string Amount { get; set; }

        /// <summary>
        /// Coin (optional)
        /// </summary>
        [JsonProperty("fee")]
        public string Fee { get; set; }

        /// <summary>
        /// Coin (optional)
        /// </summary>
        [JsonProperty("balance")]
        public string Balance { get; set; }

        /// <summary>
        /// Coin (optional)
        /// </summary>
        [JsonProperty("bizType")]
        public string BizType { get; set; }

        /// <summary>
        /// Coin (optional)
        /// </summary>
        [JsonProperty("direction")]
        public OrderSide Side { get; set; }

        /// <summary>
        /// Coin (optional)
        /// </summary>
        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        /// <summary>
        /// Coin (optional)
        /// </summary>
        [JsonProperty("context")]
        public string Context { get; set; }
    }
}
