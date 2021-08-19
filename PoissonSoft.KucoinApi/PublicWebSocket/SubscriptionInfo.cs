using System.Collections.Generic;
using PoissonSoft.KuCoinApi.Contracts.Enums;
using PoissonSoft.KuCoinApi.PublicWebSocket;

namespace PoissonSoft.BinanceApi.MarketDataStreams
{
    /// <summary>
    /// Subscription information
    /// </summary>
    public class SubscriptionInfo
    {
        /// <summary>
        /// Subscription Id used to unsubscribe
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Type of Data Stream
        /// </summary>
        public Topic Topic { get; set; }

        /// <summary>
        /// Type of Data Stream
        /// </summary>
        public string TopicString { get; set; }

        /// <summary>
        /// Other Stream parameters
        /// </summary>
        public Dictionary<string, string> Parameters { get; set; }
    }
}
