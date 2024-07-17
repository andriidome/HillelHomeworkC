using System;
namespace CustomLibraryInterfaces
{
	public interface ICustomTree<T>: ICustomCollection<T>
    {
		public void Clear();
    }
}

