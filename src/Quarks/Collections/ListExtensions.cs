using System;
using System.Collections;
using System.Collections.Generic;

namespace Codestellation.Quarks.Collections
{
    public static class ListExtensions
    {
        /// <summary>
        /// Allocation free partition of the list
        /// </summary>
        public static BatchEnumerable<T> Partition<T>(this IList<T> self, int batchSize)
            => new BatchEnumerable<T>(self, batchSize);

        public struct SliceEnumerable<T> : IEnumerable<T>
        {
            private readonly IList<T> _list;
            private readonly int _from;
            private readonly int _to;

            public SliceEnumerable(IList<T> list, int from, int to)
            {
                _list = list;
                _from = from;
                _to = to;
            }

            public SliceEnumerator<T> GetEnumerator() => new SliceEnumerator<T>(_list, _from, _to);

            IEnumerator<T> IEnumerable<T>.GetEnumerator() => new SliceEnumerator<T>(_list, _from, _to);

            IEnumerator IEnumerable.GetEnumerator() => new SliceEnumerator<T>(_list, _from, _to);
        }

        [Serializable]
        public struct SliceEnumerator<T> : IEnumerator<T>
        {
            private readonly IList<T> _list;
            private readonly int _from;
            private readonly int _to;
            private int _index;

            public T Current { get; private set; }

            object IEnumerator.Current => Current;

            public SliceEnumerator(IList<T> list, int from, int to)
            {
                _list = list;
                _from = from;
                _to = to;
                _index = from;
                Current = default;
            }

            public bool MoveNext()
            {
                IList<T> list = _list;
                if (_index >= _to)
                {
                    _index = _to + 1;
                    Current = default;
                    return false;
                }

                Current = list[_index];
                _index += 1;
                return true;
            }

            void IEnumerator.Reset()
            {
                _index = _from;
                Current = default;
            }

            public void Dispose()
            {
            }
        }

        public struct BatchEnumerable<T> : IEnumerable<SliceEnumerable<T>>
        {
            private readonly IList<T> _list;
            private readonly int _batchSize;

            public BatchEnumerable(IList<T> list, int batchSize)
            {
                _list = list;
                _batchSize = batchSize;
            }

            public BatchEnumerator<T> GetEnumerator() => new BatchEnumerator<T>(_list, _batchSize);

            IEnumerator<SliceEnumerable<T>> IEnumerable<SliceEnumerable<T>>.GetEnumerator() => GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Serializable]
        public struct BatchEnumerator<T> : IEnumerator<SliceEnumerable<T>>
        {
            private readonly IList<T> _list;
            private readonly int _batchSize;
            private readonly int _totalBatches;
            private int _index;

            public BatchEnumerator(IList<T> list, int batchSize)
            {
                _list = list;
                _batchSize = batchSize;
                Current = new SliceEnumerable<T>();
                _totalBatches = list.Count / batchSize + (list.Count % batchSize == 0 ? 0 : 1);
                _index = 0;
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                IList<T> list = _list;

                if (_index >= _totalBatches)
                {
                    _index = _totalBatches + 1;
                    Current = default;
                    return false;
                }

                int first = _index * _batchSize;
                int last = Math.Min(list.Count, first + _batchSize);
                Current = new SliceEnumerable<T>(list, first, last);
                _index += 1;
                return true;
            }

            public void Reset()
            {
                _index = 0;
                Current = default;
            }

            public SliceEnumerable<T> Current { get; private set; }

            object IEnumerator.Current => Current;
        }
    }
}