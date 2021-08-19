using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.KuCoinApi.Contracts.Enums;

namespace PoissonSoft.KuCoinApi.Contracts.Trade.Request
{
    public class OrderParameters
    {
        // дополнительные параметры, зависящие от типа ордера
        //LIMIT ORDER PARAMETERS
        /// <summary>
        /// price per base currency
        /// </summary>
        [JsonProperty("price")]
        public string Price { get; set; }

        /// <summary>
        /// amount of base currency to buy or sell
        /// </summary>
        [JsonProperty("size")]
        public string size { get; set; }

        /// <summary>
        /// [Optional] GTC, GTT, IOC, or FOK (default is GTC)
        /// </summary>
        [JsonProperty("timeInForce")]
        public TimeInForce TimeInForce { get; set; }

        /// <summary>
        /// [Optional] cancel after n seconds, requires timeInForce to be GTT
        /// </summary>
        [JsonProperty("cancelAfter")]
        public long CancelAfter { get; set; }

        /// <summary>
        /// [Optional] Post only flag, invalid when timeInForce is IOC or FOK
        /// </summary>
        [JsonProperty("postOnly")]
        public bool PostOnly { get; set; }

        /// <summary>
        /// [Optional] Order will not be displayed in the order book
        /// </summary>
        [JsonProperty("hidden")]
        public bool Hidden { get; set; }

        /// <summary>
        /// [Optional] Only aportion of the order is displayed in the order book
        /// </summary>
        [JsonProperty("iceberg")]
        public bool Iceberg { get; set; }

        /// <summary>
        /// [Optional] The maximum visible size of an iceberg order
        /// </summary>
        [JsonProperty("visibleSize")]
        public string VisibleSize { get; set; }

        // MARKET ORDER PARAMETERS
        /// <summary>
        /// [Optional] The maximum visible size of an iceberg order
        /// </summary>
        [JsonProperty("funds")]
        public string Funds { get; set; }
    }
}
