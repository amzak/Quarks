using System;
using System.Linq;
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
            // when
            var results = ListOf(() => Some.Enum(enumType));
            var distinctResults = results
                .Distinct()
                .ToList();

            // then
            Assert.That(distinctResults.Count, Is.Not.EqualTo(1), "All generated values are the same");
        }

        [Test]
        [TestCase(typeof(SimpleEnum))]
        [TestCase(typeof(NumberedEnum))]
        public void Should_generate_uniform_distribution(Type enumType)
        {
            // given
            const int listSize = 10000;

            // when
            var results = ListOf(() => Some.Enum(enumType), listSize);
            var stats = StatsFor(results);
            var minFrequency = stats.MinFrequency;
            var maxFrequency = stats.MaxFrequency;

            // then
            Assert.That(minFrequency, Is.Positive);
            Assert.That(maxFrequency, Is.AtLeast(minFrequency));
            Assert.That(maxFrequency / (double)minFrequency, Is.AtMost(1.1), "Frequency variation is too high");
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Should_throw_if_type_is_not_enum()
        {
            Some.Enum<int>();
        }
    }
}