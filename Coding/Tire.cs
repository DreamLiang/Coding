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
