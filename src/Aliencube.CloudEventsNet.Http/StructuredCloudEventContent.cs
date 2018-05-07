using System;
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
        /// <summary>
        /// Initializes a new instance of the <see cref="StructuredCloudEventContent{T}"/> class.
        /// </summary>
        /// <param name="ce"></param>
        public StructuredCloudEventContent(CloudEvent<T> ce)
            : base(GetContentByteArray(ce))
        {
            this.CloudEvent = ce ?? throw new ArgumentNullException(nameof(ce));
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