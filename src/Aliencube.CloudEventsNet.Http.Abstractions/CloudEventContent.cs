using System;
using System.Net.Http;
using System.Net.Http.Headers;

using Aliencube.CloudEventsNet.Abstractions;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Aliencube.CloudEventsNet.Http.Abstractions
{
    /// <summary>
    /// This represents the HTTP content entity for CloudEvent. This MUST be inherited.
    /// </summary>
    public abstract class CloudEventContent<T> : ByteArrayContent
    {
        private const string CeEventTypeHeaderKey = "CE-EventType";
        private const string CeEventTypeVersionHeaderKey = "CE-EventTypeVersion";
        private const string CeCloudEventsVersionHeaderKey = "CE-CloudEventsVersion";
        private const string CeSourceHeaderKey = "CE-Source";
        private const string CeEventIdHeaderKey = "CE-EventID";
        private const string CeEventTimeHeaderKey = "CE-EventTime";
        private const string CeSchemaUrlHeaderKey = "CE-SchemaUrl";

        /// <summary>
        /// Initializes a new instance of the <see cref="CloudEventContent{T}"/> class.
        /// </summary>
        /// <param name="content">Content as byte array.</param>
        protected CloudEventContent(byte[] content)
            : base(content)
        {
            this.SetHeaders();
        }

        /// <summary>
        /// Gets or sets the <see cref="CloudEvent{T}"/> instance.
        /// </summary>
        protected CloudEvent<T> CloudEvent { get; set; }

        /// <summary>
        /// Checks whether the content type of the <see cref="CloudEvent{T}"/> instance indicates structured or not.
        /// </summary>
        /// <param name="ce"><see cref="CloudEvent{T}"/> instance.</param>
        /// <returns>Returns <c>True</c>, if the content type indicates structured; otherwise returns <c>False</c>.</returns>
        protected static bool IsStructuredCloudEventContentType(CloudEvent<T> ce)
        {
            if (ce.ContentType.Equals("application/json", StringComparison.CurrentCultureIgnoreCase))
            {
                return true;
            }

            if (ce.ContentType.EndsWith("+json", StringComparison.CurrentCultureIgnoreCase))
            {
                return true;
            }

            return false;
        }

        private void SetHeaders()
        {
            this.Headers.ContentType = new MediaTypeHeaderValue(this.CloudEvent.ContentType) { CharSet = "utf-8" };

            this.SetCloudEventHeaders();
            this.SetCloudEventExtensionHeaders();
        }

        private void SetCloudEventHeaders()
        {
            this.Headers.Add(CeEventTypeHeaderKey.ToUpperInvariant(), this.CloudEvent.EventType);
            this.Headers.Add(CeEventTypeVersionHeaderKey.ToUpperInvariant(), this.CloudEvent.EventTypeVersion);
            this.Headers.Add(CeCloudEventsVersionHeaderKey.ToUpperInvariant(), this.CloudEvent.CloudEventsVersion);
            this.Headers.Add(CeSourceHeaderKey.ToUpperInvariant(), this.CloudEvent.Source.ToString());
            this.Headers.Add(CeEventIdHeaderKey.ToUpperInvariant(), this.CloudEvent.EventId);
            this.Headers.Add(CeEventTimeHeaderKey.ToUpperInvariant(), this.CloudEvent.EventTime.ToString("yyyy-MM-ddTHH:mm:ss.fffffffzzz"));
            this.Headers.Add(CeSchemaUrlHeaderKey.ToUpperInvariant(), this.CloudEvent.SchemaUrl.ToString());
        }

        private void SetCloudEventExtensionHeaders()
        {
            var serislised = JsonConvert.SerializeObject(this.CloudEvent.Extensions);
            var deserialised = JsonConvert.DeserializeObject<JObject>(serislised);

            foreach (var jp in deserialised.Properties())
            {
                this.Headers.Add($"CE-X-{jp.Name.ToUpperInvariant()}", deserialised[jp.Name].ToString());
            }
        }
    }
}