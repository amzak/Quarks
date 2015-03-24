using System;
using System.Linq;
using Codestellation.Quarks.Testing;
using NUnit.Framework;

namespace Codestellation.Quarks.Tests.Testing
{
    [TestFixture]
    public class SomeUInt32Tests : SomeTests
    {
        [Test]
        [TestCase(0u, 1u)]
        [TestCase(0u, uint.MaxValue)]
        [TestCase(uint.MaxValue - 1, uint.MaxValue)]
        public void Should_generate_values_between_min_and_max(uint min, uint max)
        {
            // when
            var results = ListOf(() => Some.UInt32(min, max));

            // then
            Assert.That(results, Has.All.InRange(min, max));
        }

        [Test]
        [TestCase(0u, 1u)]
        [TestCase(0u, uint.MaxValue)]
        [TestCase(uint.MaxValue - 1, uint.MaxValue)]
        public void Should_generate_different_values(uint min, uint max)
        {
            // when
            var results = ListOf(() => Some.UInt32(min, max));
            var distinctResults = results.Distinct().ToList();

            // then
            Assert.That(distinctResults.Count, Is.Not.EqualTo(1), "All generated values are the same");
        }

        [Test]
        [TestCase(0u)]
        [TestCase(1u)]
        [TestCase(uint.MaxValue)]
        public void Should_return_min_if_min_equal_to_max(uint bound)
        {
            // when
            var results = ListOf(() => Some.UInt32(bound, bound));

            // then
            Assert.That(results, Has.All.EqualTo(bound));
        }

        [Test]
        [TestCase(1u, 0u)]
        [TestCase(uint.MaxValue, uint.MaxValue - 1)]
        [ExpectedException(typeof(ArgumentException))]
        public void Should_throw_if_min_greater_than_max(uint min, uint max)
        {
            Some.UInt32(min, max);
        }

        [Test]
        [TestCase(0u, 1u)]
        [TestCase(0u, 100u)]
        public void Should_generate_uniform_distribution(uint min, uint max)
        {
            // given
            const int listSize = 10000;

            // when
            var results = ListOf(() => Some.UInt32(min, max), listSize);
            var stats = StatsFor(results);
            var minFrequency = stats.MinFrequency;
            var maxFrequency = stats.MaxFrequency;

            // then
            Assert.That(minFrequency, Is.Positive);
            Assert.That(maxFrequency, Is.AtLeast(minFrequency));
            Assert.That(maxFrequency / (double)minFrequency, Is.AtMost(3.5), "Frequency variation is too high");
        }
    }
}