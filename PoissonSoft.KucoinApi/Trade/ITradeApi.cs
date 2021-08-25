using PoissonSoft.KuCoinApi.Contracts.MarketData.Request;
using PoissonSoft.KuCoinApi.Contracts.Trade;
using PoissonSoft.KuCoinApi.Contracts.Trade.Request;
using PoissonSoft.KuCoinApi.Contracts.Trade.Response;
using PoissonSoft.KuCoinApi.Contracts.User.Response;
using RespOrdersList = PoissonSoft.KuCoinApi.Contracts.Trade.RespOrdersList;

namespace PoissonSoft.KuCoinApi.Trade
{
    public interface ITradeApi
    {
        RespOrderId NewOrder(ReqNewOrder request, bool isHighPriority);
        RespFillsList ListFills(ReqFills request);
        RespOrdersList RecentOrders();
        PageOrdersList ListOrders(ReqOrderList request);
        RespFillsList RecentFills();
        RespOrderList GetOrder(SpecialBuildQuery request);
        RespOrderList GetSingleActiveOrderByClientOid(SpecialBuildQuery request);
        RespMarginOrderInfo NewMarginOrder(ReqNewMarginOrder request);
        RespBulkOrdersInfo PlaceBulkOrders(ReqBulkOrder request);
        RespCancelAnOrder CancelOrder(SpecialBuildQuery request);
        RespCancelByClientOid CancelSingleOrderByClientOid(SpecialBuildQuery request);
        RespCancelAnOrder CancelAllOrders(ReqCancelOrders request);
        RespHistoricalOrdersList GetHistoricalOrdersList(ReqHistoricalOrder request);
        RespOrderId PlaceNewStopOrder(ReqNewOrder request);
        RespCancelAnOrder CancelStopOrder(SpecialBuildQuery request);
        RespCancelAnOrder CancelStopOrders(ReqCancelOrders request);
        RespStopOrderInfo GetStopSingleOrderInfo(SpecialBuildQuery request);
        RespStopOrdersList ListStopOrders(ReqListStopOrder request);
        RespStopOrdersInfo GetStopSingleOrderByClientOId(ReqOrderByClientOId request);
        RespCancelByClientOid CancelStopSingleOrderByClientOId(ReqOrderByClientOId request);
    }
}
