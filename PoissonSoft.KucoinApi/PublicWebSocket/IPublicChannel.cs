using System;
using System.Collections.Generic;
using System.Text;
using PoissonSoft.BinanceApi.Contracts.MarketDataStream;
using PoissonSoft.BinanceApi.MarketDataStreams;
using PoissonSoft.KuCoinApi.Contracts.Enums;
using PoissonSoft.KuCoinApi.Contracts.PublicWebSocket;
using PoissonSoft.KuCoinApi.Contracts.PublicWebSocket.Responce;

namespace PoissonSoft.KuCoinApi.PublicWebSocket
{
    public interface IPublicChannel
    {
        SubscriptionInfo SubscribeSymbolTicker(string symbol, Action<SymbolTicker> callbackAction);
        SubscriptionInfo SubscribeAllSymbolTicker(Action<SymbolTicker> callbackAction);
        SubscriptionInfo SubscribeSymbolSnapshot(string symbol, Action<SymbolSnapshot> callbackAction);
        SubscriptionInfo SubscribeMarketSnapshot(string ticker, Action<SymbolSnapshot> callbackAction);
        SubscriptionInfo SubscribeLevel2MarketData(string instrument, Action<MarketLevel2> callbackAction);

        SubscriptionInfo SubscribeSpotMarketLevel2Depth(string instrument, int depth,
            Action<SpotMarketLevel2Depth5> callbackAction);
        SubscriptionInfo SubscribeKlines(string instrument, CandlestickPattern interval,
            Action<KlinesInfo> callbackAction);

        SubscriptionInfo SubscribeMatchExecutionData(string instrument, Action<MatchExecution> callbackAction);
        SubscriptionInfo SubscribeIndexPrice(string instruments, Action<IndexPrice> callbackAction);
        SubscriptionInfo SubscribeMarkPrice(string instrument, Action<IndexPrice> callbackAction);
        SubscriptionInfo SubscribeOrderBookChange(string ticker, Action<FundingBook> callbackAction);
        SubscriptionInfo SubscribePrivateOrderChangeEvents(Action<OrderChangeEvent> callbackAction);
        SubscriptionInfo SubscribeAccountBalance(Action<AccountBalance> callbackAction);
        SubscriptionInfo SubscribeDebtRatioChange(Action<DeptRatio> callbackAction);
        SubscriptionInfo SubscribePositionStatusChangeEvent(Action<PositionStatusEvent> callbackAction);
        SubscriptionInfo SubscribeMarginTradeOrderEntersEvent(string ticker, Action<MarginTradeOrderEvent> callbackAction);
        SubscriptionInfo SubscribeMarginOrderUpdateEvent(string ticker, Action<MarginOrderUpdateEvent> callbackAction);
        SubscriptionInfo SubscribeMarginOrderDoneEvent(string ticker, Action<MarginOrderDoneEvent> callbackAction);
        SubscriptionInfo SubscribeStopOrderEvent(Action<StopOrderEvent> callbackAction);


        bool Unsubscribe(long subscriptionId);
    }
}
