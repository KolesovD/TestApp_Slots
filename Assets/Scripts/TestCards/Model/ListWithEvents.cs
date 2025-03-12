using AxGrid;
using System.Collections;
using System.Collections.Generic;

namespace TestCards.Model
{
    public class ListWithEvents<T> : IList<T>
    {
        private string _listId;
        private List<T> _list;

        public ListWithEvents(string listId)
        {
            _listId = listId;

            _list = new();
            FireOnCountChangedEvent();
        }

        private void FireOnCountChangedEvent()
        {
            Settings.Model.Set($"{_listId}Count", _list.Count);
        }

        public T this[int index] { get => _list[index]; set => _list[index] = value; }

        public int Count => _list.Count;

        public bool IsReadOnly => (_list as ICollection<T>).IsReadOnly;

        public void Add(T item)
        {
            _list.Add(item);
            FireOnCountChangedEvent();
        }

        public void Clear()
        {
            _list.Clear();
            FireOnCountChangedEvent();
        }

        public bool Contains(T item) => _list.Contains(item);

        public void CopyTo(T[] array, int arrayIndex) => _list.CopyTo(array, arrayIndex);

        public IEnumerator<T> GetEnumerator() => _list.GetEnumerator();

        public int IndexOf(T item) => _list.IndexOf(item);

        public void Insert(int index, T item)
        {
            _list.Insert(index, item);
            FireOnCountChangedEvent();
        }

        public bool Remove(T item)
        {
            bool result = _list.Remove(item);

            if (result)
                FireOnCountChangedEvent();

            return result;
        }

        public void RemoveAt(int index)
        {
            _list.RemoveAt(index);
            FireOnCountChangedEvent();
        }

        IEnumerator IEnumerable.GetEnumerator() => (_list as IEnumerable).GetEnumerator();
    }
}
