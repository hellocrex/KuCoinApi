using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.KuCoinApi.Contracts.User.Response;

namespace PoissonSoft.KuCoinApi.Contracts.User
{
    public class SubAccountsList
    {
        /// <summary>
        /// Код http ответа
        /// </summary>
        [JsonProperty("code")]
        public int SystemCode { get; set; }

        /// <summary>
        /// Account info
        /// </summary>
        [JsonProperty("data")]
        public SubInfo[] Data { get; set; }
    }
}
