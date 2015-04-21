using System;
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
            new object[] { 0m, 1m },
            new object[] { -1m, 0m },
            new object[] { 1m, decimal.MaxValue },
            new object[] { decimal.MinValue, -1m },
            new object[] { decimal.MinValue, decimal.MaxValue },
            new object[] { decimal.MaxValue - 1, decimal.MaxValue },
            new object[] { decimal.MinValue, decimal.MinValue + 1 }
        };

        private static readonly object[] EqualBoundCases =
        {
            new object[] { 0m },
            new object[] { 1m },
            new object[] { -1m },
            new object[] { decimal.MaxValue },
            new object[] { decimal.MinValue },
            new object[] { 1234567890.098765432123456789m }
        };

        private static readonly object[] MinGreaterThanMaxCases =
        {
            new object[] { Epsilon, 0m },
            new object[] { 0m, -Epsilon },
            new object[] { decimal.MaxValue, decimal.MaxValue - 0.5m },
            new object[] { decimal.MinValue + 0.5m, decimal.MinValue }
        };

        private static readonly object[] UniformDistributionCases =
        {
            new object[] { 0m, 0.00001m },
            new object[] { 0m, 1m },
            new object[] { 0m, 100m },
            new object[] { decimal.MaxValue - 1, decimal.MaxValue },
            new object[] { decimal.MinValue, decimal.MinValue + 1 }
        };

        [Test, TestCaseSource("CommonCases")]
        public void Should_generate_values_between_min_and_max(decimal min, decimal max)
        {
            Should_generate_values_between_min_and_max(Some.Decimal, min, max);
        }

        [Test, TestCaseSource("CommonCases")]
        public void Should_generate_different_values(decimal min, decimal max)
        {
            Should_generate_different_values(Some.Decimal, min, max);
        }

        [Test, TestCaseSource("EqualBoundCases")]
        public void Should_return_min_if_min_equal_to_max(decimal bound)
        {
            Should_return_min_if_min_equal_to_max(Some.Decimal, bound);
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
            Should_generate_uniform_distribution(Some.Decimal, min, max, 2.0);
        }
    }
}