using System.Linq;
using System.Reflection;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aliencube.CloudEventsNet.Tests
{
    [TestClass]
    public class CloudEventsVersionTests
    {
        [TestMethod]
        public void Given_ClassType_Should_BeStatic()
        {
            typeof(CloudEventsVersion).Should().BeStatic();
        }

        [TestMethod]
        public void Given_ClassType_Should_HaveConstant()
        {
            var mi = typeof(CloudEventsVersion).GetMembers().SingleOrDefault(p => p.Name.Equals("Version01"));

            mi.Should().NotBeNull();
        }
    }
}