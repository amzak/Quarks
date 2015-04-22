using System;
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
            Should_generate_values_between_min_and_max(Some.Int32, min, max);
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
            Should_generate_different_values(Some.Int32, min, max);
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(-1)]
        [TestCase(int.MaxValue)]
        [TestCase(int.MinValue)]
        public void Should_return_min_if_min_equal_to_max(int bound)
        {
            Should_return_min_if_min_equal_to_max(Some.Int32, bound);
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

        [Test, Explicit]
        [TestCase(0, 1)]
        [TestCase(0, 100)]
        public void Should_generate_uniform_distribution(int min, int max)
        {
            Should_generate_uniform_distribution(Some.Int32, min, max);
        }
    }
}