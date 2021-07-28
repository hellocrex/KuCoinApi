using System;

namespace PoissonSoft.KuСoinApi.Contracts.Exceptions
{
    /// <summary>
    /// Исключение при взаимодействии с endpoint
    /// </summary>
    public class EndpointCommunicationException : Exception
    {
        /// <inheritdoc />
        public EndpointCommunicationException() : base() { }

        /// <inheritdoc />
        public EndpointCommunicationException(string msg) : base(msg) { }

        /// <inheritdoc />
        public EndpointCommunicationException(string msg, Exception innerException)
            : base(msg, innerException) { }

    }
}