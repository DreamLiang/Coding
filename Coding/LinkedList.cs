using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding
{
    class LinkedList
    {
        public bool IsPalindrome(ListNode head)
        {
            if(head==null||head.next==null)
            {
                return true;
            }

            ListNode slow = head, fast = head;

            while(fast.next!=null&&fast.next.next!=null)
            {
                slow = slow.next;
                fast = fast.next.next;
            }

            ListNode secondHead = slow.next;
            slow.next = null;

            ListNode p = secondHead, pnext = p.next;
            p.next = null;

            while(pnext!=null)
            {
                ListNode temp = pnext.next;
                pnext.next = p;
                p = pnext;
                pnext = temp;
            }

            ListNode p1 = head, p2 = p;

            while(p2!=null)
            {
                if(p1.val!=p2.val)
                {
                    return false;
                }

                p1 = p1.next;
                p2 = p2.next;
            }

            return true;
        }
    }

    public class ListNode
    {
      public int val;
      public ListNode next;
      public ListNode(int x) { val = x; }
    }
}
