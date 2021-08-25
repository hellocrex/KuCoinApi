using PoissonSoft.KuCoinApi.Contracts.MarketData.Request;
using PoissonSoft.KuCoinApi.Contracts.Trade;
using PoissonSoft.KuCoinApi.Contracts.Trade.Request;
using PoissonSoft.KuCoinApi.Contracts.Trade.Response;
using PoissonSoft.KuCoinApi.Contracts.User.Response;
using OrdersList = PoissonSoft.KuCoinApi.Contracts.Trade.OrdersList;

namespace PoissonSoft.KuCoinApi.Trade
{
    public interface ITradeApi
    {
        OrderIdResp NewOrder(NewOrderRequest request, bool isHighPriority);
        FillsList ListFills(FillsReq request);
        OrdersList RecentOrders();
        PageOrdersList ListOrders(OrderReq request);
        FillsList RecentFills();
        OrderList GetOrder(SpecialBuildQuery request);
        OrderList GetSingleActiveOrderByClientOid(SpecialBuildQuery request);
        NewOrderRequest NewMarginOrder(NewMargin request);
        NewOrderRequest PlaceBulkOrders(BulkOrder request);
        NewOrderRequest CancelOrder(SpecialBuildQuery request);
        NewOrderRequest CancelSingleOrderByClientOid(SpecialBuildQuery request);
        CancelAllOrders CancelAllOrders(CancelOrders request);
        HistoricalOrder GetHistoricalOrdersList(HistoricalOrderReq request);
        NewOrderRequest PlaceNewStopOrder(NewStopOrder request);
        NewOrderRequest CancelStopOrder(SpecialBuildQuery request);
        NewOrderRequest CancelStopOrders(CancelStopOrder request);
        NewOrderRequest GetStopSingleOrderInfo(SpecialBuildQuery request);
        NewOrderRequest ListStopOrders(ListStopOrder request);
        NewOrderRequest GetStopSingleOrderByClientOId(SingleOrderByClientOId request);
        NewOrderRequest CancelStopSingleOrderByClientOId(SingleOrderByClientOId request);
    }
}
