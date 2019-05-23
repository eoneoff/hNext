using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace hNext.Infrastructure
{
    public class UniqueList<T> : IList<T>, ICollection
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

        public void Add(T item)
        {
            if(!internalList.Contains(item)) internalList.Add(item);
        }

        public void Insert(int index, T item)
        {
            if(!internalList.Contains(item)) internalList.Insert(index, item);
        }

        public int Count => internalList.Count;
        bool ICollection<T>.IsReadOnly => (internalList as ICollection<T>).IsReadOnly;
        public void Clear() => internalList.Clear();
        public bool Contains(T item) => internalList.Contains(item);
        public void CopyTo(T[] array, int arrayIndex) => internalList.CopyTo(array, arrayIndex);
        public IEnumerator<T> GetEnumerator() => internalList.GetEnumerator();
        public int IndexOf(T item) => internalList.IndexOf(item);
        public bool Remove(T item) => internalList.Remove(item);
        public void RemoveAt(int index) => internalList.RemoveAt(index);
        IEnumerator IEnumerable.GetEnumerator() => internalList.GetEnumerator();
        bool ICollection.IsSynchronized => (internalList as ICollection).IsSynchronized;
        object ICollection.SyncRoot => (internalList as ICollection).SyncRoot;
        void ICollection.CopyTo(Array array, int index) => (internalList as ICollection).CopyTo(array, index);
    }
}
