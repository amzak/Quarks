using System;
using Codestellation.Quarks.Testing;
using NUnit.Framework;

namespace Codestellation.Quarks.Tests.Testing
{
    [TestFixture]
    public class SomeTimeSpanTests : SomeTests
    {
        private static readonly object[] MinMaxCases =
        {
            new object[] { TimeSpan.MinValue, TimeSpan.MaxValue },
            new object[] { TimeSpan.Zero, TimeSpan.FromTicks(1) },
            new object[] { TimeSpan.FromMilliseconds(1), TimeSpan.FromMilliseconds(2) },
            new object[] { TimeSpan.FromDays(1), TimeSpan.FromDays(2) },
        };

        private static readonly object[] MinMaxUniformDistributionCases =
        {
            new object[] { TimeSpan.MinValue, TimeSpan.MaxValue },
            new object[] { TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2) },
            new object[] { TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(2) },
        };

        private static readonly object[] EqualBoundCases =
        {
            new object[] { TimeSpan.MinValue },
            new object[] { TimeSpan.MaxValue },
            new object[] { TimeSpan.Zero },
            new object[] { TimeSpan.FromTicks(1) },
        };

        private static readonly object[] PositiveCases =
        {
            new object[] { TimeSpan.MaxValue },
            new object[] { TimeSpan.FromTicks(1) },
            new object[] { TimeSpan.FromMilliseconds(1) },
            new object[] { TimeSpan.FromDays(1) },
        };

        private static readonly object[] PositiveUniformDistributionCases =
        {
            new object[] { TimeSpan.MaxValue },
            new object[] { TimeSpan.FromSeconds(1) },
            new object[] { TimeSpan.FromMinutes(1) },
        };

        private static readonly object[] NonPositiveCases =
        {
            new object[] { TimeSpan.Zero },
            new object[] { TimeSpan.FromTicks(-1) },
            new object[] { TimeSpan.FromMilliseconds(-1) },
        };

        [Test, TestCaseSource("MinMaxCases")]
        public void Should_generate_different_values(TimeSpan min, TimeSpan max)
        {
            Should_generate_different_values(Some.TimeSpan, min, max);
        }

        [Test, TestCaseSource("MinMaxUniformDistributionCases")]
        public void Should_generate_uniform_distribution(TimeSpan min, TimeSpan max)
        {
            Should_generate_uniform_distribution(Some.TimeSpan, min, max, 2);
        }

        [Test, TestCaseSource("MinMaxCases")]
        public void Should_generate_values_between_min_and_max(TimeSpan min, TimeSpan max)
        {
            Should_generate_values_between_min_and_max(Some.TimeSpan, min, max);
        }

        [Test, TestCaseSource("EqualBoundCases")]
        public void Should_return_min_if_min_equal_to_max(TimeSpan bound)
        {
            Should_return_min_if_min_equal_to_max(Some.TimeSpan, bound);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Should_throw_if_min_greater_than_max()
        {
            // given
            var min = TimeSpan.MinValue + TimeSpan.FromTicks(1);
            var max = TimeSpan.MinValue;

            // when
            Some.TimeSpan(min, max);
        }

        [Test, TestCaseSource("PositiveCases")]
        public void Should_generate_different_positive_values(TimeSpan max)
        {
            Should_generate_different_values(Some.PositiveTimeSpan, max);
        }

        [Test, TestCaseSource("PositiveUniformDistributionCases")]
        public void Should_generate_uniform_distribution_of_positive_values(TimeSpan max)
        {
            Should_generate_uniform_distribution(Some.PositiveTimeSpan, max, 2);
        }

        [Test, TestCaseSource("PositiveCases")]
        public void Should_generate_positive_values_between_zero_and_max(TimeSpan max)
        {
            Should_generate_values_between_zero_and_max(Some.PositiveTimeSpan, TimeSpan.Zero, max);
        }

        [Test, TestCaseSource("NonPositiveCases")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Should_throw_while_generating_positive_value_if_max_is_zero_or_negative(TimeSpan max)
        {
            Some.PositiveTimeSpan(max);
        }
    }
}