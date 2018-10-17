using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding
{
    class Tire
    {
        private TireNode root;

        public Tire()
        {
            root = new TireNode();
        }

        public IList<string> FindAllConcatenatedWordsInADict(string[] words)
        {
            IList<string> res = new List<string>();

            if(words==null||words.Length==0)
            {
                return res;
            }

            TireNode root = new TireNode();

            foreach (string word in words)
            {
                if(word.Length>0)
                {
                    AddWord(root, word);
                }
            }

            foreach(string word in words)
            {
                if(word.Length>0)
                {
                    if(CheckWord(root,0,0,word))
                    {
                        res.Add(word);
                    }
                }
            }

            return res;
        }

        bool CheckWord(TireNode root,int index,int count,string word)
        {
            if(index==word.Length&&count>1)
            {
                return true;
            }

            TireNode cur = root;
            int n = word.Length;

            for(int i=index;i<n;i++)
            {
                if (cur.next[word[i] - 'a'] == null)
                {
                    return false;
                }
                
                if(cur.next[word[i] - 'a'].isWord)
                {
                    if(CheckWord(root,i+1,count+1,word))
                    {
                        return true;
                    }
                }

                cur = cur.next[word[i] - 'a'];
            }

            return false;
        }

        void TestWord(TireNode root, int index, string word,List<string> ls,IList<string> res)
        {
            if(index==word.Length)
            {
                StringBuilder sb = new StringBuilder();
                foreach(string s in ls)
                {
                    sb.Append(s + " ");
                }

                string str = sb.ToString().TrimEnd();

                if(!res.Contains(str))
                {
                    res.Add(str);
                }

                return;
            }

            TireNode cur = root;
            int n = word.Length;

            for (int i = index; i < n; i++)
            {
                if (cur.next[word[i] - 'a'] == null)
                {
                    return;
                }

                if (cur.next[word[i] - 'a'].isWord)
                {
                    string ss = word.Substring(index, i - index + 1);
                    ls.Add(ss);
                    TestWord(root, i + 1, word, ls, res);
                    ls.RemoveAt(ls.Count - 1);
                }

                cur = cur.next[word[i] - 'a'];
            }
        }

        public IList<string> WordBreak(string s, IList<string> wordDict)
        {
            IList<string> res = new List<string>();

            if (string.IsNullOrEmpty(s) || wordDict.Count == 0)
            {
                return res;
            }


            TireNode root = new TireNode();

            foreach (string word in wordDict)
            {
                if (word.Length > 0)
                {
                    AddWord(root, word);
                }
            }
            List<string> ls = new List<string>();
            TestWord(root, 0, s, ls, res);

            return res;
        }

        void AddWord(TireNode root, string str)
        {
            TireNode node = root;

            for(int i=0;i<str.Length;i++)
            {
                int index = str[i] - 'a';

                if(node.next[index]==null)
                {
                    node.next[index] = new TireNode();
                }

                node = node.next[index];
            }

            node.isWord = true;
        }

        public void insert(string word)
        {
            TireNode node = root;

            for(int i=0;i<word.Length;i++)
            {
                int index = word[i] - 'a';

                if (node.next[index]==null)
                {
                    node.next[index] = new TireNode();
                }

                node = node.next[index];
            }

            node.isWord = true;
        }

        string GetShortestPrefix(string str)
        {
            TireNode node = root;

            for(int i=0;i<str.Length;i++)
            {
                int index = str[i] - 'a';

                if(node!=null&&node.isWord)
                {
                    return str.Substring(0, i + 1);
                }

                if(node.next[index]==null)
                {
                    break;
                }
                else
                {
                    node = node.next[index];
                }
            }

            return str;
        }

        public string ReplaceWords(IList<string> dict, string sentence)
        {
            if(dict.Count==0||sentence.Length==0)
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();
            Tire t = new Tire();

            foreach(string word in dict)
            {
                t.insert(word);
            }

            string[] strs = sentence.Split(' ');

            foreach(string str in  strs)
            {
                sb.Append(t.GetShortestPrefix(str)).Append(" ");
            }

            return sb.ToString().Trim();
        }
    }

    class TireNode
    {
        public TireNode[] next;
        public bool isWord;

        public TireNode()
        {
            next = new TireNode[26];
        }
    }
}
