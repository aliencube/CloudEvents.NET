using System;

using Aliencube.CloudEventsNet.Abstractions;

namespace Aliencube.CloudEventsNet
{
    /// <summary>
    /// This represents the CloudEvent entity containing string data.
    /// </summary>
    public class StringEvent : CloudEvent<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringEvent"/> class.
        /// </summary>
        /// <param name="cloudEventsVersion"><see cref="CloudEventsVersion"/> value.</param>
        public StringEvent(string cloudEventsVersion = CloudEventsNet.CloudEventsVersion.Version01)
        {
            this.CloudEventsVersion = cloudEventsVersion;
        }

        /// <inheritdoc />
        protected override bool IsValidDataType(string data)
        {
            if (this.ContentType.StartsWith("text/", StringComparison.CurrentCultureIgnoreCase))
            {
                return true;
            }

            return false;
        }
    }
}