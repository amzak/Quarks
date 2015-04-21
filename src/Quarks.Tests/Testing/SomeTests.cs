using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Codestellation.Quarks.Tests.Testing
{
    public abstract class SomeTests
    {
        protected List<T> ListOf<T>(Func<T> createElement, int size = 1000)
        {
            return Enumerable
                .Range(0, size)
                .Select(i => createElement())
                .ToList();
        }

        protected FrequencyStatistics<T> StatsFor<T>(List<T> list)
        {
            var stats = new FrequencyStatistics<T>(list.Count);
            list.ForEach(stats.Add);
            stats.Print();
            return stats;
        }

        protected void Should_generate_values_between_min_and_max<T>(Func<T, T, T> some, T min, T max)
            where T : IComparable<T>
        {
            // when
            var results = ListOf(() => some(min, max));

            // then
            Assert.That(results, Has.All.InRange(min, max));
        }

        protected void Should_return_min_if_min_equal_to_max<T>(Func<T, T, T> some, T min)
            where T : IEquatable<T>
        {
            // when
            var results = ListOf(() => some(min, min));

            // then
            Assert.That(results, Has.All.EqualTo(min));
        }

        protected void Should_generate_different_values<T>(Func<T, T, T> some, T min, T max)
        {
            Should_generate_different_values(() => some(min, max));
        }

        protected void Should_generate_different_values<T>(Func<T> some)
        {
            // when
            var results = ListOf(some);
            var distinctResults = results
                .Distinct()
                .ToList();

            // then
            Assert.That(distinctResults.Count, Is.Not.EqualTo(1), "All generated values are the same");
        }

        protected void Should_generate_uniform_distribution<T>(Func<T, T, T> some, T min, T max, double maxVariation)
        {
            Should_generate_uniform_distribution(() => some(min, max), maxVariation);
        }

        protected void Should_generate_uniform_distribution<T>(Func<T> some, double maxVariation)
        {
            // given
            const int listSize = 10000;

            // when
            var results = ListOf(some, listSize);
            var stats = StatsFor(results);

            // then
            Assert.That(stats.MinFrequency, Is.Positive);
            Assert.That(stats.MaxFrequency, Is.AtLeast(stats.MinFrequency));
            Assert.That(stats.FrequencyVariation, Is.AtMost(maxVariation), "Frequency variation is too high");
        }
    }
}