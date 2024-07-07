using System;
namespace CustomCollections
{
	public class CustomNode<T>
	{
		private CustomNode<T>? left;
		private CustomNode<T>? right;
		private T? key;

		public CustomNode(T? value)
		{
			key = value;
			left = null;
			right = null;
		}

        public T Key
        {
            get
            {
                return key;
            }
            set
            {
                key = value;
            }
        }

        public CustomNode<T> Left
        {
            get
            {
                return left;
            }
            set
            {
                left = value;
            }
        }

        public CustomNode<T> Right
        {
            get
            {
                return right;
            }
            set
            {
                right = value;
            }
        }
    }

	public class CustomTree<T> where T : IComparable<T>
    {
		private CustomNode<T> root;
		public int Count { get; private set; }

        public CustomNode<T> Root
        {
            get
            {
                return root;
            }
            set
            {
                root = value;
            }
        }

        public bool Contains(T? value)
        {
            return Contains(Root, value);
        }

        private bool Contains(CustomNode<T>? root, T? value)
        {
            if (root == null || root.Key?.CompareTo(value) == 0)
            {
                return root != null;
            }

            if (value?.CompareTo(root.Key) < 0)
            {
                return Contains(root.Left, value);
            }
            else
            {
                return Contains(root.Right, value);
            }
        }


        public void Add(T? value)
        {
            root = Add(root, value);
        }

        private CustomNode<T> Add(CustomNode<T>? root, T? value)
        {
            if (root == null)
            {
                root = new CustomNode<T>(value);
                Count++;
                return root;
            }

            var comparisonResult = value?.CompareTo(root.Key);
            if (comparisonResult < 0)
            {
                root.Left = Add(root.Left, value);
            }
            else if (comparisonResult > 0)
            {
                root.Right = Add(root.Right, value);
            }

            return root;
        }

        public T?[] ToArray()
        {
            T?[] result = new T?[Count];
            int index = 0;

            void ConvertToArray(CustomNode<T>? node)
            {
                if (node != null)
                {
                    ConvertToArray(node.Left);
                    result[index++] = node.Key;
                    ConvertToArray(node.Right);
                }
            }

            ConvertToArray(Root);
            return result;
        }
    }
}

