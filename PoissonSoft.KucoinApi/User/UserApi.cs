using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using NLog;
using PoissonSoft.KuCoinApi.Contracts;
using PoissonSoft.KuCoinApi.Contracts.Enums;
using PoissonSoft.KuCoinApi.Contracts.MarketData;
using PoissonSoft.KuCoinApi.Contracts.MarketData.Request;
using PoissonSoft.KuCoinApi.Contracts.MarketData.Response;
using PoissonSoft.KuCoinApi.Contracts.Trade;
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
            sApiClient = new RestClient(logger, "https://openapi-sandbox.kucoin.com/api", 
            //sApiClient = new RestClient(logger, "https://api.kucoin.com/api",
                new[] {EndpointSecurityType.Trade}, credentials, apiClient.Throttler);

            //coinsInfoCache = new SimpleCache<BinanceCoinInfo[]>(LoadCoinsInformation, logger, "CoinsInfoCache",
            //    data => data.Select(x => (BinanceCoinInfo)x.Clone()).ToArray());
        }

        private readonly SimpleCache<KuCoinInfo[]> coinsInfoCache;
        public KuCoinInfo[] AllCoinsInformation(int cacheValidityIntervalSec = 600)
        {
            return coinsInfoCache.GetValue(TimeSpan.FromSeconds(cacheValidityIntervalSec));
        }

        public SubAccountsList UserInfo()
        {
            return sApiClient.MakeRequest<SubAccountsList>(new RequestParameters(HttpMethod.Get, "v1/sub/user", 1));
        }

        // Не хватает пары методов
        #region Account
        public Account CreateAccount(ReqAccount request)
        {
            return sApiClient.MakeRequest<Account>(
                new RequestParameters(HttpMethod.Post, "v1/accounts", 1)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public AccountsList GetListAccounts(ReqAccount request)
        {
            return sApiClient.MakeRequest<AccountsList>(
                new RequestParameters(HttpMethod.Get, $"v1/accounts", 1)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                }
                );
        }

        public AccountList GetAccount(SpecialBuildQuery request)
        {
            return sApiClient.MakeRequest<AccountList>(
                new RequestParameters(HttpMethod.Get, "v1/accounts", 1)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }
        // под вопросом
        public Ledgers GetAccountLedgersDeprecated(ReqLedgersDeprecated request)
        {
            return sApiClient.MakeRequest<Contracts.User.Response.Ledgers>(
                new RequestParameters(HttpMethod.Get, $"v1/accounts/{request.AccountId}/ledgers", 1)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public LegendersInfo GetAccountLedgers(Contracts.User.Request.ReqLedgers request)
        {
            return sApiClient.MakeRequest<LegendersInfo>(
                new RequestParameters(HttpMethod.Get, "v1/accounts/ledgers", 18 / 3)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public SubAccount GetAccountBalanceOfSubAccount(SpecialBuildQuery request)
        {
            return sApiClient.MakeRequest<SubAccount>(
                new RequestParameters(HttpMethod.Get, "v1/sub-accounts", 1)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public SubAccount GetAggregatedBalanceOfAllSubAccounts()
        {
            return sApiClient.MakeRequest<SubAccount>(new RequestParameters(HttpMethod.Get, "v1/sub-accounts", 1));
        }

        public Transferable GetTransferable(ReqAccount request)
        {
            return sApiClient.MakeRequest<Transferable>(
                new RequestParameters(HttpMethod.Get, "v1/transferable", 1)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public RespOrderId TransferBetweenAccounts(ReqTransferBetweenAcc request)
        {
            //v2
            return sApiClient.MakeRequest<RespOrderId>(
                new RequestParameters(HttpMethod.Get, "v2/accounts/sub-transfer", 1)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public RespOrderId InnerTransfer(string currency, AccountType fromAcc, AccountType toAcc, decimal amount)
        {
            // v2
            var request = new ReqTransfer
            {
                ClientOid = Guid.NewGuid().ToString(),
                Currency = currency,
                From = fromAcc,
                To = toAcc,
                Amount = amount
            };
            return sApiClient.MakeRequest<RespOrderId>(
                new RequestParameters(HttpMethod.Get, "v2/accounts/inner-transfer", 1)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        #endregion


        #region Deposit

        public AddressList CreateDepositAddressV1(ReqDepositAddress request)
        {
            return sApiClient.MakeRequest<AddressList>(
                new RequestParameters(HttpMethod.Post, "v1/deposit-addresses", 1)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public AddressListV2 GetDepositAddressV2(ReqDepositAddress request)
        {
            return sApiClient.MakeRequest<AddressListV2>(
                new RequestParameters(HttpMethod.Get, "v2/deposit-addresses", 1)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public AddressList GetDepositAddress(string coin, string chain = null)
        {
            return sApiClient.MakeRequest<AddressList>(
                new RequestParameters(HttpMethod.Get, "v1/deposit-addresses", 1)
                {
                    Parameters = new Dictionary<string, string>
                    {
                        ["currency"] = coin
                    }
                });
        }

        public DepositListInfo GetDepositList(ReqDepositAndWithdrawList request)
        {
            return sApiClient.MakeRequest<DepositListInfo>(
                new RequestParameters(HttpMethod.Get, "v1/deposits", 6 / 3)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public HistoricalListInfo GetHistoricalDepositsList(ReqDepositAndWithdrawList request)
        {
            return sApiClient.MakeRequest<HistoricalListInfo>(
                new RequestParameters(HttpMethod.Get, "v1/hist-deposits", 6 / 3)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }


        #endregion

        #region Withdrawals
        public WithdrawListInfo GetWithdrawalsList(ReqDepositAndWithdrawList request)
        {
            return sApiClient.MakeRequest<WithdrawListInfo>(
                new RequestParameters(HttpMethod.Get, "v1/withdrawals", 6 / 3)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public HistoricalListInfo GetHistoricalWithdrawList(ReqDepositAndWithdrawList request)
        {
            return sApiClient.MakeRequest<HistoricalListInfo>(
                new RequestParameters(HttpMethod.Get, "v1/hist-withdrawals", 6 / 3)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public WithdrawQuotaInfo GetWithdrawalQuotas(ReqCurrencyInfo request)
        {
            return sApiClient.MakeRequest<WithdrawQuotaInfo>(
                new RequestParameters(HttpMethod.Get, "v1/withdrawals/quotas", 1)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public Withdraw ApplyWithdraw(ReqWithdraw request)
        {
            return sApiClient.MakeRequest<Withdraw>(
                new RequestParameters(HttpMethod.Post, "v1/withdrawals", 1)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        public Withdraw CancelWithdrawal(SpecialBuildQuery request)
        {
            return sApiClient.MakeRequest<Withdraw>(
                new RequestParameters(HttpMethod.Delete, "v1/withdrawals", 1)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }

        #endregion

        public FeeInfo GetBasicUserFee()
        {
            return sApiClient.MakeRequest<FeeInfo>(new RequestParameters(HttpMethod.Get, "v1/base-fee", 1));
        }

        public FeeList ActualFeeRateTradingPair(ReqInstruments request)
        {
            return sApiClient.MakeRequest<FeeList>(
                new RequestParameters(HttpMethod.Get, "v1/trade-fees", 1)
                {
                    Parameters = RequestParameters.GenerateParametersFromObject(request)
                });
        }


    }
}
