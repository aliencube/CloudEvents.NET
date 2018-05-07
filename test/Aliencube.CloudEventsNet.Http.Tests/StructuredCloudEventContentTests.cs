using System;

using Aliencube.CloudEventsNet.Abstractions;
using Aliencube.CloudEventsNet.Http.Abstractions;
using Aliencube.CloudEventsNet.Tests.Common;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

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

            var ce = new AnotherFakeEvent() { ContentType = "text/plain" };

            action = () => new StructuredCloudEventContent<string>(ce);
            action.Should().Throw<InvalidContentTypeException>();
        }
    }
}