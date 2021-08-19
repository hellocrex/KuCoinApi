using Newtonsoft.Json;
using PoissonSoft.KuCoinApi.Contracts.User.Account.Response;

namespace PoissonSoft.KuCoinApi.Contracts.User
{
    public class AccountsList
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
        public AccountInfo[] Data { get; set; }
    }
}
