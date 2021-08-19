using System;
using System.Collections.Generic;
using System.Text;
using PoissonSoft.KuCoinApi.Contracts;
using PoissonSoft.KuCoinApi.Contracts.MarketData;
using PoissonSoft.KuCoinApi.Contracts.MarketData.Request;
using PoissonSoft.KuCoinApi.Contracts.MarketData.Response;
using PoissonSoft.KuCoinApi.Contracts.Trade.Request;
using PoissonSoft.KuCoinApi.Contracts.User;
using PoissonSoft.KuCoinApi.Contracts.User.Account.Request;
using PoissonSoft.KuCoinApi.Contracts.User.Request;
using PoissonSoft.KuCoinApi.Contracts.User.Response;

namespace PoissonSoft.KuCoinApi.User
{
    public interface IUserApi
    {
        KuCoinInfo[] AllCoinsInformation(int cacheValidityIntervalSec = 10 * 60);

        /// <summary>
        /// Fetch deposit history.
        /// </summary>
        /// <returns></returns>
        DepositInfo[] DepositHistory(DepositHistoryRequest request);

        AccountsList GetListAccounts(AccountC request);
        SubAccountsList UserInfo();
        Account CreateAccount(AccountC request);
        Ledgers GetAccountLedgersDeprecated(LedgersDeprecatedReq request);
        Ledgers GetAccountLedgers(LedgersReq request);
        SubAccount GetAccountBalanceOfSubAccount(Url request);
        SubAccount GetAggregatedBalanceOfAllSubAccounts();
        Transferable GetTransferable(AccountC request);
        DepositAddress CreateDepositAddressV1(CurrencyReq request);
        DepositAddress GetDepositAddressV2(CurrencyReq request);
        AddressList GetDepositAddress(CurrencyReq request);
        DepositList GetDepositList(CurrencyReq request);
        HistoricalList GetV1HistoricalDepositsList(DepositReq request);
        AccountList GetAccount(Url request);
        FeeInfo GetBasicUserFee();
        FeeList ActualFeeRateTradingPair(TradePairs request);
        WithdrawListInfo GetWithdrawalsList(DepositReq request);
        WithdrawQuotaInfo GetWithdrawalQuotas(WithdrawQuota request);
        HistoricalWithdrawListInfo GetV1HistoricalWithdrawList(HistoricalWithdrawList request);
        FeeList ApplyWithdraw(WithdrawReq request);
        FeeList CancelWithdrawal(Url request);

    }


}
