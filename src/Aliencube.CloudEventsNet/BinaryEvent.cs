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
        /// <param name="contentType">Content type value. Default is <c>application/octet-stream</c>.</param>
        public BinaryEvent(string cloudEventsVersion = CloudEventsNet.CloudEventsVersion.Version01, string contentType = "application/octet-stream")
        {
            this.CloudEventsVersion = cloudEventsVersion;
            this.ContentType = contentType;
        }

        /// <inheritdoc />
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore, TypeNameHandling = TypeNameHandling.None)]
        public override byte[] Data { get => base.Data; set => base.Data = value; }

        /// <inheritdoc />
        protected override bool IsValidContentType(string contentType)
        {
            if (string.IsNullOrWhiteSpace(contentType))
            {
                return true;
            }

            if (ContentTypeValidator.IsJson(contentType))
            {
                return false;
            }

            if (ContentTypeValidator.HasJsonSuffix(contentType))
            {
                return false;
            }

            if (ContentTypeValidator.ImpliesJson(contentType))
            {
                return false;
            }

            if (ContentTypeValidator.IsText(contentType))
            {
                return false;
            }

            return true;
        }

        /// <inheritdoc />
        protected override bool IsValidDataType(byte[] data)
        {
            // Returns true because there is no content type defined for comparison.
            if (string.IsNullOrWhiteSpace(this.ContentType))
            {
                return true;
            }

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