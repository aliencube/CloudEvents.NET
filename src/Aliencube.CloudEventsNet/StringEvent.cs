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
            var lowered = this.ContentType.ToLowerInvariant();

            if (lowered.StartsWith("text/json"))
            {
                return false;
            }

            if (lowered.StartsWith("text/"))
            {
                return true;
            }

            return false;
        }
    }
}