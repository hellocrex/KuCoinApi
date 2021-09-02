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

        /// <summary>
        /// Transaction
        /// </summary>
        [EnumMember(Value = "transaction")]
        Transaction,

        /// <summary>
        /// Direction
        /// </summary>
        [EnumMember(Value = "direction")]
        Direction,

        /// <summary>
        /// In
        /// </summary>
        [EnumMember(Value = "in")]
        In,

        /// <summary>
        /// Out
        /// </summary>
        [EnumMember(Value = "out")]
        Out,
    }
}