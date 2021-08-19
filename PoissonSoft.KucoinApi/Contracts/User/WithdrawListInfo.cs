using Newtonsoft.Json;
using PoissonSoft.KuCoinApi.Contracts.User.Response;

namespace PoissonSoft.KuCoinApi.Contracts.User
{
    public class WithdrawListInfo
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
        public WithdrawPage Data { get; set; }
    }

    public class WithdrawPage
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("currentPage")]
        public int CurrentPage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("pageSize")]
        public int PageSize { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("totalNum")]
        public int TotalNum { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("totalPage")]
        public int TotalPage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("items")]
        public WithdrawalsListResp[] Items { get; set; }
    }

}
