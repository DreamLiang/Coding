using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Coding
{
    class Program
    {
        static void Main(string[] args)
        {
            //if(CheckBeforeProceeding(args))
            //{
            //    string[] files = Directory.GetFiles(args[0]);
            //    List<string> filesums = new List<string>();

            //    foreach (string file in files)
            //        filesums.Add(GetFileSum(file));

            //    List<string> dupes = SearchForDupes(filesums);
            //    PrintDupes(filesums, dupes, files);
            //}
            //Program PC = new Program();
            //ThreadStart TS = new ThreadStart(PC.PrintLiveDgClock);
            //Thread t = new Thread(TS);
            //t.Start();
            //Console.ReadLine();
            //string str="";

            //FileStream st = File.Open(str, FileMode.Open);

            //int[] d = { 10, 5, 2, 6 };

            //dfs.NumSubarrayProductLessThanK1(d, 100);

            //Console.WriteLine(DP.MinWindow1("cnhczmccqouqadqtmjjzl", "cm"));
            string strs = "wreorttvosuidhrxvmvo";
            int[] indexs = {14, 12, 10, 5, 0, 18 };
            string[] sources = { "rxv", "dh", "ui", "ttv", "wreor", "vo" };
            string[] targets = { "frs", "c", "ql", "qpir", "gwbeve", "n" };

            Console.WriteLine(StringCode.FindReplaceString(strs, indexs, sources,targets));
            Console.ReadLine();
        }

        static void PrintDupes(List<string> sums, List<string> dupes, string[] files)
        {
            foreach (string dupe in dupes)
            {
                Console.WriteLine("{0}\n----------", dupe);

                for (int i = 0; i <= (files.Length - 1); i++)
                    if (sums[i] == dupe)
                        Console.WriteLine(files[i]);

                Console.WriteLine();
            }
        }

        static List<string> SearchForDupes(List<string> sums)
        {
            List<string> dupes = new List<string>();

            for (int i = 0; i <= (sums.Count - 2); i++)
                for (int j = (i + 1); j <= (sums.Count - 2); j++)
                    if (sums[i] == sums[j])
                        if (!dupes.Contains(sums[i]))
                            dupes.Add(sums[i]);

            return dupes;
        }

        static bool CheckBeforeProceeding(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Error: No directory provided");
                return false;
            }

            if (!Directory.Exists(args[0]))
            {
                Console.WriteLine("Error: '{0}' is not a valid directory", args[0]);
                return false;
            }

            if (Directory.GetFiles(args[0]).Length == 0)
            {
                Console.WriteLine("Error: '{0}' does not contain any files", args[0]);
                return false;
            }

            if (Directory.GetFiles(args[0]).Length == 1)
            {
                Console.WriteLine("Error: '{0}' only contains 1 file", args[0]);
                return false;
            }

            return true;
        }

        static string GetFileSum(string file)
        {
            using (var sum = MD5.Create())
            using (var stream = File.OpenRead(file))
                return BitConverter.ToString(sum.ComputeHash(stream)).Replace("-", "").ToLower();
        }

        static int CountIslandsNum(int[,] arr)
        {
            if(arr==null||arr.Length==0)
            {
                return 0;
            }

            int rowsLen = arr.GetLength(0);
            int columnsLen = arr.GetLength(1);
            int count = 0;

            for(int i=0;i<rowsLen;i++)
            {
                for(int j=0;j<columnsLen;j++)
                {
                    if(arr[i,j]==1)
                    {
                        DFSHelper(arr, i, j,rowsLen,columnsLen);
                        count++;
                    }
                }
            }

            return count;
        }

       static void DFSHelper(int[,] arr, int rowIndex, int columnIndex, int rowLen,int columnLen)
        {
            if(rowIndex<0||rowIndex>=rowLen||columnIndex<0||columnIndex>=columnLen||arr[rowIndex,columnIndex]==0)
            {
                return;
            }

            arr[rowIndex, columnIndex] = 0;

            DFSHelper(arr, rowIndex - 1, columnIndex, rowLen, columnLen);
            DFSHelper(arr, rowIndex + 1, columnIndex, rowLen, columnLen);
            DFSHelper(arr, rowIndex, columnIndex-1, rowLen, columnLen);
            DFSHelper(arr, rowIndex, columnIndex + 1, rowLen, columnLen);
            DFSHelper(arr, rowIndex-1, columnIndex - 1, rowLen, columnLen);
            DFSHelper(arr, rowIndex+1, columnIndex +1, rowLen, columnLen);
            DFSHelper(arr, rowIndex-1, columnIndex + 1, rowLen, columnLen);
            DFSHelper(arr, rowIndex+1, columnIndex - 1, rowLen, columnLen);
        }

        MinMax FindMinMax(int[] arr)
        {
            MinMax res = new MinMax();
            int min = arr[0];
            int max = arr[0];

            for(int i=0;i<arr.Length;i++)
            {
                if(arr[i]<min)
                {
                    min = arr[i];
                }
                else if(arr[i]>max)
                {
                    max = arr[i];
                }
            }

            res.Min = min;
            res.Max = max;

            return res;
        }

        void PrintLiveDgClock()
        {
            for (;;)
            {
                string hour = DateTime.Now.Hour.ToString();
                string min = DateTime.Now.Minute.ToString();
                string sec = DateTime.Now.Second.ToString();

                Console.WriteLine(hour+":"+min+":"+sec);
                Console.WriteLine("\a");
                Thread.Sleep(1000);
                Console.Clear();
            }
        }
    }

    class MinMax
    {
        public int Min;
        public int Max;
    }

    public class MagicDictionary
    {
        Dictionary<string, int> dct;

        /** Initialize your data structure here. */
        public MagicDictionary()
        {
            dct = new Dictionary<string, int>();
        }

        /** Build a dictionary through a list of words */
        public void BuildDict(string[] dict)
        {
            foreach(string str in dict)
            {
                if(!dct.Keys.Contains(str))
                {
                    dct.Add(str, str.Length);
                }
            }
        }

        /** Returns if there is any word in the trie that equals to the given word after modifying exactly one character */
        public bool Search(string word)
        {
            if(string.IsNullOrEmpty(word))
            {
                return false;
            }

            int len = word.Length;

            if(!dct.Values.Contains(len))
            {
                return false;
            }

            foreach(string str in dct.Keys)
            {
                if(str.Length==word.Length)
                {
                    int count = 0;

                    for(int i=0;i<str.Length;i++)
                    {
                        if(str[i]==word[i])
                        {
                            count++;
                        }
                    }

                    if(count==len-1)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }

    struct Statistics
    {
        bool valid;        // true only if mean and max fields are valid    
        double mean;
        double max;
    }; 

    //    class StatisticsGenerator
    //{
    //    InputStream istr;

    //    StatisticsGenerator(string filePath)
    //    {
    //        istr= new InputStream(filePath);
    //    }
    //    public bool HasNext()
    //    {

    //    }
    //    // Get the current statistics.
    //    static KeyValuePair<Statistics, Statistics> GetNext()
    //    {

    //    }
    //}

    class InputStream
    {
        FileStream fs;
        public InputStream(string path)
        {
            fs = File.Open(path, FileMode.Open);
        }

        public bool HasNext()
        {
            if(fs.Position<fs.Length)
            {
                string str = fs.Seek(fs.Position, SeekOrigin.Begin).ToString();

                if(!str.Equals("None"))
                {
                    return true;
                }
            }

            return false;
        }

        public double GetNext()
        {
            if (HasNext())
            {
                double d = double.Parse(fs.Seek(fs.Position, SeekOrigin.Begin).ToString());
                fs.Position++;

                return d;
            }

            return double.NaN;
        }
    }

    public class MaxAverage
    {
        public double avg;
        public double max;
    }

    public class RollingMaxAverage
    {
        Queue<double> queue;
        int smallWindows,bigWindows;
        MaxAverage res;
        InputStream istr;
        public RollingMaxAverage(int smallWindows, int bigWindows, string filePath)
        {
            this.queue = new Queue<double>();
            this.smallWindows = smallWindows;
            this.bigWindows = bigWindows;
            istr = new InputStream(filePath);
        }

        MaxAverage next(InputStream istr)
        {
            double val = istr.GetNext();

            if(val==double.NaN)
            {
                return null;
            }

            double avg;
            double max=int.MinValue;

            queue.Enqueue(val);

            if (queue.Count> this.bigWindows)
            {
                int sum = 0;

                foreach (int i in queue)
                {
                    sum += i;
                    max = Math.Max(i, max);
                }

                avg = (double)sum / this.bigWindows;
                res = new MaxAverage();
                res.avg = avg;
                res.max = max;

                return res;
            }
            else if (queue.Count > this.smallWindows)
            {
                int sum = 0;

                foreach (int i in queue)
                {
                    sum += i;
                    max = Math.Max(i, max);
                }

                avg = (double)sum / this.smallWindows;
                res = new MaxAverage();
                res.avg = avg;
                res.max = max;

                return res;
            }
            else
            {
                return null;
            }
        }
    }
}
