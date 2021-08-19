using System.Net.Http;
using NLog;
using PoissonSoft.KuCoinApi.Contracts.MarketData.Request;
using PoissonSoft.KuCoinApi.Contracts.Trade;
using PoissonSoft.KuCoinApi.Contracts.Trade.Request;
using PoissonSoft.KuCoinApi.Contracts.Trade.Response;
using PoissonSoft.KuCoinApi.Transport;
using PoissonSoft.KuCoinApi.Transport.Rest;
using OrdersList = PoissonSoft.KuCoinApi.Contracts.Trade.OrdersList;

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

        public OrderIdResp NewOrder(NewOrderRequest request, bool isHighPriority)
        {
            return client.MakeRequest<OrderIdResp>(new RequestParameters(HttpMethod.Post, "orders", 45 / 3)
            {
                IsHighPriority = isHighPriority,
                IsOrderRequest = true,
                PassAllParametersInQueryString = true,
                Parameters = RequestParameters.GenerateParametersFromObject(request)
            });
        }

        public NewOrderRequest NewMarginOrder(NewMargin request)
        {
            return client.MakeRequest<NewOrderRequest>(new RequestParameters(HttpMethod.Post, "margin/order", 0)
            {
                IsOrderRequest = true,
                PassAllParametersInQueryString = true,
                Parameters = RequestParameters.GenerateParametersFromObject(request)
            });
        }

        public NewOrderRequest PlaceBulkOrders(BulkOrder request)
        {
            return client.MakeRequest<NewOrderRequest>(new RequestParameters(HttpMethod.Post, "orders/multi", 3 / 3)
            {
                IsOrderRequest = true,
                PassAllParametersInQueryString = true,
                Parameters = RequestParameters.GenerateParametersFromObject(request)
            });
        }

        public NewOrderRequest CancelOrder(Url request)
        {
            return client.MakeRequest<NewOrderRequest>(new RequestParameters(HttpMethod.Delete, "orders", 60 / 3)
            {
                IsOrderRequest = true,
                PassAllParametersInQueryString = true,
                Parameters = RequestParameters.GenerateParametersFromObject(request)
            });
        }

        public NewOrderRequest CancelSingleOrderByClientOid(Url request)
        {
            return client.MakeRequest<NewOrderRequest>(new RequestParameters(HttpMethod.Delete, "order/client-order", 0)
            {
                IsOrderRequest = true,
                PassAllParametersInQueryString = true,
                Parameters = RequestParameters.GenerateParametersFromObject(request)
            });
        }

        public CancelAllOrders CancelAllOrders(CancelOrders request)
        {
            return client.MakeRequest<CancelAllOrders>(new RequestParameters(HttpMethod.Delete, "orders", 3 / 3)
            {
                IsOrderRequest = true,
                PassAllParametersInQueryString = true,
                Parameters = RequestParameters.GenerateParametersFromObject(request)
            });
        }

        public PageOrdersList ListOrders(OrderReq request)
        {
            return client.MakeRequest<PageOrdersList>(
                new RequestParameters(HttpMethod.Get, "orders", 30 / 3)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public HistoricalOrder GetHistoricalOrdersList(HistoricalOrderReq request)
        {
            return client.MakeRequest<HistoricalOrder>(
                new RequestParameters(HttpMethod.Get, "hist-orders", 0)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public OrdersList RecentOrders()
        {
            return client.MakeRequest<OrdersList>(new RequestParameters(HttpMethod.Get, "limit/orders", 0));
        }

        public OrderList GetOrder(Url request)
        {
            return client.MakeRequest<OrderList>(
                new RequestParameters(HttpMethod.Get, "orders", 0)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public OrderList GetSingleActiveOrderByClientOid(Url request)
        {
            return client.MakeRequest<OrderList>(
                new RequestParameters(HttpMethod.Get, "order/client-order", 0)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        #endregion

        #region Fills

        public FillsList ListFills(FillsReq request)
        {
            return client.MakeRequest<FillsList>(
                new RequestParameters(HttpMethod.Get, "fills", 9 / 3)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public FillsList RecentFills()
        {
            return client.MakeRequest<FillsList>(new RequestParameters(HttpMethod.Get, "limit/fills", 0));
        }

        #endregion


        #region Stop Order
        public NewOrderRequest PlaceNewStopOrder(NewStopOrder request)
        {
            return client.MakeRequest<NewOrderRequest>(new RequestParameters(HttpMethod.Post, "stop-order", 0)
            {
                IsOrderRequest = true,
                PassAllParametersInQueryString = true,
                Parameters = RequestParameters.GenerateParametersFromObject(request)
            });
        }

        public NewOrderRequest CancelStopOrder(Url request)
        {
            return client.MakeRequest<NewOrderRequest>(new RequestParameters(HttpMethod.Delete, "stop-order", 0)
            {
                IsOrderRequest = true,
                PassAllParametersInQueryString = true,
                Parameters = RequestParameters.GenerateParametersFromObject(request)
            });
        }

        public NewOrderRequest CancelStopOrders(CancelStopOrder request)
        {
            return client.MakeRequest<NewOrderRequest>(new RequestParameters(HttpMethod.Delete, "stop-order/cancel", 0)
            {
                IsOrderRequest = true,
                PassAllParametersInQueryString = true,
                Parameters = RequestParameters.GenerateParametersFromObject(request)
            });
        }

        public NewOrderRequest GetStopSingleOrderInfo(Url request)
        {
            return client.MakeRequest<NewOrderRequest>(new RequestParameters(HttpMethod.Get, "stop-order", 0)
            {
                IsOrderRequest = true,
                PassAllParametersInQueryString = true,
                Parameters = RequestParameters.GenerateParametersFromObject(request)
            });
        }

        public NewOrderRequest ListStopOrders(ListStopOrder request)
        {
            return client.MakeRequest<NewOrderRequest>(new RequestParameters(HttpMethod.Get, "stop-order", 0)
            {
                IsOrderRequest = true,
                PassAllParametersInQueryString = true,
                Parameters = RequestParameters.GenerateParametersFromObject(request)
            });
        }

        public NewOrderRequest GetStopSingleOrderByClientOId(SingleOrderByClientOId request)
        {
            return client.MakeRequest<NewOrderRequest>(new RequestParameters(HttpMethod.Get, "stop-order/queryOrderByClientOid", 0)
            {
                IsOrderRequest = true,
                PassAllParametersInQueryString = true,
                Parameters = RequestParameters.GenerateParametersFromObject(request)
            });
        }

        public NewOrderRequest CancelStopSingleOrderByClientOId(SingleOrderByClientOId request)
        {
            return client.MakeRequest<NewOrderRequest>(new RequestParameters(HttpMethod.Delete, "stop-order/cancelOrderByClientOid", 0)
            {
                IsOrderRequest = true,
                PassAllParametersInQueryString = true,
                Parameters = RequestParameters.GenerateParametersFromObject(request)
            });
        }
        #endregion

    }
}
