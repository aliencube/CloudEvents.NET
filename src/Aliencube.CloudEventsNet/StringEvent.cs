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
        /// <param name="contentType">Content type value. Default is <c>text/plain</c>.</param>
        public StringEvent(string cloudEventsVersion = CloudEventsNet.CloudEventsVersion.Version01, string contentType = "text/plain")
        {
            this.CloudEventsVersion = cloudEventsVersion;
        }

        /// <inheritdoc />
        protected override bool IsValidContentType(string contentType)
        {
            if (string.IsNullOrWhiteSpace(contentType))
            {
                return true;
            }

            if (ContentTypeValidator.ImpliesJson(contentType))
            {
                return false;
            }

            if (ContentTypeValidator.IsText(contentType))
            {
                return true;
            }

            return false;
        }

        /// <inheritdoc />
        protected override bool IsValidDataType(string data)
        {
            // Returns true because there is no content type defined for comparison.
            if (string.IsNullOrWhiteSpace(this.ContentType))
            {
                return true;
            }

            if (ContentTypeValidator.ImpliesJson(this.ContentType))
            {
                return false;
            }

            if (ContentTypeValidator.IsText(this.ContentType))
            {
                return true;
            }

            return false;
        }
    }
}