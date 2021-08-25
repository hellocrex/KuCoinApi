using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.KuCoinApi.Contracts.Enums;
using PoissonSoft.KuCoinApi.Contracts.Trade.Request;

namespace PoissonSoft.KuCoinApi.Contracts.Trade.Response
{
    public class Order : ReqOrderList
    {
        /// <summary>
        /// Order ID, the ID of an order.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Operation type: DEAL
        /// </summary>
        [JsonProperty("opType")]
        public string OpType { get; set; }

        /// <summary>
        /// order price
        /// </summary>
        [JsonProperty("price")]
        public decimal Price { get; set; }

        /// <summary>
        /// order quantity
        /// </summary>
        [JsonProperty("size")]
        public string Size { get; set; }

        /// <summary>
        /// order funds
        /// </summary>
        [JsonProperty("funds")]
        public string Funds { get; set; }

        /// <summary>
        /// executed size of funds
        /// </summary>
        [JsonProperty("dealFunds")]
        public string DealFunds { get; set; }

        /// <summary>
        /// executed quantity
        /// </summary>
        [JsonProperty("dealSize")]
        public string DealSize { get; set; }

        /// <summary>
        /// fee
        /// </summary>
        [JsonProperty("fee")]
        public string Fee { get; set; }

        /// <summary>
        /// charge fee currency
        /// </summary>
        [JsonProperty("feeCurrency")]
        public string FeeCurrency { get; set; }

        /// <summary>
        /// self trade prevention,include CN,CO,DC,CB
        /// </summary>
        [JsonProperty("stp")]
        public STP STP { get; set; }

        /// <summary>
        /// stop type, include entry and loss
        /// </summary>
        [JsonProperty("stop")]
        public string Stop { get; set; }
        
        /// <summary>
        /// stop order is triggered or not
        /// </summary>
        [JsonProperty("stopTriggered")]
        public string StopTriggered { get; set; }

        /// <summary>
        /// stop price
        /// </summary>
        [JsonProperty("stopPrice")]
        public decimal StopPrice { get; set; }

        /// <summary>
        /// time InForce,include GTC,GTT,IOC,FOK
        /// </summary>
        [JsonProperty("timeInForce")]
        public TimeInForce TimeInForce { get; set; }

        /// <summary>
        /// postOnly
        /// </summary>
        [JsonProperty("postOnly")]
        public string PostOnly { get; set; }

        /// <summary>
        /// hidden order
        /// </summary>
        [JsonProperty("hidden")]
        public string Hidden { get; set; }

        /// <summary>
        /// iceberg order
        /// </summary>
        [JsonProperty("iceberg")]
        public string Iceberg { get; set; }

        /// <summary>
        /// displayed quantity for iceberg order
        /// </summary>
        [JsonProperty("visibleSize")]
        public decimal VisibleSize { get; set; }

        /// <summary>
        /// cancel orders time，requires timeInForce to be GTT
        /// </summary>
        [JsonProperty("cancelAfter")]
        public long CancelAfter { get; set; }

        /// <summary>
        /// order source
        /// </summary>
        [JsonProperty("channel")]
        public string Channel { get; set; }

        /// <summary>
        /// user-entered order unique mark
        /// </summary>
        [JsonProperty("clientOid")]
        public string ClientOid { get; set; }

        /// <summary>
        /// remark
        /// </summary>
        [JsonProperty("remark")]
        public string Comment { get; set; }

        /// <summary>
        /// tag order source
        /// </summary>
        [JsonProperty("tags")]
        public string Tags { get; set; }

        /// <summary>
        /// order status, true and false. If true, the order is active, if false, the order is fillled or cancelled
        /// </summary>
        [JsonProperty("isActive")]
        public string isActive { get; set; }

        /// <summary>
        /// order cancellation transaction record
        /// </summary>
        [JsonProperty("cancelExist")]
        public string CancelExist { get; set; }

        /// <summary>
        /// create time
        /// </summary>
        [JsonProperty("createdAt")]
        public long CreatedAt { get; set; }
    }
}
