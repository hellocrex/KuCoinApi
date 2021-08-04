using PoissonSoft.KuСoinApi.Contracts.MarketData.Request;
using PoissonSoft.KuСoinApi.Contracts.Trade;
using PoissonSoft.KuСoinApi.Contracts.Trade.Request;
using PoissonSoft.KuСoinApi.Contracts.Trade.Response;
using PoissonSoft.KuСoinApi.Contracts.User.Response;
using OrdersList = PoissonSoft.KuСoinApi.Contracts.Trade.OrdersList;

namespace PoissonSoft.KuСoinApi.Trade
{
    public interface ITradeApi
    {
        NewOrderRequest NewOrder(NewOrderRequest request, bool isHighPriority);
        FillsList ListFills(FillsReq request);
        OrdersList RecentOrders();
        Deposit ListOrders(OrderReq request);
        FillsList RecentFills();
        OrderList GetOrder(Url request);
        OrderList GetSingleActiveOrderByClientOid(Url request);
        NewOrderRequest NewMarginOrder(MarginReq request);
        NewOrderRequest PlaceBulkOrders(BulkOrder request);
        NewOrderRequest CancelAnOrder(Url request);
        NewOrderRequest CancelSingleOrderByClientOid(Url request);
        CancelAllOrders CancelAllOrders(CancelOrders request);
        Deposit GetHistoricalOrdersList(HistoricalOrderReq request);
        NewOrderRequest PlaceNewStopOrder(NewStopOrder request);
        NewOrderRequest CancelStopOrder(Url request);
        NewOrderRequest CancelStopOrders(CancelStopOrder request);
        NewOrderRequest GetStopSingleOrderInfo(Url request);
        NewOrderRequest ListStopOrders(ListStopOrder request);
        NewOrderRequest GetStopSingleOrderByClientOId(SingleOrderByClientOId request);
        NewOrderRequest CancelStopSingleOrderByClientOId(SingleOrderByClientOId request);
    }
}
