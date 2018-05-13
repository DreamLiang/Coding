using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding
{
    class Interval
    {
        public int start;
        public int end;
        public Interval() { start = 0; end = 0; }
        public Interval(int s, int e) { start = s; end = e; }

        public int EraseOverlapIntervals(Interval[] intervals)
        {
            if(intervals==null||intervals.Length==0)
            {
                return 0;
            }

            Array.Sort(intervals, (x, y) => x.end.CompareTo(y.end));

            int end = intervals[0].end;
            int count = 1;

            for(int i=1;i<intervals.Length;i++)
            {
                if(intervals[i].start>=end)
                {
                    end = intervals[i].end;
                    count++;
                }
            }

            return intervals.Length - count;
        }
}

    public class MyCalendar
    {
        List<Interval> res;
        public MyCalendar()
        {
            res = new List<Interval>();
        }

        public bool Book(int start, int end)
        {
            Interval newInterval = new Interval(start, end);
            if (res.Count == 0)
            {
                res.Add(newInterval);
                return true;
            }

            bool bookable = true;
            
            for (int i = 0; i < res.Count; i++)
            {
                if(end<=res[i].start||start>=res[i].end)
                {
                    continue;
                }
                else
                {
                    bookable = false;
                    break;
                }
            }

            if(bookable)
            res.Add(newInterval);

            return bookable;
        }
    }

    public class MyCalendarTwo
    {
        List<Interval> res;
        public MyCalendarTwo()
        {
            res = new List<Interval>();
        }

        public bool Book(int start, int end)
        {
            Interval newInterval = new Interval(start, end);
            if (res.Count <2)
            {
                res.Add(newInterval);
                return true;
            }

            List<Interval> commList = new List<Interval>();
            for (int i = 0; i < res.Count; i++)
            {
                if (end <= res[i].start || start >= res[i].end)
                {
                    continue;
                }
                else
                {
                    commList.Add(new Interval(Math.Max(start, res[i].start), Math.Min(end, res[i].end)));
                }
            }

            for(int i=0;i<commList.Count-1;i++)
            {
                for(int j=i+1;j< commList.Count;j++)
                {
                    if(commList[j].end<=commList[i].start||commList[j].start>=commList[i].end)
                    {
                        continue;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            res.Add(newInterval);

            return true;
        }
    }
}
