using System;
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
            Should_generate_values_between_min_and_max(Some.Int64, min, max);
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
            Should_generate_different_values(Some.Int64, min, max);
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(-1)]
        [TestCase(long.MaxValue)]
        [TestCase(long.MinValue)]
        public void Should_return_min_if_min_equal_to_max(long bound)
        {
            Should_return_min_if_min_equal_to_max(Some.Int64, bound);
        }

        [Test]
        [TestCase(1, 0)]
        [TestCase(0, -1)]
        [TestCase(long.MaxValue, long.MaxValue - 1)]
        [TestCase(long.MinValue + 1, long.MinValue)]
        public void Should_throw_if_min_greater_than_max(long min, long max)
        {
            Assert.Throws<ArgumentException>(() => Some.Int64(min, max));
        }

        [Test, Explicit]
        [TestCase(0, 1)]
        [TestCase(0, 100)]
        public void Should_generate_uniform_distribution(long min, long max)
        {
            Should_generate_uniform_distribution(Some.Int64, min, max);
        }
    }
}