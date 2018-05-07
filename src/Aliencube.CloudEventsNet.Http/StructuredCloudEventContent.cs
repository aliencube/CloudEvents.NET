using System;
using System.Net.Http.Headers;
using System.Text;

using Aliencube.CloudEventsNet.Abstractions;
using Aliencube.CloudEventsNet.Http.Abstractions;

using Newtonsoft.Json;

namespace Aliencube.CloudEventsNet.Http
{
    /// <summary>
    /// This represents the CloudEvent content entity as structured mode.
    /// </summary>
    /// <typeparam name="T">Type of CloudEvent data.</typeparam>
    public class StructuredCloudEventContent<T> : CloudEventContent<T>
    {
        private const string DefaultContentType = "application/cloudevents+json";

        /// <summary>
        /// Initializes a new instance of the <see cref="StructuredCloudEventContent{T}"/> class.
        /// </summary>
        /// <param name="ce"></param>
        public StructuredCloudEventContent(CloudEvent<T> ce)
            : base(ce, GetContentByteArray(ce))
        {
        }

        /// <inheritdoc />
        protected override MediaTypeHeaderValue GetContentTypeHeader()
        {
            return new MediaTypeHeaderValue(this.CloudEvent.ContentType ?? DefaultContentType) { CharSet = "utf-8" };
        }

        private static byte[] GetContentByteArray(CloudEvent<T> ce)
        {
            if (ce == null)
            {
                throw new ArgumentNullException(nameof(ce));
            }

            if (!IsStructuredCloudEventContentType(ce))
            {
                throw new InvalidContentTypeException();
            }

            var serialised = JsonConvert.SerializeObject(ce);

            return Encoding.UTF8.GetBytes(serialised);
        }
    }
}