using Aliencube.CloudEventsNet.Abstractions;

namespace Aliencube.CloudEventsNet.Tests.Common
{
    /// <summary>
    /// This represents the fake CloudEvent entity.
    /// </summary>
    public class FakeEvent : CloudEvent<bool>
    {
        /// <inheritdoc />
        protected override bool IsValidDataType(bool data)
        {
            return data;
        }
    }
}
