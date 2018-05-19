using System;
using System.Reflection;

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
    }
}