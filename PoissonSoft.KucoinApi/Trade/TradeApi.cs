using System.Net.Http;
using NLog;
using PoissonSoft.KuCoinApi.Contracts.MarketData.Request;
using PoissonSoft.KuCoinApi.Contracts.Trade;
using PoissonSoft.KuCoinApi.Contracts.Trade.Request;
using PoissonSoft.KuCoinApi.Contracts.Trade.Response;
using PoissonSoft.KuCoinApi.Transport;
using PoissonSoft.KuCoinApi.Transport.Rest;
using RespOrdersList = PoissonSoft.KuCoinApi.Contracts.Trade.RespOrdersList;

namespace PoissonSoft.KuCoinApi.Trade
{
    internal sealed class TradeApi : ITradeApi
    {
        private readonly KuCoinApiClient apiClient;
        private readonly RestClient client;

        public TradeApi(KuCoinApiClient apiClient, KuCoinApiClientCredentials credentials, ILogger logger)
        {

            this.apiClient = apiClient;// ?? throw new ArgumentNullException(nameof(apiClient));
            client = new RestClient(logger, "https://openapi-sandbox.kucoin.com/api/v1",
             //   client = new RestClient(logger, "https://api.kucoin.com/api/v1",
                new[] { EndpointSecurityType.Trade }, credentials, this.apiClient.Throttler);
        }

        #region Orders

        public RespOrderId NewOrder(ReqNewOrder request, bool isHighPriority)
        {
            return client.MakeRequest<RespOrderId>(new RequestParameters(HttpMethod.Post, "orders", 45 / 3)
            {
                IsHighPriority = isHighPriority,
                IsOrderRequest = true,
                PassAllParametersInQueryString = true,
                Parameters = RequestParameters.GenerateParametersFromObject(request)
            });
        }

        public RespMarginOrderInfo NewMarginOrder(ReqNewMarginOrder request)
        {
            return client.MakeRequest<RespMarginOrderInfo>(new RequestParameters(HttpMethod.Post, "margin/order", 0)
            {
                IsOrderRequest = true,
                PassAllParametersInQueryString = true,
                Parameters = RequestParameters.GenerateParametersFromObject(request)
            });
        }
        // 
        public RespBulkOrdersInfo PlaceBulkOrders(ReqBulkOrder request)
        {
            return client.MakeRequest<RespBulkOrdersInfo>(new RequestParameters(HttpMethod.Post, "orders/multi", 3 / 3)
            {
                IsOrderRequest = true,
                PassAllParametersInQueryString = true,
                Parameters = RequestParameters.GenerateParametersFromObject(request)
            });
        }

        public RespCancelAnOrder CancelOrder(SpecialBuildQuery request)
        {
            return client.MakeRequest<RespCancelAnOrder>(new RequestParameters(HttpMethod.Delete, "orders", 60 / 3)
            {
                IsOrderRequest = true,
                PassAllParametersInQueryString = true,
                Parameters = RequestParameters.GenerateParametersFromObject(request)
            });
        }

        public RespCancelByClientOid CancelSingleOrderByClientOid(SpecialBuildQuery request)
        {
            return client.MakeRequest<RespCancelByClientOid>(new RequestParameters(HttpMethod.Delete, "order/client-order", 0)
            {
                IsOrderRequest = true,
                PassAllParametersInQueryString = true,
                Parameters = RequestParameters.GenerateParametersFromObject(request)
            });
        }

        public RespCancelAnOrder CancelAllOrders(ReqCancelOrders request)
        {
            return client.MakeRequest<RespCancelAnOrder>(new RequestParameters(HttpMethod.Delete, "orders", 3 / 3)
            {
                IsOrderRequest = true,
                PassAllParametersInQueryString = true,
                Parameters = RequestParameters.GenerateParametersFromObject(request)
            });
        }

        public PageOrdersList ListOrders(ReqOrderList request)
        {
            return client.MakeRequest<PageOrdersList>(
                new RequestParameters(HttpMethod.Get, "orders", 30 / 3)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public RespHistoricalOrdersList GetHistoricalOrdersList(ReqHistoricalOrder request)
        {
            return client.MakeRequest<RespHistoricalOrdersList>(
                new RequestParameters(HttpMethod.Get, "hist-orders", 0)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public RespOrdersList RecentOrders()
        {
            return client.MakeRequest<RespOrdersList>(new RequestParameters(HttpMethod.Get, "limit/orders", 0));
        }

        public RespOrderList GetOrder(SpecialBuildQuery request)
        {
            return client.MakeRequest<RespOrderList>(
                new RequestParameters(HttpMethod.Get, "orders", 0)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public RespOrderList GetSingleActiveOrderByClientOid(SpecialBuildQuery request)
        {
            return client.MakeRequest<RespOrderList>(
                new RequestParameters(HttpMethod.Get, "order/client-order", 0)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        #endregion

        #region Fills

        public RespFillsList ListFills(ReqFills request)
        {
            return client.MakeRequest<RespFillsList>(
                new RequestParameters(HttpMethod.Get, "fills", 9 / 3)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public RespFillsList RecentFills()
        {
            return client.MakeRequest<RespFillsList>(new RequestParameters(HttpMethod.Get, "limit/fills", 0));
        }

        #endregion


        #region Stop Order
        public RespOrderId PlaceNewStopOrder(ReqNewOrder request)
        {
            return client.MakeRequest<RespOrderId>(new RequestParameters(HttpMethod.Post, "stop-order", 0)
            {
                IsOrderRequest = true,
                PassAllParametersInQueryString = true,
                Parameters = RequestParameters.GenerateParametersFromObject(request)
            });
        }

        public RespCancelAnOrder CancelStopOrder(SpecialBuildQuery request)
        {
            return client.MakeRequest<RespCancelAnOrder>(new RequestParameters(HttpMethod.Delete, "stop-order", 0)
            {
                IsOrderRequest = true,
                PassAllParametersInQueryString = true,
                Parameters = RequestParameters.GenerateParametersFromObject(request)
            });
        }
        // множественное удаление: orderId,orderId
        public RespCancelAnOrder CancelStopOrders(ReqCancelOrders request)
        {
            return client.MakeRequest<RespCancelAnOrder>(new RequestParameters(HttpMethod.Delete, "stop-order/cancel", 0)
            {
                IsOrderRequest = true,
                PassAllParametersInQueryString = true,
                Parameters = RequestParameters.GenerateParametersFromObject(request)
            });
        }

        public RespStopOrderInfo GetStopSingleOrderInfo(SpecialBuildQuery request)
        {
            return client.MakeRequest<RespStopOrderInfo>(new RequestParameters(HttpMethod.Get, "stop-order", 0)
            {
                IsOrderRequest = true,
                PassAllParametersInQueryString = true,
                Parameters = RequestParameters.GenerateParametersFromObject(request)
            });
        }
        //Many select
        public RespStopOrdersList ListStopOrders(ReqListStopOrder request)
        {
            return client.MakeRequest<RespStopOrdersList>(new RequestParameters(HttpMethod.Get, "stop-order", 0)
            {
                IsOrderRequest = true,
                PassAllParametersInQueryString = true,
                Parameters = RequestParameters.GenerateParametersFromObject(request)
            });
        }

        public RespStopOrdersInfo GetStopSingleOrderByClientOId(ReqOrderByClientOId request)
        {
            return client.MakeRequest<RespStopOrdersInfo>(new RequestParameters(HttpMethod.Get, "stop-order/queryOrderByClientOid", 0)
            {
                IsOrderRequest = true,
                PassAllParametersInQueryString = true,
                Parameters = RequestParameters.GenerateParametersFromObject(request)
            });
        }

        public RespCancelByClientOid CancelStopSingleOrderByClientOId(ReqOrderByClientOId request)
        {
            return client.MakeRequest<RespCancelByClientOid>(new RequestParameters(HttpMethod.Delete, "stop-order/cancelOrderByClientOid", 0)
            {
                IsOrderRequest = true,
                PassAllParametersInQueryString = true,
                Parameters = RequestParameters.GenerateParametersFromObject(request)
            });
        }
        #endregion

    }
}
