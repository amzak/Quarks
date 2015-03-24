using System;
using System.Linq;
using Codestellation.Quarks.Testing;
using NUnit.Framework;

namespace Codestellation.Quarks.Tests.Testing
{
    [TestFixture]
    public class SomeInt64Tests : SomeTests
    {
        [Test]
        [TestCase(0, 1)]
        [TestCase(-1, 0)]
        [TestCase(1, long.MaxValue)]
        [TestCase(long.MinValue, -1)]
        [TestCase(long.MinValue, long.MaxValue)]
        [TestCase(long.MaxValue - 1, long.MaxValue)]
        [TestCase(long.MinValue, long.MinValue + 1)]
        public void Should_generate_values_between_min_and_max(long min, long max)
        {
            // when
            var results = ListOf(() => Some.Int64(min, max));

            // then
            Assert.That(results, Has.All.InRange(min, max));
        }

        [Test]
        [TestCase(0, 1)]
        [TestCase(-1, 0)]
        [TestCase(1, long.MaxValue)]
        [TestCase(long.MinValue, -1)]
        [TestCase(long.MinValue, long.MaxValue)]
        [TestCase(long.MaxValue - 1, long.MaxValue)]
        [TestCase(long.MinValue, long.MinValue + 1)]
        public void Should_generate_different_values(long min, long max)
        {
            // when
            var results = ListOf(() => Some.Int64(min, max));
            var distinctResults = results.Distinct().ToList();

            // then
            Assert.That(distinctResults.Count, Is.Not.EqualTo(1), "All generated values are the same");
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(-1)]
        [TestCase(long.MaxValue)]
        [TestCase(long.MinValue)]
        public void Should_return_min_if_min_equal_to_max(long bound)
        {
            // when
            var results = ListOf(() => Some.Int64(bound, bound));

            // then
            Assert.That(results, Has.All.EqualTo(bound));
        }

        [Test]
        [TestCase(1, 0)]
        [TestCase(0, -1)]
        [TestCase(long.MaxValue, long.MaxValue - 1)]
        [TestCase(long.MinValue + 1, long.MinValue)]
        [ExpectedException(typeof(ArgumentException))]
        public void Should_throw_if_min_greater_than_max(long min, long max)
        {
            Some.Int64(min, max);
        }

        [Test]
        [TestCase(0, 1)]
        [TestCase(0, 100)]
        public void Should_generate_uniform_distribution(long min, long max)
        {
            // given
            const int listSize = 10000;

            // when
            var results = ListOf(() => Some.Int64(min, max), listSize);
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