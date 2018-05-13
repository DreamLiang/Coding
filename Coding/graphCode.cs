using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding
{
    class graphCode
    {
        public UndirectedGraphNode CloneGraph(UndirectedGraphNode node)
        {
            if(node==null)
            {
                return node;
            }

            UndirectedGraphNode newNode = new UndirectedGraphNode(node.label);
            Dictionary<UndirectedGraphNode, UndirectedGraphNode> dct = new Dictionary<UndirectedGraphNode, UndirectedGraphNode>();
            dct.Add(node, newNode);
            Queue<UndirectedGraphNode> q = new Queue<UndirectedGraphNode>();
            q.Enqueue(node);

            while(q.Count!=0)
            {
                UndirectedGraphNode nd = q.Dequeue();

                foreach(UndirectedGraphNode unode in nd.neighbors)
                {
                    if(!dct.ContainsKey(unode))
                    {
                        q.Enqueue(unode);
                        UndirectedGraphNode newUnode = new UndirectedGraphNode(unode.label);
                        dct.Add(unode, newUnode);
                    }

                    dct[nd].neighbors.Add(dct[unode]);
                }
            }

            return newNode;
        }

        public UndirectedGraphNode CloneHelper(UndirectedGraphNode node, Dictionary<UndirectedGraphNode,UndirectedGraphNode> dct)
        {
            if(node==null)
            {
                return node;
            }

            if(dct.ContainsKey(node))
            {
                return dct[node];
            }

            UndirectedGraphNode newNode = new UndirectedGraphNode(node.label);
            dct.Add(node, newNode);

            foreach(UndirectedGraphNode unode in node.neighbors)
            {
                newNode.neighbors.Add(CloneHelper(unode, dct));
            }

            return newNode;
        }

        public IList<int> FindMinHeightTrees(int n, int[,] edges)
        {
            if(n==1)
            {
                IList<int> res = new List<int>();
                res.Add(0);
                return res;
            }

            Dictionary<int, List<int>> dct = new Dictionary<int, List<int>>();

            for(int i=0;i<n;i++)
            {
                dct.Add(i, new List<int>());
            }

            for(int i=0;i<edges.GetLength(0);i++)
            {
                dct[edges[i, 0]].Add(edges[i, 1]);
                dct[edges[i, 1]].Add(edges[i, 0]);
            }

            List<int> leafList = new List<int>();

            foreach(int key in dct.Keys)
            {
                if(dct[key].Count==1)
                {
                    leafList.Add(key);
                }
            }

            while(n>2)
            {
                n -= leafList.Count();
                List<int> newLeafList = new List<int>();

                foreach(int leaf in leafList)
                {
                    int k = dct[leaf][0];
                    dct[k].Remove(leaf);

                    if(dct[k].Count==1)
                    {
                        newLeafList.Add(k);
                    }
                }

                leafList = newLeafList;
            }

            return leafList;
        }

        public double[] CalcEquation(string[,] equations, double[] values, string[,] queries)
        {
            if(equations==null||values==null||queries==null||equations.Length==0||values.Length==0||queries.Length==0)
            {
                return (new double[0]);
            }

            int n = queries.GetLength(0);
            double[] res = new double[n];
            Dictionary<string, List<string>> strDct = new Dictionary<string, List<string>>();
            Dictionary<string, List<double>> valDct = new Dictionary<string, List<double>>();

            for(int i=0;i<equations.GetLength(0);i++)
            {
                string str1 = equations[i, 0];
                string str2 = equations[i, 1];

                if(!strDct.ContainsKey(str1))
                {
                    strDct.Add(str1, new List<string>());
                    valDct.Add(str1, new List<double>());
                }

                if(!strDct.ContainsKey(str2))
                {
                    strDct.Add(str2, new List<string>());
                    valDct.Add(str2, new List<double>());
                }

                strDct[str1].Add(str2);
                strDct[str2].Add(str1);
                valDct[str1].Add(values[i]);
                valDct[str2].Add(1 / values[i]);
            }

            for(int i=0;i<n;i++)
            {
                res[i] = DFSHelper(queries[i, 0], queries[i, 1], strDct, valDct, new HashSet<string>(), 1.0);

                if (res[i] == 0.0)
                {
                    res[i] = -1;
                }
            }

            return res;
        }

        double DFSHelper(string start, string end, Dictionary<string, List<string>> strDct, Dictionary<string,List<double>> valuesDct,HashSet<string> hs, double value)
        {
            if(hs.Contains(start))
            {
                return 0.0;
            }

            if(!strDct.ContainsKey(start))
            {
                return 0.0;
            }

            if(start.Equals(end))
            {
                return value;
            }

            hs.Add(start);

            List<string> strList = strDct[start];
            List<double> valueList = valuesDct[start];
            double tmp = 0.0;
            for (int i=0;i<strList.Count;i++)
            {
                 tmp= DFSHelper(strList[i], end, strDct, valuesDct, hs, value * valueList[i]);

                if(tmp!=0.0)
                {
                    break;
                }
            }

            hs.Remove(start);
            return tmp;
        }

        public int ScheduleCourse(int[,] courses)
        {
            if(courses==null||courses.Length==0)
            {
                return 0;
            }

            int m = courses.GetLength(0);
            List<int[]> ls = new List<int[]>();
            
            for(int i=0;i<m;i++)
            {
                ls.Add(new int[] { courses[i, 0], courses[i, 1] });
            }

            ls.Sort((a, b) => (a[0] - b[0]));

            int time = 0;
            List<int> list = new List<int>();

            foreach(var item in ls)
            {
                time += item[0];
                list.Add(item[0]);
                list.Sort();

                if(time>item[1])
                {
                    time -= list.Last();
                    list.RemoveAt(list.Count - 1);
                }
            }

            return list.Count;
        }

        public UndirectedGraphNode CloneGraph1(UndirectedGraphNode node)
        {
            if(node==null)
            {
                return null;
            }

            Dictionary<UndirectedGraphNode, UndirectedGraphNode> dct = new Dictionary<UndirectedGraphNode, UndirectedGraphNode>();
            Queue<UndirectedGraphNode> q = new Queue<UndirectedGraphNode>();

            dct.Add(node, new UndirectedGraphNode(node.label));
            q.Enqueue(node);

            while(q.Count!=0)
            {
                UndirectedGraphNode gn = q.Dequeue();

                foreach(UndirectedGraphNode g in gn.neighbors)
                {
                    if(!dct.ContainsKey(g))
                    {
                        dct.Add(g, new UndirectedGraphNode(g.label));
                        q.Enqueue(g);
                    }

                    dct[gn].neighbors.Add(dct[g]);
                }
            }

            return dct[node];
        }

        public int[] FindRedundantConnection(int[,] edges)
        {
            if(edges==null||edges.Length==0)
            {
                return new int[0];
            }

            int m = edges.GetLength(0);
            Dictionary<int, HashSet<int>> dct = new Dictionary<int, HashSet<int>>();
            

            for(int i=0;i<m;i++)
            {
                int from = edges[i, 0], to = edges[i, 1];
                Queue<int> q = new Queue<int>();
                HashSet<int> hs = new HashSet<int>();

                q.Enqueue(from);
                hs.Add(to);

                
            }

            return new int[0];
        }
    }

    public class UndirectedGraphNode
    {
       public int label;
       public IList<UndirectedGraphNode> neighbors;
       public UndirectedGraphNode(int x) { label = x; neighbors = new List<UndirectedGraphNode>(); }
   }
}
