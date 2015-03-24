using System;
using System.Linq;
using Codestellation.Quarks.Testing;
using NUnit.Framework;

namespace Codestellation.Quarks.Tests.Testing
{
    [TestFixture]
    public class SomeInt32Tests : SomeTests
    {
        [Test]
        [TestCase(0, 1)]
        [TestCase(-1, 0)]
        [TestCase(1, int.MaxValue)]
        [TestCase(int.MinValue, -1)]
        [TestCase(int.MinValue, int.MaxValue)]
        [TestCase(int.MaxValue - 1, int.MaxValue)]
        [TestCase(int.MinValue, int.MinValue + 1)]
        public void Should_generate_values_between_min_and_max(int min, int max)
        {
            // when
            var results = ListOf(() => Some.Int32(min, max));

            // then
            Assert.That(results, Has.All.InRange(min, max));
        }

        [Test]
        [TestCase(0, 1)]
        [TestCase(-1, 0)]
        [TestCase(1, int.MaxValue)]
        [TestCase(int.MinValue, -1)]
        [TestCase(int.MinValue, int.MaxValue)]
        //[TestCase(int.MaxValue - 1, int.MaxValue)] TODO: support this case
        [TestCase(int.MinValue, int.MinValue + 1)]
        public void Should_generate_different_values(int min, int max)
        {
            // when
            var results = ListOf(() => Some.Int32(min, max));
            var distinctResults = results.Distinct().ToList();

            // then
            Assert.That(distinctResults.Count, Is.Not.EqualTo(1), "All generated values are the same");
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(-1)]
        [TestCase(int.MaxValue)]
        [TestCase(int.MinValue)]
        public void Should_return_min_if_min_equal_to_max(int bound)
        {
            // when
            var results = ListOf(() => Some.Int32(bound, bound));

            // then
            Assert.That(results, Has.All.EqualTo(bound));
        }

        [Test]
        [TestCase(1, 0)]
        [TestCase(0, -1)]
        [TestCase(int.MaxValue, int.MaxValue - 1)]
        [TestCase(int.MinValue + 1, int.MinValue)]
        [ExpectedException(typeof(ArgumentException))]
        public void Should_throw_if_min_greater_than_max(int min, int max)
        {
            Some.Int32(min, max);
        }

        [Test]
        [TestCase(0, 1)]
        [TestCase(0, 100)]
        public void Should_generate_uniform_distribution(int min, int max)
        {
            // given
            const int listSize = 10000;

            // when
            var results = ListOf(() => Some.Int32(min, max), listSize);
            var stats = StatsFor(results);
            var minFrequency = stats.MinFrequency;
            var maxFrequency = stats.MaxFrequency;

            // then
            Assert.That(minFrequency, Is.Positive);
            Assert.That(maxFrequency, Is.AtLeast(minFrequency));
            Assert.That(maxFrequency / (double)minFrequency, Is.AtMost(2.0), "Frequency variation is too high");
        }
    }
}