using System;
using System.Threading.Tasks;

using Aliencube.CloudEventsNet.Abstractions;
using Aliencube.CloudEventsNet.Http.Abstractions;
using Aliencube.CloudEventsNet.Tests.Common;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aliencube.CloudEventsNet.Http.Tests
{
    [TestClass]
    public class BinaryCloudEventContentTests
    {
        [TestMethod]
        public void Given_ClassType_Should_BeDerivedFrom()
        {
            typeof(BinaryCloudEventContent<string>).Should().BeDerivedFrom<CloudEventContent<string>>();
        }

        [TestMethod]
        public void Given_NoParameter_When_Instantiated_Should_ThrowException()
        {
            Action action = () => new BinaryCloudEventContent<string>(null);
            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Given_Parameter_When_Instantiated_Should_ThrowException()
        {
            var data = "hello world";

            var ce = new AnotherFakeEvent();

            Action action = () => new BinaryCloudEventContent<string>(ce);
            action.Should().Throw<InvalidOperationException>();

            ce.ContentType = "application/json";
            ce.Data = data;

            action = () => new BinaryCloudEventContent<string>(ce);
            action.Should().Throw<InvalidContentTypeException>();
        }

        [TestMethod]
        public void Given_Parameter_When_Instantiated_Should_HaveContentType()
        {
            var contentType = "text/plain";
            var charset = "utf-8";
            var data = "hello world";

            var ce = new AnotherFakeEvent();
            ce.EventType = "com.example.someevent";
            ce.Source = (new Uri("http://localhost")).ToString();
            ce.EventId = Guid.NewGuid().ToString();
            ce.ContentType = contentType;
            ce.Data = data;

            var content = new BinaryCloudEventContent<string>(ce);

            content.Headers.ContentType.MediaType.Should().Be(contentType);
            content.Headers.ContentType.CharSet.Should().Be(charset);
        }

        [TestMethod]
        public async Task Given_Parameter_When_Instantiated_Should_HaveContent()
        {
            var contentType = "text/plain";
            var data = "hello world";

            var ce = new AnotherFakeEvent();
            ce.EventType = "com.example.someevent";
            ce.Source = (new Uri("http://localhost")).ToString();
            ce.EventId = Guid.NewGuid().ToString();
            ce.ContentType = contentType;
            ce.Data = data;

            var content = new BinaryCloudEventContent<string>(ce);

            var result = await content.ReadAsStringAsync().ConfigureAwait(false);

            result.Should().Be(data);
        }
    }
}