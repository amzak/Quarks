using System;
using System.Collections.Generic;
using System.Linq;
using Codestellation.Quarks.Collections;

namespace Codestellation.Quarks.Tests.Testing
{
    public class FrequencyStatistics<T>
    {
        private readonly Dictionary<T, int> _stats;

        public FrequencyStatistics(int capacity)
        {
            _stats = new Dictionary<T, int>(capacity);
        }

        public void Add(T item)
        {
            var frequency = _stats.GetOrAdd(item, i => 0);
            _stats[item] = frequency + 1;
        }

        public int MinFrequency
        {
            get { return _stats.Min(s => s.Value); }
        }

        public int MaxFrequency
        {
            get { return _stats.Max(s => s.Value); }
        }

        public double FrequencyVariation
        {
            get { return MaxFrequency / (double)MinFrequency; }
        }

        public void Print()
        {
            Console.WriteLine("Frequencies:\n");
            foreach (var stat in _stats.OrderByDescending(s => s.Value))
            {
                Console.WriteLine("\t{0} : {1} times", stat.Key, stat.Value);
            }

            Console.WriteLine(
                "\nFrequency variation = Max / Min = {0} / {1} = {2}",
                MaxFrequency,
                MinFrequency,
                FrequencyVariation);
        }
    }
}