using System;
using System.Collections.Generic;
using System.Linq;
using Codestellation.Quarks.Collections;
using Codestellation.Quarks.Testing;
using NUnit.Framework;

namespace Codestellation.Quarks.Tests.Testing
{
    [TestFixture]
    public class SomeCollectionTests : SomeTests
    {
        private IList<int> _list;

        [SetUp]
        public void Setup()
        {
            const int size = 10;
            _list = Enumerable
                .Range(0, size)
                .ConvertToArray(i => i, size);
        }

        [Test]
        public void ElementOf_should_generate_different_values()
        {
            // when
            var results = ListOf(() => Some.ElementOf(_list));
            var distinctResults = results.Distinct()
                .ToList();

            // then
            Assert.That(distinctResults.Count, Is.Not.EqualTo(1), "All generated values are the same");
        }

        [Test]
        public void ElementOf_should_generate_uniform_distribution()
        {
            // given
            const int listSize = 10000;

            // when
            var results = ListOf(() => Some.ElementOf(_list), listSize);
            var stats = StatsFor(results);
            var minFrequency = stats.MinFrequency;
            var maxFrequency = stats.MaxFrequency;

            // then
            Assert.That(minFrequency, Is.Positive);
            Assert.That(maxFrequency, Is.AtLeast(minFrequency));
            Assert.That(maxFrequency / (double)minFrequency, Is.AtMost(1.2), "Frequency variation is too high");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ElementOf_should_throw_if_list_is_null()
        {
            Some.ElementOf((int[])null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ElementOf_should_throw_if_list_is_empty()
        {
            Some.ElementOf(new int[0]);
        }

        [Test]
        public void IndexOf_should_generate_different_values()
        {
            // when
            var results = ListOf(() => Some.IndexOf(_list));
            var distinctResults = results.Distinct()
                .ToList();

            // then
            Assert.That(distinctResults.Count, Is.Not.EqualTo(1), "All generated values are the same");
        }

        [Test]
        public void IndexOf_should_generate_uniform_distribution()
        {
            // given
            const int listSize = 10000;

            // when
            var results = ListOf(() => Some.IndexOf(_list), listSize);
            var stats = StatsFor(results);
            var minFrequency = stats.MinFrequency;
            var maxFrequency = stats.MaxFrequency;

            // then
            Assert.That(minFrequency, Is.Positive);
            Assert.That(maxFrequency, Is.AtLeast(minFrequency));
            Assert.That(maxFrequency / (double)minFrequency, Is.AtMost(1.2), "Frequency variation is too high");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IndexOf_should_throw_if_list_is_null()
        {
            Some.IndexOf((int[])null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void IndexOf_should_throw_if_list_is_empty()
        {
            Some.IndexOf(new int[0]);
        }
    }
}