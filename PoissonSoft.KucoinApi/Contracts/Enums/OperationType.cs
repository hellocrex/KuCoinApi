using System;
using System.Collections.Generic;
using System.Text;

namespace PoissonSoft.KuCoinApi.Contracts.Enums
{
    public enum OperationType
    {
        /// <summary>
        /// is pending order
        /// </summary>
        DEAL = 0,

        /// <summary>
        /// is cancel order
        /// </summary>
        CANCEL = 1,
    }
}
