using Aliencube.CloudEventsNet.Abstractions;

namespace Aliencube.CloudEventsNet
{
    /// <summary>
    /// This represents the CloudEvent entity containing object data.
    /// </summary>
    public class ObjectEvent : ObjectEvent<object>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectEvent"/> class.
        /// </summary>
        /// <param name="cloudEventsVersion"><see cref="CloudEventsVersion"/> value.</param>
        /// <param name="contentType">Content type value. Default is <c>application/cloudevents+json</c>.</param>
        public ObjectEvent(string cloudEventsVersion = CloudEventsNet.CloudEventsVersion.Version01, string contentType = "application/cloudevents+json")
            : base(cloudEventsVersion, contentType)
        {
        }
    }

    /// <summary>
    /// This represents the CloudEvent entity containing object data.
    /// </summary>
    /// <typeparam name="T">Type of data object.</typeparam>
    public class ObjectEvent<T> : CloudEvent<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectEvent{T}"/> class.
        /// </summary>
        /// <param name="cloudEventsVersion"><see cref="CloudEventsVersion"/> value.</param>
        /// <param name="contentType">Content type value. Default is "application/cloudevents+json".</param>
        public ObjectEvent(string cloudEventsVersion = CloudEventsNet.CloudEventsVersion.Version01, string contentType = "application/cloudevents+json")
        {
            this.CloudEventsVersion = cloudEventsVersion;
            this.ContentType = contentType;

            if (ContentTypeValidator.IsTypeString(typeof(T)))
            {
                throw new TypeArgumentException();
            }

            if (ContentTypeValidator.IsTypeByteArray(typeof(T)))
            {
                throw new TypeArgumentException();
            }
        }

        /// <inheritdoc />
        protected override bool IsValidContentType(string contentType)
        {
            if (string.IsNullOrWhiteSpace(contentType))
            {
                return true;
            }

            if (ContentTypeValidator.IsJson(contentType))
            {
                return true;
            }

            if (ContentTypeValidator.HasJsonSuffix(contentType))
            {
                return true;
            }

            if (ContentTypeValidator.ImpliesJson(contentType))
            {
                return true;
            }

            return false;
        }

        /// <inheritdoc />
        protected override bool IsValidDataType(T data)
        {
            if (ContentTypeValidator.IsDataString<T>(data))
            {
                return false;
            }

            if (ContentTypeValidator.IsDataByteArray<T>(data))
            {
                return false;
            }

            // Returns true because there is no content type defined for comparison.
            if (string.IsNullOrWhiteSpace(this.ContentType))
            {
                return true;
            }

            if (ContentTypeValidator.IsJson(this.ContentType))
            {
                return true;
            }

            if (ContentTypeValidator.HasJsonSuffix(this.ContentType))
            {
                return true;
            }

            if (ContentTypeValidator.ImpliesJson(this.ContentType))
            {
                return true;
            }

            return false;
        }
    }
}