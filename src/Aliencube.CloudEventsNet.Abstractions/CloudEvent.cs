using System;

using Newtonsoft.Json;

namespace Aliencube.CloudEventsNet.Abstractions
{

    /// <summary>
    /// This represents the CloudEvent entity. This MUST be inherited.
    /// </summary>
    /// <typeparam name="T">Type of data. This can be string, byte array or object.</typeparam>
    public abstract class CloudEvent<T>
    {
        /// <summary>
        /// Gets or sets the type of occurrence which has happened. Often this property is used for routing, observability, policy enforcement, etc.
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>REQUIRED</item>
        /// <item>MUST be a non-empty string</item>
        /// <item>SHOULD be prefixed with a reverse-DNS name. The prefixed domain dictates the organization which defines the semantics of this event type.</item>
        /// </list>
        /// </remarks>
        /// <example>
        /// <list type="bullet">
        /// <item><c>com.github.pull.create</c></item>
        /// </list>
        /// </example>
        [JsonProperty("eventType", Required = Required.Always)]
        public virtual string EventType { get; set; }

        /// <summary>
        /// Gets or sets the version of the eventType. This enables the interpretation of data by eventual consumers, requires the consumer to be knowledgeable about the producer.
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>OPTIONAL</item>
        /// <item>If present, MUST be a non-empty string</item>
        /// </list>
        /// </remarks>
        [JsonProperty("eventTypeVersion", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string EventTypeVersion { get; set; }

        /// <summary>
        /// Gets or sets the version of the CloudEvents specification which the event uses. This enables the interpretation of the context.
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>REQUIRED</item>
        /// <item>MUST be a non-empty string</item>
        /// </list>
        /// </remarks>
        [JsonProperty("cloudEventsVersion", Required = Required.Always)]
        public virtual string CloudEventsVersion { get; set; }

        /// <summary>
        /// Gets or sets the source URI. This describes the event producer. Often this will include information such as the type of the event source, the organization publishing the event, and some unique identifiers. The exact syntax and semantics behind the data encoded in the URI is event producer defined.
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>REQUIRED</item>
        /// </list>
        /// </remarks>
        [JsonProperty("source", Required = Required.Always)]
        public virtual Uri Source { get; set; }

        /// <summary>
        /// Gets or sets the ID of the event. The semantics of this string are explicitly undefined to ease the implementation of producers. Enables deduplication.
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>REQUIRED</item>
        /// <item>MUST be a non-empty string</item>
        /// <item>MUST be unique within the scope of the producer</item>
        /// </list>
        /// </remarks>
        /// <example>
        /// <list type="bullet">
        /// <item>A database commit ID</item>
        /// </list>
        /// </example>
        [JsonProperty("eventID", Required = Required.Always)]
        public virtual string EventId { get; set; }

        /// <summary>
        /// Gets or sets the timestamp of when the event happened.
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>OPTIONAL</item>
        /// <item>If present, MUST adhere to the format specified in RFC 3339 (https://tools.ietf.org/html/rfc3339).</item>
        /// </list>
        /// </remarks>
        [JsonProperty("eventTime", NullValueHandling = NullValueHandling.Ignore)]
        public virtual DateTimeOffset EventTime { get; set; }

        /// <summary>
        /// Gets or sets a link to the schema that the data attribute adheres to.
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>OPTIONAL</item>
        /// <item>If present, MUST adhere to the format specified in RFC 3986 (https://tools.ietf.org/html/rfc3986).</item>
        /// </list>
        /// </remarks>
        [JsonProperty("schemaURL", NullValueHandling = NullValueHandling.Ignore)]
        public virtual Uri SchemaUrl { get; set; }

        /// <summary>
        /// Gets or sets a content type that describes the data encoding format.
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>OPTIONAL</item>
        /// <item>If present, MUST adhere to the format specified in RFC 2046 (https://tools.ietf.org/html/rfc2046).</item>
        /// </list>
        /// </remarks>
        /// <example>
        /// <list type="bullet">
        /// <item>For Media Type examples see IANA Media Types (http://www.iana.org/assignments/media-types/media-types.xhtml).</item>
        /// </list>
        /// </example>
        [JsonProperty("contentType", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the extensions. This is for additional metadata and this does not have a mandated structure. This enables a place for custom fields a producer or middleware might want to include and provides a place to test metadata before adding them to the CloudEvents specification. See the Extensions document (https://github.com/cloudevents/spec/blob/master/extensions.md) for a list of possible properties.
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>OPTIONAL</item>
        /// <item>If present, MUST contain at least one entry</item>
        /// </list>
        /// </remarks>
        /// <example>
        /// <list type="bullet">
        /// <item>authorization data</item>
        /// </list>
        /// </example>
        [JsonProperty("extensions", NullValueHandling = NullValueHandling.Ignore)]
        public virtual object Extensions { get; set; }

        private T _data;

        /// <summary>
        /// Gets or sets the event payload. The payload depends on the eventType, schemaURL and eventTypeVersion, the payload is encoded into a media format which is specified by the <c>contentType</c> attribute (e.g. application/json).
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>OPTIONAL</item>
        /// </list>
        /// </remarks>
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public virtual T Data
        {
            get
            {
                return this._data;
            }
            set
            {
                if (!this.IsValidDataType(value))
                {
                    throw new InvalidDataTypeException();
                }

                this._data = value;
            }
        }

        /// <summary>
        /// Checks whether the data has a valid type or not.
        /// </summary>
        /// <param name="data">Data instance.</param>
        /// <returns>Returns <c>True</c>, if data type is valid; otherwise returns <c>False</c>.</returns>
        protected abstract bool IsValidDataType(T data);
    }
}