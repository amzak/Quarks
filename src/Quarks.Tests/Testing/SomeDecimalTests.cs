using System;
using System.Linq;
using Codestellation.Quarks.Testing;
using NUnit.Framework;

namespace Codestellation.Quarks.Tests.Testing
{
    [TestFixture]
    public class SomeDecimalTests : SomeTests
    {
        private const decimal Epsilon = 0.0000000000000000000000001m;

        private static readonly object[] CommonCases =
        {
            new object[] {0m, 1m},
            new object[] {-1m, 0m},
            new object[] {1m, decimal.MaxValue},
            new object[] {decimal.MinValue, -1m},
            new object[] {decimal.MinValue, decimal.MaxValue},
            new object[] {decimal.MaxValue - 1, decimal.MaxValue},
            new object[] {decimal.MinValue, decimal.MinValue + 1}
        };

        private static readonly object[] EqualBoundCases =
        {
            new object[] {0m},
            new object[] {1m},
            new object[] {-1m},
            new object[] {decimal.MaxValue},
            new object[] {decimal.MinValue},
            new object[] {1234567890.098765432123456789m}
        };

        private static readonly object[] MinGreaterThanMaxCases =
        {
            new object[] {Epsilon, 0m},
            new object[] {0m, -Epsilon},
            new object[] {decimal.MaxValue, decimal.MaxValue - 0.5m},
            new object[] {decimal.MinValue + 0.5m, decimal.MinValue}
        };

        private static readonly object[] UniformDistributionCases =
        {
            new object[] {0m, 0.00001m},
            new object[] {0m, 1m},
            new object[] {0m, 100m},
            new object[] {decimal.MaxValue - 1, decimal.MaxValue},
            new object[] {decimal.MinValue, decimal.MinValue + 1}
        };

        [Test, TestCaseSource("CommonCases")]
        public void Should_generate_values_between_min_and_max(decimal min, decimal max)
        {
            // when
            var results = ListOf(() => Some.Decimal(min, max));

            // then
            Assert.That(results, Has.All.InRange(min, max));
        }

        [Test, TestCaseSource("CommonCases")]
        public void Should_generate_different_values(decimal min, decimal max)
        {
            // when
            var results = ListOf(() => Some.Decimal(min, max));
            var distinctResults = results.Distinct().ToList();

            // then
            Assert.That(distinctResults.Count, Is.Not.EqualTo(1), "All generated values are the same");
        }

        [Test, TestCaseSource("EqualBoundCases")]
        public void Should_return_min_if_min_equal_to_max(decimal bound)
        {
            // when
            var results = ListOf(() => Some.Decimal(bound, bound));

            // then
            Assert.That(results, Has.All.EqualTo(bound));
        }

        [Test, TestCaseSource("MinGreaterThanMaxCases")]
        [ExpectedException(typeof(ArgumentException))]
        public void Should_throw_if_min_greater_than_max(decimal min, decimal max)
        {
            Some.Decimal(min, max);
        }

        [Test, TestCaseSource("UniformDistributionCases")]
        public void Should_generate_uniform_distribution(decimal min, decimal max)
        {
            // given
            const int listSize = 10000;

            // when
            var results = ListOf(() => Some.Decimal(min, max), listSize);
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