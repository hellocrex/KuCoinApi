using Newtonsoft.Json;

namespace PoissonSoft.KuCoinApi.Contracts.DataStream
{
    /// <summary>
    /// Response to Create Listen Key Request
    /// </summary>
    public class CreateListenKeyResponse
    {
        /// <summary>
        /// System error codes
        /// </summary>
        [JsonProperty("code")]
        public int SystemCode { get; set; }
        /// <summary>
        /// Listen Key
        /// </summary>
        [JsonProperty("data")]
        public ConnectionInfo Data { get; set; }
    }
}
