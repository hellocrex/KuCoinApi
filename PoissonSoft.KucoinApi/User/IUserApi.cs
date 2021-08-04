using System;
using System.Collections.Generic;
using System.Text;
using PoissonSoft.KuСoinApi.Contracts;
using PoissonSoft.KuСoinApi.Contracts.MarketData;
using PoissonSoft.KuСoinApi.Contracts.MarketData.Request;
using PoissonSoft.KuСoinApi.Contracts.MarketData.Response;
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
        CurrencyList UserInfo();
        Account CreateAccount(AccountC request);
        DeprecatedLedgers GetAccountLedgersDeprecated(LedgersDeprecated request);
        Account GetAccountLedgers(Ledgers request);
        SubAccount GetAccountBalanceOfSubAccount(Url request);
        SubAccount GetAggregatedBalanceOfAllSubAccounts();
        Transferable GetTransferable(AccountC request);
        Deposit CreateDepositAddressV1(CurrencyReq request);
        Deposit GetDepositAddressV2(CurrencyReq request);
        Deposit GetDepositAddress(CurrencyReq request);
        Deposit GetDepositList(CurrencyReq request);
        Deposit GetV1HistoricalDepositsList(CurrencyReq request);
        AccountList GetAccount(Url request);
        Deposit GetBasicUserFee();

    }


}
