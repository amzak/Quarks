using System;
using Codestellation.Quarks.Testing;
using NUnit.Framework;

namespace Codestellation.Quarks.Tests.Testing
{
    [TestFixture]
    public class SomeDateTimeTests : SomeTests
    {
        private static readonly object[] CommonCases =
        {
            new object[] { DateTime.MinValue, DateTime.MaxValue },
            new object[] { new DateTime(2015, 1, 1), new DateTime(2016, 1, 1) },
            new object[] { new DateTime(2015, 1, 1), new DateTime(2015, 2, 1) },
            new object[] { new DateTime(2015, 1, 1), new DateTime(2015, 1, 2) },
            new object[] { new DateTime(2015, 1, 1, 1, 1, 1), new DateTime(2015, 1, 1, 2, 1, 1) },
            new object[] { new DateTime(2015, 1, 1, 1, 1, 1), new DateTime(2015, 1, 1, 1, 2, 1) },
            new object[] { new DateTime(2015, 1, 1, 1, 1, 1), new DateTime(2015, 1, 1, 1, 1, 2) },
        };

        private static readonly object[] EqualBoundCases =
        {
            new object[] { DateTime.MinValue },
            new object[] { DateTime.MaxValue },
            new object[] { new DateTime(2015, 1, 1) },
        };

        private static readonly object[] GreaterThanCases =
        {
            new object[] { DateTime.MinValue },
            new object[] { new DateTime(2015, 1, 1) },
            new object[] { DateTime.MaxValue.AddTicks(-1) },
        };

        private static readonly object[] LessThanCases =
        {
            new object[] { DateTime.MinValue.AddTicks(1) },
            new object[] { new DateTime(2015, 1, 1) },
            new object[] { DateTime.MaxValue },
        };

        [Test, TestCaseSource("CommonCases")]
        public void Should_generate_different_values(DateTime min, DateTime max)
        {
            Should_generate_different_values(Some.DateTime, min, max);
        }

        [Test, TestCaseSource("CommonCases"), Explicit]
        public void Should_generate_uniform_distribution(DateTime min, DateTime max)
        {
            Should_generate_uniform_distribution(Some.DateTime, min, max);
        }

        [Test, TestCaseSource("CommonCases")]
        public void Should_generate_values_between_min_and_max(DateTime min, DateTime max)
        {
            Should_generate_values_between_min_and_max(Some.DateTime, min, max);
        }

        [Test, TestCaseSource("EqualBoundCases")]
        public void Should_return_min_if_min_equal_to_max(DateTime bound)
        {
            Should_return_min_if_min_equal_to_max(Some.DateTime, bound);
        }

        [Test]
        public void Should_throw_if_min_greater_than_max()
        {
            // given
            var min = DateTime.MinValue.AddTicks(1);
            var max = DateTime.MinValue;

            // when
            Assert.Throws<ArgumentException>(() => Some.DateTime(min, max));
        }

        [Test, TestCaseSource("GreaterThanCases")]
        public void Should_generate_values_greater_than_specified(DateTime specified)
        {
            // when
            var greater = Some.DateTimeGreaterThan(specified);
            // then
            Assert.That(greater, Is.GreaterThan(specified));
        }

        [Test, TestCaseSource("LessThanCases")]
        public void Should_generate_values_less_than_specified(DateTime specified)
        {
            // when
            var greater = Some.DateTimeLessThan(specified);
            // then
            Assert.That(greater, Is.LessThan(specified));
        }

        [Test]
        public void Should_throw_if_generating_value_greater_than_max_date_time()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Some.DateTimeGreaterThan(DateTime.MaxValue));
        }

        [Test]
        public void Should_throw_if_generating_value_less_than_min_date_time()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Some.DateTimeLessThan(DateTime.MinValue));
        }

        [Test]
        [TestCase(0, 1)]
        [TestCase(123456789012345678L, 123456789012345679L)]
        public void Should_generate_values_from_ticks(long lesserTick, long greaterTick)
        {
            // when
            var lesser = Some.DateTime(lesserTick);
            var greater = Some.DateTime(greaterTick);

            // then
            Assert.That(lesser, Is.LessThan(greater));
        }
    }
}