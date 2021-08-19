using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.KuCoinApi.Contracts.Enums;

namespace PoissonSoft.KuCoinApi.Contracts.PublicWebSocket.Response
{
    public class StopOrderEventData
    {
        /// <summary>
        /// Currency
        /// </summary>
        [JsonProperty("createdAt")]
        public long CreatedAt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("orderId")]
        public string OrderId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("orderPrice")]
        public decimal OrderPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("orderType")]
        public OrderType OrderType { get; set; }

        /// <summary>
        /// side
        /// </summary>
        [JsonProperty("side")]
        public OrderSide Side { get; set; }

        /// <summary>
        /// Size
        /// </summary>
        [JsonProperty("size")]
        public  int Size { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("stop")]
        public string Stop { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("stopPrice")]
        public decimal StopPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("tradeType")]
        public TradeType TradeType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("triggerSuccess")]
        public bool triggerSuccess { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("ts")]
        public long Time { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
