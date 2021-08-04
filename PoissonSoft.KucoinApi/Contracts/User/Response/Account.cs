using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.KuСoinApi.Contracts.MarketData.Response;

namespace PoissonSoft.KuСoinApi.Contracts.User.Response
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
