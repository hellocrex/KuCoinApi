using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.KuCoinApi.Contracts.Enums;

namespace PoissonSoft.KuCoinApi.Contracts.User.Request
{
    public class ReqTransferBetweenAcc
    {
        /// <summary>
        /// Unique order id created by users to identify their orders, e.g. UUID.
        /// </summary>
        [JsonProperty("clientOid", NullValueHandling = NullValueHandling.Ignore)]
        public string ClientOid { get; set; }

        /// <summary>
        /// Currency
        /// </summary>
        [JsonProperty("currency", NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; }

        /// <summary>
        /// Transfer amount, the amount is a positive integer multiple of the currency precision.
        /// </summary>
        [JsonProperty("amount", NullValueHandling = NullValueHandling.Ignore)]
        public string Amount { get; set; }

        /// <summary>
        /// OUT — the master user to sub user
        /// IN — the sub user to the master user.
        /// </summary>
        [JsonProperty("direction", NullValueHandling = NullValueHandling.Ignore)]
        public Direction Direction { get; set; }

        /// <summary>
        /// [Optional] The account type of the master user: MAIN, TRADE, MARGIN or CONTRACT
        /// </summary>
        [JsonProperty("accountType", NullValueHandling = NullValueHandling.Ignore)]
        public AccountType? AccountType { get; set; }

        /// <summary>
        /// [Optional] The account type of the sub user: MAIN, TRADE, MARGIN or CONTRACT, default is MAIN.
        /// </summary>
        [JsonProperty("subAccountType", NullValueHandling = NullValueHandling.Ignore)]
        public AccountType? SubAccountType { get; set; }

        /// <summary>
        /// the user ID of a sub-account.
        /// </summary>
        [JsonProperty("subUserId")]
        public string SubUserId { get; set; }


    }
}
