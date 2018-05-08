using System;
using System.Text;

using Aliencube.CloudEventsNet.Abstractions;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Aliencube.CloudEventsNet.Tests
{
    [TestClass]
    public class BinaryEventTests
    {
        [TestMethod]
        public void Given_ClassType_Should_BeDerivedFrom()
        {
            typeof(BinaryEvent).Should().BeDerivedFrom<CloudEvent<byte[]>>();
        }

        [TestMethod]
        public void Given_NoVersion_Should_HaveDefaultCloudEventsVersion()
        {
            var ev = new BinaryEvent();

            ev.CloudEventsVersion.Should().Be(CloudEventsVersion.Version01);
        }

        [TestMethod]
        public void Given_Version_Should_HaveVersion()
        {
            var version = "v1";

            var ev = new BinaryEvent(version);

            ev.CloudEventsVersion.Should().Be(version);
        }

        [TestMethod]
        public void Given_InvalidContentType_When_DataIsSet_Should_ThrowException()
        {
            var content = "hello world";
            var data = Encoding.UTF8.GetBytes(content);

            var ev = new BinaryEvent();

            ev.ContentType = "text/plain";
            Action action = () => ev.Data = data;
            action.Should().Throw<InvalidDataTypeException>();

            ev.ContentType = "application/json";
            action = () => ev.Data = data;
            action.Should().Throw<InvalidDataTypeException>();

            ev.ContentType = "application/cloudevents+json";
            action = () => ev.Data = data;
            action.Should().Throw<InvalidDataTypeException>();
        }

        [TestMethod]
        public void Given_ValidContentType_When_DataIsSet_Should_BeOk()
        {
            var content = "hello world";
            var data = Encoding.UTF8.GetBytes(content);

            var ev = new BinaryEvent();

            ev.ContentType = "application/octet-stream";
            ev.Data = data;

            var result = Encoding.UTF8.GetString(ev.Data);

            result.Should().Be(content);
        }

        [TestMethod]
        public void Given_NoRequiredProperties_When_Serialised_Should_ThrowException()
        {
            var ev = new BinaryEvent();

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
            var ev = new BinaryEvent();

            ev.EventType = "com.example.someevent";
            ev.Source = (new Uri("http://localhost")).ToString();
            ev.EventId = Guid.NewGuid().ToString();

            var serialised = string.Empty;
            Action action = () => serialised = JsonConvert.SerializeObject(ev);

            action.Should().NotThrow<Exception>();
        }

        [TestMethod]
        public void Given_Data_When_Serialised_Should_BeBase64EncodedData()
        {
            var content = "hello world";
            var data = Encoding.UTF8.GetBytes(content);
            var encoded = Convert.ToBase64String(data);

            var ev = new BinaryEvent();

            ev.EventType = "com.example.someevent";
            ev.Source = (new Uri("http://localhost")).ToString();
            ev.EventId = Guid.NewGuid().ToString();
            ev.ContentType = "application/octet-stream";
            ev.Data = data;

            var serialised = JsonConvert.SerializeObject(ev);
            var deserialised = JsonConvert.DeserializeObject<JObject>(serialised);

            deserialised["data"].Should().NotBeNull();
            deserialised["data"].ToString().Should().Be(encoded);
        }
    }
}