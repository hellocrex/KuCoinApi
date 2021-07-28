using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PoissonSoft.KuСoinApi.Contracts.Enums
{
    public enum Status
    {
        /// <summary>
        /// Main
        /// </summary>
        [EnumMember(Value = "active")]
        Active,

        /// <summary>
        /// Main
        /// </summary>
        [EnumMember(Value = "done")]
        Done
    }
}
