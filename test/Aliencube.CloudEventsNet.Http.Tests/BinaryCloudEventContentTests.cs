using System;

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
            var data = "hello world";

            Action action = () => new BinaryCloudEventContent<string>(null);
            action.Should().Throw<ArgumentNullException>();

            var ce = new AnotherFakeEvent() { ContentType = "application/json" };

            action = () => new BinaryCloudEventContent<string>(ce);
            action.Should().Throw<InvalidOperationException>();

            ce.Data = data;

            action = () => new BinaryCloudEventContent<string>(ce);
            action.Should().Throw<InvalidContentTypeException>();
        }
    }
}