using System;
namespace CustomLibraryInterfaces
{
	public interface ICustomCollection<T>
	{
        public int Count { get; }

        public void Add(T obj);

        public bool Contains(T obj);

        public T?[] ToArray();

    }
}
