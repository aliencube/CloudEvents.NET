using System;
using System.Threading.Tasks;

using Aliencube.CloudEventsNet.Abstractions;
using Aliencube.CloudEventsNet.Http.Abstractions;
using Aliencube.CloudEventsNet.Tests.Common;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

namespace Aliencube.CloudEventsNet.Http.Tests
{
    [TestClass]
    public class StructuredCloudEventContentTests
    {
        [TestMethod]
        public void Given_ClassType_Should_BeDerivedFrom()
        {
            typeof(StructuredCloudEventContent<string>).Should().BeDerivedFrom<CloudEventContent<string>>();
        }

        [TestMethod]
        public void Given_NoParameter_When_Instantiated_Should_ThrowException()
        {
            Action action = () => new StructuredCloudEventContent<string>(null);
            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Given_Parameter_When_Instantiated_Should_ThrowException()
        {
            var ce = new AnotherFakeEvent();
            ce.ContentType = "text/plain";

            Action action = () => new StructuredCloudEventContent<string>(ce);
            action.Should().Throw<InvalidContentTypeException>();
        }

        [TestMethod]
        public void Given_Parameter_When_Instantiated_Should_HaveContentType()
        {
            var contentType = "application/json";
            var charset = "utf-8";
            var data = new FakeData() { FakeProperty = "hello world" };

            var ce = new TheOtherFakeEvent();
            ce.EventType = "com.example.someevent";
            ce.CloudEventsVersion = "0.1";
            ce.Source = (new Uri("http://localhost")).ToString();
            ce.EventId = Guid.NewGuid().ToString();
            ce.ContentType = contentType;
            ce.Data = data;

            var content = new StructuredCloudEventContent<FakeData>(ce);

            content.Headers.ContentType.MediaType.Should().Be(contentType);
            content.Headers.ContentType.CharSet.Should().Be(charset);
        }

        [TestMethod]
        public async Task Given_Parameter_When_Instantiated_Should_HaveContent()
        {
            var contentType = "application/json";
            var charset = "utf-8";
            var data = new FakeData() { FakeProperty = "hello world" };

            var ce = new TheOtherFakeEvent();
            ce.EventType = "com.example.someevent";
            ce.CloudEventsVersion = "0.1";
            ce.Source = (new Uri("http://localhost")).ToString();
            ce.EventId = Guid.NewGuid().ToString();
            ce.ContentType = contentType;
            ce.Data = data;

            var serialised = JsonConvert.SerializeObject(ce);

            var content = new StructuredCloudEventContent<FakeData>(ce);

            var result = await content.ReadAsStringAsync().ConfigureAwait(false);

            result.Should().Be(serialised);
        }
    }
}