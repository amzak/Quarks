using System;
using System.Collections.Generic;

namespace Codestellation.Quarks.Collections
{
    public static class AlgorithmExtensions
    {
        static readonly Random Random = new Random();

        /// <summary>
        /// Fisher–Yates shuffle
        /// shuffles sequence, passed into function, doesn't copy anything
        /// </summary>
        public static IList<T> Shuffle<T>(this IList<T> sequence)
        {
            int length = sequence.Count;
            for (int i = length - 1; i > 0; i--)
            {
                int randomIndex = Random.Next(i + 1);
                T item = sequence[randomIndex];
                sequence[randomIndex] = sequence[i];
                sequence[i] = item;
            }

            return sequence;
        }

    }
}