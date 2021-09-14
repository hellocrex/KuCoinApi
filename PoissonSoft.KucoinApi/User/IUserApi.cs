using System;
using System.Collections.Generic;
using System.Text;
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

namespace PoissonSoft.KuCoinApi.User
{
    public interface IUserApi
    {
        KuCoinInfo[] AllCoinsInformation(int cacheValidityIntervalSec = 10 * 60);

        /// <summary>
        /// Fetch deposit history.
        /// </summary>
        /// <returns></returns>

        AccountsList GetListAccounts(ReqAccount request);
        SubAccountsList UserInfo();
        Account CreateAccount(ReqAccount request);
        Ledgers GetAccountLedgersDeprecated(ReqLedgersDeprecated request);
        LegendersInfo GetAccountLedgers(ReqLedgers request);
        SubAccount GetAccountBalanceOfSubAccount(SpecialBuildQuery request);
        SubAccount GetAggregatedBalanceOfAllSubAccounts();
        Transferable GetTransferable(ReqAccount request);
        RespOrderId TransferBetweenAccounts(ReqTransferBetweenAcc request);
        RespOrderId InnerTransfer(string currency, AccountType fromAcc, AccountType toAcc, decimal amount);
        AddressList CreateDepositAddressV1(ReqDepositAddress request);
        AddressListV2 GetDepositAddressV2(ReqDepositAddress request);
        AddressList GetDepositAddress(string coin, string chain = null);
        DepositListInfo GetDepositList(ReqDepositAndWithdrawList request);
        HistoricalListInfo GetHistoricalDepositsList(ReqDepositAndWithdrawList request);
        AccountList GetAccount(SpecialBuildQuery request);
        FeeInfo GetBasicUserFee();
        FeeList ActualFeeRateTradingPair(ReqInstruments request);
        WithdrawListInfo GetWithdrawalsList(ReqDepositAndWithdrawList request);
        HistoricalListInfo GetHistoricalWithdrawList(ReqDepositAndWithdrawList request);
        WithdrawQuotaInfo GetWithdrawalQuotas(ReqCurrencyInfo request);


        Withdraw ApplyWithdraw(ReqWithdraw request);
        Withdraw CancelWithdrawal(SpecialBuildQuery request);

    }


}
