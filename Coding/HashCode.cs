using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding
{
    class HashCode
    {
        public int hashCode(char[] key, int HASH_SIZE)
        {
            if(key==null||key.Length==0)
            {
                return 0;
            }

            long num = 0;

            for(int i=0;i<key.Length;i++)
            {
                num = num * 33 + key[i];
                num %= HASH_SIZE;
            }

            return (int)num;
        }

        public int FindPairs(int[] nums, int k)
        {
            if(nums==null||nums.Length==0)
            {
                return 0;
            }

            Dictionary<int, int> dct = new Dictionary<int, int>();
            int count = 0;

            foreach(int num in nums)
            {
                if(dct.ContainsKey(num))
                {
                    dct[num]++;
                }
                else
                {
                    dct.Add(num, 1);
                }
            }

            foreach(int num in dct.Keys)
            {
                if(k==0)
                {
                    if(dct[num]>1)
                    {
                        count++;
                    }
                }
                else
                {
                    if(dct.ContainsKey(num+k))
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        public int LongestPalindrome(string s)
        {
            if(string.IsNullOrEmpty(s))
            {
                return 0;
            }

            if(s.Length==1)
            {
                return 1;
            }

            Dictionary<char, int> dct = new Dictionary<char, int>();
            int len = 0;
            bool isSingle = false;

            foreach(char c in s)
            {
                if(dct.ContainsKey(c))
                {
                    dct[c]++;
                }
                else
                {
                    dct.Add(c, 1);
                }
            }

            foreach(char c in dct.Keys)
            {
                if (dct[c] % 2 == 0)
                {
                    len += dct[c];
                }
                else
                {
                    len += dct[c] / 2 * 2;
                    isSingle = true;
                }
            }

            if(isSingle)
            {
                len++;
            }

            return len;
        }

        public string[] FindRestaurant(string[] list1, string[] list2)
        {
            List<string> res = new List<string>();

            if(list1==null||list1.Length==0||list2==null||list2.Length==0)
            {
                return new string[0];
            }

            Dictionary<string, int> dct = new Dictionary<string, int>();
            int index = int.MaxValue;

            for(int i=0;i<list1.Length;i++)
            {
                if(!dct.ContainsKey(list1[i]))
                {
                    dct.Add(list1[i], i);
                }
            }

            for(int j=0;j<list2.Length;j++)
            {
                if(dct.ContainsKey(list2[j]))
                {
                    int sum = j + dct[list2[j]];

                    if(sum<=index)
                    {
                        if(sum<index)
                        {
                            index = sum;
                            res.Clear();
                        }

                        res.Add(list2[j]);
                    }         
                }
            }

            return res.ToArray();
        }

        public bool IsHappy(int n)
        {
            if(n<=0)
            {
                return false;
            }

            int s = 0;
            HashSet<int> hs = new HashSet<int>();

            while(hs.Add(n))
            {
                while(n>0)
                {
                    s += (n % 10) * (n % 10);
                    n /= 10;
                }

                n = s;

                if(n==1)
                {
                    return true;
                }

                s = 0;
            }

            return false;
        }

        public int[] FindErrorNums(int[] nums)
        {
            if(nums==null||nums.Length<2)
            {
                return null;
            }

            int[] res = new int[2];
            HashSet<int> hs = new HashSet<int>();
            int i = 1;
            int s = 0;
            foreach(int num in nums)
            {
                if (!hs.Contains(num))
                {
                    hs.Add(num);
                    s += i - num;
                }
                else
                {
                    res[0] = num;
                    s += i;
                }

                i++;
            }

            res[1] = s;

            return res;
        }

        public RandomListNode CopyRandomList(RandomListNode head)
        {
            if(head==null)
            {
                return head;
            }

            RandomListNode p = head;
            Dictionary<RandomListNode, RandomListNode> dct = new Dictionary<RandomListNode, RandomListNode>();

            while(p!=null)
            {
                dct.Add(p, new RandomListNode(p.label));
                p = p.next;
            }
            
            p = head;

            while(p!=null)
            {
                dct[p].next = p.next!=null? dct[p.next]:null;
                dct[p].random = p.random != null ? dct[p.random] : null;
                p = p.next;
            }

            return dct[head];
        }

        public RandomListNode CopyRandomList1(RandomListNode head)
        {

            if (head == null)
            {
                return head;
            }

            RandomListNode p = head;

            while (p != null)
            {
                RandomListNode pnext = p.next;
                RandomListNode next = new RandomListNode(p.label);
                p.next = next;
                next.next = pnext;
                p = pnext;
            }

            p = head;

            while (p != null)
            {
                RandomListNode pnext = p.next;

                if (p.random != null)
                {
                    pnext.random = p.random.next;
                }

                p = pnext.next;
            }

            RandomListNode newHead = head.next;
            p = head;

            while (p != null)
            {
                RandomListNode pnext = p.next;
                p.next = pnext.next;
                p = p.next;

                if (p != null)
                {
                    pnext.next = p.next;
                }
            }

            return newHead;
        }
        public int MaxProfit(int[] prices)
        {
            if(prices==null||prices.Length==0)
            {
                return 0;
            }

            int n = prices.Length;
            int sell1 = 0, buy1 = int.MinValue, sell2 = 0, buy2 = int.MinValue;
            
            for(int i=0;i<n;i++)
            {
                buy1 = Math.Max(buy1, -prices[i]);
                sell1 = Math.Max(sell1, buy1 + prices[i]);
                buy2 = Math.Max(buy2, sell1 - prices[i]);
                sell2 = Math.Max(sell2, buy2 + prices[i]);
            }

            return sell2;
        }

        public int MaxProfit(int[] prices, int fee)
        {
            if (prices == null || prices.Length == 0)
            {
                return 0;
            }

            int min = prices[0];
            int maxProfit = 0;

            for(int i=1;i<prices.Length;i++)
            {
                if(prices[i]-min>fee)
                {
                    maxProfit += prices[i] - min - fee;
                    min = prices[i];
                }
                else if(prices[i]<min)
                {
                    min = prices[i];
                }
            }

            return maxProfit;
        }

        public int MaxProfit1(int[] prices, int fee)
        {
            if (prices == null || prices.Length == 0)
            {
                return 0;
            }

            int min = prices[0];
            int maxProfit = 0;
            int max1 = 0;
            int min1 = prices[0];
            int d = 0;

            for (int i = 1; i < prices.Length; i++)
            {
                if (prices[i] - min > fee&&(i==prices.Length-1||prices[i]>prices[i+1]))
                {
                    maxProfit += prices[i] - min - fee;
                    min = prices[i];
                }
                else 
                {
                    if (prices[i] < min)
                    {
                        min = prices[i];
                    } 
                }

                if(min1>prices[i])
                {
                    min1 = prices[i];
                    d = i;
                }

                if(i>d&&max1<prices[i])
                {
                    max1 = prices[i];
                }
            }
            maxProfit = Math.Max(maxProfit, max1 - min1);

            return maxProfit;
        }

        public int canJump(List<int> A)
        {
            if(A==null||A.Count==1)
            {
                return 1;
            }

            int steps = 0;
            int i = 0;

            while(i<=steps&&i<A.Count)
            {
                steps =Math.Max(steps, i + A[i]);

                if(steps>=A.Count-1)
                {
                    return 1;
                }

                i++;
            }

            return 0;
        }
    }

    public class RandomListNode
    {
      public int label;
      public RandomListNode next, random;
      public RandomListNode(int x) { this.label = x; }
  };
}
