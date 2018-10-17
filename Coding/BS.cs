using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding
{
    class BS
    {
        public int MinEatingSpeed(int[] piles, int H)
        {
            if(piles==null||piles.Length==0)
            {
                return 0;
            }

            if(piles.Length>H)
            {
                return -1;
            }

            int n = piles.Length;
            int max = piles[0];

            for(int i=1;i<n;i++)
            {
                max = Math.Max(max, piles[i]);
            }

            
            int left = 1, right = max;

            while(left<=right)
            {
                int total = 0;
                int mid = left + (right - left) / 2;

                foreach (int pile in piles)
                {
                    if(pile<=mid)
                    {
                        total += 1;
                    }
                    else if(pile%mid==0)
                    {
                        total += pile / mid;
                    }
                    else
                    {
                        total += pile / mid + 1;
                    }
                }

                if(total<=H)
                {
                    right = mid - 1;
                }
                else
                {
                    left = mid + 1;
                }
            }

            return left;
        }
    }
}
