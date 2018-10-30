using System;

using Aliencube.CloudEventsNet.Tests.Common;

using FluentAssertions;
using FluentAssertions.Common;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

namespace Aliencube.CloudEventsNet.Abstractions.Tests
{
    [TestClass]
    public class CloudEventTests
    {
        [TestMethod]
        public void Given_ClassType_Should_BeAbstract()
        {
            typeof(CloudEvent<object>).Should().BeAbstract();
        }

        [TestMethod]
        public void Given_ClassType_Should_HaveProperties()
        {
            typeof(CloudEvent<int>).Should().HaveProperty<string>("EventType")
                                            .Subject.Should().BeDecoratedWith<JsonPropertyAttribute>().And
                                            .Subject.Should().BeVirtual();
            typeof(CloudEvent<int>).Should().HaveProperty<string>("EventTypeVersion")
                                            .Subject.Should().BeDecoratedWith<JsonPropertyAttribute>().And
                                            .Subject.Should().BeVirtual();
            typeof(CloudEvent<int>).Should().HaveProperty<string>("CloudEventsVersion")
                                            .Subject.Should().BeDecoratedWith<JsonPropertyAttribute>().And
                                            .Subject.Should().BeVirtual();
            typeof(CloudEvent<int>).Should().HaveProperty<string>("Source")
                                            .Subject.Should().BeDecoratedWith<JsonPropertyAttribute>().And
                                            .Subject.Should().BeVirtual();
            typeof(CloudEvent<int>).Should().HaveProperty<string>("EventId")
                                            .Subject.Should().BeDecoratedWith<JsonPropertyAttribute>().And
                                            .Subject.Should().BeVirtual();
            typeof(CloudEvent<int>).Should().HaveProperty<DateTimeOffset?>("EventTime")
                                            .Subject.Should().BeDecoratedWith<JsonPropertyAttribute>().And
                                            .Subject.Should().BeVirtual();
            typeof(CloudEvent<int>).Should().HaveProperty<Uri>("SchemaUrl")
                                            .Subject.Should().BeDecoratedWith<JsonPropertyAttribute>().And
                                            .Subject.Should().BeVirtual();
            typeof(CloudEvent<int>).Should().HaveProperty<string>("ContentType")
                                            .Subject.Should().BeDecoratedWith<JsonPropertyAttribute>().And
                                            .Subject.Should().BeVirtual();
            typeof(CloudEvent<int>).Should().HaveProperty<object>("Extensions")
                                            .Subject.Should().BeDecoratedWith<JsonPropertyAttribute>().And
                                            .Subject.Should().BeVirtual();
            typeof(CloudEvent<int>).Should().HaveProperty<int>("Data")
                                            .Subject.Should().BeDecoratedWith<JsonPropertyAttribute>().And
                                            .Subject.Should().BeVirtual();
        }

        [TestMethod]
        public void Given_InvalidContentType_When_Instantiated_Should_ThrowException()
        {
            var ev = new FakeEvent();

            Action action = () => ev.ContentType = "text/csv";

            action.Should().Throw<InvalidContentTypeException>();
        }

        [TestMethod]
        public void Given_InvalidDataType_When_Instantiated_Should_ThrowException()
        {
            var ev = new FakeEvent();

            Action action = () => ev.Data = false;

            action.Should().Throw<InvalidDataTypeException>();
        }

        [TestMethod]
        public void Given_ValidContentType_When_Instantiated_Should_BeOk()
        {
            var ev = new FakeEvent();

            ev.ContentType = "text/plain";

            ev.ContentType.Should().Be("text/plain");
        }

        [TestMethod]
        public void Given_ValidDataType_When_Instantiated_Should_BeOk()
        {
            var ev = new FakeEvent();

            ev.Data = true;

            ev.Data.Should().BeTrue();
        }

        [TestMethod]
        public void Given_ClassType_Should_HaveMethods()
        {
            typeof(CloudEvent<int>).Should().HaveMethod("IsValidContentType", new[] { typeof(string) })
                                            .Subject.Should().BeVirtual().And
                                            .Subject.Should().HaveAccessModifier(CSharpAccessModifier.Protected).And
                                            .Subject.Should().Return<bool>();
            typeof(CloudEvent<int>).Should().HaveMethod("IsValidDataType", new[] { typeof(int) })
                                            .Subject.Should().BeVirtual().And
                                            .Subject.Should().HaveAccessModifier(CSharpAccessModifier.Protected).And
                                            .Subject.Should().Return<bool>();
        }
    }
}