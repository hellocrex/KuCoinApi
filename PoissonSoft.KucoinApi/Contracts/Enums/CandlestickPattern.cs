using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using PoissonSoft.BinanceApi.Contracts.Serialization;
using PoissonSoft.KuCoinApi.Contracts.WebSocketStream;

namespace PoissonSoft.KuCoinApi.Contracts.Enums
{
    public enum CandlestickPattern
    {
        /// <summary>
        /// Unknown (erroneous)
        /// </summary>
        Unknown,

        /// <summary>
        /// Main
        /// </summary>
        /// [EnumMember(Value = "1min")]
        [Description("1min")]
        Min,

        /// <summary>
        /// Main
        /// </summary>
        [Description("3min")]
        Min3,

        /// <summary>
        /// Main
        /// </summary>
        [Description("5min")]
        Min5,

        /// <summary>
        /// Main
        /// </summary>
        [Description("15min")]
        Min15,

        /// <summary>
        /// Main
        /// </summary>
        [Description("30min")]
        Min30,

        /// <summary>
        /// Main
        /// </summary>
        [Description("1hour")]
        Hour1,

        /// <summary>
        /// Main
        /// </summary>
        [Description("2hour")]
        Hour2,

        /// <summary>
        /// Main
        /// </summary>
        [Description("4hour")]
        Hour4,

        /// <summary>
        /// Main
        /// </summary>
        [Description("6hour")]
        Hour6,

        /// <summary>
        /// Main
        /// </summary>
        [Description("8hour")]
        Hour8,

        /// <summary>
        /// Main
        /// </summary>
        [Description("12hour")]
        HalfDay,

        /// <summary>
        /// Main
        /// </summary>
        [Description("1day")]
        Day,

        /// <summary>
        /// Main
        /// </summary>
        [Description("1week")]
        Week
    }
}
