using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using NLog;
using PoissonSoft.KuCoinApi.Contracts;
using PoissonSoft.KuCoinApi.Contracts.MarketData;
using PoissonSoft.KuCoinApi.Contracts.MarketData.Request;
using PoissonSoft.KuCoinApi.Contracts.MarketData.Response;
using PoissonSoft.KuCoinApi.Contracts.Trade.Request;
using PoissonSoft.KuCoinApi.Contracts.User;
using PoissonSoft.KuCoinApi.Contracts.User.Request;
using PoissonSoft.KuCoinApi.Contracts.User.Response;
using PoissonSoft.KuCoinApi.Transport;
using PoissonSoft.KuCoinApi.Transport.Rest;
using PoissonSoft.KuCoinApi.Utils;

namespace PoissonSoft.KuCoinApi.User
{
    internal sealed class UserApi: IUserApi
    {
        private readonly RestClient sApiClient;

        public UserApi(KuCoinApiClient apiClient, KuCoinApiClientCredentials credentials, ILogger logger)
        {
            if (apiClient == null) throw new ArgumentNullException(nameof(apiClient));
            //sApiClient = new RestClient(logger, "https://openapi-sandbox.kucoin.com/api/v1",
            sApiClient = new RestClient(logger, "https://api.kucoin.com/api/v1",
                new[] {EndpointSecurityType.Trade}, credentials, apiClient.Throttler);

            //coinsInfoCache = new SimpleCache<BinanceCoinInfo[]>(LoadCoinsInformation, logger, "CoinsInfoCache",
            //    data => data.Select(x => (BinanceCoinInfo)x.Clone()).ToArray());
        }

        private readonly SimpleCache<KuCoinInfo[]> coinsInfoCache;
        public KuCoinInfo[] AllCoinsInformation(int cacheValidityIntervalSec = 600)
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

        public SubAccountsList UserInfo()
        {
            return sApiClient.MakeRequest<SubAccountsList>(new RequestParameters(HttpMethod.Get, "sub/user", 1));
        }

        // Не хватает пары методов
        #region Account
        public Account CreateAccount(ReqAccount request)
        {
            return sApiClient.MakeRequest<Account>(
                new RequestParameters(HttpMethod.Post, "accounts", 1)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public AccountsList GetListAccounts(ReqAccount request)
        {
            return sApiClient.MakeRequest<AccountsList>(
                new RequestParameters(HttpMethod.Get, "accounts", 1)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public AccountList GetAccount(SpecialBuildQuery request)
        {
            return sApiClient.MakeRequest<AccountList>(
                new RequestParameters(HttpMethod.Get, "accounts", 1)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }
        // под вопросом
        public Ledgers GetAccountLedgersDeprecated(ReqLedgersDeprecated request)
        {
            return sApiClient.MakeRequest<Contracts.User.Response.Ledgers>(
                new RequestParameters(HttpMethod.Get, $"accounts/{request.AccountId}/ledgers", 1)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public Ledgers GetAccountLedgers(Contracts.User.Request.ReqLedgers request)
        {
            return sApiClient.MakeRequest<Ledgers>(
                new RequestParameters(HttpMethod.Get, "accounts/ledgers", 18 / 3)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public SubAccount GetAccountBalanceOfSubAccount(SpecialBuildQuery request)
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

        public Transferable GetTransferable(ReqAccount request)
        {
            return sApiClient.MakeRequest<Transferable>(
                new RequestParameters(HttpMethod.Get, "sub-accounts", 3 / 3)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }
        
        #endregion


        #region Deposit

        public DepositAddress CreateDepositAddressV1(ReqDepositAddress request)
        {
            return sApiClient.MakeRequest<DepositAddress>(
                new RequestParameters(HttpMethod.Post, "deposit-addresses", 1)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public DepositAddress GetDepositAddressV2(ReqDepositAddress request)
        {
            return sApiClient.MakeRequest<DepositAddress>(
                new RequestParameters(HttpMethod.Get, "v2/deposit-addresses", 1)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public AddressList GetDepositAddress(ReqDepositAddress request)
        {
            return sApiClient.MakeRequest<AddressList>(
                new RequestParameters(HttpMethod.Get, "deposit-addresses", 1)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public DepositList GetDepositList(ReqDepositList request)
        {
            return sApiClient.MakeRequest<DepositList>(
                new RequestParameters(HttpMethod.Get, "deposits", 6 / 3)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public HistoricalListInfo GetV1HistoricalDepositsList(ReqDepositList request)
        {
            return sApiClient.MakeRequest<HistoricalListInfo>(
                new RequestParameters(HttpMethod.Get, "hist-deposits", 6 / 3)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }


        #endregion

        #region Withdrawals
        public WithdrawListInfo GetWithdrawalsList(ReqDepositList request)
        {
            return sApiClient.MakeRequest<WithdrawListInfo>(
                new RequestParameters(HttpMethod.Get, "withdrawals", 6 / 3)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public HistoricalListInfo GetV1HistoricalWithdrawList(ReqDepositList request)
        {
            return sApiClient.MakeRequest<HistoricalListInfo>(
                new RequestParameters(HttpMethod.Get, "hist-withdrawals", 6 / 3)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public WithdrawQuotaInfo GetWithdrawalQuotas(ReqAccount request)
        {
            return sApiClient.MakeRequest<WithdrawQuotaInfo>(
                new RequestParameters(HttpMethod.Get, "withdrawals/quotas", 1)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public FeeList ApplyWithdraw(ReqWithdraw request)
        {
            return sApiClient.MakeRequest<FeeList>(
                new RequestParameters(HttpMethod.Post, "withdrawals", 1)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public FeeList CancelWithdrawal(SpecialBuildQuery request)
        {
            return sApiClient.MakeRequest<FeeList>(
                new RequestParameters(HttpMethod.Delete, "withdrawals", 1)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        #endregion

        public FeeInfo GetBasicUserFee()
        {
            return sApiClient.MakeRequest<FeeInfo>(new RequestParameters(HttpMethod.Get, "base-fee", 1));
        }

        public FeeList ActualFeeRateTradingPair(ReqInstruments request)
        {
            return sApiClient.MakeRequest<FeeList>(
                new RequestParameters(HttpMethod.Get, "trade-fees", 1)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }


    }
}
