using System.Net.Http;
using NLog;
using PoissonSoft.KuСoinApi.Contracts;
using PoissonSoft.KuСoinApi.Contracts.Enums;
using PoissonSoft.KuСoinApi.Contracts.MarketData.Request;
using PoissonSoft.KuСoinApi.Contracts.Trade;
using PoissonSoft.KuСoinApi.Contracts.Trade.Request;
using PoissonSoft.KuСoinApi.Contracts.Trade.Response;
using PoissonSoft.KuСoinApi.Contracts.User.Response;
using PoissonSoft.KuСoinApi.Transport;
using PoissonSoft.KuСoinApi.Transport.Rest;
using OrdersList = PoissonSoft.KuСoinApi.Contracts.Trade.OrdersList;

namespace PoissonSoft.KuСoinApi.Trade
{
    internal sealed class TradeApi : ITradeApi
    {
        private readonly KuСoinApiClient apiClient;
        private readonly RestClient client;

        public TradeApi(KuСoinApiClient apiClient, KuСoinApiClientCredentials credentials, ILogger logger)
        {

            this.apiClient = apiClient;// ?? throw new ArgumentNullException(nameof(apiClient));
            client = new RestClient(logger, "https://openapi-sandbox.kucoin.com/api/v1",
               // client = new RestClient(logger, "https://api.kucoin.com/api/v1",
                new[] { EndpointSecurityType.Trade }, credentials);
            //,this.apiClient.Throttler);
        }
        
        public OrderIdResp NewOrder(NewOrderRequest request, bool isHighPriority)
        {
            return client.MakeRequest<OrderIdResp>(new RequestParameters(HttpMethod.Post, "orders", 1)
            {
                IsHighPriority = isHighPriority,
                IsOrderRequest = true,
                PassAllParametersInQueryString = true,
                Parameters = RequestParameters.GenerateParametersFromObject(request)
            });
        }

        public NewOrderRequest NewMarginOrder(NewMargin request)
        {
            return client.MakeRequest<NewOrderRequest>(new RequestParameters(HttpMethod.Post, "margin/order", 1)
            {
                IsOrderRequest = true,
                PassAllParametersInQueryString = true,
                Parameters = RequestParameters.GenerateParametersFromObject(request)
            });
        }

        public NewOrderRequest PlaceBulkOrders(BulkOrder request)
        {
            return client.MakeRequest<NewOrderRequest>(new RequestParameters(HttpMethod.Post, "orders/multi", 1)
            {
                IsOrderRequest = true,
                PassAllParametersInQueryString = true,
                Parameters = RequestParameters.GenerateParametersFromObject(request)
            });
        }

        public NewOrderRequest CancelAnOrder(Url request)
        {
            return client.MakeRequest<NewOrderRequest>(new RequestParameters(HttpMethod.Delete, "orders", 1)
            {
                IsOrderRequest = true,
                PassAllParametersInQueryString = true,
                Parameters = RequestParameters.GenerateParametersFromObject(request)
            });
        }

        public NewOrderRequest CancelSingleOrderByClientOid(Url request)
        {
            return client.MakeRequest<NewOrderRequest>(new RequestParameters(HttpMethod.Delete, "order/client-order", 1)
            {
                IsOrderRequest = true,
                PassAllParametersInQueryString = true,
                Parameters = RequestParameters.GenerateParametersFromObject(request)
            });
        }

        public CancelAllOrders CancelAllOrders(CancelOrders request)
        {
            return client.MakeRequest<CancelAllOrders>(new RequestParameters(HttpMethod.Delete, "orders", 1)
            {
                IsOrderRequest = true,
                PassAllParametersInQueryString = true,
                Parameters = RequestParameters.GenerateParametersFromObject(request)
            });
        }

        public PageOrdersList ListOrders(OrderReq request)
        {
            return client.MakeRequest<PageOrdersList>(
                new RequestParameters(HttpMethod.Get, "orders", 1)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }
        
        public HistoricalOrder GetHistoricalOrdersList(HistoricalOrderReq request)
        {
            return client.MakeRequest<HistoricalOrder>(
                new RequestParameters(HttpMethod.Get, "hist-orders", 1)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public OrdersList RecentOrders()
        {
            return client.MakeRequest<OrdersList>(new RequestParameters(HttpMethod.Get, "limit/orders", 1));
        }

        public OrderList GetOrder(Url request)
        {
            return client.MakeRequest<OrderList>(
                new RequestParameters(HttpMethod.Get, "orders", 1)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public OrderList GetSingleActiveOrderByClientOid(Url request)
        {
            return client.MakeRequest<OrderList>(
                new RequestParameters(HttpMethod.Get, "order/client-order", 1)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public FillsList ListFills(FillsReq request)
        {
            return client.MakeRequest<FillsList>(
                new RequestParameters(HttpMethod.Get, "fills", 1)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public FillsList RecentFills()
        {
            return client.MakeRequest<FillsList>(new RequestParameters(HttpMethod.Get, "limit/fills", 1));
        }

        public NewOrderRequest PlaceNewStopOrder(NewStopOrder request)
        {
            return client.MakeRequest<NewOrderRequest>(new RequestParameters(HttpMethod.Post, "stop-order", 1)
            {
                IsOrderRequest = true,
                PassAllParametersInQueryString = true,
                Parameters = RequestParameters.GenerateParametersFromObject(request)
            });
        }

        public NewOrderRequest CancelStopOrder(Url request)
        {
            return client.MakeRequest<NewOrderRequest>(new RequestParameters(HttpMethod.Delete, "stop-order", 1)
            {
                IsOrderRequest = true,
                PassAllParametersInQueryString = true,
                Parameters = RequestParameters.GenerateParametersFromObject(request)
            });
        }

        public NewOrderRequest CancelStopOrders(CancelStopOrder request)
        {
            return client.MakeRequest<NewOrderRequest>(new RequestParameters(HttpMethod.Delete, "stop-order/cancel", 1)
            {
                IsOrderRequest = true,
                PassAllParametersInQueryString = true,
                Parameters = RequestParameters.GenerateParametersFromObject(request)
            });
        }

        public NewOrderRequest GetStopSingleOrderInfo(Url request)
        {
            return client.MakeRequest<NewOrderRequest>(new RequestParameters(HttpMethod.Get, "stop-order", 1)
            {
                IsOrderRequest = true,
                PassAllParametersInQueryString = true,
                Parameters = RequestParameters.GenerateParametersFromObject(request)
            });
        }

        public NewOrderRequest ListStopOrders(ListStopOrder request)
        {
            return client.MakeRequest<NewOrderRequest>(new RequestParameters(HttpMethod.Get, "stop-order", 1)
            {
                IsOrderRequest = true,
                PassAllParametersInQueryString = true,
                Parameters = RequestParameters.GenerateParametersFromObject(request)
            });
        }

        public NewOrderRequest GetStopSingleOrderByClientOId(SingleOrderByClientOId request)
        {
            return client.MakeRequest<NewOrderRequest>(new RequestParameters(HttpMethod.Get, "stop-order/queryOrderByClientOid", 1)
            {
                IsOrderRequest = true,
                PassAllParametersInQueryString = true,
                Parameters = RequestParameters.GenerateParametersFromObject(request)
            });
        }

        public NewOrderRequest CancelStopSingleOrderByClientOId(SingleOrderByClientOId request)
        {
            return client.MakeRequest<NewOrderRequest>(new RequestParameters(HttpMethod.Delete, "stop-order/cancelOrderByClientOid", 1)
            {
                IsOrderRequest = true,
                PassAllParametersInQueryString = true,
                Parameters = RequestParameters.GenerateParametersFromObject(request)
            });
        }

    }
}
