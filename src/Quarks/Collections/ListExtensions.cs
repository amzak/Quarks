using System;
using System.Collections;
using System.Collections.Generic;

namespace Codestellation.Quarks.Collections
{
    public static class ListExtensions
    {
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

            public SliceEnumerator<T> GetEnumerator()
            {
                return new SliceEnumerator<T>(_list, _from, _to);
            }

            IEnumerator<T> IEnumerable<T>.GetEnumerator()
            {
                return new SliceEnumerator<T>(_list, _from, _to);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return new SliceEnumerator<T>(_list, _from, _to);
            }
        }

        [Serializable]
        public struct SliceEnumerator<T> : IEnumerator<T>
        {
            private readonly IList<T> _list;
            private readonly int _from;
            private readonly int _to;
            private int _index;
            private T _current;

            public T Current => _current;

            object IEnumerator.Current => _current;

            public SliceEnumerator(IList<T> list, int from, int to)
            {
                _list = list;
                _from = from;
                _to = to;
                _index = from;
                _current = default(T);
            }

            public bool MoveNext()
            {
                IList<T> list = _list;
                if (_index >= _to)
                {
                    _index = _to + 1;
                    _current = default(T);
                    return false;
                }
                _current = list[_index];
                _index += 1;
                return true;
            }

            void IEnumerator.Reset()
            {
                _index = _from;
                _current = default(T);
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

            public BatchEnumerator<T> GetEnumerator()
            {
                return new BatchEnumerator<T>(_list, _batchSize);
            }

            IEnumerator<SliceEnumerable<T>> IEnumerable<SliceEnumerable<T>>.GetEnumerator()
            {
                return GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        [Serializable]
        public struct BatchEnumerator<T> : IEnumerator<SliceEnumerable<T>>
        {
            private readonly IList<T> _list;
            private readonly int _batchSize;
            private readonly int _totalBatches;
            private SliceEnumerable<T> _current;
            private int _index;

            public BatchEnumerator(IList<T> list, int batchSize)
            {
                _list = list;
                _batchSize = batchSize;
                _current = new SliceEnumerable<T>();
                _totalBatches = (list.Count / batchSize) + (list.Count % batchSize == 0 ? 0 : 1);
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
                    _current = default(SliceEnumerable<T>);
                    return false;
                }
                var first = _index * _batchSize;
                var last = Math.Min(list.Count, first + _batchSize);
                _current = new SliceEnumerable<T>(list, first, last);
                _index += 1;
                return true;
            }

            public void Reset()
            {
                _index = 0;
                _current = default(SliceEnumerable<T>);
            }

            public SliceEnumerable<T> Current => _current;

            object IEnumerator.Current => _current;
        }

        public static BatchEnumerable<T> Partition<T>(this List<T> self, int batchSize)
        {
            return new BatchEnumerable<T>(self, batchSize);
        }
    }
}