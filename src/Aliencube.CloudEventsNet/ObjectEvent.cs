using Aliencube.CloudEventsNet.Abstractions;

namespace Aliencube.CloudEventsNet
{
    /// <summary>
    /// This represents the CloudEvent entity containing object data.
    /// </summary>
    public class ObjectEvent : ObjectEvent<object>
    {
    }

    /// <summary>
    /// This represents the CloudEvent entity containing object data.
    /// </summary>
    /// <typeparam name="T">Type of data object.</typeparam>
    public class ObjectEvent<T> : CloudEvent<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectEvent"/> class.
        /// </summary>
        /// <param name="cloudEventsVersion"><see cref="CloudEventsVersion"/> value.</param>
        public ObjectEvent(string cloudEventsVersion = CloudEventsNet.CloudEventsVersion.Version01)
        {
            this.CloudEventsVersion = cloudEventsVersion;

            if (typeof(T) == typeof(string))
            {
                throw new TypeArgumentException();
            }

            if (typeof(T) == typeof(byte[]))
            {
                throw new TypeArgumentException();
            }
        }

        /// <inheritdoc />
        protected override bool IsValidDataType(T data)
        {
            if (data.GetType() == typeof(string))
            {
                return false;
            }

            if (data.GetType() == typeof(byte[]))
            {
                return false;
            }

            var lowered = this.ContentType.ToLowerInvariant();

            if (lowered.StartsWith("application/json"))
            {
                return true;
            }

            if (lowered.Contains("+json"))
            {
                return true;
            }

            if (lowered.StartsWith("text/json"))
            {
                return true;
            }

            return false;
        }
    }
}