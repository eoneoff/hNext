using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace hNext.Infrastructure
{
    public class UniqueList<T> : IList<T>
    {
        private List<T> internalList = new List<T>();

        public T this[int index]
        {
            get => internalList[index];
            set
            {
                if (!internalList.Contains(value)) internalList[index] = value;
            }
        }

        public int Count => internalList.Count;

        bool ICollection<T>.IsReadOnly { get; }

        public void Add(T item)
        {
            if(!internalList.Contains(item)) internalList.Add(item);
        }

        public void Clear()
        {
            internalList.Clear();
        }

        public bool Contains(T item)
        {
            return internalList.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            internalList.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return internalList.GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return internalList.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            if(!internalList.Contains(item)) internalList.Insert(index, item);
        }

        public bool Remove(T item)
        {
            return internalList.Remove(item);
        }

        public void RemoveAt(int index)
        {
            internalList.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return internalList.GetEnumerator();
        }
    }
}
