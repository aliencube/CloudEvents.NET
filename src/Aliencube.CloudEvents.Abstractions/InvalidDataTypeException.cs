using System;
using System.Runtime.Serialization;

namespace Aliencube.CloudEvents.Abstractions
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

        protected InvalidDataTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}