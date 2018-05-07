using System;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aliencube.CloudEventsNet.Abstractions.Tests
{
    [TestClass]
    public class InvalidDataTypeExceptionTests
    {
        [TestMethod]
        public void Given_ClassType_Should_BeDerivedType()
        {
            typeof(InvalidDataTypeException).Should().BeDerivedFrom<Exception>();
        }

        [TestMethod]
        public void Given_ClassType_When_Instantiated_Should_HaveProperties()
        {
            typeof(InvalidDataTypeException).Should().HaveDefaultConstructor();
            typeof(InvalidDataTypeException).Should().HaveConstructor(new[] { typeof(string) });
            typeof(InvalidDataTypeException).Should().HaveConstructor(new[] { typeof(string), typeof(Exception) });
        }

        [TestMethod]
        public void Given_NoMessage_When_Instantiated_Should_HaveDefaultMessage()
        {
            var ex = new InvalidDataTypeException();

            ex.Message.Should().Be("Invalid CloudEvent data type.");
        }

        [TestMethod]
        public void Given_Message_When_Instantiated_Should_HaveMessage()
        {
            var msg = "Hello World";

            var ex = new InvalidDataTypeException(msg);

            ex.Message.Should().Be(msg);
        }

        [TestMethod]
        public void Given_Message_And_Exception_When_Instantiated_Should_HaveMessage_And_Exception()
        {
            var msg = "Hello World";
            var innerex = new Exception();

            var ex = new InvalidDataTypeException(msg, innerex);

            ex.Message.Should().Be(msg);
            ex.InnerException.Should().Be(innerex);
        }
    }
}