using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding
{
    class greedy
    {
        public int FindMaximizedCapital(int k, int W, int[] Profits, int[] Capital)
        {
            if (Profits == null || Capital == null || Profits.Length == 0 || Capital.Length == 0 || Profits.Length != Capital.Length)
            {
                return W;
            }

            int res = 0;
            int max = int.MinValue;
            List<int> ls = new List<int>();
            List<int> list = new List<int>();

            for (int i = 0; i < Profits.Length; i++)
            {
                if (Capital[i] <= W)
                {
                    ls.Add(i);
                }
            }

            foreach (int index in ls)
            {
                if (Profits[index] - Capital[index] > max)
                {
                    max = Profits[index] - Capital[index];
                    res = index;
                }
            }

            for (int i = 0; i < Profits.Length; i++)
            {
                list.Add(i);
            }

            list.Remove(res);
            k--;
            W += max;
            max = W;
            res = -1;
            for (int i = 0; i < k; i++)
            {
                foreach (int index in list)
                {
                    if (W + Profits[index] - Capital[index] > max)
                    {
                        max = W + Profits[index] - Capital[index];
                        res = index;
                    }
                }

                if (max < 0)
                {
                    break;
                }

                list.Remove(res);
            }

            return W;
        }

        //public IList<int[]> GetSkyline(int[,] buildings)
        //{
        //    IList<int[]> res = new List<int[]>();

        //    if(buildings==null||buildings.Length==0)
        //    {
        //        return res;
        //    }

        //    int m = buildings.GetLength(0);
        //    int n = buildings.GetLength(1);
        //    Dictionary<int, int> height = new Dictionary<int, int>();
        //    Dictionary<int, int[]> dct = new Dictionary<int, int[]>();
        //    height.Add(0, 1);

        //    for(int i=0;i<m;i++)
        //    {
        //        dct.Add(i, new int[] { buildings[i, 0], buildings[i, 2] });
        //    }

        //    foreach(int k in dct.Keys)
        //    {
        //        if(height.ContainsKey(k))
        //        {
        //            height.Add(buildings[k, 2], 1);
        //        }
        //        else
        //        {
        //            height[buildings[k, 2]]++;
        //        }


        //    }
        //}

        public double FindMaxAverage(int[] nums, int k)
        {

            if (k <= 0 || nums == null || nums.Length < k)
            {
                return 0;
            }

            int sum = 0;
            int max = 0;

            for (int i = 0; i < k; i++)
            {
                sum += nums[i];
            }
            max = sum;
            for (int i = k; i < nums.Length; i++)
            {
                sum += nums[i] - nums[i - k];

                if (sum > max)
                {
                    max = sum;
                }
            }

            return (double)max / k;
        }

        //public double FindMaxAverage2(int[] nums, int k)
        //{
        //    if (k <= 0 || nums == null || nums.Length < k)
        //    {
        //        return 0;
        //    }

        //    int sum = 0;
        //    int max = 0;

        //    for (int i = 0; i < k; i++)
        //    {
        //        sum += nums[i];
        //    }
        //}

        public int FindLongestChain(int[][] pairs)
        {
            if (pairs == null || pairs.Length == 0)
            {
                return 0;
            }

            int n = pairs.Length;

            Array.Sort(pairs, (a, b) => a[1] - b[1]);

            int count = 0;

            for (int i = 0; i < n; i++)
            {
                count++;
                int currEnd = pairs[i][1];

                while (i + 1 < n && pairs[i + 1][0] <= currEnd)
                {
                    i++;
                }
            }

            return count;
        }

        public bool IsPossible(int[] nums)
        {
            if (nums == null || nums.Length < 3)
            {
                return false;
            }

            Dictionary<int, int> dct = new Dictionary<int, int>();
            Dictionary<int, int> appendDct = new Dictionary<int, int>();

            foreach (int num in nums)
            {
                if (!dct.ContainsKey(num))
                {
                    dct.Add(num, 1);
                }
                else
                {
                    dct[num]++;
                }
            }


            foreach (int num in nums)
            {
                if (dct[num] != 0)
                {
                    if (appendDct.ContainsKey(num) && appendDct[num] > 0)
                    {
                        appendDct[num]--;

                        if (!appendDct.ContainsKey(num + 1))
                        {
                            appendDct.Add(num + 1, 1);
                        }
                        else
                        {
                            appendDct[num + 1]++;
                        }
                    }
                    else if (dct.ContainsKey(num + 1) && dct[num + 1] > 0 && dct.ContainsKey(num + 2) && dct[num + 2] > 0)
                    {
                        dct[num + 1]--;
                        dct[num + 2]--;

                        if (!appendDct.ContainsKey(num + 3))
                        {
                            appendDct.Add(num + 3, 1);
                        }
                        else
                        {
                            appendDct[num + 3]++;
                        }
                    }
                    else
                    {
                        return false;
                    }

                    dct[num]--;
                }
            }

            return true;
        }

        public string RemoveKdigits(string num, int k)
        {
            if (string.IsNullOrEmpty(num))
            {
                return string.Empty;
            }

            if (k <= 0)
            {
                return num;
            }

            int len = num.Length;
            int digits = len - k;
            char[] stk = new char[len];
            int index = 0;

            for (int i = 0; i < len; i++)
            {
                char c = num[i];
                while (index > 0 && stk[index - 1] > c && k > 0)
                {
                    index--;
                    k--;
                }

                stk[index++] = c;
            }

            int j = 0;
            while (j < digits && stk[j] == '0')
            {
                j++;
            }

            return j == digits ? "0" : new string(stk, j, digits - j);
        }

        public int[] MaxNumber(int[] nums1, int[] nums2, int k)
        {
            if (k < 0)
            {
                return new int[0];
            }

            int m = 0, n = 0;

            if (nums1 != null && nums1.Length != 0)
            {
                m = nums1.Length;
            }

            if (nums2 != null && nums2.Length != 0)
            {
                n = nums2.Length;
            }

            int i = 0, j = 0;
            int[] res = new int[k];
            int index = 0;

            while (i < m || j < n)
            {
                int i1 = i, j1 = j;

                while (i1 < m && j1 < n)
                {
                    if (i1 + j + k + 1 <= m + n && nums1[i1] <= nums2[j1])
                    {
                        i1++;
                    }

                    if (i + j1 + k + 1 <= m + n && nums1[i1] >= nums2[j1])
                    {
                        j1++;
                    }
                }

                if (i1 < m)
                {
                    if ((j1 < n && nums1[i1] > nums2[j1]) || j1 >= n)
                    {
                        i = i1;
                        res[index] = nums1[i1];
                    }
                }

                if (j1 < n)
                {
                    if ((i1 < n && nums1[i1] < nums2[j1]) || i1 >= m)
                    {
                        j = j1;
                        res[index] = nums1[j1];
                    }
                }

                index++;
            }

            return res;
        }

        //public int LeastInterval(char[] tasks, int n)
        //{
        //    if (tasks == null || tasks.Length == 0)
        //    {
        //        return 0;
        //    }

        //    if (n < 1)
        //    {
        //        return tasks.Length;
        //    }

        //    Dictionary<char, int> dct = new Dictionary<char, int>();

        //    foreach (char c in tasks)
        //    {
        //        if (dct.ContainsKey(c))
        //        {
        //            dct[c]++;
        //        }
        //        else
        //        {
        //            dct.Add(c, 1);
        //        }
        //    }

        //    dct.OrderBy(dct.Values);

        //    int count = 0;
        //}

        public bool CanJump(int[] nums)
        {
            if (nums == null || nums.Length == 0)
            {
                return true;
            }

            int len = nums.Length;
            int i = 0;

            for (int reach = 0; i <= reach && i < len; i++)
            {
                if (i + nums[i] > reach)
                {
                    reach = nums[i] + i;
                }
            }

            return i == len;
        }

        public int Jump(int[] nums)
        {
            if (nums == null || nums.Length < 2)
            {
                return 0;
            }

            int steps = 0;
            int reach = 0;
            int nextmax = 0;
            int i = 0;


            while (i <= reach)
            {
                while (i <= reach)
                {
                    if (nextmax < i + nums[i])
                    {
                        nextmax = i + nums[i];
                    }

                    if (nextmax >= nums.Length - 1)
                    {
                        return steps + 1;
                    }

                    i++;
                }
                steps++;
                reach = nextmax;
            }

            return 0;
        }

        //public IList<Interval> Merge(IList<Interval> intervals)
        //{
        //    IList<Interval> res = new List<Interval>();

        //    if (intervals.Count < 2)
        //    {
        //        return intervals;
        //    }



        //}

        int CompareInterval(Interval v1,Interval v2)
        {
            if(v1.end==v2.end)
            {
                return v1.start - v2.start;
            }

            return v1.end - v2.end;
        }

        public int[] AdvantageCount1(int[] A, int[] B)
        {
            if ((A == null && B == null) || (A.Length == 0 && B.Length == 0))
            {
                return new int[0];
            }

            SortedDictionary<int, List<int>> sdt = new SortedDictionary<int, List<int>>();
            int n = A.Length;
            int[] res = new int[n];

            for(int i=0;i<n;i++)
            {
                if(!sdt.ContainsKey(B[i]))
                {
                    sdt.Add(B[i], new List<int>());
                }

                sdt[B[i]].Add(i);
            }

            Array.Sort(A);

            int low = 0, high = n - 1;
            
            while(low<=high)
            {
                int num = sdt.Last().Key;
                int m = sdt[num].Count;
                int index = sdt[num].Last();
                sdt[num].RemoveAt(m - 1);

                if(sdt[num].Count==0)
                {
                    sdt.Remove(num);
                }

                if(A[high]>num)
                {
                    res[index] = A[high];
                    high--;
                }
                else
                {
                    res[index] = A[low];
                    low++;
                }
            }

            return res;
        }

        public int[] AdvantageCount(int[] A, int[] B)
        {
            if((A==null&&B==null)||(A.Length==0&&B.Length==0))
            {
                return new int[0];
            }

            int n = B.Length;

            for(int i=0;i<n;i++)
            {
                int j = i;
                int diff = int.MaxValue;
                int index = i;
                int minIndex = i;

                while(j<n)
                {
                    if(A[j]>B[i]&&diff>A[j]-B[i])
                    {
                        diff = A[j] - B[i];
                        index = j;
                    }

                    if(A[minIndex]>A[j])
                    {
                        minIndex = j;
                    }

                    j++;
                }

                if(diff!=int.MaxValue)
                {
                    if(i!=index)
                    {
                        Swap(A, i, index);
                    } 
                }
                else
                {
                    if(i!=minIndex)
                    {
                        Swap(A, i, minIndex);
                    }
                }
            }

            return A;
        }

        void Swap(int[] a,int i,int j)
        {
            int temp = a[i];
            a[i] = a[j];
            a[j] = temp;
        }
    }
}
