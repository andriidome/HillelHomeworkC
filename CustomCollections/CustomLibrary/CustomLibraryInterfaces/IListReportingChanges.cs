using System;
namespace CustomLibraryInterfaces
{
	public interface IReportingChanges<T>
	{
        public event EventHandler<ItemAddedEventArgs>? ItemAdded;

        public event EventHandler<ItemRemovedEventArgs>? ItemRemoved;

        public event EventHandler<ItemInsertedEventArgs>? ItemInserted;

        public class ItemAddedEventArgs : EventArgs
        {
            public T AddedItem { get; set; }
        }

        public class ItemRemovedEventArgs : EventArgs
        {
            public T RemovedItem { get; set; }
        }

        public class ItemInsertedEventArgs : EventArgs
        {
            public T Item { get; set; }
            public int Index { get; set; }
        }
    }
}

