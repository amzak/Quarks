using System;
using Codestellation.Quarks.Testing;
using NUnit.Framework;

namespace Codestellation.Quarks.Tests.Testing
{
    [TestFixture]
    public class SomeUInt32Tests : SomeTests
    {
        [Test]
        [TestCase(0u, 1u)]
        [TestCase(0u, uint.MaxValue)]
        [TestCase(uint.MaxValue - 1, uint.MaxValue)]
        public void Should_generate_values_between_min_and_max(uint min, uint max)
        {
            Should_generate_values_between_min_and_max(Some.UInt32, min, max);
        }

        [Test]
        [TestCase(0u, 1u)]
        [TestCase(0u, uint.MaxValue)]
        [TestCase(uint.MaxValue - 1, uint.MaxValue)]
        public void Should_generate_different_values(uint min, uint max)
        {
            Should_generate_different_values(Some.UInt32, min, max);
        }

        [Test]
        [TestCase(0u)]
        [TestCase(1u)]
        [TestCase(uint.MaxValue)]
        public void Should_return_min_if_min_equal_to_max(uint bound)
        {
            Should_return_min_if_min_equal_to_max(Some.UInt32, bound);
        }

        [Test]
        [TestCase(1u, 0u)]
        [TestCase(uint.MaxValue, uint.MaxValue - 1)]
        [ExpectedException(typeof(ArgumentException))]
        public void Should_throw_if_min_greater_than_max(uint min, uint max)
        {
            Some.UInt32(min, max);
        }

        [Test]
        [TestCase(0u, 1u)]
        [TestCase(0u, 100u)]
        public void Should_generate_uniform_distribution(uint min, uint max)
        {
            Should_generate_uniform_distribution(Some.UInt32, min, max, 3.5);
        }
    }
}