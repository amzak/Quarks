using System;
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
            new object[] { 0.0, 1.0 },
            new object[] { -1.0, 0.0 },
            new object[] { 1.0, double.MaxValue },
            new object[] { double.MinValue, -1.0 },
            new object[] { double.MinValue, double.MaxValue },
            new object[] { double.MaxValue / 2, double.MaxValue },
            new object[] { double.MinValue, double.MinValue / 2 }
        };

        private static readonly object[] EqualBoundCases =
        {
            new object[] { 0.0 },
            new object[] { 1.0 },
            new object[] { -1.0 },
            new object[] { double.MaxValue },
            new object[] { double.MinValue },
            new object[] { 1234567890.0987654321234567890 }
        };

        private static readonly object[] MinGreaterThanMaxCases =
        {
            new object[] { Epsilon, 0.0 },
            new object[] { 0.0, -Epsilon },
            new object[] { double.MaxValue, double.MaxValue / 2 },
            new object[] { double.MinValue / 2, double.MinValue }
        };

        private static readonly object[] UniformDistributionCases =
        {
            new object[] { 0.0, Epsilon },
            new object[] { 0.0, 1.0 },
            new object[] { 0.0, 100.0 },
            new object[] { double.MaxValue / 2, double.MaxValue },
            new object[] { double.MinValue, double.MinValue / 2 }
        };

        [Test, TestCaseSource("CommonCases")]
        public void Should_generate_values_between_min_and_max(double min, double max)
        {
            Should_generate_values_between_min_and_max(Some.Double, min, max);
        }

        [Test, TestCaseSource("CommonCases")]
        public void Should_generate_different_values(double min, double max)
        {
            Should_generate_different_values(Some.Double, min, max);
        }

        [Test, TestCaseSource("EqualBoundCases")]
        public void Should_return_min_if_min_equal_to_max(double bound)
        {
            Should_return_min_if_min_equal_to_max(Some.Double, bound);
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
            Should_generate_uniform_distribution(Some.Double, min, max, 2.0);
        }
    }
}