using System;

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
    public class ObjectEvent<T> : CloudEvent<T> where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectEvent"/> class.
        /// </summary>
        /// <param name="cloudEventsVersion"><see cref="CloudEventsVersion"/> value.</param>
        public ObjectEvent(string cloudEventsVersion = CloudEventsNet.CloudEventsVersion.Version01)
        {
            this.CloudEventsVersion = cloudEventsVersion;
        }

        /// <inheritdoc />
        protected override bool IsValidDataType(T data)
        {
            if (!this.ContentType.Equals("application/json", StringComparison.CurrentCultureIgnoreCase) &&
                !this.ContentType.EndsWith("+json", StringComparison.CurrentCultureIgnoreCase))
            {
                return false;
            }

            return true;
        }
    }
}