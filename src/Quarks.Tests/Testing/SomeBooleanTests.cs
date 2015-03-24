using System.Linq;
using Codestellation.Quarks.Testing;
using NUnit.Framework;

namespace Codestellation.Quarks.Tests.Testing
{
    [TestFixture]
    public class SomeBooleanTests : SomeTests
    {
        [Test]
        public void Should_generate_different_values()
        {
            // when
            var results = ListOf(Some.Boolean);
            var distinctResults = results.Distinct().ToList();

            // then
            Assert.That(distinctResults.Count, Is.Not.EqualTo(1), "All generated values are the same");
        }

        [Test]
        public void Should_generate_uniform_distribution()
        {
            // given
            const int listSize = 10000;

            // when
            var results = ListOf(Some.Boolean, listSize);
            var stats = StatsFor(results);
            var minFrequency = stats.MinFrequency;
            var maxFrequency = stats.MaxFrequency;

            // then
            Assert.That(minFrequency, Is.Positive);
            Assert.That(maxFrequency, Is.AtLeast(minFrequency));
            Assert.That(maxFrequency / (double)minFrequency, Is.AtMost(1.1), "Frequency variation is too high");
        }
    }
}