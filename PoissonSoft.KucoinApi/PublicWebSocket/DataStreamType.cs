using System.ComponentModel;

namespace PoissonSoft.BinanceApi.MarketDataStreams
{
    /// <summary>
    /// Type of Data Stream
    /// </summary>
    public enum DataStreamType
    {
  
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

        [Description("/indicator/index")]
        IndicatorIndex,

        [Description("/indicator/markPrice")]
        IndicatorMarkPrice,

        [Description("/margin/fundingBook")]
        MarginFundingBook,

        [Description("/spotMarket/tradeOrders")]
        SpotMarketTradeOrders,
    }
}
