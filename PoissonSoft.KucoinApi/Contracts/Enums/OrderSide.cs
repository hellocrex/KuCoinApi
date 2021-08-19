using System.Runtime.Serialization;

namespace PoissonSoft.KuCoinApi.Contracts.Enums
{
    /// <summary>
    /// Направление сделки
    /// </summary>
    public enum OrderSide
    {
        /// <summary>
        /// Unknown (erroneous) type
        /// </summary>
        Unknown,

        /// <summary>
        /// BUY
        /// </summary>
        [EnumMember(Value = "buy")]
        Buy,

        /// <summary>
        /// SELL
        /// </summary>
        [EnumMember(Value = "sell")]
        Sell,
    }
}