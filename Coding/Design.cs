using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding
{
    class Design
    {

    }

    public class Solution
    {
        private int[] nums;
        private Random random;
        public Solution(int[] nums)
        {
            this.nums = nums;
            this.random = new Random();
        }

        /** Resets the array to its original configuration and return it. */
        public int[] Reset()
        {
            return nums;
        }

        /** Returns a random shuffling of the array. */
        public int[] Shuffle()
        {
            if(nums==null)
            {
                return nums;
            }

            int[] arr = (int[])nums.Clone();

            for(int j=1;j<arr.Length;j++)
            {
                int i = random.Next(j + 1);
                swap(arr, j, i);
            }

            return arr;
        }

        private void swap(int[] a, int i, int j)
        {
            int t = a[i];
            a[i] = a[j];
            a[j] = t;
        }
    }

    public class myclass<T>
    {

        List<T> ls = new List<T>();
        Dictionary<T, int> dct = new Dictionary<T, int>();
        Random rand = new Random();
        void Add(T item)
        {
            if(!dct.ContainsKey(item))
            {
                int num = ls.Count;
                dct.Add(item, num);
                ls.Add(item);
            }
        }

        void Remove(T item)
        {
            if(dct.ContainsKey(item))
            {
                int n = dct[item];
                int index = ls.Count - 1;
                T lastItem = ls[index];
                dct[lastItem] = n;
                dct[item] = index;
                ls[n] = lastItem;
                ls[index] = item;

                dct.Remove(item);
                ls.RemoveAt(index);
            }
        }

        public T RemoveRandom()
        {
            T item = ls[rand.Next(ls.Count - 1)];

            int n = dct[item];
            int index = ls.Count - 1;
            T lastItem = ls[index];
            dct[lastItem] = n;
            dct[item] = index;
            ls[n] = lastItem;
            ls[index] = item;
            dct.Remove(item);
            ls.Remove(item);
            return item;
        }
    }

    public class myclass1<T>
    {

        List<int> ls = new List<int>();
        Dictionary<T, int> dct = new Dictionary<T, int>();
        Dictionary<int, T> dct1 = new Dictionary<int, T>();
        Random rand = new Random();
        void Add(T item)
        {
            if (!dct.ContainsKey(item))
            {
                int num = ls.Count;
                dct.Add(item, num);
                dct1.Add(num, item);
                ls.Add(num);
            }
        }

        void Remove(T item)
        {
            if (dct.ContainsKey(item))
            {
                int n = dct[item];
                int index = ls.Count - 1;
                T lastItem = dct1[index];
                dct[lastItem] = n;
                dct1[n] = lastItem;
                dct1[index] = item;
                dct[item] = index;
                ls[n] = index;
                ls[index] = n;

                dct.Remove(item);
                dct1.Remove(index);
                ls.RemoveAt(index);
            }
        }

        public T RemoveRandom()
        {
            int itemIndex = ls[rand.Next(ls.Count - 1)];

            T item = dct1[itemIndex];
            int index = ls.Count - 1;
            T lastItem = dct1[index];
            dct[lastItem] = itemIndex;
            dct[item] = index;
            dct1[index] = item;
            dct1[itemIndex] = lastItem;
            ls[index] = itemIndex;
            ls[itemIndex] = index;
            dct.Remove(item);
            ls.Remove(itemIndex);

            return item;
        }
    }
}
