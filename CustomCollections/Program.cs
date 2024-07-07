using CustomCollections;

class Program
{
    static void Main(string[] args)
    {
        CustomList<int> newIntList = new CustomList<int>(5);
        newIntList.Add(0);
        newIntList.Add(1);
        newIntList.Add(2);
        newIntList.Add(3);
        newIntList.Add(4);
        newIntList.Add(5);
        newIntList.Add(6);
        newIntList.Add(7);
        newIntList.Add(8);

        Console.WriteLine();
        for (int i = 0; i < newIntList.Count; i++)
        {
            Console.Write($"{newIntList[i]} ");
        }

        newIntList.RemoveAt(3);

        Console.WriteLine();
        for (int i = 0; i < newIntList.Count; i++)
        {
            Console.Write($"{newIntList[i]} ");
        }


        newIntList.Insert(999, 2);

        Console.WriteLine();
        for (int i = 0; i < newIntList.Count; i++)
        {
            Console.Write($"{newIntList[i]} ");
        }

        CustomTree<int> tree = new CustomTree<int>();

        tree.Add(5);
        tree.Add(3);
        tree.Add(7);
        tree.Add(6);
        tree.Add(1);
        tree.Add(2);
        tree.Add(4);
        tree.Add(9);
        tree.Add(10);
        tree.Add(8);

        int[] arr = tree.ToArray();

        Console.WriteLine();
        Console.WriteLine("TREE");
        foreach (int i in arr)
        {
            Console.Write($"{i} ");
        }



    }
}

