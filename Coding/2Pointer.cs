using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding
{
    class _2Pointer
    {
        public bool JudgeSquareSum(int c)
        {
            if (c < 0)
            {
                return false;
            }

            int left = 0, right = (int)Math.Sqrt(c);

            while (left <= right)
            {
                int curr = left * left + right * right;

                if (curr < c)
                {
                    left++;
                }
                else if (curr > c)
                {
                    right--;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        public static string FindLongestWord(string s, IList<string> d)
        {
            if (string.IsNullOrEmpty(s) || d.Count == 0)
            {
                return string.Empty;
            }

            string res = "";

            foreach (string str in d)
            {
                int i = 0;
                foreach (char c in s)
                {
                    if (i < str.Length && c == str[i])
                    {
                        i++;
                    }
                }

                if (str.Length == i && str.Length >= res.Length)
                {
                    if (str.Length > res.Length || str.CompareTo(res) < 0)
                    {
                        res = str;
                    }
                }
            }

            return res;
        }

        public static double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            if ((nums1 == null || nums1.Length == 0) && (nums2 == null && nums2.Length == 0))
            {
                return -1;
            }

            int len1 = nums1.Length, len2 = nums2.Length;

            if (len1 > len2)
            {
                return FindMedianSortedArrays(nums2, nums1);
            }

            int m = (len1 + len2 - 1) / 2;
            int left = 0, right = Math.Min(m, len1);

            while (left < right)
            {
                int midA = (left + right) / 2;
                int midB = m - midA;

                if (nums1[midA] < nums2[midB])
                {
                    left = midA + 1;
                }
                else
                {
                    right = midA;
                }
            }

            double a = Math.Max(left > 0 ? nums1[left - 1] : int.MinValue, m - left >= 0 ? nums2[m - left] : int.MinValue);
            double b = Math.Min(left < len1 ? nums1[left] : int.MaxValue, m - left + 1 < len2 ? nums2[m - left + 1] : int.MaxValue);

            if ((len1 + len2) % 2 != 0)
            {
                return a;
            }
            else
            {
                return (a + b) / 2;
            }
        }

        public static int countTriangles(int[] a)
        {
            if(a==null||a.Length<3)
            {
                return 0;
            }

            int len = a.Length;
            int count = 0;
            Array.Sort(a);

            for(int i=0;i<len-2;i++)
            {
                int left = i + 1;
                int right = len - 1;
                
                while(left<right)
                {
                    if(a[i]+a[left]>a[right])
                    {
                        count += right - left;
                    }
                    else
                    {
                        int low = left, high = right;
                        int mid = (low + high) / 2;

                        while(low<high)
                        {
                            if(a[i]+a[mid]>a[right])
                            {
                                high = mid;
                            }
                            else
                            {
                                low = mid;
                            }

                            mid = (low + high) / 2;
                        }

                        count += right - low;
                    }

                    right--;
                }
            }

            return count;
        }

        public int MaxProfitAssignment(int[] difficulty, int[] profit, int[] worker)
        {
            if(difficulty==null||difficulty.Length==0||profit==null||profit.Length==0||worker==null||worker.Length==0)
            {
                return 0;
            }

            Dictionary<int, int> dct = new Dictionary<int, int>();
            int maxDifficulty = 0;

            for(int i=0;i<difficulty.Length;i++)
            {
                if(dct.ContainsKey(difficulty[i]))
                {
                    dct[difficulty[i]] = Math.Max(profit[i], dct[difficulty[i]]);
                }
                else
                {
                    dct.Add(difficulty[i], profit[i]);
                }

                maxDifficulty = Math.Max(maxDifficulty, difficulty[i]);
            }

            int[] dp = new int[maxDifficulty + 1];

            for(int i=1;i<=maxDifficulty;i++)
            {
                dp[i] = Math.Max(dp[i - 1], dct.ContainsKey(i) ? dct[i] : 0);
            }

            int maxProfits = 0;

            for(int i=0;i<worker.Length;i++)
            {
                if(worker[i]>=maxDifficulty)
                {
                    maxProfits += dp[maxDifficulty];
                }
                else
                {
                    maxProfits += dp[worker[i]];
                }
            }

            return maxProfits;
        }
    }
}
