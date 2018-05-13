using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding
{
    class BFS
    {

        public int shortestDistance(int[][] grid)
        {
            if (grid == null || grid.Length == 0 || grid[0].Length == 0)
            {
                return -1;
            }

            int min = int.MaxValue;
            int m = grid.Length, n = grid[0].Length;
            int[,] dist = new int[m, n];
            Queue<int[]> q = new Queue<int[]>();
            int walk = 1;
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (grid[i][j] == 1)
                    {
                        bfsHelper(grid, dist, i, j, --walk);
                    }
                }
            }

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (grid[i][j] == walk)
                    {
                        min = Math.Min(min, dist[i, j]);
                    }
                }
            }

            return min == int.MaxValue ? -1 : min;
        }

        void bfsHelper(int[][] grid, int[,] dis, int row, int col, int walk)
        {
            Queue<int[]> q = new Queue<int[]>();
            q.Enqueue(new int[] { row, col });
            int level = 0;

            while (q.Count != 0)
            {
                int size = q.Count;
                level++;
                for (int i = 0; i < size; i++)
                {
                    int[] a = q.Dequeue();
                    int r = a[0], c = a[1];

                    if (AddPath(grid, dis, r + 1, c, walk, level))
                    {
                        q.Enqueue(new int[] { r + 1, c });
                    }

                    if (AddPath(grid, dis, r - 1, c, walk, level))
                    {
                        q.Enqueue(new int[] { r - 1, c });
                    }

                    if (AddPath(grid, dis, r, c + 1, walk, level))
                    {
                        q.Enqueue(new int[] { r, c + 1 });
                    }

                    if (AddPath(grid, dis, r, c - 1, walk, level))
                    {
                        q.Enqueue(new int[] { r, c - 1 });
                    }
                }
            }
        }

        bool AddPath(int[][] grid, int[,] dis, int row, int col, int walk, int level)
        {
            if (row < 0 || row >= grid.Length || col < 0 || col >= grid[0].Length)
            {
                return false;
            }

            if (grid[row][col] == walk)
            {
                dis[row, col] += level;
                grid[row][col]--;
                return true;
            }

            return false;
        }

        public IList<IList<string>> FindLadders(string beginWord, string endWord, IList<string> wordList)
        {
            IList<IList<string>> res = new List<IList<string>>();

            if (string.IsNullOrEmpty(beginWord) || string.IsNullOrEmpty(endWord))
            {
                return res;
            }

            if (beginWord == endWord)
            {
                List<string> item = new List<string>();
                item.Add(beginWord);
                res.Add(new List<string>(item));
                return res;
            }

            List<string> itemList = new List<string>();
            itemList.Add(beginWord);
            FindLaddersHelper(beginWord, endWord, wordList, itemList, res);

            return res;
        }

        void FindLaddersHelper(string beginWord, string endWord, IList<string> wordList, List<string> ls, IList<IList<string>> res)
        {
            if (beginWord.Equals(endWord))
            {
                if (res.Count != 0)
                {
                    while (res.Count != 0 && res.Last().Count > ls.Count)
                    {
                        res.RemoveAt(res.Count - 1);
                    }

                    if (res.Count == 0 || res.Last().Count == ls.Count)
                        res.Add(new List<string>(ls));
                }
                else
                {
                    res.Add(new List<string>(ls));
                }

                return;
            }

            for (int i = 0; i < beginWord.Length; i++)
            {
                for (char c = 'a'; c <= 'z'; c++)
                {
                    if (beginWord[i] != c)
                    {
                        char[] chArr = beginWord.ToCharArray();
                        chArr[i] = c;
                        string str = new string(chArr);
                        if (wordList.Contains(str))
                        {
                            ls.Add(str);
                            wordList.Remove(str);
                            FindLaddersHelper(str, endWord, wordList, ls, res);
                            ls.Remove(str);
                            wordList.Add(str);
                        }
                    }
                }
            }
        }

        public void Solve(char[,] board)
        {
            if (board == null || board.Length <= 1)
            {
                return;
            }


            int m = board.GetLength(0);
            int n = board.GetLength(1);

            for (int i = 0; i < m; i++)
            {
                SolveHelper(board, i, 0, m, n);

                if (n > 1)
                {
                    SolveHelper(board, i, n - 1, m, n);
                }
            }

            for (int i = 1; i < n - 1; i++)
            {
                SolveHelper(board, 0, i, m, n);

                if (m > 1)
                {
                    SolveHelper(board, m - 1, i, m, n);
                }
            }

            for (int i = 0; i < m - 1; i++)
            {
                for (int j = 1; j < n - 1; j++)
                {
                    if (board[i, j] == 'O')
                    {
                        board[i, j] = 'X';
                    }
                }
            }

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (board[i, j] == 'z')
                    {
                        board[i, j] = 'O';
                    }
                }
            }
        }

        void SolveHelper(char[,] board, int r, int c, int m, int n)
        {
            if (board[r, c] == 'O')
            {
                board[r, c] = 'z';

                if (r > 1)
                    SolveHelper(board, r - 1, c, m, n);

                if (r < m - 1)
                    SolveHelper(board, r + 1, c, m, n);

                if (c > 1)
                    SolveHelper(board, r, c - 1, m, n);

                if (c < n - 1)
                    SolveHelper(board, r, c + 1, m, n);
            }
        }

        public int[] FindOrder(int numCourses, int[,] prerequisites)
        {
            if (numCourses == 0)
            {
                return new int[0];
            }

            List<int> res = new List<int>();
            int[] outDegree = new int[numCourses];
            int m = prerequisites.GetLength(0);
            Queue<int> q = new Queue<int>();

            for (int i = 0; i < m; i++)
            {
                outDegree[prerequisites[i, 0]]++;
            }

            for (int i = 0; i < numCourses; i++)
            {
                if (outDegree[i] == 0)
                {
                    q.Enqueue(i);
                }
            }

            if (q.Count == 0)
            {
                return new int[0];
            }

            while (q.Count != 0)
            {
                int num = q.Dequeue();
                res.Add(num);

                for (int j = 0; j < m; j++)
                {
                    if (prerequisites[j, 1] == num)
                    {
                        outDegree[prerequisites[j, 0]]--;

                        if (outDegree[prerequisites[j, 0]] == 0)
                        {
                            q.Enqueue(prerequisites[j, 0]);
                        }
                    }
                }
            }

            if (res.Count == numCourses)
            {
                return res.ToArray();
            }

            return new int[0];
        }

        public int ArrayNesting(int[] nums)
        {
            if (nums == null || nums.Length == 0)
            {
                return 0;
            }

            if (nums.Length == 1)
            {
                return 1;
            }

            int max = 1;

            for (int i = 0; i < nums.Length; i++)
            {
                int len = 0;
                int k = i;

                while (nums[k] >= 0)
                {
                    len++;
                    int temp = nums[k];
                    nums[k] = -1;
                    k = temp;
                }

                max = Math.Max(len, max);
            }

            return max;
        }

        public double FindMaxAverage(int[] nums, int k)
        {
            if (nums == null || nums.Length < k)
            {
                return 0.0;
            }

            double sum = 0;
            //int max = 0;
            double aver = 0;
            int start = 0;

            for (int i = 0; i < k; i++)
            {
                sum += nums[i];
            }

            aver = sum / k;
            for (int i = k; i < nums.Length; i++)
            {
                sum += nums[i];

                if ((sum / (i - start + 1)) > (sum - nums[start]) / (i - start))
                {
                    aver = Math.Max((sum / (i - start + 1)), aver);
                }
                else
                {
                    sum -= nums[start];
                    aver = Math.Max(((sum - nums[start]) / (double)(i - start)), aver);
                    start++;
                }
            }

            return aver;
        }

        public IList<string> RemoveInvalidParentheses(string s)
        {
            IList<string> res = new List<string>();

            if (s == null)
            {
                return res;
            }

            HashSet<string> visited = new HashSet<string>();
            Queue<string> q = new Queue<string>();

            q.Enqueue(s);
            visited.Add(s);

            bool found = false;

            while (q.Count != 0)
            {
                string str = q.Dequeue();

                if (IsValid(str))
                {
                    found = true;
                    res.Add(str);
                }

                if (!found)
                {
                    for (int i = 0; i < str.Length; i++)
                    {
                        if (str[i] == '(' || str[i] == ')')
                        {
                            string t = str.Substring(0, i) + str.Substring(i + 1);

                            if (!visited.Contains(t))
                            {
                                q.Enqueue(t);
                                visited.Add(t);
                            }
                        }
                    }
                }
            }

            return res;
        }

        bool IsValid(string str)
        {
            int count = 0;

            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '(')
                {
                    count++;
                }

                if (str[i] == ')')
                {
                    if (count == 0)
                    {
                        return false;
                    }

                    count--;
                }
            }

            return count == 0;
        }

        //public int TrapRainWater(int[,] heightMap)
        //{
        //    if(heightMap==null||heightMap.Length==0)
        //    {
        //        return 0;
        //    }

        //    int row = heightMap.GetLength(0);
        //    int col = heightMap.GetLength(1);


        //}

        //public IList<int[]> PacificAtlantic(int[,] matrix)
        //{
        //    IList<int[]> res = new List<int[]>();

        //    if(matrix==null||matrix.Length==0)
        //    {
        //        return res;
        //    }

        //    int row = matrix.GetLength(0);
        //    int col = matrix.GetLength(1);

        //    if(row==1&&col==1)
        //    {
        //        res.Add(new int[2] { 0, 0 });
        //        return res;
        //    }

        //    int i = 0, j = 0;
        //    res.Add(new int[2] { i, col - 1 });

        //    if(col!=row)

        //    while(i<row||j<col)
        //    {

        //    }
        //}

        public int[,] UpdateMatrix(int[,] matrix)
        {

            if (matrix == null || matrix.Length == 0)
            {
                return new int[0, 0];
            }

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            int[,] res = new int[rows, cols];
            int[,] dirs = { { -1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 } };

            Queue<int[]> q = new Queue<int[]>();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (matrix[i, j] == 0)
                    {
                        res[i, j] = 0;
                        q.Enqueue(new int[2] { i, j });
                    }
                    else
                    {
                        res[i, j] = int.MaxValue;
                    }
                }
            }

            while (q.Count != 0)
            {
                int[] arr = q.Dequeue();
                int x = arr[0], y = arr[1];

                for (int i = 0; i < 4; i++)
                {
                    int x1 = x + dirs[i, 0], y1 = y + dirs[i, 1];

                    if (x1 < 0 || x1 >= rows || y1 < 0 || y1 >= cols || res[x1, y1] <= res[x, y] + 1)
                    {
                        continue;
                    }

                    res[x1, y1] = res[x, y] + 1;
                    q.Enqueue(new int[2] { x1, y1 });
                }
            }

            return res;
        }

        public int CutOffTree(IList<IList<int>> forest)
        {
            if (forest.Count == 0)
            {
                return 0;
            }

            List<int[]> fl = new List<int[]>();
            int m = forest.Count, n = forest[0].Count;

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (forest[i][j] > 1)
                    {
                        fl.Add(new int[] { forest[i][j], i, j });
                    }
                }
            }

            fl.Sort((t1, t2) => (t1[0].CompareTo(t2[0])));

            int[] start = new int[2];
            int res = 0;

            foreach (int[] h in fl)
            {
                int step = GetMinSteps(forest, start, h, m, n);

                if (step < 0)
                {
                    return -1;
                }

                start[0] = h[1];
                start[1] = h[2];

                res += step;
            }

            return res;
        }

        int GetMinSteps(IList<IList<int>> forest, int[] start, int[] dest, int m, int n)
        {
            bool[,] visited = new bool[m, n];
            int steps = 0;
            Queue<int[]> q = new Queue<int[]>();
            q.Enqueue(start);

            while (q.Count != 0)
            {
                int count = q.Count;

                while (count > 0)
                {
                    int[] curr = q.Dequeue();

                    if (curr[0] == dest[1] && curr[1] == dest[2])
                    {
                        return steps;
                    }


                    visited[curr[0], curr[1]] = true;

                    if (curr[0] > 0 && forest[curr[0] - 1][curr[1]] != 0 && !visited[curr[0] - 1, curr[1]])
                    {
                        q.Enqueue(new int[] { curr[0] - 1, curr[1] });
                    }

                    if (curr[0] < m - 1 && forest[curr[0] + 1][curr[1]] != 0 && !visited[curr[0] + 1, curr[1]])
                    {
                        q.Enqueue(new int[] { curr[0] + 1, curr[1] });
                    }

                    if (curr[1] > 0 && forest[curr[0]][curr[1] - 1] != 0 && !visited[curr[0], curr[1] - 1])
                    {
                        q.Enqueue(new int[] { curr[0], curr[1] - 1 });
                    }

                    if (curr[1] < n - 1 && forest[curr[0]][curr[1] + 1] != 0 && !visited[curr[0], curr[1] + 1])
                    {
                        q.Enqueue(new int[] { curr[0], curr[1] + 1 });
                    }

                    count--;
                }

                steps++;
            }

            return -1;
        }

        public IList<int[]> PacificAtlantic(int[,] matrix)
        {
            IList<int[]> res = new List<int[]>();

            if (matrix == null || matrix.Length == 0)
            {
                return res;
            }

            int m = matrix.GetLength(0);
            int n = matrix.GetLength(1);

            int[] rowMaxes = new int[m];
            int[] columnMaxes = new int[n];

            for (int i = 0; i < m; i++)
            {
                int max = 0;

                for (int j = 0; j < n; j++)
                {
                    max = Math.Max(matrix[i, j], max);
                }

                rowMaxes[i] = max;
            }

            for (int i = 0; i < n; i++)
            {
                int max = 0;

                for (int j = 0; j < m; j++)
                {
                    max = Math.Max(matrix[j, i], max);
                }

                columnMaxes[i] = max;
            }

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (matrix[i, j] == rowMaxes[i] || matrix[i, j] == columnMaxes[j])
                    {
                        res.Add(new int[] { i, j });
                    }
                    else if ((i == 0 && j == n - 1) || (i == m - 1 && j == 0))
                    {
                        res.Add(new int[] { i, j });
                    }
                }
            }

            return res;
        }

        public int SingleNonDuplicate(int[] nums)
        {
            if (nums == null || nums.Length % 2 == 0)
            {
                return 0;
            }

            int left = 0, right = nums.Length - 1;

            while (left < right)
            {
                int mid = left + (right - left) / 2;

                if (mid % 2 == 0)
                {
                    mid--;
                }

                if (nums[mid] == nums[mid - 1])
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }

            return nums[left];
        }

        public void GameOfLife(int[,] board)
        {
            if (board == null || board.Length == 0)
            {
                return;
            }

            int m = board.GetLength(0);
            int n = board.GetLength(1);

            int[] dx = { -1, -1, -1, 0, 1, 1, 1, 0 };
            int[] dy = { -1, 0, 1, -1, -1, 0, 1, 1 };

            for(int i=0;i<m;i++)
            {
                for(int j=0;j<n;j++)
                {
                    int lives = 0;
                    for(int k=0;k<8;k++)
                    {
                        int x = i + dx[k];
                        int y = j + dy[k];

                        if(x>=0&&x<m&&y>=0&&y<n)
                        {
                            lives += board[x, y] & 1;
                        }
                    }

                    if(board[i,j]==1&&lives>=2&&lives<=3)
                    {
                        board[i, j] = 3;
                    }
                    else if(board[i,j]==0&&lives==3)
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
    }
}
