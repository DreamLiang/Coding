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

        public int PrimePalindrome(int N)
        {
            if(N>=8&&N<=11)
            {
                return 11;
            }

            for(int i=1;i<100000;i++)
            {
                string s = i.ToString();
                string r = new string(s.Reverse().ToArray()).Substring(1);
                int y = int.Parse(s + r);

                if(y>=N&&isPrime(y))
                {
                    return y;
                }
            }

            return -1;
        }

        bool IsPrimePalindrome(int N)
        {
            if(!isPrime(N))
            {
                return false;
            }

            List<int> ls = new List<int>();

            while(N>0)
            {
                ls.Add(N % 10);
                N /= 10;
            }

            int t = ls.Count-1;

            for(int i=0;i<=t/2;i++)
            {
                if(ls[i]!=ls[t-i])
                {
                    return false;
                }
            }

            return true;
        }

        bool isPrime(int n)
        {
            if(n<2)
            {
                return false;
            }

            if(n%2==0)
            {
                return n==2;
            }

            for(int i=2;i*i<=n;i+=2)
            {
                if(n%i==0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
