using System;
using Codestellation.Quarks.Testing;
using NUnit.Framework;

namespace Codestellation.Quarks.Tests.Testing
{
    [TestFixture]
    public class SomeStringTests : SomeTests
    {
        [Test]
        public void Should_generate_different_values()
        {
            Should_generate_different_values(Some.String);
        }

        [Test, Explicit]
        public void Should_generate_uniform_distribution()
        {
            Should_generate_uniform_distribution(Some.String);
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(10)]
        [TestCase(12345)]
        public void Should_generate_string_of_required_length(int length)
        {
            // when
            var result = Some.String(length);
            // then
            Assert.That(result, Has.Length.EqualTo(length));
        }

        [Test]
        [TestCase(-1)]
        [TestCase(-10)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Should_throw_if_required_length_is_negative(int length)
        {
            Some.String(length);
        }

        [Test]
        [TestCase("hello")]
        [TestCase("prefix")]
        [TestCase("_?*/¹\\#Ú")]
        public void Should_generate_prefixed_string(string prefix)
        {
            // given
            var expectedLength = prefix.Length +
                Some.StringOptions.Delimiter.Length +
                Some.StringOptions.DefaultLength;

            // when
            var result = Some.String(prefix);

            // then
            Assert.That(result, Is.StringStarting(prefix));
            Assert.That(result, Has.Length.EqualTo(expectedLength));
        }

        [Test]
        [TestCase(null, 10)]
        [TestCase("", 12345)]
        public void Should_omit_prefix_delimiter_if_prefix_is_null_or_empty(string prefix, int length)
        {
            // when
            var result = Some.String(prefix, length);

            // then
            Assert.That(result, Is.Not.StringStarting(Some.StringOptions.Delimiter));
            Assert.That(result, Has.Length.EqualTo(length));
        }
    }
}