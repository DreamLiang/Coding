using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding
{
    class HeapCode
    {
        public void heapify(int[] A)
        {
            if(A==null||A.Length<2)
            {
                return;
            }

            int len = A.Length;

            for(int i=len/2-1;i>=0;i--)
            {
                int k = i;

                while(k<len)
                {
                    int minIndex = k;

                    if(2*k+1<len&&A[2*k+1]<A[minIndex])
                    {
                        minIndex=2*k + 1;
                    }

                    if (2 * k + 2 < len && A[2 * k + 2] < A[minIndex])
                    {
                        minIndex = 2 * k + 2;
                    }

                    if(minIndex==k)
                    {
                        break;
                    }

                    int tmp = A[k];
                    A[k] = A[minIndex];
                    A[minIndex] = tmp;

                    k = minIndex;
                }
            }
        }
    }
}
