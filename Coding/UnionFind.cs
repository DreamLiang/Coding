using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding
{
    class UnionFind
    {
        public int[] FindRedundantConnection(int[,] edges)
        {
            if (edges == null || edges.Length == 0)
            {
                return new int[0];
            }

            int n = edges.GetLength(0);
            int[] roots = new int[n + 1];

            for (int i = 0; i < n; i++)
            {
                roots[i] = -1;
            }

            for (int i = 0; i < n; i++)
            {
                int x = FindSet(roots, edges[i, 0]);
                int y = FindSet(roots, edges[i, 1]);

                if (x == y)
                {
                    return new int[2] { edges[i, 0], edges[i, 1] };
                }

                roots[x] = y;
            }

            return new int[0];
        }

        public static bool ValidTree(int n, int[,] edges)
        {
            if (edges == null || edges.Length == 0)
            {
                return true;
            }

            int[] roots = new int[n];

            for (int i = 0; i < n; i++)
            {
                roots[i] = -1;
            }

            for (int i = 0; i < edges.GetLength(0); i++)
            {
                int x = edges[i, 0];
                int y = edges[i, 1];

                if (x == y)
                {
                    return false;
                }

                roots[x] = y;
            }

            return edges.GetLength(0) == n - 1;
        }

        public static int FindSet(int[] roots, int i)
        {
            while (roots[i] != -1)
            {
                i = roots[i];
            }

            return i;
        }

        public int[] FindRedundantDirectedConnection(int[,] edges)
        {
            if (edges == null || edges.Length == 0)
            {
                return new int[0];
            }

            int[] canA = { -1, -1 };
            int[] canB = { -1, -1 };

            int n = edges.GetLength(0);
            int[] parents = new int[n + 1];

            for (int i = 0; i < n; i++)
            {
                if (parents[edges[i, 1]] == 0)
                {
                    parents[edges[i, 1]] = edges[i, 0];
                }
                else
                {
                    canA[0] = edges[i, 0];
                    canA[1] = edges[i, 1];
                    canB[0] = parents[edges[i, 1]];
                    canB[1] = edges[i, 1];

                    edges[i, 1] = 0;
                }
            }

            for (int i = 0; i <= n; i++)
            {
                parents[i] = i;
            }

            for (int i = 0; i < n; i++)
            {
                if (edges[i, 1] != 0)
                {
                    int father = edges[i, 0], child = edges[i, 1];

                    if (FindRoot(parents, father) == child)
                    {
                        if (canB[0] == -1)
                        {
                            return new int[2] { edges[i, 0], edges[i, 1] };
                        }

                        return canB;
                    }

                    parents[child] = father;
                }
            }

            return canA;
        }

        //Union Find Helper
        int FindRoot(int[] parents, int i)
        {
            while (parents[i] != i)
            {
                parents[i] = parents[parents[i]];
                i = parents[i];
            }

            return i;
        }

        public List<List<string>> AccountsMerge(IList<IList<string>> accounts)
        {
            List<List<string>> res = new List<List<string>>();

            if (accounts == null || accounts.Count == 0)
            {
                return res;
            }

            int[] parent = new int[accounts.Count];

            for (int i = 0; i < parent.Length; i++)
            {
                parent[i] = -1;
            }

            Dictionary<string, int> dct = new Dictionary<string, int>();
            for (int i = 0; i < accounts.Count; i++)
            {
                IList<string> ls = accounts[i];

                for (int j = 1; j < ls.Count; j++)
                {
                    string email = ls[j];

                    if (dct.ContainsKey(email))
                    {
                        int pre_id = dct[email];
                        int cur_id = i;
                        int parent_pre_id = findParent(parent, pre_id);
                        int parent_curr_id = findParent(parent, cur_id);

                        if (parent_pre_id != parent_curr_id)
                        {
                            parent[parent_curr_id] = parent_pre_id;
                        }
                    }
                    else
                    {
                        dct.Add(email, i);
                    }
                }
            }

            Dictionary<int, HashSet<string>> dt = new Dictionary<int, HashSet<string>>();

            for (int i = 0; i < parent.Length; i++)
            {
                int index = findParent(parent, i);

                if (!dt.ContainsKey(index))
                {
                    dt.Add(index, new HashSet<string>());
                }

                HashSet<string> hs = new HashSet<string>();

                for (int j = 1; j < accounts[i].Count; j++)
                {
                    hs.Add(accounts[i][j]);
                }

                foreach (string st in hs)
                {
                    dt[index].Add(st);
                }
            }

            foreach (int id in dt.Keys)
            {
                res.Add(new List<string>());
                res[res.Count - 1].Add(accounts[id][0]);

                List<string> emails = new List<string>(dt[id]);
                emails.Sort(
                        (String left, String right) =>
                        {
                            return String.CompareOrdinal(left, right);
                        });
                res[res.Count - 1].AddRange(emails);
            }

            return res;
        }

        int findParent(int[] parent, int i)
        {
            while (parent[i] != -1)
            {
                i = parent[i];
            }

            return i;
        }

        public int MinSwapsCouples(int[] row)
        {
            if (row == null || row.Length == 0 || row.Length % 2 != 0)
            {
                return 0;
            }

            Dictionary<int, int> dct = new Dictionary<int, int>();
            int n = row.Length;
            int res = 0;

            for (int i = 0; i < n; i += 2)
            {
                dct.Add(row[i], row[i + 1]);
                dct.Add(row[i + 1], row[i]);
            }

            int index = 0;

            while (index < n)
            {
                if (dct[index] != index + 1)
                {
                    int nextValue = dct[index + 1];
                    int currVaule = dct[index];
                    dct[index] = index + 1;
                    dct[nextValue] = currVaule;
                    dct[currVaule] = nextValue;
                    res++;
                }

                index += 2;
            }

            return res;
        }

        public int[] HitBricks(int[][] grid, int[][] hits)
        {
            if(grid==null||hits==null||grid.Length==0||hits.Length==0)
            {
                return null;
            }

            int hitLen = hits.GetLength(0);
            int rows = grid.GetLength(0);
            int cols = grid[0].Length;

            for(int i=0;i<hitLen;i++)
            {
                if(grid[hits[i][0]][hits[i][1]]==1)
                {
                    grid[hits[i][0]][hits[i][1]] = 0;
                }
                else
                {
                    grid[hits[i][0]][hits[i][1]] = -1;
                }
            }

            for(int i=0;i<cols;i++)
            {
                GridDFS(grid, 0, i);
            }

            int[] res = new int[hitLen];

            for(int i=hitLen-1;i>=0;i--)
            {
                if(grid[hits[i][0]][hits[i][1]]!=-1)
                {
                    grid[hits[i][0]][hits[i][1]] = 1;

                    if(IsConnectedTop(grid, hits[i][0], hits[i][1]))
                    {
                        res[i] = GridDFS(grid, hits[i][0], hits[i][1]);
                    }
                }
            }

            return res;
        }

        int GridDFS(int[][] grid,int row,int col)
        {
            int m = grid.GetLength(0), n = grid[0].Length;
            int ret = 0;

            if(row<0||row>=m||col<0||col>=n||grid[row][col]!=1)
            {
                return ret;
            }

            grid[row][col] = 2;

            ret += GridDFS(grid, row - 1, col);
            ret += GridDFS(grid, row + 1, col);
            ret += GridDFS(grid, row, col-1);
            ret += GridDFS(grid, row, col+1);

            return ret;
        }

        bool IsConnectedTop(int[][] grid,int row,int col)
        {
            int m = grid.GetLength(0), n = grid[0].Length;

            if ((row-1>=0&&grid[row-1][col]==2)||
                (row+1<m&&grid[row+1][col]==2)||
                (col - 1 >= 0 && grid[row][col-1] == 2) ||
                (col + 1 <n&& grid[row][col+1] == 2)||
                row==0)
            {
                return true;
            }

            return false;
        }
    }
}
