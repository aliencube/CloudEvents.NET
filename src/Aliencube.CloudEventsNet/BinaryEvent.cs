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
            var lowered = this.ContentType.ToLowerInvariant();

            if (ContentTypeValidator.IsJson(this.ContentType))
            {
                return false;
            }

            if (ContentTypeValidator.HasJsonSuffix(this.ContentType))
            {
                return false;
            }

            if (ContentTypeValidator.ImpliesJson(this.ContentType))
            {
                return false;
            }

            if (ContentTypeValidator.IsText(this.ContentType))
            {
                return false;
            }

            return true;
        }
    }
}