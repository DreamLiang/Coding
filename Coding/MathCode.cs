using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding
{
    class MathCode
    {
        public int ConsecutiveNumbersSum(int N)
        {
            int count = 0;
            int divdend = N;
            int divsor = 1;

            while(divsor<=divdend)
            {
                if(divdend%divsor==0)
                {
                    count++;
                }

                divdend -= divsor;
                divsor++;
            }

            return count;
        }

        public int ConsecutiveNumbersSum1(int N)
        {
            int count = 1;
            int upcap =(int) Math.Sqrt(2 * N);

            for(int i=2;i<=upcap;i++)
            {
                if(i%2==0 && 2*N%i==0 && 2*N/i%2==1)
                {
                    count++;
                }
                else if(i%2==1 && N%i==0)
                {
                    count++;
                }
            }

            return count;
        }
    }
}
