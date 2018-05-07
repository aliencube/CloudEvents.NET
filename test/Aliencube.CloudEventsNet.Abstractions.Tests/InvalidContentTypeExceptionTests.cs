using System;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aliencube.CloudEventsNet.Abstractions.Tests
{
    [TestClass]
    public class InvalidContentTypeExceptionTests
    {
        [TestMethod]
        public void Given_ClassType_Should_BeDerivedType()
        {
            typeof(InvalidContentTypeException).Should().BeDerivedFrom<Exception>();
        }

        [TestMethod]
        public void Given_ClassType_When_Instantiated_Should_HaveProperties()
        {
            typeof(InvalidContentTypeException).Should().HaveDefaultConstructor();
            typeof(InvalidContentTypeException).Should().HaveConstructor(new[] { typeof(string) });
            typeof(InvalidContentTypeException).Should().HaveConstructor(new[] { typeof(string), typeof(Exception) });
        }

        [TestMethod]
        public void Given_NoMessage_When_Instantiated_Should_HaveDefaultMessage()
        {
            var ex = new InvalidContentTypeException();

            ex.Message.Should().Be("Invalid CloudEvent content type.");
        }

        [TestMethod]
        public void Given_Message_When_Instantiated_Should_HaveMessage()
        {
            var msg = "Hello World";

            var ex = new InvalidContentTypeException(msg);

            ex.Message.Should().Be(msg);
        }

        [TestMethod]
        public void Given_Message_And_Exception_When_Instantiated_Should_HaveMessage_And_Exception()
        {
            var msg = "Hello World";
            var innerex = new Exception();

            var ex = new InvalidContentTypeException(msg, innerex);

            ex.Message.Should().Be(msg);
            ex.InnerException.Should().Be(innerex);
        }
    }
}