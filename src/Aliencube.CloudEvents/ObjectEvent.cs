using System;

using Aliencube.CloudEvents.Abstractions;

namespace Aliencube.CloudEvents
{
    /// <summary>
    /// This represents the CloudEvent entity containing object data.
    /// </summary>
    public class ObjectEvent<T> : CloudEvent<T> where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectEvent"/> class.
        /// </summary>
        /// <param name="cloudEventsVersion"><see cref="CloudEventsVersion"/> value.</param>
        public ObjectEvent(string cloudEventsVersion = CloudEvents.CloudEventsVersion.Version01)
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