using System;
using System.Collections.Generic;

namespace Codestellation.Quarks.Collections
{
    public static class AlgorithmExtensions
    {
        [ThreadStatic]
        private static Random _random;

        private static Random Random => _random ??= new Random();

        /// <summary>
        /// Fisher–Yates shuffle
        /// shuffles sequence, passed into function, doesn't copy anything
        /// </summary>
        public static IList<T> Shuffle<T>(this IList<T> sequence, Random random = null)
        {
            Random actualRandom = random ?? Random;

            int length = sequence.Count;
            for (int i = length - 1; i > 0; i--)
            {
                int randomIndex = actualRandom.Next(i + 1);
                T item = sequence[randomIndex];
                sequence[randomIndex] = sequence[i];
                sequence[i] = item;
            }

            return sequence;
        }
    }
}