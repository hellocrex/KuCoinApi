using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.KuСoinApi.Contracts.User.Account.Response;
using PoissonSoft.KuСoinApi.Contracts.User.Response;

namespace PoissonSoft.KuСoinApi.Contracts.User
{
    public class FeeList
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
        public Fee[] Data { get; set; }
    }
}
