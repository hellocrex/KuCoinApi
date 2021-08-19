using System;
using System.Collections.Generic;
using System.Text;

namespace PoissonSoft.KuCoinApi.Contracts.Exceptions
{
    public class RequestRateLimitBreakingException : Exception
    {
        /// <inheritdoc />
        public RequestRateLimitBreakingException() : base() { }

        /// <inheritdoc />
        public RequestRateLimitBreakingException(string msg) : base(msg) { }

        /// <inheritdoc />
        public RequestRateLimitBreakingException(string msg, Exception innerException)
            : base(msg, innerException) { }
    }
}
