using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;

using Aliencube.CloudEventsNet.Abstractions;
using Aliencube.CloudEventsNet.Tests.Common;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aliencube.CloudEventsNet.Http.Abstractions.Tests
{
    [TestClass]
    public class CloudEventContentTests
    {
        [TestMethod]
        public void Given_ClassType_Should_BeAbstract()
        {
            typeof(CloudEventContent<object>).Should().BeDerivedFrom<ByteArrayContent>();
        }

        [TestMethod]
        public void Given_Null_When_Instantiated_Should_ThrowException()
        {
            CloudEvent<object> ce = null;

            Action action = () => new FakeCloudEventContent<object>(ce);

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Given_CloudEvent_When_Instantiated_Should_HaveProperty()
        {
            var data = "hello world";

            var ev = new AnotherFakeEvent();
            ev.EventType = "com.example.someevent";
            ev.CloudEventsVersion = "0.1";
            ev.Source = new Uri("http://localhost");
            ev.EventId = Guid.NewGuid().ToString();
            ev.Data = data;

            var content = new FakeCloudEventContent<string>(ev);

            var pi = typeof(FakeCloudEventContent<string>).GetProperty("CloudEvent", BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetProperty);

            pi.GetValue(content).Should().NotBeNull();
            pi.GetValue(content).Should().Be(ev);
        }

        [TestMethod]
        public void Given_ClassType_When_Executed_Should_ReturnResult()
        {
            var data = "hello world";

            var ev = new AnotherFakeEvent();
            ev.EventType = "com.example.someevent";
            ev.CloudEventsVersion = "0.1";
            ev.Source = new Uri("http://localhost");
            ev.EventId = Guid.NewGuid().ToString();
            ev.Data = data;

            var mi = typeof(CloudEventContent<string>).GetMethod("IsStructuredCloudEventContentType", BindingFlags.IgnoreCase | BindingFlags.Static | BindingFlags.NonPublic);

            ev.ContentType = "application/json";
            var result = mi.Invoke(null, new[] { ev });
            ((bool)result).Should().BeTrue();

            ev.ContentType = "application/cloudevents+json";
            result = mi.Invoke(null, new[] { ev });
            ((bool)result).Should().BeTrue();

            ev.ContentType = "text/plain";
            result = mi.Invoke(null, new[] { ev });
            ((bool)result).Should().BeFalse();
        }

        [TestMethod]
        public void Given_CloudEvent_When_Instantiated_Should_HaveHeaders()
        {
            var data = "hello world";

            var ev = new AnotherFakeEvent();
            ev.EventType = "com.example.someevent";
            ev.CloudEventsVersion = "0.1";
            ev.Source = new Uri("http://localhost");
            ev.EventId = Guid.NewGuid().ToString();
            ev.Data = data;

            var content = new FakeCloudEventContent<string>(ev);

            content.Headers.Should().Contain(p => p.Key.Equals("CE-EventType", StringComparison.CurrentCultureIgnoreCase));
            content.Headers.Should().Contain(p => p.Key.Equals("CE-CloudEventsVersion", StringComparison.CurrentCultureIgnoreCase));
            content.Headers.Should().Contain(p => p.Key.Equals("CE-Source", StringComparison.CurrentCultureIgnoreCase));
            content.Headers.Should().Contain(p => p.Key.Equals("CE-EventID", StringComparison.CurrentCultureIgnoreCase));

            content.Headers.Should().NotContain(p => p.Key.StartsWith("CE-X-", StringComparison.CurrentCultureIgnoreCase));
        }

        [TestMethod]
        public void Given_CloudEvent_When_Instantiated_Should_HaveExtensionHeaders()
        {
            var data = "hello world";

            var ev = new AnotherFakeEvent();
            ev.EventType = "com.example.someevent";
            ev.CloudEventsVersion = "0.1";
            ev.Source = new Uri("http://localhost");
            ev.EventId = Guid.NewGuid().ToString();
            ev.Data = data;
            ev.Extensions = new Dictionary<string, object>() { { "key1", "value1" } };

            var content = new FakeCloudEventContent<string>(ev);

            content.Headers.Should().Contain(p => p.Key.Equals("CE-X-key1", StringComparison.CurrentCultureIgnoreCase));
        }
    }
}