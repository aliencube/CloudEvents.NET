using Aliencube.CloudEventsNet.Abstractions;
using Aliencube.CloudEventsNet.Http.Abstractions;

namespace Aliencube.CloudEventsNet.Http
{
    /// <summary>
    /// This represents the factory entity to create a <see cref="CloudEventContent{T}"/> instance.
    /// </summary>
    public static class CloudEventContentFactory
    {
        /// <summary>
        /// Creates a new instance of the <see cref="CloudEventContent{T}"/> class.
        /// </summary>
        /// <typeparam name="T">Type of data.</typeparam>
        /// <param name="ce"><see cref="CloudEvent{T}"/> instance.</param>
        /// <returns>Returns the <see cref="CloudEventContent{T}"/> instance.</returns>
        public static CloudEventContent<T> Create<T>(CloudEvent<T> ce)
        {
            if (IsJson(ce.ContentType))
            {
                return new StructuredCloudEventContent<T>(ce);
            }

            return new BinaryCloudEventContent<T>(ce);
        }

        private static bool IsJson(string contentType)
        {
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
    }
}