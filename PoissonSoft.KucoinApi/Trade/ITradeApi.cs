using System;
using System.Collections.Generic;
using System.Text;
using PoissonSoft.KuСoinApi.Contracts.Trade;

namespace PoissonSoft.KuСoinApi.Trade
{
    public interface ITradeApi
    {
        NewOrderRequest NewOrder(NewOrderRequest request, bool isHighPriority);
    }
}
