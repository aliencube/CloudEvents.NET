using System;

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
            var lowered = contentType.ToLowerInvariant();

            if (lowered.StartsWith("application/json"))
            {
                var objectEventised = new ObjectEvent<T>(cloudEventsVersion) { ContentType = contentType, Data = data };

                return objectEventised;
            }

            if (lowered.Contains("+json"))
            {
                var objectEventised = new ObjectEvent<T>(cloudEventsVersion) { ContentType = contentType, Data = data };

                return objectEventised;
            }

            if (lowered.StartsWith("text/json"))
            {
                var objectEventised = new ObjectEvent<T>(cloudEventsVersion) { ContentType = contentType, Data = data };

                return objectEventised;
            }

            if (lowered.StartsWith("text/"))
            {
                var stringified = data as string;
                var stringEventised = new StringEvent(cloudEventsVersion) { ContentType = contentType, Data = stringified };

                return stringEventised as CloudEvent<T>;
            }

            var binarified = data as byte[];
            var binaryEventised = new BinaryEvent(cloudEventsVersion) { ContentType = contentType, Data = binarified };

            return binaryEventised as CloudEvent<T>;
        }
    }
}