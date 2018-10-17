using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding
{
    class StackCode
    {
        public int CalPoints(string[] ops)
        {
            if(ops==null||ops.Length==0)
            {
                return 0;
            }

            Stack<int> st = new Stack<int>();
            int n = ops.Length;
            int res = 0;

            for(int i=0;i<n;i++)
            {
                if(ops[i]=="C")
                {
                    res -= st.Pop();
                }
                else if(ops[i]=="D")
                {
                    int t = st.Peek();
                    res += 2 * t;
                    st.Push(2 * t);
                }
                else if(ops[i]=="+")
                {
                    int n1 = st.Pop();
                    int n2 = st.Peek();
                    st.Push(n1);
                    st.Push(n1 + n2);
                    res += n1 + n2;
                }
                else
                {
                    int num = int.Parse(ops[i]);
                    res += num;
                    st.Push(num);
                }
            }

            return res;
        }
    }
}
