using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PoissonSoft.KuCoinApi.Contracts.Trade.Response
{
    public class NewMarginOrder
    {
        /// <summary>
        /// The ID of the order
        /// </summary>
        [JsonProperty("orderId")]
        public string OrderId { get; set; }

        /// <summary>
        /// Borrowed amount. The field is returned only after placing the order under the mode of Auto-Borrow.
        /// </summary>
        [JsonProperty("borrowSize")]
        public float BorrowSize { get; set; }

        /// <summary>
        /// ID of the borrowing response. The field is returned only after placing the order under the mode of Auto-Borrow.
        /// </summary>
        [JsonProperty("loanApplyId")]
        public string LoanApplyId { get; set; }
    }
}
