using System;
using System.Linq;
using Codestellation.Quarks.Testing;
using NUnit.Framework;

namespace Codestellation.Quarks.Tests.Testing
{
    [TestFixture]
    public class SomeDoubleTests : SomeTests
    {
        private const double Epsilon = 0.0000000000000000000000001;

        private static readonly object[] CommonCases =
        {
            new object[] {0.0, 1.0},
            new object[] {-1.0, 0.0},
            new object[] {1.0, double.MaxValue},
            new object[] {double.MinValue, -1.0},
            new object[] {double.MinValue, double.MaxValue},
            new object[] {double.MaxValue / 2, double.MaxValue},
            new object[] {double.MinValue, double.MinValue / 2}
        };

        private static readonly object[] EqualBoundCases =
        {
            new object[] {0.0},
            new object[] {1.0},
            new object[] {-1.0},
            new object[] {double.MaxValue},
            new object[] {double.MinValue},
            new object[] {1234567890.0987654321234567890}
        };

        private static readonly object[] MinGreaterThanMaxCases =
        {
            new object[] {Epsilon, 0.0},
            new object[] {0.0, -Epsilon},
            new object[] {double.MaxValue, double.MaxValue / 2},
            new object[] {double.MinValue / 2, double.MinValue}
        };

        private static readonly object[] UniformDistributionCases =
        {
            new object[] {0.0, Epsilon},
            new object[] {0.0, 1.0},
            new object[] {0.0, 100.0},
            new object[] {double.MaxValue / 2, double.MaxValue},
            new object[] {double.MinValue, double.MinValue / 2}
        };

        [Test, TestCaseSource("CommonCases")]
        public void Should_generate_values_between_min_and_max(double min, double max)
        {
            // when
            var results = ListOf(() => Some.Double(min, max));

            // then
            Assert.That(results, Has.All.InRange(min, max));
        }

        [Test, TestCaseSource("CommonCases")]
        public void Should_generate_different_values(double min, double max)
        {
            // when
            var results = ListOf(() => Some.Double(min, max));
            var distinctResults = results.Distinct().ToList();

            // then
            Assert.That(distinctResults.Count, Is.Not.EqualTo(1), "All generated values are the same");
        }

        [Test, TestCaseSource("EqualBoundCases")]
        public void Should_return_min_if_min_equal_to_max(double bound)
        {
            // when
            var results = ListOf(() => Some.Double(bound, bound));

            // then
            Assert.That(results, Has.All.EqualTo(bound));
        }

        [Test, TestCaseSource("MinGreaterThanMaxCases")]
        [ExpectedException(typeof(ArgumentException))]
        public void Should_throw_if_min_greater_than_max(double min, double max)
        {
            Some.Double(min, max);
        }

        [Test, TestCaseSource("UniformDistributionCases")]
        public void Should_generate_uniform_distribution(double min, double max)
        {
            // given
            const int listSize = 10000;

            // when
            var results = ListOf(() => Some.Double(min, max), listSize);
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