using System;

#if !NETSTANDARD1_3
using System.Runtime.Serialization;
#endif

namespace Aliencube.CloudEventsNet.Abstractions
{
    /// <summary>
    /// This represents the exception entity when the content type is invalid.
    /// </summary>
#if !NETSTANDARD1_3
    [Serializable]
#endif
    public class InvalidContentTypeException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidContentTypeException"/> class.
        /// </summary>
        public InvalidContentTypeException()
            : base("Invalid CloudEvent content type.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidContentTypeException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public InvalidContentTypeException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidContentTypeException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="innerException">Inner exception instance.</param>
        public InvalidContentTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }

#if !NETSTANDARD1_3
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidContentTypeException"/> class.
        /// </summary>
        /// <param name="info"><see cref="SerializationInfo"/> instance.</param>
        /// <param name="context"><see cref="StreamingContext"/> instance.</param>
        protected InvalidContentTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
#endif
    }
}