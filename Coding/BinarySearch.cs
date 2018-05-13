using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding
{
    class BinarySearch
    {
        public IList<int> FindClosestElements(IList<int> arr, int k, int x)
        {
            List<int> lessResult = new List<int>(), greatReSult = new List<int>();
            List<int> less = new List<int>(), great = new List<int>();

            foreach(int num in arr)
            {
                if(num<x)
                {
                    less.Add(num);
                }
                else
                {
                    great.Add(num);
                }
            }

            //less.Reverse();

            int i = less.Count - 1, j = 0;

            for(int s=0;s<k;s++)
            {
                if(i>=0&&j<great.Count)
                {
                    if(Math.Abs(less[i]-x)>Math.Abs(great[j]-x))
                    {
                        greatReSult.Add(great[j]);
                        j++;
                    }
                    else
                    {
                        lessResult.Add(less[i]);
                        i--;
                    }
                }
                else if(i>=0)
                {
                    lessResult.Add(less[i]);
                    i--;
                }
                else if(j<great.Count)
                {
                    greatReSult.Add(great[j]);
                    j++;
                }
            }

            lessResult.Reverse();
            lessResult.AddRange(greatReSult);

            return lessResult;
        }

    }
}
