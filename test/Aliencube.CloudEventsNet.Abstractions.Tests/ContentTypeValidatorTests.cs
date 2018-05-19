using System;
using System.Reflection;
using System.Text;

using FluentAssertions;
using FluentAssertions.Common;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aliencube.CloudEventsNet.Abstractions.Tests
{
    [TestClass]
    public class ContentTypeValidatorTests
    {
        [TestMethod]
        public void Given_ClassType_Should_BeStatic()
        {
            typeof(ContentTypeValidator)
                .Should().BeStatic()
                .And.HaveAccessModifier(CSharpAccessModifier.Public);
        }

        [TestMethod]
        public void Given_ClassType_Should_HaveMethodOf_IsTypeString_And_NoParameter_And_ReturnTypeOf_Boolean()
        {
            var method = typeof(ContentTypeValidator).GetTypeInfo()
                                                     .GetMethod("IsTypeString", BindingFlags.Public | BindingFlags.Static);
            method.Should().NotBeNull();

            method.GetParameters()
                .Should().Contain(p => p.Name.Equals("type") && p.ParameterType == typeof(Type))
                .And.HaveCount(1);

            method.Should().Return<bool>();
        }

        [TestMethod]
        public void Given_Type_Should_IsTypeString_ReturnValue()
        {
            var result = ContentTypeValidator.IsTypeString(typeof(int));
            result.Should().BeFalse();

            result = ContentTypeValidator.IsTypeString(typeof(string));
            result.Should().BeTrue();
        }

        [TestMethod]
        public void Given_ClassType_Should_HaveMethodOf_IsTypeByteArray_And_NoParameter_And_ReturnTypeOf_Boolean()
        {
            var method = typeof(ContentTypeValidator).GetTypeInfo()
                                                     .GetMethod("IsTypeByteArray", BindingFlags.Public | BindingFlags.Static);
            method.Should().NotBeNull();

            method.GetParameters()
                .Should().Contain(p => p.Name.Equals("type") && p.ParameterType == typeof(Type))
                .And.HaveCount(1);

            method.Should().Return<bool>();
        }

        [TestMethod]
        public void Given_Type_Should_IsTypeByteArray_ReturnValue()
        {
            var result = ContentTypeValidator.IsTypeByteArray(typeof(int));
            result.Should().BeFalse();

            result = ContentTypeValidator.IsTypeByteArray(typeof(byte[]));
            result.Should().BeTrue();
        }

        [TestMethod]
        public void Given_ClassType_Should_HaveMethodOf_IsDataString_And_HaveParameter_And_ReturnTypeOf_Boolean()
        {
            var method = typeof(ContentTypeValidator).GetTypeInfo()
                                                     .GetMethod("IsDataString", BindingFlags.Public | BindingFlags.Static);
            method.Should().NotBeNull();
            method.IsGenericMethod.Should().BeTrue();

            method.GetParameters()
                .Should().Contain(p => p.Name.Equals("data") && p.ParameterType.IsGenericParameter)
                .And.HaveCount(1);

            method.Should().Return<bool>();
        }

        [TestMethod]
        public void Given_Type_Should_IsDataString_ReturnValue()
        {
            var result = ContentTypeValidator.IsDataString(1);
            result.Should().BeFalse();

            result = ContentTypeValidator.IsDataString("hello world");
            result.Should().BeTrue();
        }

        [TestMethod]
        public void Given_ClassType_Should_HaveMethodOf_IsDataByteArray_And_HaveParameter_And_ReturnTypeOf_Boolean()
        {
            var method = typeof(ContentTypeValidator).GetTypeInfo()
                                                     .GetMethod("IsDataByteArray", BindingFlags.Public | BindingFlags.Static);
            method.Should().NotBeNull();
            method.IsGenericMethod.Should().BeTrue();

            method.GetParameters()
                .Should().Contain(p => p.Name.Equals("data") && p.ParameterType.IsGenericParameter)
                .And.HaveCount(1);

            method.Should().Return<bool>();
        }

        [TestMethod]
        public void Given_Type_Should_IsDataByteArray_ReturnValue()
        {
            var result = ContentTypeValidator.IsDataByteArray(1);
            result.Should().BeFalse();

            result = ContentTypeValidator.IsDataByteArray(Encoding.UTF8.GetBytes("hello world"));
            result.Should().BeTrue();
        }

        [TestMethod]
        public void Given_ClassType_Should_HaveMethodOf_IsJson_And_HaveParameter_And_ReturnTypeOf_Boolean()
        {
            var method = typeof(ContentTypeValidator).GetTypeInfo()
                                                     .GetMethod("IsJson", BindingFlags.Public | BindingFlags.Static);
            method.Should().NotBeNull();

            method.GetParameters()
                .Should().Contain(p => p.Name.Equals("contentType") && p.ParameterType == typeof(string))
                .And.HaveCount(1);

            method.Should().Return<bool>();
        }

        [TestMethod]
        public void Given_Type_Should_IsJson_ReturnValue()
        {
            var result = ContentTypeValidator.IsJson("lorem ipsum");
            result.Should().BeFalse();

            result = ContentTypeValidator.IsJson("application/json");
            result.Should().BeTrue();
        }

        [TestMethod]
        public void Given_ClassType_Should_HaveMethodOf_HasJsonSuffix_And_HaveParameter_And_ReturnTypeOf_Boolean()
        {
            var method = typeof(ContentTypeValidator).GetTypeInfo()
                                                     .GetMethod("HasJsonSuffix", BindingFlags.Public | BindingFlags.Static);
            method.Should().NotBeNull();

            method.GetParameters()
                .Should().Contain(p => p.Name.Equals("contentType") && p.ParameterType == typeof(string))
                .And.HaveCount(1);

            method.Should().Return<bool>();
        }

        [TestMethod]
        public void Given_Type_Should_HasJsonSuffix_ReturnValue()
        {
            var result = ContentTypeValidator.HasJsonSuffix("lorem ipsum");
            result.Should().BeFalse();

            result = ContentTypeValidator.HasJsonSuffix("application/cloudevents+json");
            result.Should().BeTrue();
        }

        [TestMethod]
        public void Given_ClassType_Should_HaveMethodOf_ImpliesJson_And_HaveParameter_And_ReturnTypeOf_Boolean()
        {
            var method = typeof(ContentTypeValidator).GetTypeInfo()
                                                     .GetMethod("ImpliesJson", BindingFlags.Public | BindingFlags.Static);
            method.Should().NotBeNull();

            method.GetParameters()
                .Should().Contain(p => p.Name.Equals("contentType") && p.ParameterType == typeof(string))
                .And.HaveCount(1);

            method.Should().Return<bool>();
        }

        [TestMethod]
        public void Given_Type_Should_ImpliesJson_ReturnValue()
        {
            var result = ContentTypeValidator.ImpliesJson("lorem ipsum");
            result.Should().BeFalse();

            result = ContentTypeValidator.ImpliesJson("text/json");
            result.Should().BeTrue();
        }

        [TestMethod]
        public void Given_ClassType_Should_HaveMethodOf_IsText_And_HaveParameter_And_ReturnTypeOf_Boolean()
        {
            var method = typeof(ContentTypeValidator).GetTypeInfo()
                                                     .GetMethod("IsText", BindingFlags.Public | BindingFlags.Static);
            method.Should().NotBeNull();

            method.GetParameters()
                .Should().Contain(p => p.Name.Equals("contentType") && p.ParameterType == typeof(string))
                .And.HaveCount(1);

            method.Should().Return<bool>();
        }

        [TestMethod]
        public void Given_Type_Should_IsText_ReturnValue()
        {
            var result = ContentTypeValidator.IsText("lorem ipsum");
            result.Should().BeFalse();

            result = ContentTypeValidator.IsText("text/plain");
            result.Should().BeTrue();
        }
    }
}