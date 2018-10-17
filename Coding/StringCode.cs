using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding
{
    class StringCode
    {
        public bool CheckRecord(string s)
        {
            if(string.IsNullOrEmpty(s))
            {
                return false;
            }

            int countA = 0, countL = 0;

            for(int i=0;i<s.Length;i++)
            {
                if(s[i]=='A')
                {
                    countA++;
                    countL = 0;
                }
                else if(s[i]=='L')
                {
                    countL++;
                }
                else
                {
                    countL = 0;
                }

                if(countA>1||countL>2)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool CheckInclusion(string s1, string s2)
        {
            if(string.IsNullOrEmpty(s1))
            {
                return true;
            }

            if(string.IsNullOrEmpty(s2)||s1.Length>s2.Length)
            {
                return false;
            }

            int[] count = new int[26];
            int[] window = new int[26];

            for(int i=0;i<s1.Length;i++)
            {
                count[s1[i] - 'a']++;
            }

            for(int i=0;i<s2.Length;i++)
            {
                if(i<s1.Length)
                {
                    window[s2[i] - 'a']++;
                }
                else
                {
                    window[s2[i] - 'a']++;
                    window[s2[i-s1.Length] - 'a']--;
                }

                bool isEqual = true;

                for (int j = 0; j < 26; j++)
                {
                    if (count[j] != window[j])
                    {
                        isEqual = false;
                        break;
                    }
                }

                if(isEqual)
                {
                    return true;
                }
            }

            return false;
        }

        public static string DecodeString(string s)
        {
            Stack<int> countStack = new Stack<int>();
            Stack<string> strStack = new Stack<string>();

            string res="";
            int i = 0;

            while(i<s.Length)
            {
                if(s[i]>='0'&&s[i]<='9')
                {
                    int n = 0;
                    while(i<s.Length&& s[i] >= '0' && s[i] <= '9')
                    {
                        n = n * 10 + s[i] - '0';
                        i++;
                    }

                    countStack.Push(n);
                }
                else if(s[i]>='a'&&s[i]<='z')
                {
                    string str = "";
                    while (i < s.Length && s[i] >= 'a' && s[i] <= 'z')
                    {
                        str += s[i];
                        i++;
                    }

                    strStack.Push(str);
                }
                else if(s[i]=='[')
                {
                    strStack.Push(res);
                    res = "";
                    i++;
                }
                else if(s[i]==']')
                {
                    string temp = strStack.Pop();
                    int count = countStack.Pop();
                    res = temp;
                    for(int j=0;j<count;j++)
                    {
                        res+= temp;
                    }
                    i++;
                }
                else
                {
                    i++;
                }
            }

            return res;
        }

        public bool WordBreak(string s, IList<string> wordDict)
        {
            if(string.IsNullOrEmpty(s))
            {
                return false;
            }

            int len = s.Length;
            bool[] dp = new bool[len+1];
            dp[0] = true;
            for(int i=0;i<len;i++)
            {
                for(int j=i+1;j<=len;j++)
                {
                    if(dp[i]&&wordDict.Contains(s.Substring(i,j-i)))
                    {
                        dp[j] = true;
                    }
                }

                if(dp[len]==true)
                {
                    return true;
                }
            }

            return dp[len];
        }

        public IList<string> WordBreak1(string s, IList<string> wordDict)
        {
            IList<string> res = new List<string>();

            if(string.IsNullOrEmpty(s)||wordDict.Count==0)
            {
                return res;
            }
            Dictionary<string, List<string>> dct = new Dictionary<string, List<string>>();

            res = doWordBreaker(s, wordDict, dct);

            return res;
        }

        void wordBreakHelper(string s, IList<string> resList, List<string> ls, IList<string> wordDict,int start)
        {
            if(start==s.Length)
            {
                StringBuilder sb = new StringBuilder();

                for(int i=0;i<ls.Count;i++)
                {
                    sb.Append(ls[i]);

                    if(i!=ls.Count-1)
                    {
                        sb.Append(" ");
                    }
                }

                resList.Add(sb.ToString());
                return;
            }

            for(int i=start+1; i<=s.Length;i++)
            {
                string str = s.Substring(start, i - start);

                if(wordDict.Contains(str))
                {
                    ls.Add(str);
                    wordBreakHelper(s, resList, ls, wordDict, i);
                    ls.Remove(str);
                }
            }
        }

        List<string> doWordBreaker(string s, IList<string> wordDict,Dictionary<string,List<string>> dct)
        {
            if(dct.ContainsKey(s))
            {
                return dct[s];
            }

            List<string> res = new List<string>();

            if(s.Length==0)
            {
                res.Add("");
                return res;
            }

            foreach(string word in wordDict)
            {
                if(s.StartsWith(word))
                {
                    List<string> subList = doWordBreaker(s.Substring(word.Length), wordDict, dct);

                    foreach(string substr in subList)
                    {
                        res.Add(word + (substr.Length == 0 ? "" : " ") + substr);
                    }
                }
            }

            dct.Add(s, res);

            return res;
        }

        public int LadderLength(string beginWord, string endWord, IList<string> wordList)
        {
            if(beginWord==null||endWord==null||beginWord.Length!=endWord.Length)
            {
                return 0;
            }

            if(beginWord.Equals(endWord))
            {
                return 1;
            }

            HashSet<string> dct = new HashSet<string>();
            foreach(string word in wordList)
            {
                dct.Add(word);
            }

            Queue<string> sq = new Queue<string>();
            HashSet<string> hs = new HashSet<string>();
            int len = 1;
            sq.Enqueue(beginWord);
            hs.Add(beginWord);

            while(sq.Count!=0)
            {
                len++;
                int size = sq.Count;

                for(int i=0;i<size;i++)
                {
                    string word = sq.Dequeue();

                    foreach(string s in getNextWord(word,dct))
                    {
                        if(s.Equals(endWord))
                        {
                            return len;
                        }

                        if(!hs.Contains(s))
                        {
                            hs.Add(s);
                            sq.Enqueue(s);
                        }
                    }
                }
            }

            return 0;
        }

        public List<string> getNextWord(string str, HashSet<string> hs)
        {
            List<string> ls = new List<string>();

            for(int i=0;i<str.Length;i++)
            {
                for(char c='a';c<='z';c++)
                {
                    if(str[i]!=c)
                    {
                        char[] ch = str.ToCharArray();
                        ch[i] = c;
                        string newStr = new string(ch);
                        
                        if(hs.Contains(newStr))
                        {
                            ls.Add(newStr);
                        }
                    }
                }
            }

            return ls;
        }

        public int LadderLength1(string beginWord, string endWord, IList<string> wordList)
        {

            if (beginWord == null || endWord == null || beginWord.Length != endWord.Length)
            {
                return 0;
            }

            HashSet<string> dct = new HashSet<string>();
            foreach(string s in wordList)
            {
                dct.Add(s);
            }

            Queue<string> sq = new Queue<string>();
            Queue<int> dq = new Queue<int>();
            sq.Enqueue(beginWord);
            dq.Enqueue(1);

            while (sq.Count != 0)
            {
                string tmp = sq.Dequeue();
                int currDistance = dq.Dequeue();

                if (tmp.Equals(endWord))
                {
                    return currDistance;
                }

                for (int i = 0; i < tmp.Length; i++)
                {
                    for (char c = 'a'; c <= 'z'; c++)
                    {
                        if (tmp[i] != c)
                        {
                            char[] ch = tmp.ToCharArray();
                            ch[i] = c;
                            string newStr = new string(ch);

                            if (dct.Contains(newStr))
                            {
                                sq.Enqueue(newStr);
                                dq.Enqueue(currDistance + 1);
                                dct.Remove(newStr);
                            }
                        }
                    }
                }
            }

            return 0;
        }


        public string ValidIPAddress(string IP)
        {
            if(isValidIPV4(IP))
            {
                return "IPv4";
            }
            else if(isValidIPV6(IP))
            {
                return "IPv6";
            }
            else
            {
                return "Neither";
            }
        }

        bool isValidIPV4(string IP)
        {
            if(IP.Length<7)
            {
                return false;
            }

            if(IP[0]=='.'||IP[IP.Length-1]=='.')
            {
                return false;
            }

            string[] tokens = IP.Split('.');

            if(tokens.Length!=4)
            {
                return false;
            }

            foreach(string token in tokens)
            {
                if(!isValidIPV4Token(token))
                {
                    return false;
                }
            }

            return true;
        }

        bool isValidIPV4Token(string str)
        {
            if(str.StartsWith("0")&&str.Length>1)
            {
                return false;
            }

            try
            {
                int pInt = int.Parse(str);

                if(pInt<0||pInt>255)
                {
                    return false;
                }

                if(pInt==0&&str[0]!='0')
                {
                    return false;
                }
            }
            catch(FormatException )
            {
                return false;
            }
            catch(OverflowException)
            {
                return false;
            }

            return true;
        }

        bool isValidIPV6(string IP)
        {
            if(IP.Length<15)
            {
                return false;
            }

            if (IP[0] == ':' || IP[IP.Length - 1] == ':')
            {
                return false;
            }

            string[] tokens = IP.Split(':');

            if (tokens.Length != 8)
            {
                return false;
            }

            foreach (string token in tokens)
            {
                if (!isValidIPV6Token(token))
                {
                    return false;
                }
            }

            return true;
        }

        bool isValidIPV6Token(string token)
        {
            if(token.Length>4||token.Length==0)
            {
                return false;
            }

            char[] chars = token.ToCharArray();

            foreach(char ch in chars)
            {
                bool isDigit = (ch >= '0' && ch <= '9');
                bool isUpperCase = (ch >= 'A' && ch <= 'F');
                bool isLowerCase = (ch >= 'a' && ch <= 'f');

                if(!(isDigit||isUpperCase||isLowerCase))
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsSubsequence(string s, string t)
        {
            if(string.IsNullOrEmpty(s)||string.IsNullOrEmpty(t)||s.Length>t.Length)
            {
                return false;
            }

            Dictionary<char, List<int>> dct = new Dictionary<char, List<int>>();

            for(int i=0;i<t.Length;i++)
            {
                if(!dct.ContainsKey(t[i]))
                {
                    dct.Add(t[i], new List<int>());
                }

                dct[t[i]].Add(i);
            }
            int index = -1;
            foreach(char c in s)
            {
                if(dct.ContainsKey(c))
                {
                    if(dct[c].Count==0)
                    {
                        return false;
                    }

                    int curr = dct[c].First();

                    if(curr<=index)
                    {
                        return false;
                    }

                    index = curr;
                    dct[c].RemoveAt(0);
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsValid(string s)
        {
            if(string.IsNullOrEmpty(s))
            {
                return false;
            }

            Stack<char> st = new Stack<char>();
            int i = 0;

            while(i<s.Length)
            {
                if(s[i]=='('||s[i]=='['||s[i]=='{')
                {
                    st.Push(s[i]);
                }
                else if(s[i]==')')
                {
                    if(st.Count==0||st.Peek()!='(')
                    {
                        return false;
                    }

                    st.Pop();
                }
                else if(s[i]==']')
                {
                    if(st.Count == 0 || st.Peek()!='[')
                    {
                        return false;
                    }

                    st.Pop();
                }
                else if(s[i]=='}')
                {
                    if(st.Count == 0 || st.Peek()!='{')
                    {
                        return false;
                    }

                    st.Pop();
                }
                else
                {
                    return false;
                }

                i++;
            }
            
            if(st.Count==0&&i==s.Length)
            {
                return true;
            }

            return false;
        }

        public int LongestValidParentheses(string s)
        {
            if(s==null||s.Length<2)
            {
                return 0;
            }

            int max = 0;
            Stack<int> st = new Stack<int>();
            int last = -1;

            for(int i=0;i<s.Length;i++)
            {
                if(s[i]=='(')
                {
                    st.Push(i);
                }
                else if(st.Count==0)
                {
                    last = i;
                }
                else
                {
                    st.Pop();

                    if(st.Count==0)
                    {
                        max = Math.Max(max, i - last);
                    }
                    else
                    {
                        max = Math.Max(max, i - st.Peek());
                    }
                }
            }

            return max;
        }

        public string Convert(string s, int numRows)
        {
            if(string.IsNullOrEmpty(s)||numRows<=1)
            {
                return s;
            }

            StringBuilder sb = new StringBuilder();
            int cycle = 2 * numRows - 2;

            for(int i=0;i< numRows; i++)
            {
                for(int j=i;j<s.Length;j+=cycle)
                {
                    sb.Append(s[j]);
                    int second = (j - i) + cycle - i;

                    if(i!=0&&i!=numRows-1&&second<s.Length)
                    {
                        sb.Append(s[second]);
                    }
                }
            }

            return sb.ToString();
        }

        public int CountSubstrings(string s)
        {
            if(string.IsNullOrEmpty(s))
            {
                return 0;
            }

            int n = s.Length;
            int[,] dp = new int[n, n];
            int count = 0;

            for(int i=n-1;i>=0;i--)
            {
                dp[i, i] = 1;
                count++;
                for(int j=i+1;j<n;j++)
                {
                    if(s[j]==s[i]&&(j-i<2||dp[i+1,j-1]==1))
                    {
                        dp[i, j] = 1;
                        count++;
                    }
                }
            }

            return count;
        }

        public string PredictPartyVictory(string senate)
        {
            if(string.IsNullOrEmpty(senate))
            {
                return string.Empty;
            }

            int countR = 0, countD = 0;

            for(int i=0;i<senate.Length;i++)
            {
                if(senate[i]=='R')
                {
                    countR++;
                }

                if(senate[i]=='D')
                {
                    countD++;
                }
            }

            if(countD>countR)
            {
                return "Dire";
            }
            else if(countR>countD)
            {
                return "Radiant";
            }
            else
            {
                if(senate[senate.Length-1]=='R')
                {
                    return "Dire";
                }
                else
                {
                    return "Radiant";
                }
            }
        }

        public string Multiply(string num1, string num2)
        {
            if(string.IsNullOrEmpty(num1)||string.IsNullOrEmpty(num2))
            {
                return string.Empty;
            }

            int m1 = num1.Length, m2 = num2.Length;
            int[] arr = new int[m1 + m2];

            for(int i=m1-1;i>=0;i--)
            {
                for(int j=m2-1;j>=0;j--)
                {
                    int num = (num1[i] - '0') * (num2[j] - '0');
                    int p1 = i + j, p2 = i + j + 1;
                    int sum = num + arr[p2];

                    arr[p1] += sum / 10;
                    arr[p2] = sum % 10;
                }
            }

            StringBuilder sb = new StringBuilder();

            foreach(int a in arr)
            {
                if (sb.Length == 0 && a == 0)
                    continue;

                sb.Append(a);
            }

            return sb.Length==0?"0":sb.ToString();
        }

        public static int LengthOfLongestSubstring(string s)
        {
            if(string.IsNullOrEmpty(s))
            {
                return 0;
            }

            int start = -1;
            int maxLen = 1;
            Dictionary<char, int> dct = new Dictionary<char, int>();

            for(int i=0;i<s.Length;i++)
            {
                if(!dct.ContainsKey(s[i]))
                {
                    dct.Add(s[i], i);
                }
                else
                {
                    start = Math.Max(dct[s[i]],start);
                    dct[s[i]] = i;
                }

                if (i - start > maxLen)
                {
                    maxLen = i - start;
                }
            }

            return maxLen;
        }

        public string LongestPalindrome(string s)
        {
            if(string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            int n = s.Length;
            bool[,] dp = new bool[n, n];
            string res = s.Substring(n-1);

            dp[n - 1, n - 1] = true;

            for(int i=n-2;i>=0;i--)
            {
                dp[i, i] = true;

                for(int j=i+1;j<n;j++)
                {
                    if(s[i]==s[j]&&(j-i<3||dp[i+1,j-1]))
                    {
                        dp[i, j] = true;
                    }

                    if(dp[i,j]&&j-i+1>res.Length)
                    {
                        res = s.Substring(i, j - i + 1);
                    }
                }
            }

            return res;
        }

        public int LongestPalindromeSubseq(string s)
        {
            if(string.IsNullOrEmpty(s))
            {
                return 0;
            }

            int n = s.Length;
            int[,] dp = new int[n, n];
            dp[n - 1, n - 1] = 1;

            for(int i=n-2;i>=0;i--)
            {
                dp[i, i] = 1;

                for(int j=i+1;j<n;j++)
                {
                    if(s[i]==s[j])
                    {
                        dp[i, j] = dp[i + 1, j - 1] + 2;
                    }
                    else
                    {
                        dp[i, j] = Math.Max(dp[i + 1, j], dp[i, j - 1]);
                    }
                }
            }

            return dp[0, n - 1];
        }

        public int CountPalindromicSubsequences(string S)
        {
            if(string.IsNullOrEmpty(S))
            {
                return 0;
            }

            int n = S.Length;
            int[,] dp = new int[n, n];
            int count = 1;

            for(int i=n-1;i>=0;i--)
            {
                dp[i, i] = 1;
                count++;

                for(int j=i+1;j<n;j++)
                {
                    if(S[i]==S[j])
                    {
                        dp[i, j] = dp[i + 1, j - 1] + 1;
                        count += dp[i, j];
                    }
                }
            }

            return count;
        }

        public bool IsIsomorphic(string s, string t)
        {
            if((s==null&&t==null)||(s.Length==0&&t.Length==0))
            {
                return true;
            }

            if(s==null||t==null||s.Length==0||t.Length==0||s.Length!=t.Length)
            {
                return false;
            }

            int n = s.Length;

            Dictionary<char, char> dct = new Dictionary<char, char>();

            for(int i=0;i<n;i++)
            {
                if(!dct.ContainsKey(s[i]))
                {
                    if(dct.ContainsValue(t[i]))
                    {
                        return false;
                    }

                    dct.Add(s[i], t[i]);
                }
                else
                {
                    if(dct[s[i]]!=t[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public string CustomSortString(string S, string T)
        {
            if(string.IsNullOrEmpty(S)||string.IsNullOrEmpty(T)||S.Length>T.Length)
            {
                return string.Empty;
            }

            int[] counts = new int[26];
            StringBuilder sb = new StringBuilder();
            
            foreach(char c in T)
            {
                counts[c - 'a']++;
            }

            foreach(char c in S)
            {
                while(counts[c-'a']>0)
                {
                    sb.Append(c);
                    counts[c - 'a']--;
                }
            }

            for(char c='a';c<='z';c++)
            {
                while (counts[c - 'a'] > 0)
                {
                    sb.Append(c);
                    counts[c - 'a']--;
                }
            }

            return sb.ToString();
        }

        public IList<IList<int>> LargeGroupPositions(string S)
        {
            IList<IList<int>> res = new List<IList<int>>();

            if(string.IsNullOrEmpty(S))
            {
                return res;
            }

            int start = 0;
            int end = -1;

            for(int i=1;i<=S.Length;i++)
            {
                if(i==S.Length||S[i]!=S[start])
                {
                    end = i;

                    if(end-start>=3)
                    {
                        List<int> ls = new List<int>();
                        ls.Add(start);
                        ls.Add(end - 1);

                        res.Add(new List<int>(ls));
                    }

                    start = i;
                }
            }

            return res;
        }

        public static string MaskPII(string S)
        {
            if(string.IsNullOrEmpty(S))
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();

            if(S.IndexOf('@')>0)
            {
                int index = S.IndexOf('@');
                S = S.ToLower();

                sb.Append(S[0]);
                sb.Append("*****");
                sb.Append(S[index - 1]);
                sb.Append(S.Substring(index));
            }
            else
            {
                int len = 0;
                StringBuilder sb1 = new StringBuilder();

                for(int i=0;i<S.Length;i++)
                {
                    if(S[i]>='0'&&S[i]<='9')
                    {
                        sb1.Append(S[i]);
                        len++;
                    }
                }

                string str = sb1.ToString();

                if(len==10)
                {
                    sb.Append("***-***-");
                    sb.Append(str.Substring(6));
                }
                else 
                {
                    if (len == 11)
                    {
                        sb.Append("+*-***-***-");
                    }
                    else if (len==12)
                    {
                        sb.Append("+**-***-***-");
                    }
                    else
                    {
                        sb.Append("+***-***-***-");
                    }

                    sb.Append(str.Substring(len-4));
                }
            }

            return sb.ToString();
        }

        public static string FindReplaceString(string S, int[] indexes, string[] sources, string[] targets)
        {
            int n = indexes.Length;
            int start = 0;
            StringBuilder sb = new StringBuilder();
            SortedDictionary<int, Dictionary<string, string>> dct = new SortedDictionary<int, Dictionary<string, string>>();

            for(int i=0;i<n;i++)
            {
                dct.Add(indexes[i], new Dictionary<string, string>());
                dct[indexes[i]].Add(sources[i], targets[i]);
            }
            
            dct.OrderBy(v => v.Key);

            foreach(int key in dct.Keys)
            {
                string sourceStr = dct[key].First().Key;

                if (sourceStr.Equals(S.Substring(key,sourceStr.Length)))
                {
                    if (key > start)
                    {
                        sb.Append(S.Substring(start, key - start));
                    }

                    sb.Append(dct[key].First().Value);
                    start = key+ sourceStr.Length;
                }
            }

            if(S.Length>start)
            {
                sb.Append(S.Substring(start));
            }

            return sb.ToString();
        }

        public IList<string> WordSubsets(string[] A, string[] B)
        {
            IList<string> res = new List<string>();
            int i;
            int[] c = new int[26];
            int[] tmp;

            foreach(string s in B)
            {
                tmp = Counter(s);

                for(i=0;i<26;i++)
                {
                    c[i] = Math.Max(c[i], tmp[i]);
                }
            }

            foreach(string s in A)
            {
                tmp = Counter(s);

                for(i=0;i<26;i++)
                {
                    if(tmp[i]<c[i])
                    {
                        break;
                    }
                }

                if(i==26)
                {
                    res.Add(s);
                }
            }

            return res;
        }

        int[] Counter(string s)
        {
            int[] arr = new int[26];

            foreach(char c in s)
            {
                arr[c - 'a']++;
            }

            return arr;
        }

        public IList<string> FindAndReplacePattern(string[] words, string pattern)
        {
            IList<string> res = new List<string>();

            int n = words.Length;
            Dictionary<char, List<int>> pDct = new Dictionary<char, List<int>>();

            for(int i=0;i<pattern.Length;i++)
            {
                if(!pDct.ContainsKey(pattern[i]))
                {
                    pDct.Add(pattern[i], new List<int>());
                }

                pDct[pattern[i]].Add(i);
            }

            pDct.OrderBy(k => k.Value.Count);

            for(int i=0;i<n;i++)
            {
                if (words[i].Length == pattern.Length)
                {
                    Dictionary<char, List<int>> dct = new Dictionary<char, List<int>>();

                    for (int j = 0; j < words[i].Length; j++)
                    {
                        if (!dct.ContainsKey(words[i][j]))
                        {
                            dct.Add(words[i][j], new List<int>());
                        }

                        dct[words[i][j]].Add(j);
                    }

                    dct.OrderBy(k => k.Value.Count);

                    if (dct.Keys.Count == pDct.Keys.Count)
                    {
                        List<List<int>> ls = dct.Values.ToList();
                        List<List<int>> pl = pDct.Values.ToList();
                        int j = 0;
                        for (; j < ls.Count; j++)
                        {
                            ls[j].Sort();
                            pl[j].Sort();

                            if (ls[j].Count!=pl[j].Count||!ls[j].SequenceEqual(pl[j]))
                            {
                                break;
                            }
                        }

                        if (j == ls.Count)
                        {
                            res.Add(words[i]);
                        }
                    }
                }
            }

            return res;
        }

        public string ReverseOnlyLetters(string S)
        {
            if(string.IsNullOrEmpty(S)||S.Length==1)
            {
                return S;
            }

            string des;
            int n = S.Length;
            char[] cc = S.ToCharArray();
            int i = 0, j = n - 1;
            
            while(i<j)
            {
                while(i<j&&!IsLetter(cc[i]))
                {
                    i++;
                }

                while(i<j&&!IsLetter(cc[j]))
                {
                    j--;
                }

                if (i < j)
                {
                    char temp = cc[i];
                    cc[i] = cc[j];
                    cc[j] = temp;
                }

                i++;
                j--;
            }

            des = new string(cc);

            return des;
        }

        bool IsLetter(char c)
        {
            if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z'))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
