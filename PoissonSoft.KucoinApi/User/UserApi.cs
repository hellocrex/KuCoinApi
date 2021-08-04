using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using NLog;
using PoissonSoft.KuСoinApi.Contracts;
using PoissonSoft.KuСoinApi.Contracts.MarketData;
using PoissonSoft.KuСoinApi.Contracts.MarketData.Request;
using PoissonSoft.KuСoinApi.Contracts.MarketData.Response;
using PoissonSoft.KuСoinApi.Contracts.User;
using PoissonSoft.KuСoinApi.Contracts.User.Account.Request;
using PoissonSoft.KuСoinApi.Contracts.User.Request;
using PoissonSoft.KuСoinApi.Contracts.User.Response;
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
            if (apiClient == null) throw new ArgumentNullException(nameof(apiClient));
            sApiClient = new RestClient(logger, "https://openapi-sandbox.kucoin.com/api/v1",
                //sApiClient = new RestClient(logger, "https://api.binance.com/api/v1",
                new[] {EndpointSecurityType.Trade}, credentials);
               // apiClient.Throttler);

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

        public CurrencyList UserInfo()
        {
            return sApiClient.MakeRequest<CurrencyList>(new RequestParameters(HttpMethod.Get, "sub/user", 1));
        }

        public Account CreateAccount(AccountC request)
        {
            return sApiClient.MakeRequest<Account>(
                new RequestParameters(HttpMethod.Post, "accounts", 1)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public AccountsList GetListAccounts(AccountC request)
        {
            return sApiClient.MakeRequest<AccountsList>(
                new RequestParameters(HttpMethod.Get, "accounts", 1)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public AccountList GetAccount(Url request)
        {
            return sApiClient.MakeRequest<AccountList>(
                new RequestParameters(HttpMethod.Get, "accounts", 1)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public DeprecatedLedgers GetAccountLedgersDeprecated(LedgersDeprecated request)
        {
            return sApiClient.MakeRequest<DeprecatedLedgers>(
                new RequestParameters(HttpMethod.Get, $"accounts/{request.AccountId}/ledgers", 1)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public Account GetAccountLedgers(Ledgers request)
        {
            return sApiClient.MakeRequest<Account>(
                new RequestParameters(HttpMethod.Get, "accounts/ledgers", 1)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public SubAccount GetAccountBalanceOfSubAccount(Url request)
        {
            return sApiClient.MakeRequest<SubAccount>(
                new RequestParameters(HttpMethod.Get, "sub-accounts", 1)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public SubAccount GetAggregatedBalanceOfAllSubAccounts()
        {
            return sApiClient.MakeRequest<SubAccount>(new RequestParameters(HttpMethod.Get, "sub-accounts", 1));
        }

        public Transferable GetTransferable(AccountC request)
        {
            return sApiClient.MakeRequest<Transferable>(
                new RequestParameters(HttpMethod.Get, "sub-accounts", 1)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public Deposit CreateDepositAddressV1(CurrencyReq request)
        {
            return sApiClient.MakeRequest<Deposit>(
                new RequestParameters(HttpMethod.Post, "deposit-addresses", 1)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public Deposit GetDepositAddressV2(CurrencyReq request)
        {
            return sApiClient.MakeRequest<Deposit>(
                new RequestParameters(HttpMethod.Get, "v2/deposit-addresses", 1)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public Deposit GetDepositAddress(CurrencyReq request)
        {
            return sApiClient.MakeRequest<Deposit>(
                new RequestParameters(HttpMethod.Get, "deposit-addresses", 1)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public Deposit GetDepositList(CurrencyReq request)
        {
            //return sApiClient.MakeRequest<Deposit>(new RequestParameters(HttpMethod.Get, "deposits", 1));
            return sApiClient.MakeRequest<Deposit>(
                new RequestParameters(HttpMethod.Get, "deposits", 1)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public Deposit GetV1HistoricalDepositsList(CurrencyReq request)
        {
            return sApiClient.MakeRequest<Deposit>(new RequestParameters(HttpMethod.Get, "hist-deposits", 1));
            //return sApiClient.MakeRequest<Deposit>(
            //    new RequestParameters(HttpMethod.Get, "v1/deposit-addresses", 1)
            //    {
            //        Parameters = RequestParameters.GenerateParametersFromObject(request)
            //    });
        }

        public Deposit GetBasicUserFee()
        {
            return sApiClient.MakeRequest<Deposit>(new RequestParameters(HttpMethod.Get, "base-fee", 1));
        }

    }
}
