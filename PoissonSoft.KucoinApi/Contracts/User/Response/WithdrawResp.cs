using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KuCoinApi.Contracts.User.Response
{
    public class WithdrawResp
    {
        /// <summary>
        /// Код http ответа
        /// </summary>
        [JsonProperty("withdrawalId")]
        public int WithdrawalId { get; set; }
    }
}
