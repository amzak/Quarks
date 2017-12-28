using System;
using System.Collections.Generic;

namespace Codestellation.Quarks.Pooling
{
    public class EagerPool<TCollection, TItem>
        //It doesn't make sense for struct, because structs normally don't allocate memory from heap
        where TCollection : class, ICollection<TItem>

    {
        private int _pointer;
        private readonly Func<TCollection> _makeObject;
        private readonly TCollection[] _pool;

        public EagerPool(int maxSize, Func<TCollection> makeObject)
        {
            _makeObject = makeObject;
            _pointer = -1;
            _pool = new TCollection[maxSize];

            for (int i = 0; i < _pool.Length; i++)
            {
                Return(makeObject());
            }
        }

        public TCollection Take()
        {
            return _pointer >= 0 ? _pool[_pointer--] : _makeObject();
        }

        public void Return(TCollection item)
        {
            item.Clear();
            if (_pointer < _pool.Length && GC.GetGeneration(item) > 0)
            {
                _pool[++_pointer] = item;
            }
        }
    }
}