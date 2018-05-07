using System.Net.Http.Headers;
using System.Text;

using Aliencube.CloudEventsNet.Abstractions;
using Aliencube.CloudEventsNet.Http.Abstractions;

using Newtonsoft.Json;

namespace Aliencube.CloudEventsNet.Tests.Common
{
    /// <summary>
    /// This represents the fake CloudEventContent entity.
    /// </summary>
    public class FakeCloudEventContent<T> : CloudEventContent<T>
    {
        private const string DefaultContentType = "application/octet-stream";

        /// <summary>
        /// Initializes a new instance of the <see cref="CloudEventContent{T}"/> class.
        /// </summary>
        /// <param name="content">Content as byte array.</param>
        public FakeCloudEventContent(CloudEvent<T> ce) : base(ce, GetContentByteArray(ce))
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
                return null;
            }

            var serialised = JsonConvert.SerializeObject(ce);

            return Encoding.UTF8.GetBytes(serialised);
        }
    }
}