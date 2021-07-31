using System.Net.Http;
using NLog;
using PoissonSoft.KuСoinApi.Contracts.Trade;
using PoissonSoft.KuСoinApi.Transport;
using PoissonSoft.KuСoinApi.Transport.Rest;

namespace PoissonSoft.KuСoinApi.Trade
{
    internal sealed class TradeApi : ITradeApi
    {
        private readonly KuСoinApiClient apiClient;
        private readonly RestClient client;

        public TradeApi(KuСoinApiClient apiClient, KuСoinApiClientCredentials credentials, ILogger logger)
        {

            this.apiClient = apiClient;// ?? throw new ArgumentNullException(nameof(apiClient));
            client = new RestClient(logger, "https://api.kucoin.com/api/v1",
                new[] { EndpointSecurityType.Trade }, credentials);
            //,this.apiClient.Throttler);
        }
        

        public NewOrderRequest NewOrder(NewOrderRequest request, bool isHighPriority)
        {
            return client.MakeRequest<NewOrderRequest>(new RequestParameters(HttpMethod.Post, "deposit-addresses", 1)
            {
                IsHighPriority = isHighPriority,
                IsOrderRequest = true,
                PassAllParametersInQueryString = true,
                Parameters = RequestParameters.GenerateParametersFromObject(request)
            });
        }
    }
}
