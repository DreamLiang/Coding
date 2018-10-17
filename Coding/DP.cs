using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding
{
    class DP
    {
        public bool CheckSubarraySum(int[] nums, int k)
        {
            if (nums == null || nums.Length < 2)
            {
                return false;
            }

            int len = nums.Length;

            int[,] arr = new int[len, len];

            for (int i = 0; i < len; i++)
            {
                arr[i, i] = nums[i];

                for (int j = i + 1; j < len; j++)
                {
                    arr[i, j] += arr[i, j - 1] + nums[j];

                    if ((k != 0 && arr[i, j] % k == 0) || (k == 0 && arr[i, j] == 0))
                    {
                        return true;
                    }

                }
            }

            return false;
        }

        public bool CheckSubarraySum1(int[] nums, int k)
        {
            if (nums == null || nums.Length < 2)
            {
                return false;
            }

            int len = nums.Length;

            Dictionary<int, int> dct = new Dictionary<int, int>();
            int presum = 0;
            dct.Add(0, -1);

            for (int i = 0; i < len; i++)
            {
                presum += nums[i];

                if (k != 0)
                {
                    presum %= k;
                }

                if (dct.ContainsKey(presum))
                {
                    if (i - dct[presum] > 1)
                    {
                        return true;
                    }
                }
                else
                {
                    dct.Add(presum, i);
                }
            }

            return false;
        }

        public int[] CountBits(int num)
        {
            if (num < 0)
            {
                return new int[0];
            }

            int[] dp = new int[num + 1];
            int offset = 1;

            for (int i = 1; i <= num; i++)
            {
                if (offset * 2 == i)
                {
                    offset *= 2;
                }

                dp[i] = dp[i - offset] + 1;
            }

            return dp;
        }

        public int MaxRotateFunction(int[] A)
        {
            if (A == null || A.Length < 2)
            {
                return 0;
            }

            int len = A.Length;
            long max = int.MinValue;
            int s = 0;
            int f0 = 0;

            for (int i = 0; i < len; i++)
            {
                s += A[i];
            }

            for (int i = 0; i < len; i++)
            {
                f0 += i * A[i];
            }

            if (max < f0)
            {
                max = f0;
            }

            int f1 = 0;

            for (int i = 1; i < len; i++)
            {
                f1 = f0 + s - len * A[len - i];

                if (f1 > max)
                {
                    max = f1;
                }

                f0 = f1;
            }

            return (int)max;
        }

        public int FindTargetSumWays(int[] nums, int S)
        {
            if (nums == null || nums.Length == 0)
            {
                return 0;
            }

            int sum = 0;

            foreach (int num in nums)
            {
                sum += num;
            }

            if ((sum + S) % 2 != 0 || sum < S)
            {
                return 0;
            }

            return GetSubSet(nums, (sum + S) / 2);
        }

        int GetSubSet(int[] nums, int target)
        {
            int[] dp = new int[target + 1];
            dp[0] = 1;

            foreach (int num in nums)
            {
                for (int i = target; i >= num; i--)
                {
                    dp[i] += dp[i - num];
                }
            }

            return dp[target];
        }

        public int CountNumbersWithUniqueDigits(int n)
        {
            if (n == 0)
            {
                return 1;
            }

            int res = 10;
            int uniqueDig = 9, availableNumber = 9;

            while (n > 1 && availableNumber > 0)
            {
                uniqueDig *= availableNumber;
                res += uniqueDig;
                availableNumber--;
                n--;
            }

            return res;
        }

        public int NumTrees(int n)
        {
            if (n < 0)
            {
                return 0;
            }

            int[] dp = new int[n + 1];
            dp[0] = 1;
            dp[1] = 1;

            for (int i = 2; i <= n; i++)
            {
                for (int j = 1; j <= i; j++)
                {
                    dp[i] += dp[i - j] * dp[j - 1];
                }
            }

            return dp[n];
        }

        public IList<TreeNode> GenerateTrees(int n)
        {
            if (n <= 0)
            {
                return new List<TreeNode>();
            }

            return DoGenerateTrees(1, n);
        }

        IList<TreeNode> DoGenerateTrees(int start, int end)
        {
            IList<TreeNode> res = new List<TreeNode>();

            if (start > end)
            {
                res.Add(null);
                return res;
            }

            for (int i = start; i <= end; i++)
            {
                IList<TreeNode> left = DoGenerateTrees(start, i - 1);
                IList<TreeNode> right = DoGenerateTrees(i + 1, end);

                foreach (TreeNode r in right)
                {
                    foreach (TreeNode l in left)
                    {
                        TreeNode node = new TreeNode(i);
                        node.left = l;
                        node.right = r;
                        res.Add(node);
                    }
                }
            }

            return res;
        }

        public int MinSteps(int n)
        {
            if (n == 0 || n == 1)
            {
                return 0;
            }

            int[] dp = new int[n + 1];

            for (int i = 2; i <= n; i++)
            {
                dp[i] = i;

                for (int j = i - 1; j > 1; j--)
                {
                    if (i % j == 0)
                    {
                        dp[i] = dp[j] + i / j;
                        break;
                    }
                }
            }

            return dp[n];
        }

        public int MaxA(int N)
        {
            if (N <= 0)
            {
                return 0;
            }

            int[] dp = new int[N + 1];

            for (int i = 1; i <= N; i++)
            {
                dp[i] = i;

                for (int j = 3; j < i; j++)
                {
                    dp[i] = Math.Max(dp[i], dp[i - j] * (j - 1));
                }
            }

            return dp[N];
        }

        public int plantFlowers(int[] field)
        {
            if (field == null || field.Length == 0)
            {
                return 0;
            }

            if (field.Length == 1 && field[0] == 0)
            {
                return 1;
            }

            int len = field.Length;

            int[] dp = new int[len];

            for (int i = 0; i < len - 1; i++)
            {
                if (field[i] == 0 && field[i + 1] == 0)
                {
                    if (i == 0)
                    {
                        dp[i] = 1;
                        i++;
                    }
                    else if (i >= 2 && dp[i - 1] == 0)
                    {
                        dp[i] = dp[i - 2] + 1;
                    }
                    else
                    {
                        dp[i] = dp[i - 1];
                    }
                }
            }

            return dp[len - 1];
        }

        public int MinPathSum(int[,] grid)
        {
            if (grid == null || grid.Length == 0)
            {
                return 0;
            }

            int m = grid.GetLength(0);
            int n = grid.GetLength(1);
            int[,] dp = new int[m, n];
            dp[0, 0] = grid[0, 0];

            for (int i = 1; i < n; i++)
            {
                dp[0, i] = dp[0, i - 1] + grid[0, i];
            }

            for (int i = 1; i < m; i++)
            {
                dp[i, 0] = grid[i, 0] + dp[i - 1, 0];
            }

            for (int i = 1; i < m; i++)
            {
                for (int j = 1; j < n; j++)
                {
                    dp[i, j] = Math.Min(dp[i - 1, j], dp[i, j - 1]) + grid[i, j];
                }
            }

            return dp[m - 1, n - 1];
        }

        public int ClimbStairs(int n)
        {
            if (n <= 0)
            {
                return 0;
            }

            if (n <= 2)
            {
                return n;
            }

            int[] dp = new int[n + 1];
            dp[0] = 0;
            dp[1] = 1;
            dp[2] = 2;
            for (int i = 3; i <= n; i++)
            {
                dp[i] = dp[i - 1] + dp[i - 2];
            }

            return dp[n];
        }

        public bool IsMatch(string s, string p)
        {
            if (s == null || p == null)
            {
                return false;
            }

            int m = s.Length, n = p.Length;
            bool[,] dp = new bool[m + 1, n + 1];
            dp[0, 0] = true;

            for (int i = 0; i < n; i++)
            {
                if (p[i] == '*' && dp[0, i - 1])
                {
                    dp[0, i + 1] = true;
                }
            }

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (p[j] == '.')
                    {
                        dp[i + 1, j + 1] = dp[i, j];
                        continue;
                    }

                    if (s[i] == p[j])
                    {
                        dp[i + 1, j + 1] = dp[i, j];
                        continue;
                    }

                    if (p[j] == '*')
                    {
                        if (p[j - 1] != s[i] && p[j - 1] != '.')
                        {
                            dp[i + 1, j + 1] = dp[i + 1, j - 1];
                        }
                        else
                        {
                            dp[i + 1, j + 1] = dp[i + 1, j] || dp[i, j + 1] || dp[i + 1, j - 1];
                        }
                    }
                }
            }

            return dp[m, n];
        }

        public int MinDistance(string word1, string word2)
        {
            int m = word1.Length;
            int n = word2.Length;

            int[,] costs = new int[m + 1, n + 1];

            for (int i = 0; i <= m; i++)
            {
                costs[i, 0] = i;
            }

            for (int i = 0; i <= n; i++)
            {
                costs[0, i] = i;
            }

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (word1[i] == word2[j])
                    {
                        costs[i + 1, j + 1] = costs[i, j];
                    }
                    else
                    {
                        int a = costs[i, j];
                        int b = costs[i, j + 1];
                        int c = costs[i + 1, j];

                        if (a < b)
                        {
                            if (a < c)
                            {
                                costs[i + 1, j + 1] = a;
                            }
                            else
                            {
                                costs[i + 1, j + 1] = c;
                            }
                        }
                        else
                        {
                            if (b > c)
                            {
                                costs[i + 1, j + 1] = c;
                            }
                            else
                            {
                                costs[i + 1, j + 1] = b;
                            }
                        }

                        costs[i + 1, j + 1]++;
                    }
                }
            }

            return costs[m, n];
        }

        public int NumDecodings(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return 0;
            }

            int n = s.Length;
            int[] dp = new int[n + 1];
            dp[n] = 1;

            if (s[n - 1] != '0')
            {
                dp[n - 1] = 1;
            }
            else
            {
                dp[n - 1] = 0;
            }

            for (int i = n - 2; i >= 0; i--)
            {
                if (s[i] == '0')
                {
                    continue;
                }

                if (int.Parse(s.Substring(i, 2)) <= 26)
                {
                    dp[i] = dp[i + 1] + dp[i + 2];
                }
                else
                {
                    dp[i] = dp[i + 1];
                }
            }

            return dp[0];
        }

        public int FindShortestSubArray(int[] nums)
        {
            if (nums == null || nums.Length == 0)
            {
                return 0;
            }

            Dictionary<int, List<int>> dct = new Dictionary<int, List<int>>();
            int maxDegree = 0;
            int res = nums.Length;

            for (int i = 0; i < nums.Length; i++)
            {
                if (!dct.ContainsKey(nums[i]))
                {
                    dct.Add(nums[i], new List<int>());
                }

                dct[nums[i]].Add(i);
            }

            foreach (int m in dct.Keys)
            {
                if (dct[m].Count > maxDegree)
                {
                    maxDegree = dct[m].Count;
                }
            }

            foreach (int key in dct.Keys)
            {
                if (dct[key].Count == maxDegree)
                {
                    int len = dct[key].Last() - dct[key].First() + 1;

                    if (len < res)
                    {
                        res = len;
                    }
                }
            }

            return res;
        }

        public int CountBinarySubstrings(string s)
        {
            if (s == null || s.Length < 2)
            {
                return 0;
            }

            int count = 0;
            int n = s.Length;

            for (int i = 0; i < s.Length; i++)
            {
                int count0 = 0, count1 = 0;

                int k = i;

                if (s[k] == '0')
                {
                    while (k < n && s[k] == '0')
                    {
                        count0++;
                        k++;
                    }

                    while (k < n && s[k] == '1')
                    {
                        count1++;
                        k++;

                        if (count0 == count1)
                        {
                            count++;
                            break;
                        }
                    }
                }
                else if (s[k] == '1')
                {
                    while (k < n && s[k] == '1')
                    {
                        count1++;
                        k++;
                    }

                    while (k < n && s[k] == '0')
                    {
                        count0++;
                        k++;

                        if (count0 == count1)
                        {
                            count++;
                            break;
                        }
                    }
                }
            }

            return count;
        }

        public bool CanPartitionKSubsets(int[] nums, int k)
        {
            if (nums.Length == 0 || k <= 0)
            {
                return false;
            }

            int sum = 0;

            foreach (int num in nums)
            {
                sum += num;
            }

            if (sum % k != 0)
            {
                return false;
            }

            int target = sum / k;

            return CanPartitionKSubsetsHelper(nums, k, target, 0, target);

        }

        bool CanPartitionKSubsetsHelper(int[] nums, int k, int target, int start, int sum)
        {
            if (k == 1)
            {
                return true;
            }

            if (sum == 0)
            {
                return CanPartitionKSubsetsHelper(nums, k - 1, target, 0, target);
            }

            for (int i = start; i < nums.Length; i++)
            {
                if (CanPartitionKSubsetsHelper(nums, k, target, i + 1, sum - nums[i]))
                {
                    return true;
                }
            }

            return false;
        }

        public bool CanPartitionKSubsetsDP(int[] nums, int k)
        {
            if (nums.Length == 0 || k <= 0)
            {
                return false;
            }

            int sum = 0;

            foreach (int num in nums)
            {
                sum += num;
            }

            if (sum % k != 0)
            {
                return false;
            }

            int target = sum / k;

            bool[] res = new bool[target + 1];
            res[0] = true;

            for (int i = 0; i < nums.Length; i++)
            {
                for (int j = target; j >= nums[i]; j--)
                {
                    res[j] = res[j] || res[j - nums[i]];
                }

                if (res[target])
                {
                    return true;
                }
            }

            return res[target];

        }

        public IList<int> FallingSquares(int[,] positions)
        {
            IList<int> res = new List<int>();

            if (positions == null || positions.Length == 0)
            {
                return res;
            }

            int currHeight = positions[0, 1];
            int currLeft = positions[0, 0], currRight = positions[0, 0] + currHeight;
            int len = positions.GetLength(0);

            res.Add(currHeight);

            for (int i = 1; i < len; i++)
            {
                if (positions[i, 0] >= currRight || positions[i, 0] + positions[i, 1] <= currLeft)
                {
                    currHeight = Math.Max(currHeight, positions[i, 1]);

                    if (positions[i, 0] >= currRight)
                    {
                        currRight = positions[i, 0] + positions[i, 1];
                    }
                    else
                    {
                        currLeft = positions[i, 0];
                    }

                }
                else
                {
                    currHeight += positions[i, 1];

                    if (currRight < positions[i, 0] + positions[i, 1])
                    {
                        currRight = positions[i, 0] + positions[i, 1];
                    }

                    if (currLeft > positions[i, 0])
                    {
                        currLeft = positions[i, 0];
                    }
                }

                res.Add(currHeight);
            }

            return res;
        }

        public int MaximumProduct(int[] nums)
        {
            if (nums == null || nums.Length < 3)
            {
                return int.MinValue;
            }

            int[,] res = new int[3, 2];

            for (int i = 0; i < 3; i++)
            {
                res[i, 0] = 1;
                res[i, 1] = 1;
            }

            for (int i = 0; i < nums.Length; i++)
            {
                for (int j = 2; j >= 0; j--)
                {
                    if (i <= j)
                    {
                        res[j, 0] *= nums[i];
                        res[j, 1] *= nums[i];
                    }
                    else if (j == 0)
                    {
                        res[j, 0] = Math.Min(res[j, 0], nums[i]);
                        res[j, 1] = Math.Max(res[j, 1], nums[i]);
                    }
                    else
                    {
                        res[j, 0] = Math.Min(res[j, 0], Math.Min(res[j - 1, 0] * nums[i], res[j - 1, 1] * nums[i]));
                        res[j, 1] = Math.Max(res[j, 1], Math.Max(res[j - 1, 0] * nums[i], res[j - 1, 1] * nums[i]));
                    }
                }
            }

            return res[2, 1];
        }

        public string LongestWord(string[] words)
        {
            if (words == null || words.Length == 0)
            {
                return string.Empty;
            }

            HashSet<string> hs = new HashSet<string>();
            List<string> ls = new List<string>();

            Array.Sort(words);

            foreach (string word in words)
            {
                hs.Add(word);
            }

            for (int i = words.Length - 1; i >= 0; i--)
            {
                string str = words[i];
                int j = 0;

                while (j < str.Length)
                {
                    if (hs.Contains(str.Substring(0, j + 1)))
                    {
                        j++;
                    }
                    else
                    {
                        break;
                    }
                }

                if (j == str.Length)
                {
                    if (ls.Count == 0)
                    {
                        ls.Add(str);
                    }
                    else if (ls.Last().Length <= j)
                    {
                        ls.Add(str);
                    }
                }
            }

            if (ls.Count > 0)
            {
                ls.Sort();

                return ls[0];
            }

            return string.Empty;
        }

        public string LongestWord1(string[] words)
        {
            if (words == null || words.Length == 0)
            {
                return string.Empty;
            }

            string res = string.Empty;
            HashSet<string> hs = new HashSet<string>();
            Array.Sort(words);

            foreach (string word in words)
            {
                if (word.Length == 1 || hs.Contains(word.Substring(0, word.Length - 1)))
                {
                    if (word.Length > res.Length)
                    {
                        res = word;
                    }

                    hs.Add(word);
                }

            }

            return res;
        }

        public IList<IList<string>> AccountsMerge(IList<IList<string>> accounts)
        {
            IList<IList<string>> res = new List<IList<string>>();

            if (accounts.Count == 0)
            {
                return res;
            }

            Dictionary<string, List<string>> dct = new Dictionary<string, List<string>>();

            foreach (IList<string> account in accounts)
            {
                string name = account[0];

                if (!dct.ContainsKey(name))
                {
                    dct.Add(name, new List<string>());
                    for (int i = 1; i < account.Count; i++)
                    {
                        dct[name].Add(account[i]);
                    }

                    dct[name].Sort();
                }
                else
                {
                    bool isInclude = false;
                    for (int i = 1; i < account.Count; i++)
                    {
                        if (dct[name].Contains(account[i]))
                        {
                            isInclude = true;
                        }
                    }

                    if (isInclude)
                    {
                        for (int i = 1; i < account.Count; i++)
                        {
                            if (!dct[name].Contains(account[i]))
                            {
                                dct[name].Add(account[i]);
                            }
                        }
                    }
                    else
                    {
                        List<string> ls = new List<string>();

                        for (int i = 1; i < account.Count; i++)
                        {
                            ls.Add(account[i]);
                        }

                        ls.Sort();
                        ls.Insert(0, name);
                        res.Add(new List<string>(ls));
                    }
                }
            }

            foreach (string key in dct.Keys)
            {
                List<string> ls = dct[key];
                ls.Sort();
                ls.Insert(0, key);
                res.Add(new List<string>(ls));
            }

            SortedSet<string> ss = new SortedSet<string>();

            return res;
        }

        public static IList<string> RemoveComments(string[] source)
        {
            IList<string> res = new List<string>();

            if (source == null || source.Length == 0)
            {
                return res;
            }

            bool inBlock = false;
            string str = string.Empty;

            foreach (string s in source)
            {
                int n = s.Length;

                for (int i = 0; i < n;)
                {
                    if (!inBlock)
                    {
                        if (i == n - 1)
                        {
                            str += s[i];
                            i++;
                        }
                        else
                        {
                            string m = s.Substring(i, 2);

                            if (m == "/*")
                            {
                                inBlock = true;
                                i += 2;
                            }
                            else if (m == "//")
                            {
                                break;
                            }
                            else
                            {
                                str += s[i];
                                i++;
                            }
                        }
                    }
                    else
                    {
                        if (i == n - 1)
                        {
                            i++;
                        }
                        else
                        {
                            string m = s.Substring(i, 2);

                            if (m == "*/")
                            {
                                inBlock = false;
                                i += 2;
                            }
                            else
                            {
                                i++;
                            }
                        }
                    }
                }

                if (str.Length > 0 && !inBlock)
                {
                    res.Add(str);
                    str = string.Empty;
                }
            }

            return res;
        }

        public int PivotIndex(int[] nums)
        {
            if (nums == null || nums.Length < 2)
            {
                return -1;
            }

            int n = nums.Length;
            int[] dpleft = new int[n];
            int[] dpright = new int[n];
            int left = 0, right = 0;

            for (int i = 0, j = n - 1; i < n && j >= 0; i++, j--)
            {
                left += nums[i];
                right += nums[j];
                dpleft[i] = left;
                dpright[j] = right;
            }

            for (int i = 0; i < n; i++)
            {
                if (i == 0)
                {
                    if (dpright[1] == 0)
                    {
                        return 0;
                    }
                }
                else if (i == n - 1)
                {
                    if (dpleft[n - 2] == 0)
                    {
                        return n - 1;
                    }
                }
                else if (dpleft[i - 1] == dpright[i + 1])
                {
                    return i;
                }
            }

            return -1;
        }

        //public ListNode[] SplitListToParts(ListNode root, int k)
        //{
        //    if (k <= 0)
        //    {
        //        return new ListNode[0];
        //    }

        //    int n = 0;
        //    ListNode p = root;
        //    ListNode[] res = new ListNode[k];

        //    while (p != null)
        //    {
        //        n++;
        //        p = p.next;
        //    }

        //    int i = 0;
        //    p = root;

        //    if (n <= k)
        //    {
        //        while (p != null)
        //        {
        //            ListNode temp = p;
        //            res[i] = temp;
        //            temp = temp.next;
        //            p = temp;
        //            temp = null;
        //            i++;
        //        }

        //        while (i < k)
        //        {
        //            res[i] = null;
        //            i++;
        //        }
        //    }
        //    else
        //    {
        //        int e = n % k;
        //        int c = n / k;
        //        if (e == 0)
        //        {
        //            while (i < k)
        //            {
        //                int j = 1;
        //                res[i] = p;
        //                while (j < c)
        //                {
        //                    ListNode temp = p;
        //                    temp = temp.next;
        //                    p = temp;
        //                    temp = null;
        //                    j++;
        //                }
        //                i++;
        //            }
        //        }
        //        else
        //        {
        //            while (i < k)
        //            {
        //                int j = 1;
        //                res[i] = p;
        //                if (e > 0)
        //                {
        //                    while (j < c + 1)
        //                    {
        //                        ListNode temp = p;
        //                        temp = temp.next;
        //                        p = temp;
        //                        temp = null;
        //                        j++;
        //                    }
        //                    e--;
        //                }
        //                else
        //                {
        //                    while (j < c)
        //                    {
        //                        ListNode temp = p;
        //                        temp = temp.next;
        //                        p = temp;
        //                        temp = null;
        //                        j++;
        //                    }
        //                }

        //                i++;
        //            }
        //        }
        //    }

        //    return res;
        //}

        public static string MinWindow(string S, string T)
        {
            if (string.IsNullOrEmpty(S) || string.IsNullOrEmpty(T))
            {
                return string.Empty;
            }

            Dictionary<char, List<int>> map = new Dictionary<char, List<int>>();
            int start = 0, end = 0, count = T.Length;
            int minLen = int.MaxValue, minStart = 0;
            int last = 0;
            int n = T.Length - 1;
            for (int i = 0; i < T.Length; i++)
            {
                if (!map.ContainsKey(T[i]))
                {
                    map.Add(T[i], new List<int>());
                }

                map[T[i]].Add(i);
            }

            while (end < S.Length)
            {
                if (map.ContainsKey(S[end]))
                {
                    if (map[S[end]].Contains(last))
                    {
                        count--;
                        last++;
                    }
                }


                start = end;

                while (count == 0 && start >= 0)
                {
                    if (map.ContainsKey(S[start]) && map[S[start]].Contains(n))
                    {
                        n--;
                    }

                    if (S[start] == T[0])
                    {

                        if (n == -1)
                        {
                            count = T.Length;
                            last = 0;
                            n = T.Length - 1;
                            if (end - start + 1 < minLen)
                            {
                                minLen = end - start + 1;
                                minStart = start;
                            }
                        }
                    }

                    start--;
                }

                end++;
            }

            return (minLen == int.MaxValue) ? string.Empty : S.Substring(minStart, minLen);
        }

        public static string MinWindow1(string S, string T)
        {
            if (string.IsNullOrEmpty(S) || string.IsNullOrEmpty(T))
            {
                return string.Empty;
            }

            Dictionary<char, List<int>> map = new Dictionary<char, List<int>>();
            int start = 0, end = 0, count = T.Length;
            int minLen = int.MaxValue, minStart = 0;
            int last = 0;

            for (int i = 0; i < T.Length; i++)
            {
                if (!map.ContainsKey(T[i]))
                {
                    map.Add(T[i], new List<int>());
                }

                map[T[i]].Add(i);
            }

            while (end < S.Length)
            {
                if (map.ContainsKey(S[end]))
                {
                    if (map[S[end]].Contains(last))
                    {
                        count--;
                        last++;
                    }
                }


                while (count == 0)
                {
                    if (S[start] == T[0])
                    {
                        count = T.Length;
                        last = 0;
                        if (end - start + 1 < minLen)
                        {
                            minLen = end - start + 1;
                            minStart = start;
                        }
                    }

                    start++;
                }

                end++;
            }

            return (minLen == int.MaxValue) ? string.Empty : S.Substring(minStart, minLen);
        }

        public static string MinWindow2(string S, string T)
        {
            if (string.IsNullOrEmpty(S) || string.IsNullOrEmpty(T))
            {
                return string.Empty;
            }

            Dictionary<char, List<int>> map = new Dictionary<char, List<int>>();
            int start = 0, end = 0, count = T.Length;
            int minLen = int.MaxValue, minStart = 0;
            int last = 0;

            for (int i = 0; i < T.Length; i++)
            {
                if (!map.ContainsKey(T[i]))
                {
                    map.Add(T[i], new List<int>());
                }

                map[T[i]].Add(i);
            }

            while (end < S.Length)
            {
                if (map.ContainsKey(S[end]))
                {
                    if (map[S[end]].Contains(last))
                    {
                        count--;
                        last++;
                    }
                }


                start = end;

                while (count == 0)
                {
                    if (map.ContainsKey(S[start]) && map[S[start]].Contains(last - 1))
                    {
                        last--;
                    }

                    if (S[start] == T[0])
                    {

                        if (last == 0)
                        {
                            count = T.Length;

                            if (end - start + 1 < minLen)
                            {
                                minLen = end - start + 1;
                                minStart = start;
                            }
                        }
                    }

                    start--;
                }

                end++;
            }

            return (minLen == int.MaxValue) ? string.Empty : S.Substring(minStart, minLen);
        }

        public string MinWindow3(string S, string T)
        {
            if (string.IsNullOrEmpty(S) || string.IsNullOrEmpty(T))
            {
                return string.Empty;
            }

            int m = S.Length, n = T.Length;
            int[,] dp = new int[n, m];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    dp[i, j] = -1;
                }
            }

            for (int i = 0; i < m; i++)
            {
                if (T[0] == S[i])
                {
                    dp[0, i] = i;
                }
            }

            for (int i = 1; i < n; i++)
            {
                int k = -1;

                for (int j = 0; j < m; j++)
                {
                    if (k != -1 && T[i] == S[j])
                    {
                        dp[i, j] = k;
                    }

                    if (dp[i - 1, j] != -1)
                    {
                        k = dp[i - 1, j];
                    }
                }
            }

            int st = -1, len = int.MaxValue;

            for (int i = 0; i < m; i++)
            {
                if (dp[n - 1, i] != -1 && i - dp[n - 1, i] + 1 < len)
                {
                    len = i - dp[n - 1, i] + 1;
                    st = dp[n - 1, i];
                }
            }

            if (st == -1)
            {
                return string.Empty;
            }
            else
            {
                return S.Substring(st, len);
            }
        }

        public int MaxSumSubmatrix(int[,] matrix, int k)
        {
            if (matrix == null || matrix.Length == 0)
            {
                return 0;
            }

            int m = matrix.GetLength(0);
            int n = matrix.GetLength(1);
            int[,] colSum = new int[m, n];
            int[,] allSum = new int[m, n];
            int[,] rSum = new int[m, n];
            int res = matrix[0, 0];
            int min = int.MaxValue;

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == 0)
                    {
                        colSum[i, j] += matrix[i, j];
                    }
                    else
                    {
                        colSum[i, j] += colSum[i - 1, j] + matrix[i, j];
                    }

                    if (colSum[i, j] == k || matrix[i, j] == k)
                    {
                        return k;
                    }
                    else
                    {
                        if (Math.Abs(colSum[i, j] - k) < min)
                        {
                            res = colSum[i, j];
                            min = Math.Abs(colSum[i, j] - k);
                        }

                        if (Math.Abs(matrix[i, j] - k) < min)
                        {
                            res = matrix[i, j];
                            min = Math.Abs(matrix[i, j] - k);
                        }
                    }
                }
            }

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (j == 0)
                    {
                        allSum[i, j] = colSum[i, j];
                    }
                    else
                    {
                        allSum[i, j] += allSum[i, j - 1] + colSum[i, j];
                    }

                    if (allSum[i, j] == k)
                    {
                        return k;
                    }
                    else
                    {
                        if (Math.Abs(allSum[i, j] - k) < min)
                        {
                            res = allSum[i, j];
                            min = Math.Abs(allSum[i, j] - k);
                        }
                    }
                }
            }

            for (int i = 0; i < m - 1; i++)
            {
                for (int j = 0; j < n - 1; j++)
                {
                    for (int l = i; k < m; k++)
                    {
                        for (int t = j + 1; t < n; t++)
                        {
                            int res1 = int.MaxValue;

                            if (i == l)
                            {
                                res1 = allSum[l, t] - allSum[i, j];
                            }
                            else
                            {
                                res1 = allSum[l, t] - allSum[l, j] - allSum[i, t] + allSum[i, j];
                            }

                            if (Math.Abs(res1 - k) < min)
                            {
                                res = res1;
                                min = Math.Abs(res1 - k);
                            }
                        }
                    }
                }
            }

            return res;
        }

        public int FindSubstringInWraproundString(string p)
        {
            if (string.IsNullOrEmpty(p))
            {
                return 0;
            }

            int n = p.Length;
            int[,] dp = new int[n, n];
            HashSet<string> hs = new HashSet<string>();

            for (int i = n - 1; i >= 0; i--)
            {
                for (int j = i; j < n; j++)
                {
                    string s = p.Substring(i, j - i + 1);
                    if (i == j)
                    {
                        dp[i, j] = 1;
                        if (!hs.Contains(s))
                        {
                            hs.Add(s);
                        }
                    }
                    else
                    {
                        if (dp[i, j - 1] == 1 && (p[j] - p[j - 1] == 1 || p[j] - p[j - 1] == -25))
                        {
                            dp[i, j] = 1;
                            if (!hs.Contains(s))
                            {
                                hs.Add(s);
                            }
                        }
                    }
                }
            }

            return hs.Count;
        }

        public int FindSubstringInWraproundString1(string p)
        {
            if (string.IsNullOrEmpty(p))
            {
                return 0;
            }

            int[] dp = new int[26];
            int count = 0;
            int res = 0;

            for (int i = 0; i < p.Length; i++)
            {
                if (i > 0 && (p[i] - p[i - 1] == 1 || p[i - 1] - p[i] == 25))
                {
                    count++;
                }
                else
                {
                    count = 1;
                }

                int index = p[i] - 'a';
                dp[index] = Math.Max(dp[index], count);
            }

            for (int i = 0; i < 26; i++)
            {
                res += dp[i];
            }

            return res;
        }

        public IList<int> SelfDividingNumbers(int left, int right)
        {
            IList<int> res = new List<int>();

            if (left > right || right <= 0)
            {
                return res;
            }

            if (left <= 0)
            {
                left = 1;
            }

            for (int i = left; i <= right; i++)
            {
                if (IsSelfDividingNumber(i))
                {
                    res.Add(i);
                }
            }

            return res;
        }

        bool IsSelfDividingNumber(int n)
        {
            if (n <= 0)
            {
                return false;
            }

            if (n < 10)
            {
                return true;
            }

            int d = n;

            while (n > 0)
            {
                int num = n % 10;

                if (num == 0 || d % num != 0)
                {
                    return false;
                }

                n /= 10;
            }

            return true;
        }

        public int CherryPickup(int[,] grid)
        {
            if (grid == null || grid.Length == 0)
            {
                return 0;
            }

            int m = grid.GetLength(0);
            int n = grid.GetLength(1);

            int[,] dp = new int[m, n];

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        if (grid[i, j] == -1)
                        {
                            return 0;
                        }
                        else
                        {
                            dp[i, j] = grid[i, j];
                        }
                    }
                    else if (i == 0)
                    {
                        if (grid[i, j] != -1 && dp[i, j - 1] != -1)
                        {
                            dp[i, j] = dp[i, j - 1] + grid[i, j];
                        }
                        else
                        {
                            dp[i, j] = -1;
                        }
                    }
                    else if (j == 0)
                    {
                        if (grid[i, j] != -1 && dp[i - 1, j] != -1)
                        {
                            dp[i, j] = dp[i - 1, j] + grid[i, j];
                        }
                        else
                        {
                            dp[i, j] = -1;
                        }
                    }
                    else
                    {
                        if (grid[i, j] != -1)
                        {
                            if (dp[i - 1, j] != -1 && dp[i, j - 1] != -1)
                            {
                                dp[i, j] = dp[i - 1, j] + dp[i, j - 1] + grid[i, j];
                            }
                            else if (dp[i - 1, j] != -1)
                            {
                                dp[i, j] = dp[i - 1, j] + grid[i, j];
                            }
                            else if (dp[i, j - 1] != -1)
                            {
                                dp[i, j] = dp[i, j - 1] + grid[i, j];
                            }
                            else
                            {
                                dp[i, j] = -1;
                            }
                        }
                        else
                        {
                            dp[i, j] = -1;
                        }
                    }
                }
            }

            return dp[m - 1, n - 1] == -1 ? 0 : dp[m - 1, n - 1];
        }

        public int MinCostClimbingStairs(int[] cost)
        {
            if (cost == null || cost.Length < 2)
            {
                return 0;
            }

            int n = cost.Length;
            int[] dp = new int[n + 1];

            dp[0] = 0;
            dp[1] = 0;

            for (int i = 2; i <= n; i++)
            {
                dp[i] = Math.Min(dp[i - 1] + cost[i - 1], dp[i - 2] + cost[i - 2]);
            }

            return dp[n];
        }

        public int[] MaxSumOfThreeSubarrays(int[] nums, int k)
        {
            if (nums == null || nums.Length == 0)
            {
                return new int[0];
            }

            int maxSum = 0;
            int n = nums.Length;
            int[] sum = new int[n + 1];
            int[] res = new int[3];
            int[] left = new int[n];
            int[] right = new int[n];

            for (int i = 0; i < n; i++)
            {
                sum[i + 1] = sum[i] + nums[i];
            }

            int total = sum[k] - sum[0];

            for (int i = k; i < n; i++)
            {
                if (sum[i + 1] - sum[i - k + 1] > total)
                {
                    total = sum[i + 1] - sum[i - k + 1];
                    left[i] = i - k + 1;
                }
                else
                {
                    left[i] = left[i - 1];
                }
            }

            total = sum[n] - sum[n - k];
            right[n - k] = n - k;

            for (int j = n - k - 1; j >= 0; j--)
            {
                if (sum[j + k] - sum[j] >= total)
                {
                    right[j] = j;
                    total = sum[j + k] - sum[j];
                }
                else
                {
                    right[j] = right[j + 1];
                }
            }

            for (int i = k; i <= n - 2 * k; i++)
            {
                int l = left[i - 1], r = right[i + k];
                total = sum[i + k] - sum[i] + sum[l + k] - sum[l] + sum[r + k] - sum[r];

                if (maxSum < total)
                {
                    maxSum = total;
                    res[0] = l;
                    res[1] = i;
                    res[2] = r;
                }
            }

            return res;
        }

        public int NumDecodings2(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return 0;
            }

            int m = 1000000007;
            int n = s.Length;
            long[] dp = new long[n + 1];

            dp[n] = 1;

            if (s[n - 1] == '0')
            {
                dp[n - 1] = 0;
            }
            else if (s[n - 1] == '*')
            {
                dp[n - 1] = 9;
            }
            else
            {
                dp[n - 1] = 1;
            }

            for (int i = n - 2; i >= 0; i--)
            {
                if (s[i] == '0')
                {
                    continue;
                }

                if (s[i] == '*')
                {
                    dp[i] += dp[i + 1] * 9;
                }
                else
                {
                    dp[i] += dp[i + 1];
                }

                if (s[i + 1] == '*')
                {
                    if (s[i] == '1')
                    {
                        dp[i] += dp[i + 2] * 9;
                    }
                    else if (s[i] == '2')
                    {
                        dp[i] += dp[i + 2] * 6;
                    }
                    else if (s[i] == '*')
                    {
                        dp[i] += 15 * dp[i + 2];
                    }
                }
                else
                {
                    if (s[i] == '*')
                    {
                        if (s[i + 1] <= '6')
                        {
                            dp[i] += 2 * dp[i + 2];
                        }
                        else
                        {
                            dp[i] += dp[i + 2];
                        }
                    }
                    else
                    {
                        if (int.Parse(s.Substring(i, 2)) <= 26)
                        {
                            dp[i] += dp[i + 2];
                        }
                    }
                }

                dp[i] %= m;
            }

            return (int)dp[0];
        }

        public bool StoneGame(int[] piles)
        {
            if (piles == null || piles.Length == 0)
            {
                return false;
            }

            int n = piles.Length;
            int[,] dp = new int[n, n];

            for (int i = 0; i < n; i++)
            {
                dp[i, i] = piles[i];
            }

            for (int d = 1; d < n; d++)
            {
                for (int i = 0; i < n - d; i++)
                {
                    dp[i, i + d] = Math.Max(dp[i + 1, i + d], dp[i, i + d - 1]);
                }
            }

            return dp[0, n - 1] > 0;
        }

        public int NumMusicPlaylists(int N, int L, int K)
        {
            long mod = (long)1e9 + 7;
            long[,] dp = new long[N + 1, L + 1];

            for (int i = K + 1; i <= N; i++)
            {
                for (int j = i; j <= L; j++)
                {
                    if (i == j || i == K + 1)
                    {
                        dp[i, j] = FB(i, mod);
                    }
                    else
                    {
                        dp[i, j] = (dp[i - 1, j - 1] * i + dp[i, j - 1] * (i - K)) % mod;
                    }
                }
            }

            return (int)dp[N, L];
        }

        long FB(int n, long mod)
        {
            return n > 0 ? n * FB(n - 1, mod) % mod : 1;
        }

        public double LargestSumOfAverages(int[] A, int K)
        {
            int n = A.Length;
            double[,] dp = new double[n + 1, K + 1];
            double s = 0;
            for (int i = 0; i < n; i++)
            {
                s += A[i];
                dp[i + 1, 1] = s / (i + 1);
            }

            return lsaHelper(A, K, dp, n);
        }

        double lsaHelper(int[] A, int k, double[,] dp, int n)
        {
            if (dp[n, k] > 0)
            {
                return dp[n, k];
            }

            if (n < k)
            {
                return 0;
            }

            double s = 0;
            for (int i = n - 1; i > 0; i--)
            {
                s += A[i];
                dp[n, k] = Math.Max(dp[n, k], lsaHelper(A, k - 1, dp, i) + s / (n - i));
            }

            return dp[n, k];
        }

        /*Navie Approach */
        public double LargestSumOfAveragesDFS(int[] A, int K)
        {
            int n = A.Length;
            int s = 0;
            double[] sum = new double[n];

            for (int i = 0; i < n; i++)
            {
                s += A[i];
                sum[i] = s;
            }

            return hp(A, K, sum, n, 0);
        }

        double hp(int[] A, int k, double[] sum, int n, int start)
        {
            if (k == 1)
            {
                return (sum[n - 1] - sum[start] + A[start]) / (n - start);
            }

            double num = 0;

            for (int i = start; i + k <= n; i++)
            {
                num = Math.Max(num, ((double)((sum[i] - sum[start] + A[start]) / (i - start + 1)) + hp(A, k - 1, sum, n, i + 1)));
            }

            return num;
        }

        /* Recursion + Top-Bottom Memoralization - Seperate moving pieces */

        public double LargestSumOfAveragesDFSMemo(int[] A, int K)
        {
            int n = A.Length;
            int s = 0;
            double[] sum = new double[n];

            for (int i = 0; i < n; i++)
            {
                s += A[i];
                sum[i] = s;
            }

            double[,] dp = new double[n, K + 1];
            return hpMemo(A, K, sum, dp, n, 0);
        }

        double hpMemo(int[] A, int k, double[] sum, double[,] dp, int n, int start)
        {
            if (dp[start, k] != 0)
            {
                return dp[start, k];
            }

            if (k == 1)
            {
                dp[start, k] = (sum[n - 1] - sum[start] + A[start]) / (n - start);

                return dp[start, k];
            }

            for (int i = start; i + k <= n; i++)
            {
                dp[start, k] = Math.Max(dp[start, k], ((double)((sum[i] - sum[start] + A[start]) / (i - start + 1)) + hpMemo(A, k - 1, sum, dp, n, i + 1)));
            }

            return dp[start, k];
        }


        /* Bottom-UP 2D DP*/
        public double LargestSumOfAveragesBUp(int[] A, int K)
        {
            int n = A.Length;
            int s = 0;
            double[] sum = new double[n];

            for (int i = 0; i < n; i++)
            {
                s += A[i];
                sum[i] = s;
            }

            double[,] dp = new double[n, K + 1];

            for (int groups = 1; groups <= K; groups++)
            {
                for (int i = 0; i + groups <= n; i++)
                {
                    if (groups == 1)
                    {
                        dp[i, groups] = (sum[n - 1] - sum[i] + A[i]) / (n - i);
                    }
                    else
                    {
                        for (int j = i; j + groups <= n; j++)
                        {
                            dp[i, groups] = Math.Max(dp[i, groups], dp[j + 1, groups - 1] + (sum[j] - sum[i] + A[i]) / (j - i + 1));
                        }
                    }
                }
            }

            return dp[0, K];
        }

        /* Bottom-UP 1D DP*/
        public double LargestSumOfAveragesBUp1D(int[] A, int K)
        {
            int n = A.Length;
            int s = 0;
            double[] sum = new double[n];

            for (int i = 0; i < n; i++)
            {
                s += A[i];
                sum[i] = s;
            }

            double[] dp = new double[n];

            for (int groups = 1; groups <= K; groups++)
            {
                for (int i = 0; i + groups <= n; i++)
                {
                    if (groups == 1)
                    {
                        dp[i] = (sum[n - 1] - sum[i] + A[i]) / (n - i);
                    }
                    else
                    {
                        for (int j = i; j + groups <= n; j++)
                        {
                            dp[i] = Math.Max(dp[i], dp[j + 1] + (sum[j] - sum[i] + A[i]) / (j - i + 1));
                        }
                    }
                }
            }

            return dp[0];
        }

        public int FindCheapestPrice(int n, int[][] flights, int src, int dst, int K)
        {
            if (flights == null || flights.Length == 0)
            {
                return -1;
            }

            int t = 0;
            int m = flights.GetLength(0);
            int max = int.MaxValue;
            Queue<int[]> q = new Queue<int[]>();
            q.Enqueue(new int[2] { src, 0 });

            while (q.Count > 0 && t <= K)
            {
                int size = q.Count;

                for (int i = 0; i < size; i++)
                {
                    int[] arr = q.Dequeue();

                    for (int j = 0; j < m; j++)
                    {
                        if (flights[j][0] == arr[0])
                        {
                            int cost = flights[j][2] + arr[1];

                            if (flights[j][1] == dst)
                            {
                                if (max > cost)
                                {
                                    max = cost;
                                }
                            }

                            if (cost < max)
                            {
                                q.Enqueue(new int[] { flights[j][1], cost });
                            }
                        }
                    }
                }

                t++;
            }

            return max == int.MaxValue ? -1 : max;
        }

        public int DeleteAndEarn(int[] nums)
        {
            if (nums == null || nums.Length == 0)
            {
                return 0;
            }

            int[] values = new int[10001];
            int n = values.Length;
            int skip = 0, take = 0;

            foreach (int num in nums)
            {
                values[num] += num;
            }

            for(int i=0;i<n;i++)
            {
                int takei = values[i]+skip;
                int skipi = Math.Max(skip, take);
                skip = skipi;
                take = takei;
            }

            return Math.Max(take, skip);
        }

        public int MinRefuelStops(int target, int startFuel, int[][] stations)
        {
            int n = stations.GetLength(0);

            if (n == 0 && startFuel >= target)
            {
                return 0;
            }

            long[] dp = new long[n + 1];
            dp[0] = startFuel;

            for (int i = 0; i < n; i++)
            {
                for (int t = i; t >= 0 && dp[t] >= stations[i][0]; t--)
                {
                    dp[t + 1] = Math.Max(dp[t + 1], dp[t] + stations[i][1]);
                }
            }

            for (int i = 0; i <= n; i++)
            {
                if (dp[i] >= target)
                {
                    return i;
                }
            }

            return -1;
        }

        public bool PredictTheWinner(int[] nums)
        {
            if(nums==null||nums.Length==0)
            {
                return true;
            }

            int n = nums.Length;

            if(n%2==0)
            {
                return true;
            }

            int[,] dp = new int[n, n];

            for(int i=0;i<n;i++)
            {
                dp[i, i] = nums[i];
            }

            for(int len=1;len<n;len++)
            {
                for(int i=0;i<n-len;i++)
                {
                    int j = i + len;
                    dp[i, j] = Math.Max(nums[i] - dp[i + 1, j], nums[j] - dp[i, j - 1]);
                }
            }

            return dp[0, n - 1]>=0;
        }

        public bool PredictTheWinner1DP(int[] nums)
        {
            if (nums == null || nums.Length == 0)
            {
                return true;
            }

            int n = nums.Length;

            if (n % 2 == 0)
            {
                return true;
            }

            int[] dp = new int[n];

            for(int i=n-1;i>=0;i--)
            {
                for(int j=i;j<n;j++)
                {
                    if(i==j)
                    {
                        dp[i] = nums[i];
                    }
                    else
                    {
                        dp[j] = Math.Max(nums[i] - dp[j], nums[j] - dp[j - 1]);
                    }
                }
            }

            return dp[n - 1] >= 0;
        }
    }
}
