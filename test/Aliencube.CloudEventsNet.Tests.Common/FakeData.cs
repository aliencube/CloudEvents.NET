using Newtonsoft.Json;

namespace Aliencube.CloudEventsNet.Tests.Common
{
    /// <summary>
    /// This represents the fake data entity.
    /// </summary>
    public class FakeData
    {
        /// <summary>
        /// Gets or sets the fake property value.
        /// </summary>
        [JsonProperty("fakeProperty")]
        public string FakeProperty { get; set; }
    }
}
