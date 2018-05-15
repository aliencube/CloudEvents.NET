using System.Text;

using Aliencube.CloudEventsNet.Tests.Common;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

namespace Aliencube.CloudEventsNet.Tests
{
    [TestClass]
    public class CloudEventFactoryTests
    {
        [TestMethod]
        public void Given_ContentTypeOfString_And_Data_Should_ReturnResult()
        {
            var ev = CloudEventFactory.Create("text/plain", "hello world");

            ev.Should().BeAssignableTo<StringEvent>();
        }

        [TestMethod]
        public void Given_ContentTypeOfObject_And_Data_Should_ReturnResult()
        {
            var data = new FakeData() { FakeProperty = "hello world" };

            var ev = CloudEventFactory.Create("text/json", data);
            ev.Should().BeAssignableTo<ObjectEvent<FakeData>>();

            ev = CloudEventFactory.Create("application/json", data);
            ev.Should().BeAssignableTo<ObjectEvent<FakeData>>();

            ev = CloudEventFactory.Create("application/json-seq", data);
            ev.Should().BeAssignableTo<ObjectEvent<FakeData>>();

            ev = CloudEventFactory.Create("application/cloudevents+json", data);
            ev.Should().BeAssignableTo<ObjectEvent<FakeData>>();

            ev = CloudEventFactory.Create("application/geo+json-seq", data);
            ev.Should().BeAssignableTo<ObjectEvent<FakeData>>();
        }

        [TestMethod]
        public void Given_ContentTypeOfBinary_And_Data_Should_ReturnResult()
        {
            var data = new FakeData() { FakeProperty = "hello world" };
            var serialised = JsonConvert.SerializeObject(data);
            var binarified = Encoding.UTF8.GetBytes(serialised);

            var ev = CloudEventFactory.Create("application/octet-stream", binarified);

            ev.Should().BeAssignableTo<BinaryEvent>();
        }
    }
}