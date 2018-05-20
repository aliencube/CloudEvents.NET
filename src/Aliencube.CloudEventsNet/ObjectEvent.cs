using Aliencube.CloudEventsNet.Abstractions;

namespace Aliencube.CloudEventsNet
{
    /// <summary>
    /// This represents the CloudEvent entity containing object data.
    /// </summary>
    public class ObjectEvent : ObjectEvent<object>
    {
    }

    /// <summary>
    /// This represents the CloudEvent entity containing object data.
    /// </summary>
    /// <typeparam name="T">Type of data object.</typeparam>
    public class ObjectEvent<T> : CloudEvent<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectEvent"/> class.
        /// </summary>
        /// <param name="cloudEventsVersion"><see cref="CloudEventsVersion"/> value.</param>
        public ObjectEvent(string cloudEventsVersion = CloudEventsNet.CloudEventsVersion.Version01)
        {
            this.CloudEventsVersion = cloudEventsVersion;

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