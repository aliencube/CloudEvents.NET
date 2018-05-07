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

    /// <summary>
    /// This represents the fake CloudEvent entity.
    /// </summary>
    public class AnotherFakeEvent : CloudEvent<string>
    {
        /// <inheritdoc />
        protected override bool IsValidDataType(string data)
        {
            return true;
        }
    }

    /// <summary>
    /// This represents the fake CloudEvent entity.
    /// </summary>
    public class TheOtherFakeEvent : CloudEvent<FakeData>
    {
        /// <inheritdoc />
        protected override bool IsValidDataType(FakeData data)
        {
            return true;
        }
    }
}
