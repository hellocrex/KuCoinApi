using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KuСoinApi.Contracts.User.Response
{
    public class SubInfo
    {
        /// <summary>
        /// The user ID of a sub-user
        /// </summary>
        [JsonProperty("userId")]
        public string UserId { get; set; }

        /// <summary>
        /// The user ID of a sub-user
        /// </summary>
        [JsonProperty("subName")]
        public string SubName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("type")]
        public int Type { get; set; }

        /// <summary>
        /// The user ID of a sub-user
        /// </summary>
        [JsonProperty("remarks")]
        public string Remarks { get; set; }
    }
}
