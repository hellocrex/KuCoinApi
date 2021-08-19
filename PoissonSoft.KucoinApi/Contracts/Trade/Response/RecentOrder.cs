using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KuCoinApi.Contracts.Trade.Response
{
    /// <summary>
    /// Запрос возвращает 1000 выставленных ордеров за последние 24ч.
    /// </summary>
    public class RecentOrder : Order
    {
        /// <summary>
        /// symbol
        /// </summary>
        [JsonProperty("orderId")]
        public string OrderId { get; set; }

    }
}
