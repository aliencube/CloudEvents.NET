using System;

using Aliencube.CloudEventsNet.Abstractions;

using Newtonsoft.Json;

namespace Aliencube.CloudEventsNet
{
    /// <summary>
    /// This represents the CloudEvent entity containing binary data.
    /// </summary>
    public class BinaryEvent : CloudEvent<byte[]>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryEvent"/> class.
        /// </summary>
        /// <param name="cloudEventsVersion"><see cref="CloudEventsVersion"/> value.</param>
        public BinaryEvent(string cloudEventsVersion = CloudEventsNet.CloudEventsVersion.Version01)
        {
            this.CloudEventsVersion = cloudEventsVersion;
        }

        /// <inheritdoc />
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore, TypeNameHandling = TypeNameHandling.None)]
        public override byte[] Data { get => base.Data; set => base.Data = value; }

        /// <inheritdoc />
        protected override bool IsValidDataType(byte[] data)
        {
            if (this.ContentType.StartsWith("text/", StringComparison.CurrentCultureIgnoreCase))
            {
                return false;
            }

            if (this.ContentType.Equals("application/json", StringComparison.CurrentCultureIgnoreCase))
            {
                return false;
            }

            if (this.ContentType.EndsWith("+json", StringComparison.CurrentCultureIgnoreCase))
            {
                return false;
            }

            return true;
        }
    }
}