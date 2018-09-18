using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding
{
    class Backtracking
    {
        public int ScoreOfParentheses(string S)
        {
            if(string.IsNullOrEmpty(S))
            {
                return 0;
            }

            Stack<int> s = new Stack<int>();

            for(int i=0;i<S.Length;i++)
            {
                if(S[i]=='(')
                {
                    s.Push(-1);
                }
                else
                {
                    int cur = 0;

                    while(s.Peek()!=-1)
                    {
                        cur += s.Pop();
                    }

                    s.Pop();

                    if(cur==0)
                    {
                        s.Push(1);
                    }
                    else
                    {
                        s.Push(2 * cur);
                    }
                }
            }

            int res = 0;

            while(s.Count>0)
            {
                res += s.Pop();
            }

            return res;
        }


        public IList<IList<int>> AllPathsSourceTarget(int[][] graph)
        {
            IList<IList<int>> res = new List<IList<int>>();

            if(graph==null||graph.Length==0|| graph[0].Length==0)
            {
                return res;
            }

            int end = graph.GetLength(0) - 1;
            List<int> ls = new List<int>();
            ls.Add(0);

            AllPathsSourceTargetHelper(graph, ls, res, end);

            return res;
        }

        void AllPathsSourceTargetHelper(int[][] graph,List<int> ls, IList<IList<int>> res,int end)
        {
            if(ls.Last()==end)
            {
                res.Add(new List<int>(ls));
                return;
            }

            int n = ls.Last();

            for (int i=0;i<graph[n].Length;i++)
            {
                ls.Add(graph[n][i]);

                AllPathsSourceTargetHelper(graph, ls, res, end);

                ls.RemoveAt(ls.Count - 1);
            }

        }

        public string OptimalDivision(int[] nums)
        {
            if(nums==null||nums.Length==0)
            {
                return string.Empty;
            }

            string ans = nums[0].ToString();

            if(nums.Length==1)
            {
                return ans;
            }

            if(nums.Length==2)
            {
                return ans + "/" + nums[1].ToString();
            }

            ans += "/(" + nums[1].ToString();

            for(int i=2;i<nums.Length;i++)
            {
                ans += "/" + nums[i].ToString();
            }

            ans += ")";

            return ans;
        }
    }
}
