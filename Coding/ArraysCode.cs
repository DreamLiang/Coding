using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding
{
    class ArraysCode
    {

        public int PeakIndexInMountainArray(int[] A)
        {
            if(A==null||A.Length==0)
            {
                return -1;
            }

            int n = A.Length;

            for(int i=1;i<n-1;i++)
            {
                if(A[i]>A[i-1]&&A[i]>A[i+1])
                {
                    return i;
                }
            }

            return n - 1;
        }

        public int[] SortArray(int[] arr)
        {
            if(arr==null||arr.Length<2)
            {
                return arr;
            }

            int len = arr.Length;

            QuickSort(arr, 0, len - 1);
            ReverseArray(arr, (len+1)/ 2, len - 1);

            int i = 1, count = 1,curr=arr[i];

            while(count<arr.Length)
            {
                int j = CalculateIndex(arr,i);
                if(i==j)
                {
                    i++;
                    continue;
                }
                int next = arr[j];
                arr[j] = curr;
                curr = next;
                i = j;
                count++;
            }
            return arr;
        }

        int CalculateIndex(int[] arr, int i)
        {
            if(i<=(arr.Length-1)/2)
            {
                return 2 * i;
            }
            else
            {
                return (i - (arr.Length - 1) / 2 - 1) * 2 + 1;
            }
        }
        void ReverseArray(int[] arr,int start, int end)
        {
            while(start<end)
            {
                Swap(arr, start, end);
                start++;
                end--;
            }
        }

        void Swap(int[] arr,int i, int j)
        {
            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }
        public void QuickSort(int[] arr, int start, int end)
        {
            if(start>=end)
            {
                return;
            }

            int i = start, j = end;
            int provit = arr[start];
            while(i<=j)
            {
                while(i<=j&&arr[i]>=provit)
                {
                    i++;
                }

                while (i <= j && arr[j] < provit)
                {
                    j--;
                }

                if(i<j)
                {
                    Swap(arr, i, j);
                }
            }

            if(start!=j)
            {
                Swap(arr, start, j);
            }

            QuickSort(arr, start, i - 2);
            QuickSort(arr, i , end);
        }

        public void WiggleSort(int[] nums)
        {
            if(nums==null||nums.Length==0)
            {
                return;
            }

            for(int i=1;i<nums.Length;i++)
            {
                if(i%2!=0)
                {
                    if(nums[i-1]>nums[i])
                    {
                        Swap(nums, i - 1, i);
                    }
                }
                else
                {
                    if(nums[i]>nums[i-1])
                    {
                        Swap(nums, i - 1, i);
                    }
                }
            }
        }

        public void NextPermutation(int[] nums)
        {
            if(nums==null||nums.Length<2)
            {
                return;
            }

            int n = nums.Length;
            int index = n - 1;

            while(index>0)
            {
                if(nums[index-1]<nums[index])
                {
                    break;
                }
                index--;
            }

            if(index==0)
            {
                ReverseArray(nums, index, n - 1);
                return;
            }
            else
            {
                int i = n - 1;
                while (i >= index)
                {
                    if (nums[i] > nums[index - 1])
                    {
                        break;
                    }
                    i--;
                }
                Swap(nums, index - 1, i);
                ReverseArray(nums, index, n - 1);
                return;
            }
            
        }
        public static List<List<int>> Permute(int[] nums)
        {
            List<List<int>> res = new List<List<int>>();
            List<int> ls = new List<int>();

            if (nums==null||nums.Length==0)
            {
                return res;
            }

            if(nums.Length==1)
            {
                ls.Add(nums[0]);
                res.Add(ls);
                return res;
            }

            doPermutation(nums, ls, res, 0);

            return res;
        }

       static void doPermutation(int[] nums, List<int> ls, List<List<int>> res,int index)
        {
            if(index==nums.Length)
            {
                res.Add(new List<int>(ls));
                return;
            }

            for(int i=index;i<nums.Length;i++)
            {
                if(!ls.Contains(nums[i]))
                {
                    ls.Add(nums[i]);
                    doPermutation(nums, ls, res, index+1);
                    ls.Remove(nums[i]);
                }
            }
        }

        public int FindLHS(int[] nums)
        {
            if(nums==null||nums.Length==0)
            {
                return 0;
            }

            Dictionary<int, int> dct = new Dictionary<int, int>();

            foreach(int num in nums)
            {
                if(dct.ContainsKey(num))
                {
                    dct[num]++;
                }
                else
                {
                    dct.Add(num, 1);
                }
            }

            int count = 0;

            foreach(int key in dct.Keys)
            {
                if(dct.ContainsKey(key+1))
                {
                    count = Math.Max(count, dct[key] + dct[key + 1]);
                }
            }

            return count;
        }

        public bool ValidSquare(int[] p1, int[] p2, int[] p3, int[] p4)
        {
            if(p1==null||p2==null||p3==null||p4==null||p1.Length<2||p2.Length < 2 || p3.Length < 2 || p4.Length < 2)
            {
                return false;
            }

            long[] lengths = {length(p1, p2), length(p2, p3), length(p3, p4),
            length(p4, p1), length(p1, p3),length(p2, p4)}; // all 6 sides

            long max = 0, nonMax = 0;
            foreach(long len in lengths)
            {
                max = Math.Max(max, len);
            }
            int count = 0;
            for (int i = 0; i < lengths.Length; i++)
            {
                if (lengths[i] == max) count++;
                else nonMax = lengths[i]; // non diagonal side.
            }
            if (count != 2) return false; // diagonals lenghts have to be same.

            foreach (long len in lengths)
            {
                if (len != max && len != nonMax) return false; // sides have to be same length
            }
            return true;
        }
        private long length(int[] p1, int[] p2)
        {
            return (long)Math.Pow(p1[0] - p2[0], 2) + (long)Math.Pow(p1[1] - p2[1], 2);
        }

        public void wiggleSort(int[] nums)
        {
            if(nums==null||nums.Length<2)
            {
                return;
            }

            Array.Sort(nums);

            int len = nums.Length;
            int[] arr = new int[len];

            for(int i=0;i<len;i++)
            {
                arr[i] = nums[i];
            }

            int p = 0, j = (len-1) / 2, k = len - 1;
            bool sign = true;
            while(p<len)
            {
                if(sign)
                {
                    nums[p++] = arr[j--];
                }
                else
                {
                    nums[p++] = arr[k--];
                }

                sign = !sign;
            }
        }

        public static int FindUnsortedSubarray(int[] nums)
        {
            if(nums==null||nums.Length<2)
            {
                return 0;
            }

            int n = nums.Length;
            int end = -2, start = -1, min = nums[n - 1], max = nums[0];

            for(int i=1;i<n;i++)
            {
                min = Math.Min(nums[n-1-i], min);
                max = Math.Max(nums[i], max);

                if(nums[i]<max)
                {
                    end = i;
                }

                if(nums[n-1-i]>min)
                {
                    start = n - 1 - i;
                }
            }

            return end - start + 1;
        }

        public int subarraySum(int[] nums, int k)
        {
            if(nums.Length==0)
            {
                return 0;
            }

            Dictionary<int, int> preSum = new Dictionary<int, int>();
            int count = 0;
            int sum = 0;

            preSum.Add(0, 1);

            for(int i=0;i<nums.Length;i++)
            {
                sum += nums[i];

                if(preSum.ContainsKey(sum-k))
                {
                    count += preSum[sum - k];
                }

                if(preSum.ContainsKey(sum))
                {
                    preSum[sum]++;
                }
                else
                {
                    preSum.Add(sum, 1);
                }
            }

            return count;
        }

        public int NextGreaterElement(int n)
        {
            if(n<10)
            {
                return -1;
            }

            char[] chArr = (n + "").ToCharArray();
            int i = chArr.Length-1;

            while(i>0)
            {
                if(chArr[i]> chArr[i-1])
                {
                    break;
                }

                i--;
            }
            
            if(i==0)
            {
                return - 1;
            }

            int smallest = i, num = chArr[i - 1];

            for(int j=i+1;j<chArr.Length;j++)
            {
                if(chArr[j]>num&&chArr[j]<=chArr[smallest])
                {
                    smallest = j;
                }
            }
            char temp = chArr[i - 1];
            chArr[i - 1] = chArr[smallest];
            chArr[smallest] = temp;

            Array.Sort(chArr, i, chArr.Length-i);

            string s = new string(chArr);
            long res = long.Parse(s);

            return res<=int.MaxValue?(int)res:-1;
        }

        public int[] NextGreaterElement(int[] findNums, int[] nums)
        {

            if (findNums == null || nums == null || findNums.Length > nums.Length)
            {
                return new int[0];
            }

            Dictionary<int, int> dct = new Dictionary<int, int>();
            Stack<int> st = new Stack<int>();

            foreach(int num in nums)
            {
                while(st.Count!=0&&st.Peek()<num)
                {
                    dct.Add(st.Pop(), num);
                }

                st.Push(num);
            }

            int i = 0;
            int[] res = new int[findNums.Length];

            foreach(int num in findNums)
            {
                if(dct.ContainsKey(num))
                {
                    res[i] = dct[num];
                }
                else
                {
                    res[i] = -1;
                }

                i++;
            }

            return res;
        }

        public int[] NextGreaterElements(int[] nums)
        {
            if(nums==null||nums.Length==0)
            {
                return new int[0];
            }

            int n = nums.Length;
            Stack<int> st = new Stack<int>();
            int[] next = new int[n];
            for(int i=0;i<n;i++)
            {
                next[i] = -1;
            }

            for(int i=0;i<2*n;i++)
            {
                int num = nums[i % n];

                while(st.Count!=0&&nums[st.Peek()]<num)
                {
                    next[st.Pop()] = num;
                }

                if(i<n)
                {
                    st.Push(i);
                }
            }

            return next;
        }

        public int FindMaxConsecutiveOnes(int[] nums)
        {
            if(nums==null||nums.Length==0)
            {
                return 0;
            }

            int count = 0, max = 0;

            for(int i=0;i<nums.Length;i++)
            {
                if(nums[i]==1)
                {
                    count++;
                }
                else
                {
                    count = 0;
                }

                max = Math.Max(max, count);
            }

            return max;
        }

        public bool Find132pattern(int[] nums)
        {
            if(nums==null||nums.Length<3)
            {
                return false;
            }

            int start = 0, end = 0;

            while(start<nums.Length-1)
            {
                while(start<nums.Length-1&&nums[start]>=nums[start+1])
                {
                    start++;
                }

                int m = start + 1;

                while(m<nums.Length-1&&nums[m]<=nums[m+1])
                {
                    m++;
                }

                int j = m + 1;

                while(j<nums.Length)
                {
                    if(nums[j]>nums[start]&&nums[j]<nums[m])
                    {
                        return true;
                    }
                    j++;
                }

                start = m + 1;
            }

            return false;
        }

        public IList<IList<int>> Generate(int numRows)
        {
            IList<IList<int>> res = new List<IList<int>>();

            if(numRows<=0)
            {
                return res;
            }
            IList<int> ls = new List<int>();
            ls.Add(1);
            res.Add(new List<int>(ls));
            if(numRows==1)
            {
                return res;
            }

            for (int i = 1; i < numRows; i++)
            {
                List<int> newList = new List<int>();
                newList.Add(1);
                int j = 0;

                while (j <= i - 2)
                {
                    int temp = ls[j] + ls[j + 1];
                    newList.Add(temp);
                    j++;
                }

                newList.Add(1);
                res.Add(new List<int>(newList));
                ls = newList;
            }

            return res;
        }

        public int FirstMissingPositive(int[] nums)
        {
            if(nums==null||nums.Length==0)
            {
                return 1;
            }

            for(int i=0;i<nums.Length;i++)
            {
                while(nums[i]>0&&nums[i]<=nums.Length&& nums[nums[i] - 1] != nums[i])
                {
                    Swap(nums, i, nums[i] - 1);
                }
            }

            
            for (int j = 0; j<nums.Length; j++)
            {
                if(nums[j] != j + 1)
                {
                    return j + 1;
                }
            }

            return nums.Length + 1;
        }

        public int FindDuplicate(int[] nums)
        {
            if(nums==null||nums.Length==0)
            {
                return 0;
            }

            int n = nums.Length;
            int slow = n;
            int fast = n;

            do
            {
                slow = nums[slow - 1];
                fast = nums[nums[fast - 1] - 1];
            } while (slow != fast);

            slow = n;
            while(slow!=fast)
            {
                slow = nums[slow - 1];
                fast = nums[fast - 1];
            }

            return slow;
        }

        public IList<int> SpiralOrder(int[,] matrix)
        {
            IList<int> resList = new List<int>();

            if(matrix==null||matrix.Length==0)
            {
                return resList;
            }

            int m = matrix.GetLength(0);
            int n = matrix.GetLength(1);
            int startRow = 0, endRow = m - 1;
            int startColumn = 0, endColumn = n - 1;

            while(startRow<=endRow&&startColumn<=endColumn)
            {
                for (int i = startColumn; i <=endColumn; i++)
                {
                    resList.Add(matrix[startRow, i]);
                }

                startRow++;

                for (int i = startRow; i <= endRow; i++)
                {
                    resList.Add(matrix[i, endColumn]);
                }

                endColumn--;

                if(startRow<=endRow)
                {
                    for (int i = endColumn; i >= startColumn; i--)
                    {
                        resList.Add(matrix[endRow, i]);
                    }
                }

                endRow--;

                if(startColumn<=endColumn)
                {
                    for (int i = endRow; i >= startRow; i--)
                    {
                        resList.Add(matrix[i, startColumn]);
                    }
                }

                startColumn++;
            }

            return resList;
        }

        void doSpiralOrder(int[,] matrix,int startRow,int endRow,int startColumn,int endColumn,IList<int> ls)
        {
            if(startRow>endRow||startColumn>endColumn)
            {
                return;
            }

            for(int i=startColumn;i<endColumn;i++)
            {
                ls.Add(matrix[startRow, i]);
            }

            for (int i = startRow; i <= endRow; i++)
            {
                ls.Add(matrix[i,endColumn]);
            }

            if(endRow!=startRow)
            {
                for (int i = endColumn-1; i > startColumn; i--)
                {
                    ls.Add(matrix[endRow, i]);
                }
            }          

            if(startColumn!=endColumn)
            {
                for (int i = endRow; i > startRow; i--)
                {
                    ls.Add(matrix[i, startColumn]);
                }
            }

            doSpiralOrder(matrix, startRow + 1, endRow - 1, startColumn + 1, endColumn - 1, ls);
        }

        public int FindKthLargest(int[] nums, int k)
        {
            if(nums==null||nums.Length==0||k<=0)
            {
                return 0;
            }

            return GetKthLargest(nums, k, 0, nums.Length - 1);
        }

        int GetKthLargest(int[] nums,int k,int low, int high)
        {
            int pviot = nums[low];
            int left = low + 1, right = high;

            while(left<=right)
            {
                while(left<=right&&nums[left]>=pviot)
                {
                    left++;
                }

                while(left<=right&&nums[right]<=pviot)
                {
                    right--;
                }

                if(left<right)
                {
                    Swap(nums, left, right);
                }
            }

            Swap(nums, low, right);

            if(right==k-1)
            {
                return pviot;
            }
            else if(right>k-1)
            {
                return GetKthLargest(nums, k, low, right - 1);
            }
            else
            {
                return GetKthLargest(nums, k, right + 1, high);
            }
        }

        public bool ContainsNearbyAlmostDuplicate(int[] nums, int k, int t)
        {
            if(nums==null||nums.Length==0||k<1||t<0)
            {
                return false;
            }
            
            List<long> ss = new List<long>();

            for(int i=0;i<nums.Length;i++)
            {
                if(i>k)
                {
                    ss.Remove(nums[i - 1 - k]);
                }

                long left = (long)nums[i] - t;
                long right =(long) nums[i] + t;
                int j = 0;
                while(ss.Count>0&&j<ss.Count)
                {
                    if(ss[j]>=left&&ss[j]<=right)
                    {
                        return true;
                    }

                    j++;
                }

                ss.Add(nums[i]);
            }

            return false;
        }

        public int MaxCount(int m, int n, int[,] ops)
        {
            if( m < 1 || n < 1)
            {
                return 0;
            }

            if(ops==null||ops.Length==0)
            {
                return m*n;
            }

            int row = ops.GetLength(0);
            int coumn = ops.GetLength(1);
            int minRow = int.MaxValue, minColumn = int.MaxValue;

            for(int i=0;i<row;i++)
            {
                minRow = Math.Min(minRow, ops[i, 0]);
                minColumn = Math.Min(minColumn, ops[i, 1]);
            }

            return minColumn * minRow;
        }

        public void GameOfLife(int[,] board)
        {
            if(board==null||board.Length==0)
            {
                return;
            }

            int m = board.GetLength(0);
            int n = board.GetLength(1);

            for(int i=0;i<m;i++)
            {
                for(int j=0;j<n;j++)
                {
                    int lives = GetLivesOfBoard(board, i, j);

                    if (board[i, j] == 1 &&lives>=2&&lives<=3)
                    {
                        board[i, j] = 3;
                    }

                    if(board[i,j]==0&&lives==3)
                    {
                        board[i, j] = 2;
                    }
                }
            }

            for(int i=0;i<m;i++)
            {
                for(int j=0;j<n;j++)
                {
                    board[i, j] >>= 1;
                }
            }
        }

        int GetLivesOfBoard(int[,] board,int i,int j)
        {
            int m = board.GetLength(0);
            int n = board.GetLength(1);
            int lives = 0;

            if(i>0&&j>0)
            {
                lives += board[i - 1, j - 1] & 1;
            }

            if(i>0)
            {
                lives += board[i - 1, j] & 1;
            }

            if(i>0&&j<n-1)
            {
                lives += board[i - 1, j+1] & 1;
            }

            if(j>0)
            {
                lives += board[i, j - 1] & 1;
            }

            if(j<n-1)
            {
                lives += board[i, j + 1] & 1;
            }

            if(i<m-1&&j>0)
            {
                lives += board[i+1, j - 1] & 1;
            }

            if(i<m-1)
            {
                lives += board[i + 1, j] & 1;
            }

            if (i < m - 1&&j<n-1)
            {
                lives += board[i + 1, j+1] & 1;
            }

            return lives;
        }

        public IList<int> CountSmaller(int[] nums)
        {
            IList<int> res = new List<int>();

            if(nums==null||nums.Length<=0)
            {
                return res;
            }

            List<int> sortList = new List<int>();

            for(int i=nums.Length-1;i>=0;i--)
            {
                int index = FindIndex(sortList, nums[i]);
                res.Insert(0, index);
                sortList.Insert(index, nums[i]);
            }

            return res;
        }

        int FindIndex(List<int> ls,int target)
        {
            if(ls.Count==0)
            {
                return 0;
            }
            
            int start = 0, end = ls.Count - 1;

            if(ls[end]<target)
            {
                return end + 1;
            }

            if(ls[start]>=target)
            {
                return start;
            }

            while(start<end)
            {
                int mid = start + (end - start) / 2;

               if(ls[mid]<target)
                {
                    start = mid + 1;
                }
               else
                {
                    end = mid;
                }
            }

            return start;
        }

        bool FindTriange(int[] arr)
        {
            if(arr==null||arr.Length<3)
            {
                return false;
            }

            int len = arr.Length;
            int min = int.MaxValue, max = int.MinValue;

            for(int i=0;i<len/2;i++)
            {

                if(arr[i]>min&&arr[i]<max)
                {
                    return true;
                }

                if(arr[i]<min)
                {
                    min = arr[i];
                }

                if(arr[len-1-i]>max)
                {
                    max = arr[len - 1 - i];
                }
            }

            return true;
        }

        public int FindLengthOfLCIS(int[] nums)
        {
            if(nums==null||nums.Length==0)
            {
                return 0;
            }

            int maxLen = 1;
            int start = 0;

            for(int i=1;i<nums.Length;i++)
            {
                if(nums[i]<=nums[i-1])
                {
                    start = i;
                }
                else
                {
                    if(maxLen<i-start+1)
                    {
                        maxLen = i - start + 1;
                    }
                }
            }

            return maxLen;
        }

        //public int FindNumberOfLIS(int[] nums)
        //{
        //    if (nums == null || nums.Length == 0)
        //    {
        //        return 0;
        //    }

        //    if (nums.Length == 1)
        //    {
        //        return 1;
        //    }

        //    int count = 1;
        //    int maxLen = 0;
        //    int[] f = new int[nums.Length];
        //    int[] dp = new int[nums.Length];
        //    Dictionary<int, List<List<int>>> dct = new Dictionary<int, List<List<int>>>();

        //    for (int i=0;i<nums.Length;i++)
        //    {
        //        f[i] = 1;
        //        dp[i] = 1;

        //        for (int j = 0; j < i; j++)
        //        {
        //            if (nums[j] < nums[i])
        //            {
        //                f[i] = Math.Max(f[i], f[j] + 1);
        //                dp[i] = dp[j] + 1;
        //            }
        //        }

        //        if (f[i] > maxLen)
        //        {
        //            maxLen = f[i];
        //        }

        //        if(dp[i]==maxLen)
        //        {
        //            if(!dct.ContainsKey(dp[i]))
        //            {
        //                dct.Add(dp[i], 1);

        //                if(i>0&&dct.ContainsKey(dp[i]-1))
        //                {
        //                    dct[dp[i]] = Math.Max(dct[dp[i] - 1], dct[dp[i]]);
        //                }
        //            }
        //            else
        //            {
        //                dct[dp[i]]++;
        //            }
        //        }
        //    }

        //    foreach (int n in dct.Keys)
        //    {
        //        count = Math.Max(count, dct[n]);
        //    }

        //    return count;
        //}

        public int Reverse(int x)
        {
            int result = 0;

            while(x!=0)
            {
                int tail = x % 10;
                int newResult = result * 10 + tail;

                if((newResult-tail)/10!=result)
                {
                    return 0;
                }

                result = newResult;
                x = x / 10;
            }

            return result;
        }

        public int Reverse1(int x)
        {
            long result = 0;

            while (x != 0)
            {
                int tail = x % 10;
                result= result * 10 + tail;

                if(result>int.MaxValue||result<int.MinValue)
                {
                    return 0;
                }
                x = x / 10;
            }

            return (int)result;
        }

        public int MyAtoi(string str)
        {
            if(string.IsNullOrEmpty(str))
            {
                return 0;
            }

            long res = 0;
            int sign=1;
            int i = 0;

            while(i<str.Length&&str[i]==' ')
            {
                i++;
            }

            if(str[i]=='-')
            {
                sign=-1;
                i++;
            }
            else if(str[i] == '+')
            {
                i++;
            }

            for(;i<str.Length;i++)
            {
                if(str[i]<'0'||str[i]>'9')
                {
                    return 0;
                }

                res = res * 10 + str[i] - '0';

                if(res>int.MaxValue)
                {
                    return sign==1?int.MaxValue:int.MinValue;
                }
            }

            return sign==1 ? (int)res : (int) -res;
        }

        public int FindLength(int[] A, int[] B)
        {
            if(A==null||A.Length==0||B==null||B.Length==0)
            {
                return 0;
            }

            Dictionary<int, List<int>> dct = new Dictionary<int, List<int>>();

            for(int i=0;i<A.Length;i++)
            {
                if(!dct.ContainsKey(A[i]))
                {
                    dct.Add(A[i], new List<int>());
                }

                dct[A[i]].Add(i);
            }

            int res = 0;
            int bi = -1;
            int ai = -1;

            for(int i=0;i<B.Length;i++)
            {
                
                if(dct.ContainsKey(B[i])&&dct[B[i]].Count>0)
                {
                    if(i-bi==1||(i-bi>1&& dct[B[i]][0]-ai==1))
                    {
                        res = Math.Max(res, i - bi);
                    }
                    
                    ai = dct[B[i]][0];
                    dct[B[i]].RemoveAt(0);
                }
                else
                {
                    bi = i;
                }
            }

            return res;
        }

        public int FindLength1(int[] A, int[] B)
        {
            if (A == null || A.Length == 0 || B == null || B.Length == 0)
            {
                return 0;
            }

            int lenA = A.Length, lenB = B.Length;
            int[,] dp = new int[lenA, lenB];
            int max = 0;

            for(int i=0;i<lenA;i++)
            {
                for(int j=0;j<lenB;j++)
                {
                    if(i==0||j==0)
                    {
                        if(A[i]==B[j])
                        {
                            dp[i, j] = 1;
                        }
                    }
                    else if(A[i] == B[j])
                    {
                        dp[i, j] = dp[i - 1, j - 1] + 1;
                    }

                    max = Math.Max(max, dp[i, j]);
                }
            }

            return max;
        }

        public bool IsOneBitCharacter(int[] bits)
        {
            if(bits==null||bits.Length==0)
            {
                return false;
            }

            int n = bits.Length;
            int i = 0;
            int[] dp = new int[n];

            while(i<n)
            {
                if(bits[i]==0)
                {
                    dp[i] = 1;
                    i++;

                }
                else
                {
                    dp[i] = 2;
                    i += 2;
                }
            }

            return dp[n - 1] == 1;
        }

        public int Compress(char[] chars)
        {
            if(chars==null||chars.Length==0)
            {
                return 0;
            }

            if(chars.Length<2)
            {
                return chars.Length;
            }

            int n = chars.Length;
            int count = 1;
            List<char> res = new List<char>();
            int i = 1;

            for(;i<n;i++)
            {
                if(chars[i]==chars[i-1])
                {
                    count++;
                }
                else
                {
                    if(count==1)
                    {
                        res.Add(chars[i - 1]);
                    }
                    else
                    {
                        res.Add(chars[i - 1]);

                        foreach(char ch in count.ToString())
                        {
                            res.Add(ch);
                        }
                    }

                    count = 1;
                }
            }

            res.Add(chars[n - 1]);

            if (count>1)
            {
                foreach (char ch in count.ToString())
                {
                    res.Add(ch);
                }
            }

            

            for(int j=0;j<res.Count;j++)
            {
                chars[j] = res[j];
            }

            return res.Count;
        }

        public int SmallestDistancePair(int[] nums, int k)
        {
            if(nums==null||nums.Length==0||k<=0)
            {
                return -1;
            }

            if(k<=0)
            {
                return 0;
            }

            return 1;
        }

        public int DominantIndex(int[] nums)
        {
            if(nums==null||nums.Length<2)
            {
                return -1;
            }

            int max = nums[0];
            int index = 0;
            int sMax = int.MinValue;

            for(int i=1;i<nums.Length;i++)
            {
                if(nums[i]>max)
                {
                    sMax = max;
                    max = nums[i];
                    index = i;
                }
                else if(nums[i]>sMax)
                {
                    sMax = nums[i];
                }
            }

            if(max>=2*sMax)
            {
                return index;
            }

            return -1;
        }

        public IList<string> LetterCasePermutation(string S)
        {
            IList<string> res = new List<string>();

            if (string.IsNullOrEmpty(S))
            {
                res.Add("");
                return res;
            }

            LetterCasePermutationHelper(S, "", 0, res);

            return res;
        }

        void LetterCasePermutationHelper(string S, string str,int start, IList<string> ls)
        {
            if(start==S.Length)
            {
                ls.Add(str);
                return;
            }
            string nextStr = str;

            if(S[start]>='A'&&S[start]<='Z')
            {
                nextStr += S[start];
                LetterCasePermutationHelper(S, nextStr, start + 1, ls);

                nextStr = str;
                nextStr += S[start].ToString().ToLower();
                LetterCasePermutationHelper(S, nextStr, start + 1, ls);
            }
            else if(S[start] >= 'a' && S[start] <= 'z')
            {
                nextStr += S[start];
                LetterCasePermutationHelper(S, nextStr, start + 1, ls);

                nextStr = str;
                nextStr += S[start].ToString().ToUpper();
                LetterCasePermutationHelper(S, nextStr, start + 1, ls);
            }
            else
            {
                nextStr += S[start];
                LetterCasePermutationHelper(S, nextStr, start + 1, ls);
            }
        }

        public int RotatedDigits(int N)
        {
            if(N<2)
            {
                return 0;
            }

            int count = 0;
            for(int i=2;i<=N;i++)
            {
                if(isValidGoodNumber(i))
                {
                    count++;
                }
            }

            return count;
        }

        bool isValidGoodNumber(int num)
        {
            bool isvalid = false;

            while (num > 0)
            {
                int d = num % 10;

                if (d == 2 || d == 5 || d == 6 || d == 9)
                {
                    isvalid = true;
                }

                if(d==3||d==4||d==7)
                {
                    return false;
                }

                num /= 10;
            }

            return isvalid;
        }

        public IList<IList<int>> FourSum(int[] nums, int target)
        {

            IList<IList<int>> res = new List<IList<int>>();

            if (nums == null || nums.Length == 0)
            {
                return res;
            }

            Array.Sort(nums);

            HashSet<List<int>> hset = new HashSet<List<int>>();

            for (int i = 0; i < nums.Length - 3; i++)
            {
                for (int j = i + 1; j < nums.Length - 2; j++)
                {
                    int left = j + 1, right = nums.Length - 1;

                    while (left < right)
                    {
                        int sum = nums[i] + nums[j] + nums[left] + nums[right];
                        if (sum  > 0)
                        {
                            right--;
                        }
                        else if (sum < 0)
                        {
                            left++;
                        }
                        else
                        {
                            List<int> ls = new List<int>();
                            ls.Add(nums[i]);
                            ls.Add(nums[j]);
                            ls.Add(nums[left]);
                            ls.Add(nums[right]);

                            if (!hset.Contains(ls))
                            {
                                res.Add(ls);
                                hset.Add(ls);
                            }

                            left++;
                            right--;
                        }
                    }
                }
            }

            return res;
        }

        public int[][] Transpose(int[][] A)
        {
            if(A==null||A.Length==0)
            {
                return null;
            }

            int m = A.Length;
            int n = A[0].Length;

            int[][] res = new int[n][];

            for(int i=0;i<n;i++)
            {
                res[i] = new int[m];
                for(int j=0;j<m;j++)
                {
                    res[i][j] = A[j][i];
                }
            }

            return res;
        }

        public static int SumSubarrayMins(int[] A)
        {
            if(A==null||A.Length==0)
            {
                return 0;
            }

            int res = 0;

            int c = 1;
            int n = A.Length;
            const int b = 10 * 10 * 10 * 10 * 10 * 10 * 10 * 10 * 10 + 7;


            while (c<=n)
            {
                int start = 0;

                for(int i=c;i<=n;i++)
                {
                    int min = A[start];

                    for(int j=start;j<i;j++)
                    {
                        if(A[j]<min)
                        {
                            min = A[j];
                        }
                    }

                    res += min;
                    start++;
                }

                c++;
            }

            return res%b;
        }

        public static int SumSubarrayMins1(int[] A)

        {
            if (A == null || A.Length == 0)
            {
                return 0;
            }

            int res = 0;

            int c = 1;
            int n = A.Length;
            const int b = 10 * 10 * 10 * 10 * 10 * 10 * 10 * 10 * 10 + 7;
            int min = int.MaxValue;

            while (c <= n)
            {
                int j = 0;
                int i = c;

                c++;
            }

            return res % b;
        }

        public int SumSubarrayMinsLR(int[] A)
        {

            if (A == null || A.Length == 0)
            {
                return 0;
            }

            int MOD = 1000000007;
            int res = 0;
            int c = 1;
            int n = A.Length;
            int[] left = new int[n];
            int[] right = new int[n];
            Stack<int[]> s1 = new Stack<int[]>();
            Stack<int[]> s2 = new Stack<int[]>();

            for(int i=0;i<n;i++)
            {
                int count = 1;

                while(s1.Count>0&& s1.Peek()[0]>A[i])
                {
                    count += s1.Pop()[1];
                }

                s1.Push(new int[] { A[i], count });
                left[i] = count;
            }

            for(int i=n-1;i>=0;i--)
            {
                int count = 1;

                while (s2.Count > 0 && s2.Peek()[0] >=A[i])
                {
                    count += s2.Pop()[1];
                }

                s2.Push(new int[] { A[i], count });
                right[i] = count;
            }

            for(int i=0;i<n;i++)
            {
                res = (res + A[i] * left[i] * right[i]) % MOD;
            }

            return res;
        }

        public int SumSubarrayMinsMap(int[] A)
        {
            if (A == null || A.Length == 0)
            {
                return 0;
            }

            int MOD = 1000000007;
            int res = 0;
            int n = A.Length;

            Dictionary<int, int> dct = new Dictionary<int, int>();
            Stack<int> st = new Stack<int>();

            for(int i=0;i<n;i++)
            {
                while(st.Count>0&&A[st.Peek()]>A[i])
                {
                    st.Pop();
                }

                int cur = 0;

                if(st.Count==0)
                {
                    cur = (i + 1) * A[i];
                }
                else
                {
                    cur = dct[st.Peek()] + (i - st.Peek()) * A[i];
                }

                cur %= MOD;
                st.Push(i);
                dct.Add(i, cur);
                res += cur;
                res %= MOD;
            }

            return res;
        }

        public int CalPoints(string[] ops)
        {
            if(ops==null||ops.Length==0)
            {
                return 0;
            }


        }

        public int SmallestRangeI(int[] A, int K)
        {
            if(A==null||A.Length<=1)
            {
                return 0;
            }

            int n = A.Length;
            int min = A[0], max = A[0];
            int res = int.MaxValue;

            for(int i=1;i<n;i++)
            {
                if(A[i]>max)
                {
                    max = A[i];
                }

                if(A[i]<min)
                {
                    min = A[i];
                }
            }

            if(max-min<=2*K)
            {
                res = 0;
            }
            else
            {
                res = max - min - 2 * K;
            }

            return res;
        }

        public int SmallestRangeII(int[] A, int K)
        {
            if (A == null || A.Length <= 1)
            {
                return 0;
            }

            Array.Sort(A);
            int n = A.Length;
            int min = A[0], max = A[n-1];
            int res = max-min;
            max = max - K;

            for(int i=0;i<n-1;i++)
            {
                max = Math.Max(A[i] + K, max);
                min = Math.Min(A[0]+K, A[i + 1] - K);
                res = Math.Min(res, max - min);
            }

            return Math.Min(res,max-min);
        }

        public int PartitionDisjoint(int[] A)
        {
            if(A==null||A.Length==0)
            {
                return 0;
            }

            int len = 0;
            int n = A.Length;
            int[] left = new int[n];
            int[] right = new int[n];
            int max = A[0];
            for(int i=0;i<n;i++)
            {
                max = Math.Max(A[i], max);
                left[i] = max;
            }

            int min = A[n - 1];

            for(int i=n-1;i>=0;i--)
            {
                min = Math.Min(A[i], min);
                right[i] = min;
            }

            for(int i=0;i<n-1;i++)
            {
                if(left[i]<=right[i+1])
                {
                    len = i + 1;
                    break;
                }
            }

            return len;
        }

        public int MaxSubarraySumCircular(int[] A)
        {
            if (A == null || A.Length == 0)
            {
                return 0;
            }

            int n = A.Length;

            int max = 0;
            int s = 0;
            int min=0;
            int maxSum = int.MinValue;
            int minSum = int.MaxValue;

            for (int i = 0; i < n; i++)
            {
                s += A[i];

                max += A[i];

                if(max<A[i])
                {
                    max = A[i];
                }

                if(maxSum<max)
                {
                    maxSum = max;
                }

                min += A[i];

                if(A[i]<min)
                {
                    min = A[i];
                }

                if(min<minSum)
                {
                    minSum = min;
                }
            }

            return maxSum>0?Math.Max(maxSum,s-minSum):maxSum;
        }
    }

    public class TopVotedCandidate
    {
        Dictionary<int, int> dct;
        public TopVotedCandidate(int[] persons, int[] times)
        {
            Dictionary<int, int> dt = new Dictionary<int, int>();
            int n = times.Length;
            int lead = -1;
            for(int i=0;i<n;i++)
            {

            }
        }

        public int Q(int t)
        {

        }
    }
}

    //public class ExamRoom
    //{

    //    public ExamRoom(int N)
    //    {

    //    }

    //    public int Seat()
    //    {

    //    }

    //    public void Leave(int p)
    //    {

    //    }
    //}
    
//}
