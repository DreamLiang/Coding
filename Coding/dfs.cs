using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding
{
    class dfs
    {
        public int FindCircleNum(int[,] M)
        {
            if(M==null||M.Length==0)
            {
                return 0;
            }

            int count = 0;
            int n = M.GetLength(0);
            int[] visited = new int[n];

            for(int i=0;i<n;i++)
            {
                if(visited[i]==0)
                {
                    fcDFS(M, visited, i);
                    count++;
                }
            }

            return count;
        }

        void fcDFS(int[,] M, int[] visited,int i)
        {
            for(int j=0;j<M.GetLength(1);j++)
            {
                if(M[i,j]==1&&visited[j]==0)
                {
                    visited[j] = 1;
                    fcDFS(M, visited, j);
                }
            }
        }

        public IList<IList<int>> FindSubsequences(int[] nums)
        {
            List<IList<int>> res = new List<IList<int>>();

            List<int> ls = new List<int>();

            fssDFSHelper(nums, ls, res, 0);

            return res;
        }

        void fssDFSHelper(int[] nums,List<int> holder, List<IList<int>> res, int index)
        {
            if(holder.Count>=2)
            {
                res.Add(new List<int>(holder));
            }

            HashSet<int> used = new HashSet<int>();
            for(int i=index;i<nums.Length;i++)
            {
                if(used.Contains(nums[i]))
                {
                    continue;
                }

                if(holder.Count==0||holder[holder.Count-1]<=nums[i])
                {
                    used.Add(nums[i]);
                    holder.Add(nums[i]);
                    fssDFSHelper(nums, holder, res, i + 1);
                    holder.RemoveAt(holder.Count - 1);
                }
            }
        }

        public int[] FindMode(TreeNode root)
        {
            if(root==null)
            {
                return new int[0];
            }

            List<int> ls = Inorder(root);
            int count = 1, num = ls[0],maxCount=0;
            List<int> res = new List<int>();
            res.Add(ls[0]);

            for (int i=1;i<ls.Count;i++)
            {
                if(ls[i]==num)
                {
                    count++;
                }

                if (ls[i] != num||i==ls.Count-1)
                {
                    if (count > maxCount)
                    {
                        res.Clear();
                        res.Add(num);
                        maxCount = count;
                    }
                    else if (count == maxCount&&!res.Contains(num))
                    {
                        res.Add(num);
                    }

                    num = ls[i];
                    count = 1;

                    if(count==maxCount && !res.Contains(num))
                    {
                        res.Add(num);
                    }
                }
            }

            return res.ToArray();
        }

        List<int> Inorder(TreeNode root)
        {
            List<int> res = new List<int>();

            if(root==null)
            {
                return res;
            }

            res.AddRange(Inorder(root.left));
            res.Add(root.val);
            res.AddRange(Inorder(root.right));

            return res;
        }

        public IList<IList<int>> CombinationSum(int[] candidates, int target)
        {
            IList<IList<int>> res = new List<IList<int>>();

            if(candidates==null||candidates.Length==0)
            {
                return res;
            }

            Array.Sort(candidates);

            DoCombinationSum(candidates, 0, target, new List<int>(), res);

            return res;
        }

        void DoCombinationSum(int[] candidates, int sIndex, int target, List<int> ls, IList<IList<int>> res )
        {
            if(target<0)
            {
                return;
            }

            if(target==0)
            {
                res.Add(new List<int>(ls));
                return;
            }

            for(int i=sIndex;i<candidates.Length;i++)
            {
                if (i > sIndex && candidates[i] == candidates[i - 1])
                    continue;

                ls.Add(candidates[i]);
                DoCombinationSum(candidates, i+1, target - candidates[i], ls, res);
                ls.RemoveAt(ls.Count - 1);
            }
        }

        public IList<IList<int>> CombinationSum3(int k, int n)
        {
            IList<IList<int>> res = new List<IList<int>>();

            if(k<=0||n<=0)
            {
                return res;
            }

            DoCominationSum3(1, k, n, new List<int>(), res);

            return res;
        }

        void DoCominationSum3(int start, int k, int target, List<int> ls, IList<IList<int>> res)
        {
            if(target<0)
            {
                return;
            }

            if(target==0&&k==0)
            {
                res.Add(new List<int>(ls));
                return;
            }

            for(int i=start;i<=9;i++)
            {
                ls.Add(i);
                DoCominationSum3(i + 1, k - 1, target - i, ls, res);
                ls.RemoveAt(ls.Count - 1);
            }
        }

        public int CombinationSum4(int[] nums, int target)
        {
            if(target==0)
            {
                return 1;
            }

            int[] dp = new int[target + 1];
            
            for(int i=1;i<=target;i++)
            {
                dp[i] = -1;
            }

            dp[0] = 1;
            
            return CombSum4Helper(nums, target, dp);
        }

        int CombSum4Helper(int[] nums,int target,int[] dp)
        {
            if(dp[target]!=-1)
            {
                return dp[target];
            }

            int res = 0;

            for (int i = 0; i < nums.Length; i++)
            {
                if (target >= nums[i])
                {
                    res += CombSum4Helper(nums, target - nums[i],dp);
                }
            }

            dp[target] = res;

            return res;
        }

        public int NumSubarrayProductLessThanK(int[] nums, int k)
        {
            if(nums==null||nums.Length==0||k<=1)
            {
                return 0;
            }

            int[] count= new int[1];
            doNumSubarrayProductLessThanK(nums, k,  count, 0, 1);

            return count[0];
        }

        void doNumSubarrayProductLessThanK(int[] nums, int k, int[] count,int start, int curr)
        {
            if(start==nums.Length)
            {
                return;
            }

            for(int i=start;i<nums.Length;i++)
            {
                if((start==0||i-start<2)&&curr*nums[i]<k)
                {
                    count[0]++;
                    doNumSubarrayProductLessThanK(nums, k, count, i + 1, curr * nums[i]);
                }
            }
        }

        public static int NumSubarrayProductLessThanK1(int[] nums, int k)
        {
            if (nums == null || nums.Length == 0 || k <= 1)
            {
                return 0;
            }

            int n = nums.Length;
            int count = 0;

            for(int i=0;i<n;i++)
            {
                int p = 1;
                for(int j=i;j<n;j++)
                {
                    p *= nums[j];
                    
                    if(p<k)
                    {
                        count++;
                        Console.WriteLine(p);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return count;
        }

        public bool AreSentencesSimilar(string[] words1, string[] words2, string[,] pairs)
        {
            if(words1==null||words2==null||pairs==null||words1.Length!=words2.Length)
            {
                return false;
            }

            int n = words1.Length;
            int m = pairs.GetLength(0);
            Dictionary<string, HashSet<string>> dct = new Dictionary<string, HashSet<string>>();

            for(int i=0;i<m;i++)
            {
                if(!dct.ContainsKey(pairs[i,0]))
                {
                    dct.Add(pairs[i, 0],new HashSet<string>());
                }

                dct[pairs[i, 0]].Add(pairs[i, 1]);
            }

            for(int i=0;i<n;i++)
            {
                if(words1[i].Equals(words2[i])||(dct.ContainsKey(words1[i])&&dct[words1[i]].Contains(words2[i]))||(dct.ContainsKey(words2[i])&&dct[words2[i]].Contains(words1[i])))
                {
                    continue;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public int[,] FloodFill(int[,] image, int sr, int sc, int newColor)
        {

            if (image == null || image.Length == 0||image[sr,sc]==newColor)
            {
                return image;
            }

            int m = image.GetLength(0);
            int n = image.GetLength(1);

            FloodFillHelper(image, sr, sc,m,n, image[sr, sc], newColor);

            return image;
        }

        void FloodFillHelper(int[,] image,int sr, int sc,int m,int n, int imgColor, int newColor)
        {
            if(sr<0||sr>=m||sc<0||sc>=n||image[sr,sc]!=imgColor)
            {
                return;
            }

            image[sr, sc] = newColor;
            FloodFillHelper(image, sr - 1, sc, m, n, imgColor, newColor);
            FloodFillHelper(image, sr, sc-1, m, n, imgColor, newColor);
            FloodFillHelper(image, sr + 1, sc, m, n, imgColor, newColor);
            FloodFillHelper(image, sr, sc+1, m, n, imgColor, newColor);
        }

        public int[] AsteroidCollision(int[] asteroids)
        {
            if(asteroids==null||asteroids.Length<2)
            {
                return asteroids;
            }

            int n = asteroids.Length;
            List<int> ls = new List<int>();
            ls.Add(asteroids[n - 1]);

            for (int i = n - 2; i >= 0; i--)
            {
                if (ls.Count > 0)
                {
                    if (ls[0] * asteroids[i] > 0)
                    {
                        ls.Insert(0, asteroids[i]);
                    }
                    else
                    {
                        if(ls[0]>0&& Math.Abs(asteroids[i]) > Math.Abs(ls[0]))
                        {
                            ls.Insert(0, asteroids[i]);
                        }
                        else if (Math.Abs(asteroids[i]) > Math.Abs(ls[0]))
                        {
                            ls[0] = asteroids[i];
                        }
                        else if (Math.Abs(asteroids[i]) == Math.Abs(ls[0]))
                        {
                            ls.RemoveAt(0);
                        }
                    }
                }
                else
                {
                    ls.Insert(0, asteroids[i]);
                }
            }

            return ls.ToArray();
        }

        public int NumIslands(char[,] grid)
        {
            if(grid==null||grid.Length==0)
            {
                return 0;
            }

            int m = grid.GetLength(0);
            int n = grid.GetLength(1);
            int count = 0;

            for(int i=0;i<m;i++)
            {
                for(int j=0;j<n;j++)
                {
                    if(grid[i,j]=='1')
                    {
                        NumIslandsHelper(grid, i, j, m, n);
                        count++;
                    }
                }
            }

            return count;
        }

        void NumIslandsHelper(char[,] grid, int i,int j,int m, int n)
        {
            if(i<0||i>=m||j<0||j>=n||grid[i,j]=='0')
            {
                return;
            }

            grid[i, j] = '0';

            NumIslandsHelper(grid, i - 1, j, m, n);
            NumIslandsHelper(grid, i + 1, j, m, n);
            NumIslandsHelper(grid, i, j - 1, m, n);
            NumIslandsHelper(grid, i, j + 1, m, n);
        }

        public static IList<int> SplitIntoFibonacci(string S)
        {
            IList<int> res = new List<int>();
            
            if(string.IsNullOrEmpty(S)||S.StartsWith("-"))
            {
                return res;
            }


            SFBHelper(S, res,0);

            return res;
        }
      
      static bool SFBHelper(string S,IList<int> ls, int start)
        {
            if(start==S.Length&&ls.Count>=3)
            {
                return true;
            }

            for(int i=start;i<S.Length;i++)
            {
                if(S[start]=='0'&&i>start)
                {
                    break;
                }

                long num = long.Parse(S.Substring(start, i - start + 1));

                if(num>int.MaxValue)
                {
                    break;
                }

                int count = ls.Count;

                if (count >= 2 && num > ls[count - 1] +ls[count-2])
                {
                    break;
                }

                if(count<2||num==ls[count-1]+ls[count-2])
                {
                    ls.Add((int)num);

                    if(SFBHelper(S,ls,i+1))
                    {
                        return true;
                    }

                    ls.RemoveAt(ls.Count - 1);
                }
            }

            return false;
        }
      static  void SplitIntoFibonacciHelper(string S, int start, int n, List<string> strls, IList<int> ls,bool[] isEnd)
        {
            if(start==n && isEnd[0] == false)
            {
                int m = strls.Count;
                int i = 0;
                int strlen = 0;

                while (i < m)
                {
                    strlen += strls[i].Length;
                    int num = int.MinValue;
                    if (!(strls[i].Length>1&&strls[i][0]=='0')&&int.TryParse(strls[i], out num))
                    {
                        ls.Add(num);
                    }
                    else
                    {
                        ls.Clear();
                        return;
                    }

                    i++;
                }

                if (strlen == S.Length)
                {

                    i = 2;
                    while (i < m && ls[i] == ls[i - 1] + ls[i - 2])
                    {
                        i++;
                    }

                    if (m >= 3 && i == m)
                    {
                        isEnd[0] = true;
                    }
                    else
                    {
                        ls.Clear();
                    }
                }
                else
                {
                    ls.Clear();
                }

                return;
            }
            else if(isEnd[0])
            {
                return;
            }

            if(isEnd[0]==false)
            {
                for(int i=start;i<n;i++)
                {
                    strls.Add(S.Substring(start, i - start + 1));
                    int len = strls.Count;

                    SplitIntoFibonacciHelper(S, i + 1, n, strls, ls, isEnd);

                    if (isEnd[0])
                    {
                        return;
                    }

                    strls.RemoveAt(len - 1);
                }
            }
        }

        public char[,] UpdateBoard(char[,] board, int[] click)
        {
            if(board==null||board.Length==0)
            {
                return board;
            }

            int row = click[0], column = click[1];
            int m = board.GetLength(0), n = board.GetLength(1);
            int count = 0;
            List<int[]> ls = new List<int[]>();

            if(board[row,column]=='M')
            {
                board[row, column] = 'X';
            }
            else
            {
                for(int i=-1;i<2;i++)
                {
                    for(int j=-1;j<2;j++)
                    {
                        int x = row + i, y = column + j;

                        if(x<0||y<0||x>=m||y>=n)
                        {
                            continue;
                        }

                        if(board[x,y]=='M')
                        {
                            count++;
                        }
                        else if(board[x,y]=='E'&&count==0)
                        {
                            int[] a = { x, y };
                            ls.Add(a);
                        }
                    }
                }

                if(count>0)
                {
                    board[row, column] = char.Parse(count.ToString());
                }
                else
                {
                    foreach(int[] arr in ls)
                    {
                        board[arr[0], arr[1]] = 'B';
                        UpdateBoard(board, arr); 
                    }
                }
            }

            return board;
        }

        public char[,] UpdateBoard1(char[,] board, int[] click)
        {
            if (board == null || board.Length == 0)
            {
                return board;
            }

            int row = click[0], column = click[1];
            int m = board.GetLength(0), n = board.GetLength(1);
            int count = 0;

            if (board[row, column] == 'M')
            {
                board[row, column] = 'X';
            }
            else
            {
                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        int x = row + i, y = column + j;

                        if (x < 0 || y < 0 || x >= m || y >= n)
                        {
                            continue;
                        }

                        if (board[x, y] == 'M')
                        {
                            count++;
                        }
                    }
                }

                if (count > 0)
                {
                    board[row, column] = char.Parse(count.ToString());
                }
                else
                {
                    board[row, column] = 'B';

                    for(int i=-1;i<2;i++)
                    {
                        for(int j=-1;j<2;j++)
                        {
                            int x = row + i, y = column + j;

                            if (x < 0 || y < 0 || x >= m || y >= n)
                            {
                                continue;
                            }

                            if (board[x, y] == 'E')
                            {
                                int[] next = { x, y };
                                UpdateBoard1(board, next);
                            }
                        }
                    }
                }
            }

            return board;
        }
    }
}
