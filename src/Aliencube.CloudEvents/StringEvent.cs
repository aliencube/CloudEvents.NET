using System;

using Aliencube.CloudEvents.Abstractions;

namespace Aliencube.CloudEvents
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
        public StringEvent(string cloudEventsVersion = CloudEvents.CloudEventsVersion.Version01)
        {
            this.CloudEventsVersion = cloudEventsVersion;
        }

        /// <inheritdoc />
        protected override bool IsValidDataType(string data)
        {
            if (!this.ContentType.StartsWith("text/", StringComparison.CurrentCultureIgnoreCase))
            {
                return false;
            }

            return true;
        }
    }
}