using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.KuCoinApi.Contracts.MarketData.Response;

namespace PoissonSoft.KuCoinApi.Contracts.User.Response
{
    public class Account
    {
        /// <summary>
        /// Код http ответа
        /// </summary>
        [JsonProperty("code")]
        public int SystemCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("id")]
        public long TradeHistoryCoin { get; set; }
    }
}
