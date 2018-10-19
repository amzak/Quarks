using System;
using Codestellation.Quarks.Testing;
using NUnit.Framework;

namespace Codestellation.Quarks.Tests.Testing
{
    [TestFixture]
    public class SomeEnumTests : SomeTests
    {
        private enum SimpleEnum
        {
            Ab,
            Abc,
            Abcd,
            Abcde,
            Abcdef,
            Abcdefg,
            Abcdefgh,
        }

        private enum NumberedEnum
        {
            Ok = 200,
            NotFound = 404,
            Unknown = 100500,
        }

        [Test]
        [TestCase(typeof(SimpleEnum))]
        [TestCase(typeof(NumberedEnum))]
        public void Should_generate_different_values(Type enumType)
        {
            Should_generate_different_values(() => Some.Enum(enumType));
        }

        [Test, Explicit]
        [TestCase(typeof(SimpleEnum))]
        [TestCase(typeof(NumberedEnum))]
        public void Should_generate_uniform_distribution(Type enumType)
        {
            Should_generate_uniform_distribution(() => Some.Enum(enumType));
        }

        [Test]
        public void Should_throw_if_type_is_not_enum()
        {
            Assert.Throws<ArgumentException>(() => Some.Enum<int>());
        }
    }
}