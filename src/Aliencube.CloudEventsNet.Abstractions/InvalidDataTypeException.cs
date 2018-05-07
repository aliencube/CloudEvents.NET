using System;

#if !NETSTANDARD1_3
using System.Runtime.Serialization;
#endif

namespace Aliencube.CloudEventsNet.Abstractions
{
    /// <summary>
    /// This represents the exception entity when the data type is invalid.
    /// </summary>
#if !NETSTANDARD1_3
    [Serializable]
#endif
    public class InvalidDataTypeException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidDataTypeException"/> class.
        /// </summary>
        public InvalidDataTypeException()
            : base("Invalid CloudEvent data type.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidDataTypeException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public InvalidDataTypeException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidDataTypeException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="innerException">Inner exception instance.</param>
        public InvalidDataTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }

#if !NETSTANDARD1_3
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidDataTypeException"/> class.
        /// </summary>
        /// <param name="info"><see cref="SerializationInfo"/> instance.</param>
        /// <param name="context"><see cref="StreamingContext"/> instance.</param>
        protected InvalidDataTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
#endif
    }
}