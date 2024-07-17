using System;
using CustomLibraryInterfaces;

namespace CustomCollections
{
	public class CustomList<T>:ICustomList<T>
	{
		private T?[] underlyingArray;
        private int capacity;

        private int usedCapacity = 0;

		public CustomList(int capacity = 4)
		{
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            else if (capacity == 0)
            {
                underlyingArray = Array.Empty<T?>();
                this.capacity = 0;
            }
            else
            {
                underlyingArray = new T?[capacity];
                this.capacity = capacity;
            }
        }

        public int Count
        {
            get
            {
                return usedCapacity;
            }
        }

        public void Add(T obj)
		{
            if (underlyingArray.Length == 0)
            {
                capacity = 4;
                underlyingArray = new T?[capacity];
            }

            EnsureCapacity(usedCapacity + 1);

            underlyingArray[usedCapacity] = obj;
            usedCapacity++;
        }

        private void EnsureCapacity(int projectedCapacity)
        {
            if (underlyingArray.Length < projectedCapacity)
            {
                capacity *= 2;
                T?[] arrayExtension = new T?[capacity];
                for (int i = 0; i < underlyingArray.Length; i++)
                {
                    arrayExtension[i] = underlyingArray[i];
                }
                underlyingArray = arrayExtension;
            }
        }

        public void Insert(T obj, int index)
        {
            EnsureCapacity(usedCapacity + 1);

            if (index < 0 || index > capacity)
            {
                throw new IndexOutOfRangeException();
            }

            for (int i = usedCapacity; i >= index; i--)
            {
                underlyingArray[i] = underlyingArray[i - 1];
            }

            underlyingArray[index] = obj;

            usedCapacity++;
        }

        public void Remove(T obj)
        {
            int index = IndexOf(obj);
            RemoveAt(index);
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= usedCapacity)
            {
                throw new IndexOutOfRangeException();
            }

            if (index == usedCapacity - 1)
            {
                underlyingArray = underlyingArray[..^1];
                usedCapacity--;
            }
            else if (index == 0)
            {
                underlyingArray = underlyingArray[0..];
                usedCapacity--;
            }
            else
            {
                for (int i = index; i < underlyingArray.Length - 1; i++)
                {
                    underlyingArray[i] = underlyingArray[i + 1];
                }
                underlyingArray[underlyingArray.Length - 1] = default(T);
                usedCapacity--;
            }
        }

        public void Clear()
        {
            for (int i =0; i < usedCapacity; i++)
            {
                underlyingArray[i] = default(T);
            }
            usedCapacity = 0;
        }

        public bool Contains(T obj)
        {
            for (int i = 0; i < usedCapacity; i++)
            {
                if (Equals(obj, underlyingArray[i]))
                {
                    return true;
                }
            }

            return false;
        }

        public int IndexOf(T obj)
        {
            for (int i = 0; i < underlyingArray.Length; i++)
            {
                if (Equals(underlyingArray[i], obj))
                {
                    return i;
                }
            }

            return -1;
        }

        public T?[] ToArray()
        {
            return underlyingArray[0..usedCapacity];
        }

        public void Reverse()
        {
            for (int i = 0; i < usedCapacity / 2; i++)
            {
                (underlyingArray[i], underlyingArray[usedCapacity - i - 1]) = (underlyingArray[usedCapacity - i - 1], underlyingArray[i]);
            }
        }

        public T? this[int index] {
            get
            {
                if (index >= usedCapacity || index < 0)
                {
                    throw new IndexOutOfRangeException();
                }

                return underlyingArray[index];
            }

            set
            {
                if (index >= usedCapacity || index < 0)
                {
                    throw new IndexOutOfRangeException();
                }

                underlyingArray[index] = value;
            }
        }
    }
}

