using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KuCoinApi.Contracts.User.Response
{
    public class Fee
    {
        /// <summary>
        /// Deposit address
        /// </summary>
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        /// <summary>
        /// Deposit address
        /// </summary>
        [JsonProperty("takerFeeRate")]
        public string TakerFeeRate { get; set; }

        /// <summary>
        /// Address remark. If there’s no remark, it is empty
        /// </summary>
        [JsonProperty("makerFeeRate")]
        public string MakerFeeRate { get; set; }
    }
}
