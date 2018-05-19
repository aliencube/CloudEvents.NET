using System;
using System.Reflection;

using Aliencube.CloudEventsNet.Tests.Common;

using FluentAssertions;
using FluentAssertions.Common;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aliencube.CloudEventsNet.Http.Tests
{
    [TestClass]
    public class CloudEventContentFactoryTests
    {
        [TestMethod]
        public void Given_ClassType_Should_BeStatic()
        {
            typeof(CloudEventContentFactory)
                .Should().BeStatic()
                .And.HaveAccessModifier(CSharpAccessModifier.Public);
        }

        [TestMethod]
        public void Given_ClassType_Should_HaveMethodOf_Create_And_Parameter_And_ReturnTypeOf_CloudEventContent()
        {
            var method = typeof(CloudEventContentFactory).GetTypeInfo()
                                                  .GetMethod("Create", BindingFlags.Public | BindingFlags.Static);
            method.Should().NotBeNull();
            method.IsGenericMethod.Should().BeTrue();

            method.GetParameters()
                .Should().Contain(p => p.Name.Equals("ce"))
                .And.HaveCount(1);

            method.ReturnType.GetTypeInfo().Name.Should().BeEquivalentTo("CloudEventContent`1");
            method.ReturnType.GetTypeInfo().IsGenericType.Should().BeTrue();
        }

        [TestMethod]
        public void Given_CloudEvent_Should_Return_StructuredCloudEventContent()
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

            var content = CloudEventContentFactory.Create(ce);

            content.Should().BeOfType<StructuredCloudEventContent<FakeData>>();
        }

        [TestMethod]
        public void Given_CloudEvent_Should_Return_BinaryCloudEventContent()
        {
            var contentType = "text/plain";
            var charset = "utf-8";
            var data = "hello world";

            var ce = new AnotherFakeEvent();
            ce.EventType = "com.example.someevent";
            ce.CloudEventsVersion = "0.1";
            ce.Source = (new Uri("http://localhost")).ToString();
            ce.EventId = Guid.NewGuid().ToString();
            ce.ContentType = contentType;
            ce.Data = data;

            var content = CloudEventContentFactory.Create(ce);

            content.Should().BeOfType<BinaryCloudEventContent<string>>();
        }
    }
}