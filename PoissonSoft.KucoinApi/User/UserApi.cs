using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using NLog;
using PoissonSoft.KuСoinApi.Contracts.User;
using PoissonSoft.KuСoinApi.Transport;
using PoissonSoft.KuСoinApi.Transport.Rest;
using PoissonSoft.KuСoinApi.Utils;

namespace PoissonSoft.KuСoinApi.User
{
    internal sealed class UserApi: IUserApi
    {
        private readonly RestClient sApiClient;

        public UserApi(KuСoinApiClient apiClient, KuСoinApiClientCredentials credentials, ILogger logger)
        {
            //if (apiClient == null) throw new ArgumentNullException(nameof(apiClient));
            //sApiClient = new RestClient(logger, "https://api.binance.com/sapi/v1",
            //    new[] { EndpointSecurityType.UserData }, credentials,
            //    apiClient.Throttler);

            //coinsInfoCache = new SimpleCache<BinanceCoinInfo[]>(LoadCoinsInformation, logger, "CoinsInfoCache",
            //    data => data.Select(x => (BinanceCoinInfo)x.Clone()).ToArray());
        }

        private readonly SimpleCache<KuСoinInfo[]> coinsInfoCache;
        public KuСoinInfo[] AllCoinsInformation(int cacheValidityIntervalSec = 600)
        {
            return coinsInfoCache.GetValue(TimeSpan.FromSeconds(cacheValidityIntervalSec));
        }

        public DepositInfo[] DepositHistory(DepositHistoryRequest request)
        {
            return sApiClient.MakeRequest<DepositInfo[]>(
                new RequestParameters(HttpMethod.Get, "capital/deposit/hisrec", 1)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }
    }
}
