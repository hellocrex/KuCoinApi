using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace PoissonSoft.KuCoinApi.Contracts.Enums
{
    public enum Topic
    {
        //[EnumMember(Value = "/market/ticker")]

        [Description("/market/ticker")]
        Ticker,

        [Description("/market/ticker:all")]
        TickerAll,

        [Description("/market/snapshot")]
        Snapshot,

        [Description("/market/level2")]
        MarketLevel2,

        [Description("/spotMarket/level2Depth")]
        SpotMarketLevel2Depth,

        [Description("/market/candles")]
        MarketCandles,

        [Description("/market/match")]
        MarketMatch,

        [Description("/indicator/index")]
        IndicatorIndex,

        [Description("/indicator/markPrice")]
        IndicatorMarkPrice,

        [Description("/margin/fundingBook")]
        MarginFundingBook,

        [Description("/spotMarket/tradeOrders")]
        SpotMarketTradeOrders,

        /// <summary>
        /// Private topic
        /// </summary>
        [Description("/account/balance")]
        AccountBalance,

        [Description("/margin/position")]
        DebtRatio,

        [Description("/margin/position")]
        PositionStatusEvent,

        [Description("/margin/loan")]
        MarginTradeOrderEvent,

        [Description("/margin/loan")]
        MarginOrderUpdateEvent,

        [Description("/margin/loan")]
        MarginOrderDoneEvent,

        [Description("/spotMarket/advancedOrders")]
        StopOrderEvent

    }

}
