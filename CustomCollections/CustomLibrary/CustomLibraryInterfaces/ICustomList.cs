using System;
namespace CustomLibraryInterfaces
{
    public interface ICustomList<T>: ICustomCollection<T>
    {
        public void Insert(T obj, int index);

        public void Remove(T obj);

        public void RemoveAt(int index);

        public void Clear();

        public int IndexOf(T obj);

        public void Reverse();

        public T? this[int index]
        {
            get; set;
        }
    }
}

