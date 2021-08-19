using Newtonsoft.Json;
using PoissonSoft.KuCoinApi.Contracts.User.Response;

namespace PoissonSoft.KuCoinApi.Contracts.User
{
    public class WithdrawQuotaInfo
    {
        /// <summary>
        /// Код http ответа
        /// </summary>
        [JsonProperty("code")]
        public int SystemCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("data")]
        public WithdrawQuotasResp Data { get; set; }
    }
}
