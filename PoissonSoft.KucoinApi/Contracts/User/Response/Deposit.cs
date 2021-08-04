using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KuСoinApi.Contracts.User.Response
{
    public class Deposit
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
        /// The chain name of currency
        /// </summary>
        [JsonProperty("chain")]
        public string Chain { get; set; }

        /// <summary>
        /// The token contract address
        /// </summary>
        [JsonProperty("contractAddress")]
        public string ContractAddress { get; set; }
    }
}
