using System;
using System.Collections;
using System.Collections.Generic;

namespace Codestellation.Quarks.Collections
{
    public static class ReadOnlyCollectionExtensions
    {
        /// <summary>
        /// Yields iterators for chunks of the collection
        /// NOT THREAD SAFE
        /// </summary>        
        public static IEnumerable<Chunk<T>> AsChunks<T>(this IReadOnlyCollection<T> self, int chunkSize)
        {
            if (chunkSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(chunkSize), "Chunk size must be a positive int");
            }

            IEnumerator<T> enumerator = self.GetEnumerator();

            int remains = self.Count % chunkSize > 0 ? 1 : 0;

            for (int i = 0; i < self.Count / chunkSize + remains; i++)
            {
                yield return new Chunk<T>(enumerator, chunkSize);
            }
        }
    }

    public struct Chunk<T> : IEnumerable<T>, IEnumerator<T>
    {
        private IEnumerator<T> _sharedEnumerator;
        private int _chunkSize;
        private int _counter;

        public Chunk(IEnumerator<T> enumerator, int chunkSize)
        {
            _sharedEnumerator = enumerator;
            _chunkSize = chunkSize;
            _counter = 0;
        }

        public T Current => _sharedEnumerator.Current;

        object IEnumerator.Current => _sharedEnumerator.Current;

        IEnumerator IEnumerable.GetEnumerator() => this;

        public IEnumerator<T> GetEnumerator() => this;

        public bool MoveNext()
        {
            if (_counter++ < _chunkSize)
            {
                return _sharedEnumerator.MoveNext();
            }

            return false;
        }

        // according to https://docs.microsoft.com/en-us/dotnet/api/system.collections.ienumerator.reset?view=net-6.0#remarks
        public void Reset() => throw new NotSupportedException("This enumerator cannot be resetted");

        public void Dispose() { }
    }
}