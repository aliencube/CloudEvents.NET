using System;

#if !NETSTANDARD1_3
using System.Runtime.Serialization;
#endif

namespace Aliencube.CloudEventsNet.Abstractions
{
    [Serializable]
    public class InvalidDataTypeException : Exception
    {
        public InvalidDataTypeException()
            : base("Invalid CloudEvent data type.")
        {
        }

        public InvalidDataTypeException(string message) : base(message)
        {
        }

        public InvalidDataTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }

#if !NETSTANDARD1_3
        protected InvalidDataTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
#endif
    }
}