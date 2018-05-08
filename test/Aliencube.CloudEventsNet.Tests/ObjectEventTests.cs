using System;

using Aliencube.CloudEventsNet.Abstractions;
using Aliencube.CloudEventsNet.Tests.Common;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Aliencube.CloudEventsNet.Tests
{
    [TestClass]
    public class ObjectEventTests
    {
        [TestMethod]
        public void Given_ClassType_Should_BeDerivedFrom()
        {
            typeof(ObjectEvent).Should().BeDerivedFrom<CloudEvent<object>>();
            typeof(ObjectEvent<FakeData>).Should().BeDerivedFrom<CloudEvent<FakeData>>();
        }

        [TestMethod]
        public void Given_NoVersion_Should_HaveDefaultCloudEventsVersion()
        {
            var ev = new ObjectEvent<FakeData>();

            ev.CloudEventsVersion.Should().Be(CloudEventsVersion.Version01);
        }

        [TestMethod]
        public void Given_Version_Should_HaveVersion()
        {
            var version = "v1";

            var ev = new ObjectEvent<FakeData>(version);

            ev.CloudEventsVersion.Should().Be(version);
        }

        [TestMethod]
        public void Given_InvalidContentType_When_DataIsSet_Should_ThrowException()
        {
            var data = new FakeData() { FakeProperty = "hello world" };

            var ev = new ObjectEvent<FakeData>();

            ev.ContentType = "text/json";
            Action action = () => ev.Data = data;
            action.Should().Throw<InvalidDataTypeException>();
        }

        [TestMethod]
        public void Given_ValidContentType_When_DataIsSet_Should_BeOk()
        {
            var data = new FakeData() { FakeProperty = "hello world" };

            var ev = new ObjectEvent<FakeData>();

            ev.ContentType = "application/json";
            ev.Data = data;

            ev.Data.Should().Be(data);
        }

        [TestMethod]
        public void Given_NoRequiredProperties_When_Serialised_Should_ThrowException()
        {
            var ev = new ObjectEvent<FakeData>();

            Action action = () => JsonConvert.SerializeObject(ev);
            action.Should().Throw<JsonSerializationException>();

            ev.EventType = null;
            ev.Source = (new Uri("http://localhost")).ToString();
            ev.EventId = Guid.NewGuid().ToString();
            action = () => JsonConvert.SerializeObject(ev);
            action.Should().Throw<JsonSerializationException>();

            ev.EventType = "com.example.someevent";
            ev.Source = null;
            ev.EventId = Guid.NewGuid().ToString();
            action = () => JsonConvert.SerializeObject(ev);
            action.Should().Throw<JsonSerializationException>();

            ev.EventType = "com.example.someevent";
            ev.Source = (new Uri("http://localhost")).ToString();
            ev.EventId = null;
            action = () => JsonConvert.SerializeObject(ev);
            action.Should().Throw<JsonSerializationException>();
        }

        [TestMethod]
        public void Given_RequiredProperties_When_Serialised_Should_BeOk()
        {
            var ev = new ObjectEvent<FakeData>();

            ev.EventType = "com.example.someevent";
            ev.Source = (new Uri("http://localhost")).ToString();
            ev.EventId = Guid.NewGuid().ToString();

            var serialised = string.Empty;
            Action action = () => serialised = JsonConvert.SerializeObject(ev);

            action.Should().NotThrow<Exception>();
        }

        [TestMethod]
        public void Given_Data_When_Serialised_Should_BeOk()
        {
            var data = new FakeData() { FakeProperty = "hello world" };

            var ev = new ObjectEvent<FakeData>();

            ev.EventType = "com.example.someevent";
            ev.Source = (new Uri("http://localhost")).ToString();
            ev.EventId = Guid.NewGuid().ToString();
            ev.ContentType = "application/json";
            ev.Data = data;

            var serialised = JsonConvert.SerializeObject(ev);
            var deserialised = JsonConvert.DeserializeObject<JObject>(serialised);

            deserialised["data"].Should().NotBeNull();
            deserialised["data"]["fakeProperty"].ToString().Should().Be(data.FakeProperty);
        }
    }
}