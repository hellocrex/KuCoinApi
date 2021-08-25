using Newtonsoft.Json;
using PoissonSoft.KuCoinApi.Contracts.Enums;

namespace PoissonSoft.KuCoinApi.Contracts.Trade.Request
{
    /// <summary>
    /// Параметры для создания запроса на отмену всех открытых ордеров
    /// Example
    /// DELETE /api/v1/orders? symbol = ETH - BTC & tradeType = TRADE
    /// </summary>
    public class ReqCancelOrders
    {
        /// <summary>
        /// [Optional] symbol, cancel the orders for the specified trade pair.
        /// </summary>
        [JsonProperty("symbol", NullValueHandling = NullValueHandling.Ignore)]
        public string Symbol { get; set; }

        /// <summary>
        /// [Optional] the type of trading, cancel the orders for the specified trading type, and the default is to cancel the spot trading order (TRADE).
        /// </summary>
        [JsonProperty("tradeType", NullValueHandling = NullValueHandling.Ignore)]
        public TradeType? TradeType { get; set; }

        /// <summary>
        /// [Optional] Comma seperated order IDs.
        /// </summary>
        [JsonProperty("orderIds", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderIds { get; set; }
    }
}
