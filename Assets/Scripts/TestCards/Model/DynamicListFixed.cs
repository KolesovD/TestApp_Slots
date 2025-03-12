using AxGrid.Model;
using System.Collections;
using System.Collections.Generic;

namespace TestCards.Model
{
    public class DynamicListFixed<T> : IList<T>, IDynamicObject
    {
        public DynamicModel ModelLink { get; set; }
        public string ModelField { get; set; }

        private List<T> _baseList;

        public DynamicListFixed()
        {
            _baseList = new List<T>();
        }

        public void Refresh()
        {
            ModelLink.Refresh(ModelField);
        }


        public IEnumerator<T> GetEnumerator()
        {
            return _baseList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _baseList.GetEnumerator();
        }

        public void Add(T item)
        {
            _baseList.Add(item);
            Refresh();
        }

        public void Clear()
        {
            _baseList.Clear();
            Refresh();
        }

        public bool Contains(T item)
        {
            return _baseList.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _baseList.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            if (_baseList.Remove(item))
            {
                Refresh();
                return true;
            }
            return false;
        }

        public int Count => _baseList.Count;
        public bool IsReadOnly => (_baseList as IList<T>).IsReadOnly;

        public int IndexOf(T item)
        {
            return _baseList.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            _baseList.Insert(index, item);
            Refresh();
        }

        public void RemoveAt(int index)
        {
            _baseList.RemoveAt(index);
            Refresh();
        }

        public T this[int index]
        {
            get => _baseList[index];
            set
            {
                _baseList[index] = value;
                Refresh();
            }
        }
    }
}
