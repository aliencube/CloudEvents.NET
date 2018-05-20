using Aliencube.CloudEventsNet.Abstractions;

namespace Aliencube.CloudEventsNet
{
    /// <summary>
    /// This represents the factory entity to create a <see cref="CloudEvent{T}"/> instance.
    /// </summary>
    public static class CloudEventFactory
    {
        /// <summary>
        /// Creates a new instance of the <see cref="CloudEvent{T}"/> class.
        /// </summary>
        /// <typeparam name="T">Type of data.</typeparam>
        /// <param name="contentType">Content type.</param>
        /// <param name="data">Event data.</param>
        /// <param name="cloudEventsVersion"><see cref="CloudEventsVersion"/> value.</param>
        /// <returns>Returns the <see cref="CloudEvent{T}"/> instance created.</returns>
        public static CloudEvent<T> Create<T>(string contentType, T data, string cloudEventsVersion = CloudEventsVersion.Version01)
        {
            if (IsJson(contentType))
            {
                var objectEventised = new ObjectEvent<T>(cloudEventsVersion) { ContentType = contentType, Data = data };

                return objectEventised;
            }

            if (ContentTypeValidator.IsText(contentType))
            {
                var stringified = data as string;
                var stringEventised = new StringEvent(cloudEventsVersion) { ContentType = contentType, Data = stringified };

                return stringEventised as CloudEvent<T>;
            }

            var binarified = data as byte[];
            var binaryEventised = new BinaryEvent(cloudEventsVersion) { ContentType = contentType, Data = binarified };

            return binaryEventised as CloudEvent<T>;
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