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

        [Test, TestCaseSource("CommonCases")]
        public void Should_generate_different_values(DateTime min, DateTime max)
        {
            Should_generate_different_values(Some.DateTime, min, max);
        }

        [Test, TestCaseSource("CommonCases")]
        public void Should_generate_uniform_distribution(DateTime min, DateTime max)
        {
            Should_generate_uniform_distribution(Some.DateTime, min, max, 2);
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
        [ExpectedException(typeof(ArgumentException))]
        public void Should_throw_if_min_greater_than_max()
        {
            // given
            var min = DateTime.MinValue.AddTicks(1);
            var max = DateTime.MinValue;

            // when
            Some.DateTime(min, max);
        }
    }
}