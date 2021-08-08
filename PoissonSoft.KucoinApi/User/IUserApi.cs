using System;
using System.Collections.Generic;
using System.Text;
using PoissonSoft.KuСoinApi.Contracts;
using PoissonSoft.KuСoinApi.Contracts.MarketData;
using PoissonSoft.KuСoinApi.Contracts.MarketData.Request;
using PoissonSoft.KuСoinApi.Contracts.MarketData.Response;
using PoissonSoft.KuСoinApi.Contracts.Trade.Request;
using PoissonSoft.KuСoinApi.Contracts.User;
using PoissonSoft.KuСoinApi.Contracts.User.Account.Request;
using PoissonSoft.KuСoinApi.Contracts.User.Request;
using PoissonSoft.KuСoinApi.Contracts.User.Response;

namespace PoissonSoft.KuСoinApi.User
{
    public interface IUserApi
    {
        KuСoinInfo[] AllCoinsInformation(int cacheValidityIntervalSec = 10 * 60);

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

    }


}
