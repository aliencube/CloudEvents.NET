using System;
using System.Net.Http.Headers;
using System.Text;

using Aliencube.CloudEventsNet.Abstractions;
using Aliencube.CloudEventsNet.Http.Abstractions;

using Newtonsoft.Json;

namespace Aliencube.CloudEventsNet.Http
{
    /// <summary>
    /// This represents the CloudEvent content entity as binary mode.
    /// </summary>
    /// <typeparam name="T">Type of CloudEvent data.</typeparam>
    public class BinaryCloudEventContent<T> : CloudEventContent<T>
    {
        private const string DefaultContentType = "text/plain";

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryCloudEventContent{T}"/> class.
        /// </summary>
        /// <param name="ce"></param>
        public BinaryCloudEventContent(CloudEvent<T> ce)
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

            if (ce.Data == null)
            {
                throw new InvalidOperationException("Data in CloudEvent can't be null");
            }

            if (IsStructuredCloudEventContentType(ce))
            {
                throw new InvalidContentTypeException();
            }

            if (ce.Data is string)
            {
                return Encoding.UTF8.GetBytes(ce.Data as string);
            }

            if (ce.Data is byte[])
            {
                return ce.Data as byte[];
            }

            var serialised = JsonConvert.SerializeObject(ce.Data);

            return Encoding.UTF8.GetBytes(serialised);
        }
    }
}