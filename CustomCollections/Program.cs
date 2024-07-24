using CustomCollections;
using CustomLibraryInterfaces;

class Program
{
    private static void c_ItemAdded(object sender, IListReportingChanges<int>.ItemAddedEventArgs e)
    {
        Console.WriteLine($"A new item was added to the list: {e.AddedItem}");
    }

    private static void c_ItemRemoved(object sender, IListReportingChanges<int>.ItemRemovedEventArgs e)
    {
        Console.WriteLine($"An item was removed from the list: {e.RemovedItem}");
    }

    private static void c_ItemInserted(object sender, IListReportingChanges<int>.ItemInsertedEventArgs e)
    {
        Console.WriteLine($"An item '{e.Item}' was inserted at index: {e.Index}");
    }

    static void Main(string[] args)
    {


        CustomList<int> newIntList = new CustomList<int>(5);

        newIntList.ItemAdded += c_ItemAdded;
        newIntList.ItemRemoved += c_ItemRemoved;
        newIntList.ItemInserted += c_ItemInserted;

        newIntList.Add(0);
        newIntList.Add(1);
        newIntList.Add(2);
        newIntList.Add(3);
        newIntList.Add(4);
        newIntList.Add(5);
        newIntList.Add(6);
        newIntList.Add(7);
        newIntList.Add(8);

        //Console.WriteLine();
        //for (int i = 0; i < newIntList.Count; i++)
        //{
        //    Console.Write($"{newIntList[i]} ");
        //}

        newIntList.RemoveAt(3);

        //Console.WriteLine();
        //for (int i = 0; i < newIntList.Count; i++)
        //{
        //    Console.Write($"{newIntList[i]} ");
        //}


        newIntList.Insert(999, 2);

        //Console.WriteLine();
        //for (int i = 0; i < newIntList.Count; i++)
        //{
        //    Console.Write($"{newIntList[i]} ");
        //}

        //CustomTree<int> tree = new CustomTree<int>();

        //tree.Add(5);
        //tree.Add(3);
        //tree.Add(7);
        //tree.Add(6);
        //tree.Add(1);
        //tree.Add(2);
        //tree.Add(4);
        //tree.Add(9);
        //tree.Add(10);
        //tree.Add(8);

        //int[] arr = tree.ToArray();

        //Console.WriteLine();
        //Console.WriteLine("TREE");
        //foreach (int i in arr)
        //{
        //    Console.Write($"{i} ");
        //}


        Console.ReadLine();
    }
}

