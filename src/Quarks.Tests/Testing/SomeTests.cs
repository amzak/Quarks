using System;
using System.Collections.Generic;
using System.Linq;

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
    }
}