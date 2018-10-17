using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding
{
    class BiTree
    {
        class Node
        {
            public Node left, right;
            public int val, sum, dup = 1;

            public Node(int v, int s)
            {
                val = v;
                sum = s;
            }
        }

        public int KthSmallest(TreeNode root, int k)
        {
            if (root == null || k <= 0)
            {
                return int.MinValue;
            }
            Stack<TreeNode> s = new Stack<TreeNode>();
            AddLeft(root, s);

            while (s.Count != 0)
            {
                TreeNode node = s.Pop();
                k--;

                if (k == 0)
                {
                    return node.val;
                }

                if (node.right != null)
                {
                    AddLeft(node.right, s);
                }
            }

            return int.MaxValue;
        }

        void AddLeft(TreeNode root, Stack<TreeNode> s)
        {
            while (root != null)
            {
                s.Push(root);
                root = root.left;
            }
        }

        public IList<TreeNode> GenerateTrees(int n)
        {
            IList<TreeNode> resList = new List<TreeNode>();

            if (n <= 0)
            {
                return resList;
            }

            List<TreeNode>[] res = new List<TreeNode>[n + 1];
            res[0] = new List<TreeNode>();
            res[0].Add(null);

            for (int i = 1; i <= n; i++)
            {
                res[i] = new List<TreeNode>();

                for (int j = 0; j < i; j++)
                {
                    foreach (TreeNode leftNode in res[j])
                    {
                        foreach (TreeNode rightNode in res[i - j - 1])
                        {
                            TreeNode node = new TreeNode(j + 1);
                            node.left = leftNode;
                            node.right = CloneTreeNode(rightNode, j + 1);
                            res[i].Add(node);
                        }
                    }
                }
            }

            return res[n];
        }

        TreeNode CloneTreeNode(TreeNode node, int offSet)
        {
            if (node == null)
            {
                return null;
            }

            TreeNode newNode = new TreeNode(node.val + offSet);
            newNode.left = CloneTreeNode(node.left, offSet);
            newNode.right = CloneTreeNode(node.right, offSet);
            return newNode;
        }

        public int DiameterOfBinaryTree(TreeNode root)
        {
            int[] diameter = new int[1];

            MaxHeight(root, diameter);

            return diameter[0];
        }

        int MaxHeight(TreeNode root, int[] diameter)
        {
            if (root == null)
            {
                return 0;
            }

            int left = MaxHeight(root.left, diameter);
            int right = MaxHeight(root.right, diameter);

            diameter[0] = Math.Max(diameter[0], left + right);

            return 1 + Math.Max(left, right);
        }

        public string serialize(TreeNode root)
        {
            StringBuilder sb = new StringBuilder();
            if (root == null) return sb.ToString();
            preorder(root, sb);
            return sb.ToString();
        }
        private void preorder(TreeNode root, StringBuilder sb)
        {
            if (root == null) return;
            sb.Append(root.val).Append("#");
            preorder(root.left, sb);
            preorder(root.right, sb);
        }

        // Decodes your encoded data to tree.
        public TreeNode deserialize(string data)
        {
            if (string.IsNullOrEmpty(data)) return null;
            string[] arr = data.Split('#');
            return buildTree(arr, 0, arr.Length - 1);
        }
        private TreeNode buildTree(string[] arr, int l, int r)
        {
            if (l > r) return null;
            TreeNode root = new TreeNode(int.Parse(arr[l]));
            int splitIndex = findIndex(arr, int.Parse(arr[l]), l + 1, r);
            root.left = buildTree(arr, l + 1, splitIndex - 1);
            root.right = buildTree(arr, splitIndex, r);
            return root;
        }
        private int findIndex(string[] arr, int target, int l, int r)
        {
            int i = l;
            for (; i <= r; i++)
            {
                if (i < arr.Length && int.Parse(arr[i]) > target)
                {
                    break;
                }
                else if (i >= arr.Length && int.Parse(arr[arr.Length - 1]) > target)
                {
                    break;
                }
            }
            return i;
        }

        public bool IsSubtree(TreeNode s, TreeNode t)
        {
            if (s == null && t == null)
            {
                return true;
            }

            if (s == null)
            {
                return false;
            }

            return IsSameTree(s, t) || IsSubtree(s.left, t) || IsSubtree(s.right, t);
        }

        bool IsSameTree(TreeNode s, TreeNode t)
        {
            if (s == null && t == null)
            {
                return true;
            }

            if (s == null || t == null)
            {
                return false;
            }

            if (s.val != t.val)
            {
                return false;
            }

            return IsSameTree(s.left, t.left) && IsSameTree(s.right, t.right);
        }

        bool IsSubtree1(TreeNode s, TreeNode t)
        {
            return Serialize(s).Contains(Serialize(t));
        }

        string Serialize(TreeNode s)
        {
            StringBuilder sb = new StringBuilder();
            DoSerialization(s, sb);
            return sb.ToString();
        }

        void DoSerialization(TreeNode s, StringBuilder sb)
        {
            if (s == null)
            {
                sb.Append(",#");
                return;
            }

            sb.Append("," + s.val);
            DoSerialization(s.left, sb);
            DoSerialization(s.right, sb);
        }

        public int FindTilt(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }

            if (root.left == null && root.right == null)
            {
                return 0;
            }
            int res = 0;
            findTiltHelper(root, ref res);

            return res;
        }

        int findTiltHelper(TreeNode root, ref int sum)
        {
            if (root == null)
            {
                return 0;
            }


            int left = findTiltHelper(root.left, ref sum);
            int right = findTiltHelper(root.right, ref sum);
            sum += Math.Abs(left - right);

            return left + right + root.val;
        }

        public TreeNode ConvertBST(TreeNode root)
        {
            if (root == null || (root.left == null && root.right == null))
            {
                return root;
            }

            int res = 0;
            convertHelper(root, ref res);
            return root;
        }

        void convertHelper(TreeNode curr, ref int sum)
        {
            if (curr == null)
            {
                return;
            }

            convertHelper(curr.right, ref sum);
            curr.val += sum;
            sum = curr.val;
            convertHelper(curr.left, ref sum);
        }

        public IList<int> LargestValues_bfs(TreeNode root)
        {
            IList<int> res = new List<int>();

            if (root == null)
            {
                return res;
            }

            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(root);

            while (q.Count != 0)
            {
                int size = q.Count;
                int max = int.MinValue;
                for (int i = 0; i < size; i++)
                {
                    TreeNode node = q.Dequeue();

                    if (node.left != null)
                    {
                        q.Enqueue(node.left);
                    }

                    if (node.right != null)
                    {
                        q.Enqueue(node.right);
                    }

                    if (node.val > max)
                    {
                        max = node.val;
                    }
                }

                res.Add(max);
            }

            return res;
        }

        public IList<int> LargestValues_dfs(TreeNode root)
        {
            IList<int> res = new List<int>();

            if (root == null)
            {
                return res;
            }

            lvDfsHelper(root, 0, res);
            return res;
        }

        void lvDfsHelper(TreeNode root, int depth, IList<int> ls)
        {
            if (root == null)
            {
                return;
            }

            if (ls.Count == depth)
            {
                ls.Add(root.val);
            }
            else
            {
                ls[depth] = Math.Max(ls[depth], root.val);
            }

            lvDfsHelper(root.left, depth + 1, ls);
            lvDfsHelper(root.right, depth + 1, ls);
        }

        public int[] FindFrequentTreeSum(TreeNode root)
        {
            if (root == null)
            {
                return new int[0];
            }

            Dictionary<int, int> dct = new Dictionary<int, int>();

            fftsHelper(root, dct);
            int max = 0;
            List<int> ls = new List<int>();

            foreach (int count in dct.Values)
            {
                max = Math.Max(count, max);
            }

            foreach (int key in dct.Keys)
            {
                if (dct[key] == max)
                {
                    ls.Add(key);
                }
            }

            return ls.ToArray();
        }

        int fftsHelper(TreeNode curr, Dictionary<int, int> dct)
        {
            if (curr == null)
            {
                return 0;
            }

            int leftSum = fftsHelper(curr.left, dct);
            int rightSum = fftsHelper(curr.right, dct);

            int sum = leftSum + rightSum + curr.val;

            if (dct.ContainsKey(sum))
            {
                dct[sum]++;
            }
            else
            {
                dct.Add(sum, 1);
            }

            return sum;
        }

        public int PathSum(TreeNode root, int sum)
        {
            if (root == null)
            {
                return 0;
            }

            int count = 0;
            PSHelper(root, sum, ref count);
            count += PathSum(root.left, sum);
            count += PathSum(root.right, sum);

            return count;
        }

        void PSHelper(TreeNode root, int sum, ref int count)
        {
            if (root == null)
            {
                return;
            }

            if (sum == root.val)
            {
                count++;
            }

            PSHelper(root.left, sum - root.val, ref count);
            PSHelper(root.right, sum - root.val, ref count);
        }

        public int PathSum1(TreeNode root, int sum)
        {
            if (root == null)
            {
                return 0;
            }

            Dictionary<int, int> dct = new Dictionary<int, int>();
            dct.Add(0, 1);
            return PSHelper(root, sum, 0, dct);
        }

        int PSHelper(TreeNode root, int target, int currSum, Dictionary<int, int> preSum)
        {
            if (root == null)
            {
                return 0;
            }

            currSum += root.val;
            int res = 0;

            if (preSum.ContainsKey(currSum - target))
            {
                res += preSum[currSum - target];
            }

            if (preSum.ContainsKey(currSum))
            {
                preSum[currSum]++;
            }
            else
            {
                preSum.Add(currSum, 1);
            }

            res += PSHelper(root.left, target, currSum, preSum) + PSHelper(root.right, target, currSum, preSum);

            preSum[currSum]--;

            return res;
        }

        public TreeNode MergeTrees(TreeNode t1, TreeNode t2)
        {
            if (t1 == null)
            {
                return t2;
            }

            if (t2 == null)
            {
                return t1;
            }

            TreeNode root = new TreeNode(t1.val + t2.val);
            root.left = MergeTrees(t1.left, t2.left);
            root.right = MergeTrees(t1.right, t2.right);

            return root;
        }

        public int CountNodes(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }

            int count = 0;

            int left = leftHeight(root);
            int right = rightHeight(root);

            if (left == right)
            {
                count = (1 << left) - 1;
            }
            else
            {
                count = 1 + CountNodes(root.left) + CountNodes(root.right);
            }

            return count;
        }

        int leftHeight(TreeNode root)
        {
            int h = 0;

            while (root != null)
            {
                h++;
                root = root.left;
            }

            return h;
        }

        int rightHeight(TreeNode root)
        {
            int h = 0;

            while (root != null)
            {
                h++;
                root = root.right;
            }

            return h;
        }

        public string Tree2str(TreeNode t)
        {
            if (t == null)
            {
                return "";
            }

            string result = t.val + "";

            string left = Tree2str(t.left);
            string right = Tree2str(t.right);

            if (left == "" && right == "")
            {
                return result;
            }
            else if (right == "")
            {
                return result + "(" + left + ")";
            }
            else if (left == "")
            {
                return result + "()" + "(" + right + ")";
            }

            return result + "(" + left + ")" + "(" + right + ")";
        }

        public TreeNode AddOneRow(TreeNode root, int v, int d)
        {
            if (d == 1)
            {
                TreeNode newRoot = new TreeNode(v);
                newRoot.left = root;
                return root;
            }

            int depth = 1;
            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(root);

            while (depth < d - 1)
            {
                int size = q.Count();

                for (int i = 0; i < size; i++)
                {
                    TreeNode node = q.Dequeue();

                    if (node.left != null)
                    {
                        q.Enqueue(node.left);
                    }

                    if (node.right != null)
                    {
                        q.Enqueue(node.right);
                    }
                }

                depth++;
            }


            while (q.Count != 0)
            {
                TreeNode pnode = q.Dequeue();
                TreeNode temp;

                temp = pnode.left;
                TreeNode left = new TreeNode(v);
                pnode.left = left;
                left.left = temp;

                temp = pnode.right;
                TreeNode right = new TreeNode(v);
                pnode.right = right;
                right.right = temp;
            }

            return root;
        }

        public int SumOfLeftLeaves1(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }

            int sum = 0;
            int leftCount = 0;
            Stack<TreeNode> st = new Stack<TreeNode>();
            st.Push(root);

            while (st.Count != 0)
            {
                TreeNode p = st.Pop();

                if (p.right != null)
                {
                    st.Push(p.right);
                }

                while (p.left != null)
                {
                    p = p.left;
                    if (p.right != null)
                    {
                        st.Push(p.right);
                    }
                    leftCount++;
                }

                if (p.right == null)
                {
                    if (leftCount != 0)
                        sum += p.val;
                }

                leftCount = 0;
            }

            return sum;
        }

        public int SumOfLeftLeaves(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }

            int sum = 0;

            if (root.left != null)
            {
                if (root.left.left == null && root.left.right == null)
                {
                    sum += root.left.val;
                }
                else
                {
                    sum += SumOfLeftLeaves(root.left);
                }
            }

            if (root.right != null)
            {
                sum += SumOfLeftLeaves(root.right);
            }

            return sum;
        }

        public IList<int> InorderTraversal(TreeNode root)
        {
            IList<int> res = new List<int>();

            Stack<TreeNode> st = new Stack<TreeNode>();
            TreeNode p = root;
            bool visited = false;
            while (p != null)
            {
                if (!visited)
                {
                    while (p.left != null)
                    {
                        st.Push(p);
                        p = p.left;
                    }
                }

                res.Add(p.val);

                if (p.right != null)
                {
                    p = p.right;
                    visited = false;
                }
                else
                {
                    if (st.Count != 0)
                    {
                        p = st.Pop();
                        visited = true;
                    }
                    else
                    {
                        p = null;
                    }
                }
            }

            return res;
        }

        public IList<int> InorderTraversal1(TreeNode root)
        {
            IList<int> res = new List<int>();

            Stack<TreeNode> st = new Stack<TreeNode>();
            TreeNode p = root;

            while (p != null || st.Count != 0)
            {
                while (p != null)
                {
                    st.Push(p);
                    p = p.left;
                }

                p = st.Pop();
                res.Add(p.val);
                p = p.right;
            }

            return res;
        }

        public IList<int> PreorderTraversal(TreeNode root)
        {
            IList<int> res = new List<int>();

            Stack<TreeNode> st = new Stack<TreeNode>();
            TreeNode p = root;

            while (p != null || st.Count != 0)
            {
                if (p == null)
                {
                    p = st.Pop();
                }

                res.Add(p.val);

                if (p.right != null)
                {
                    st.Push(p.right);
                }

                p = p.left;
            }

            return res;
        }

        public IList<int> PostorderTraversal(TreeNode root)
        {
            IList<int> res = new List<int>();

            if (root == null)
            {
                return res;
            }

            Stack<TreeNode> st = new Stack<TreeNode>();
            TreeNode p = null, prev = null;
            st.Push(root);

            while (st.Count != 0)
            {
                p = st.Peek();

                if (prev == null || prev.left == p || prev.right == p)
                {
                    if (p.left != null)
                    {
                        st.Push(p.left);
                    }
                    else if (p.right != null)
                    {
                        st.Push(p.right);
                    }
                    else
                    {
                        st.Pop();
                        res.Add(p.val);
                    }
                }
                else if (p.left == prev)
                {
                    if (p.right != null)
                    {
                        st.Push(p.right);
                    }
                    else
                    {
                        st.Pop();
                        res.Add(p.val);
                    }
                }
                else if (p.right == prev)
                {
                    st.Pop();
                    res.Add(p.val);
                }

                prev = p;
            }

            return res;
        }

        public IList<int> PostorderTraversal1(TreeNode root)
        {
            IList<int> res = new List<int>();

            if (root == null)
            {
                return res;
            }

            Stack<TreeNode> st = new Stack<TreeNode>();
            TreeNode p = root;
            st.Push(root);

            while (st.Count != 0 || p != null)
            {
                if (p != null)
                {
                    st.Push(p);
                    res.Insert(0, p.val);
                    p = p.right;
                }
                else
                {
                    TreeNode node = st.Pop();
                    p = node.left;
                }
            }

            return res;
        }

        public IList<int> CountSmaller(int[] nums)
        {
            IList<int> res = new List<int>();

            if (nums == null || nums.Length <= 0)
            {
                return res;
            }
            int[] resArr = new int[nums.Length];
            Node root = null;

            for (int i = nums.Length - 1; i >= 0; i--)
            {
                root = InsertNode(root, resArr, nums[i], i, 0);
            }

            return resArr.ToList();
        }

        Node InsertNode(Node node, int[] arr, int num, int i, int preSum)
        {
            if (node == null)
            {
                node = new Node(num, 0);
                arr[i] = preSum;
            }
            else if (node.val == num)
            {
                node.dup++;
                arr[i] = preSum + node.sum;
            }
            else if (node.val > num)
            {
                node.sum++;
                node.left = InsertNode(node.left, arr, num, i, preSum);
            }
            else
            {
                node.right = InsertNode(node.right, arr, num, i, preSum + node.dup + node.sum);
            }

            return node;
        }

        public IList<IList<int>> LevelOrderBottom(TreeNode root)
        {
            IList<IList<int>> res = new List<IList<int>>();

            if (root == null)
            {
                return res;
            }

            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(root);

            while (q.Count != 0)
            {
                int count = q.Count;
                List<int> ls = new List<int>();

                for (int i = 0; i < count; i++)
                {
                    TreeNode node = q.Dequeue();

                    ls.Add(node.val);

                    if (node.left != null)
                    {
                        q.Enqueue(node.left);
                    }

                    if (node.right != null)
                    {
                        q.Enqueue(node.right);
                    }
                }

                res.Add(new List<int>(ls));
            }

            IList<IList<int>> newRes = res.Reverse().ToList();

            return newRes;
        }

        public bool HasPathSum(TreeNode root, int sum)
        {
            if (root == null)
            {
                return false;
            }

            if (root.left == null && root.right == null && root.val == sum)
            {
                return true;
            }

            return HasPathSum(root.left, sum - root.val) || HasPathSum(root.right, sum - root.val);
        }

        public int MinDepth(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }

            if (root.left == null && root.right == null)
            {
                return 1;
            }

            int depth = 1;
            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(root);

            while (q.Count != 0)
            {
                int size = q.Count;

                for (int i = 0; i < size; i++)
                {
                    TreeNode node = q.Dequeue();

                    if (node.left != null)
                    {
                        q.Enqueue(node.left);
                    }

                    if (node.right != null)
                    {
                        q.Enqueue(node.right);
                    }

                    if (node.left == null && node.right == null)
                    {
                        return depth;
                    }
                }

                depth++;
            }

            return 0;
        }

        public IList<int> RightSideView(TreeNode root)
        {
            IList<int> res = new List<int>();

            if (root == null)
            {
                return res;
            }

            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(root);

            while (q.Count != 0)
            {

                int size = q.Count;
                res.Add(q.Last().val);

                for (int i = 0; i < size; i++)
                {
                    TreeNode node = q.Dequeue();

                    if (node.left != null)
                    {
                        q.Enqueue(node.left);
                    }

                    if (node.right != null)
                    {
                        q.Enqueue(node.right);
                    }
                }
            }

            return res;
        }

        public IList<int> RightSideView1(TreeNode root)
        {
            IList<int> res = new List<int>();

            if (root == null)
            {
                return res;
            }

            GetResults(root, res, 0);

            return res;
        }

        void GetResults(TreeNode root, IList<int> res, int depth)
        {
            if (root == null)
            {
                return;
            }

            if (res.Count == depth)
            {
                res.Add(root.val);
            }

            GetResults(root.right, res, depth + 1);
            GetResults(root.left, res, depth + 1);
        }

        public IList<TreeNode> FindDuplicateSubtrees(TreeNode root)
        {
            IList<TreeNode> res = new List<TreeNode>();

            if (root == null)
            {
                return res;
            }

            Dictionary<string, int> dct = new Dictionary<string, int>();

            serialize(root, dct, res);

            return res;
        }

        string serialize(TreeNode node, Dictionary<string, int> dct, IList<TreeNode> res)
        {
            if (node == null)
            {
                return "#";
            }

            string s = node.val.ToString() + "," + serialize(node.left, dct, res) + "," + serialize(node.right, dct, res);

            if (!dct.ContainsKey(s))
            {
                dct.Add(s, 1);
            }
            else if (dct[s] == 1)
            {
                res.Add(node);
                dct[s]++;
            }
            else
            {
                dct[s]++;
            }

            return s;
        }

        public int SumNumbers(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }

            int res = SumNumbershelper(root, 0);

            return res;
        }

        int SumNumbershelper(TreeNode root, int curr)
        {
            if (root == null)
            {
                return 0;
            }

            curr = 10 * curr + root.val;

            if (root.left == null && root.right == null)
            {
                return curr;
            }

            int left = SumNumbershelper(root.left, curr);
            int right = SumNumbershelper(root.right, curr);

            return left + right;
        }

        public int WidthOfBinaryTree(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }

            Queue<TreeNode> q = new Queue<TreeNode>();
            int width = 1;
            q.Enqueue(root);

            while (q.Count != 0)
            {
                int w = 0;
                int size = q.Count;
                bool isNode = false;

                for (int i = 0; i < size; i++)
                {

                    TreeNode node = q.Dequeue();

                    if (node != null)
                    {
                        w++;
                        isNode = true;
                        if (w > width)
                        {
                            width = w;
                        }

                        q.Enqueue(node.left);
                        q.Enqueue(node.right);
                    }
                    else if (isNode)
                    {
                        q.Enqueue(null);
                        q.Enqueue(null);
                        w++;
                    }
                }
            }

            return width;
        }

        public int LongestUnivaluePath(TreeNode root)
        {
            if (root == null || (root.left == null && root.right == null))
            {
                return 0;
            }

            int leftCount = LongestUnivaluePath(root.left);
            int rightCount = LongestUnivaluePath(root.right);

            if (root.left != null && root.right != null && root.left.val == root.val && root.val == root.right.val)
            {
                return leftCount + rightCount + 1;
            }
            else if (root.left != null && root.left.val == root.val)
            {
                return Math.Max(leftCount + 1, rightCount);
            }
            else if (root.right != null && root.right.val == root.val)
            {
                return Math.Max(rightCount + 1, leftCount);
            }
            else
            {
                return Math.Max(leftCount, rightCount);
            }
        }

        public int LongestUnivaluePath1(TreeNode root)
        {
            if(root==null)
            {
                return 0;
            }

            int[] res = new int[1];
            DFSHelper(root, res);

            return res[0];
        }

        int DFSHelper(TreeNode root, int[] res)
        {
            int left=0,right = 0;

            if(root.left!=null)
            {
                left = DFSHelper(root.left, res);
            }
            
            if(root.right!=null)
            {
                right = DFSHelper(root.right, res);
            }

            int leftCount = 0, rightCount = 0;

            if(root.left!=null&&root.left.val==root.val)
            {
                leftCount = left + 1;
            }

            if(root.right!=null && root.right.val==root.val)
            {
                rightCount = right + 1;
            }

            res[0] = Math.Max(res[0], leftCount + rightCount);

            return Math.Max(leftCount, rightCount);
        }

        public int MinDiffInBST(TreeNode root)
        {
            List<int> res = new List<int>();

            InOrderTravel(root, res);

            int diff = int.MaxValue;
            for(int i=1;i<res.Count;i++)
            {
                if(diff>res[i]-res[i-1])
                {
                    diff = res[i] - res[i - 1];
                }
            }

            return diff;
        }

        void InOrderTravel(TreeNode root, List<int> ls)
        {
            if(root==null)
            {
                return;
            }

            InOrderTravel(root.left, ls);
            ls.Add(root.val);
            InOrderTravel(root.right, ls);
        }

        List<int> InOrderTravel(TreeNode root)
        {
            List<int> ls = new List<int>();

            if(root==null)
            {
                return ls;
            }

            Stack<TreeNode> s = new Stack<TreeNode>();
            TreeNode p = root;
            s.Push(p);
            bool visited = false;

            while(s.Count>0)
            {
                while(p.left!=null&&!visited)
                {
                    p = p.left;
                    s.Push(p);
                }

                p = s.Pop();
                ls.Add(p.val);

                if(p.right!=null)
                {
                    p = p.right;
                    s.Push(p);
                    visited = false;
                }
                else
                {
                    visited = true;
                }
            }

            return ls;
        }

        public IList<IList<int>> LevelOrder(TreeNode root)
        {
            IList<IList<int>> res = new List<IList<int>>();

            if(root==null)
            {
                return res;
            }

            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(root);

            while(q.Count!=0)
            {
                IList<int> ls = new List<int>();
                int c = q.Count;

                for(int i=0;i<c;i++)
                {
                    TreeNode tn = q.Dequeue();
                    ls.Add(tn.val);

                    if(tn.left!=null)
                    {
                        q.Enqueue(tn.left);
                    }

                    if(tn.right!=null)
                    {
                        q.Enqueue(tn.right);
                    }
                }

                if(ls.Count>0)
                {
                    res.Add(ls);
                }
            }

            return res;
        }

        public TreeNode SubtreeWithAllDeepest(TreeNode root)
        {
            if(root==null)
            {
                return root;
            }

            int left = Depth(root.left);
            int right = Depth(root.right);

            if(left==right)
            {
                return root;
            }

            if(left>right)
            {
                return SubtreeWithAllDeepest(root.left);
            }

            return SubtreeWithAllDeepest(root.right);
        }

        int Depth(TreeNode node)
        {
            if(node==null)
            {
                return 0;
            }

            return Math.Max(Depth(node.left), Depth(node.right)) + 1;
        }

        TreeNode LCA(TreeNode root, HashSet<TreeNode> hs)
        {
            if(root==null||hs.Contains(root))
            {
                return root;
            }

            TreeNode left = LCA(root.left, hs);
            TreeNode right = LCA(root.right, hs);

            if(left!=null&&right!=null)
            {
                return root;
            }
            else if(left!=null)
            {
                return left;
            }
            else
            {
                return right;
            }
        }

        public TreeNode SubtreeWithAllDeepestByLevel(TreeNode root)
        {
            Dictionary<int, HashSet<TreeNode>> dct = new Dictionary<int, HashSet<TreeNode>>();
            int level = 0;
            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(root);

            while(q.Count>0)
            {
                HashSet<TreeNode> hs = new HashSet<TreeNode>();
                int n = q.Count;

                for(int i=0;i<n;i++)
                {
                    TreeNode node = q.Dequeue();

                    if(node.left!=null)
                    {
                        q.Enqueue(node.left);
                    }

                    if(node.right!=null)
                    {
                        q.Enqueue(node.right);
                    }

                    hs.Add(node);
                }

                dct.Add(level, hs);
                level++;
            }

            HashSet<TreeNode> s = dct[level-1];

            return LCA(root, s);
        }

        public TreeNode SubtreeWithAllDeepest1(TreeNode root)
        {
            if (root == null)
            {
                return root;
            }

            TreeNode[] res = new TreeNode[1];
            int[] maxDepth = new int[1];
            GetDepth(root, 0, res, maxDepth);

            return res[0];
        }

        int GetDepth(TreeNode root,int depth, TreeNode[] ts,int[] MaxDepth)
        {
            if(root==null)
            {
                return depth;
            }

            int left = GetDepth(root.left, depth + 1, ts, MaxDepth);
            int right = GetDepth(root.right, depth + 1, ts, MaxDepth);

            if(left==right&&left>=MaxDepth[0])
            {
                MaxDepth[0] = left;
                ts[0] = root;
            }

            return Math.Max(left, right);
        }

        public IList<int> DistanceK(TreeNode root, TreeNode target, int K)
        {
            IList<int> res = new List<int>();

            if(root==null)
            {
                return res;
            }

            Dictionary<TreeNode, List<TreeNode>> dct = new Dictionary<TreeNode, List<TreeNode>>();
            Queue<TreeNode> q = new Queue<TreeNode>();
            HashSet<TreeNode> visited = new HashSet<TreeNode>();

            q.Enqueue(root);

            while(q.Count>0)
            {
                TreeNode node = q.Dequeue();

                if(node.left!=null)
                {
                    q.Enqueue(node.left);

                    if(!dct.ContainsKey(node))
                    {
                        dct.Add(node, new List<TreeNode>());
                    }
                    
                    dct[node].Add(node.left);

                    if (!dct.ContainsKey(node.left))
                    {
                        dct.Add(node.left, new List<TreeNode>());
                    }

                    dct[node.left].Add(node);
                }

                if(node.right!=null)
                {
                    q.Enqueue(node.right);

                    if (!dct.ContainsKey(node))
                    {
                        dct.Add(node, new List<TreeNode>());
                    }

                    dct[node].Add(node.right);

                    if (!dct.ContainsKey(node.right))
                    {
                        dct.Add(node.right, new List<TreeNode>());
                    }

                    dct[node.right].Add(node);
                }
            }

            q.Clear();
            q.Enqueue(target);
            visited.Add(target);

            while(q.Count>0)
            {
                int size = q.Count;

                if(K==0)
                {
                    for(int i=0;i<size;i++)
                    {
                        res.Add(q.Dequeue().val);
                    }

                    return res;
                }

                for(int i=0;i<size;i++)
                {
                    TreeNode node = q.Dequeue();

                    if (dct.Count > 0)
                    {
                        foreach (TreeNode next in dct[node])
                        {
                            if (!visited.Contains(next))
                            {
                                visited.Add(next);
                                q.Enqueue(next);
                            }
                        }
                    }
                }

                K--;
            }

            return res;
        }

        public IList<int> DistanceK1(TreeNode root, TreeNode target, int K)
        {
            IList<int> res = new List<int>();

            if (root == null)
            {
                return res;
            }

            Dictionary<TreeNode, TreeNode> dct = new Dictionary<TreeNode, TreeNode>();
            Queue<TreeNode> q = new Queue<TreeNode>();
            HashSet<TreeNode> visited = new HashSet<TreeNode>();
            q.Enqueue(root);
            
            while(q.Count>0)
            {
                TreeNode node = q.Dequeue();

                if(node==target)
                {
                    break;
                }

                if(node.left!=null)
                {
                    q.Enqueue(node.left);
                    dct.Add(node.left, node);
                }

                if(node.right!=null)
                {
                    q.Enqueue(node.right);
                    dct.Add(node.right, node);
                }
            }

            q.Clear();
            q.Enqueue(target);
            visited.Add(target);

            while (K>0)
            {
                if(q.Count==0)
                {
                    return res;
                }

                for (int i = q.Count; i > 0; i--)
                {
                    TreeNode node = q.Dequeue();

                    if (node.left != null && visited.Add(node.left))
                    {
                        q.Enqueue(node.left);
                    }

                    if (node.right != null && visited.Add(node.right))
                    {
                        q.Enqueue(node.right);
                    }

                    if (dct.ContainsKey(node) && visited.Add(dct[node]))
                    {
                        q.Enqueue(dct[node]);
                    }
                }

                K--;
            }

            while(q.Count>0)
            {
                res.Add(q.Dequeue().val);
            }

            return res;
        }

        public TreeNode ConstructFromPrePost(int[] pre, int[] post)
        {
            int n = pre.Length;

            return BuildTreeNodeHelperNew(pre, post, new int[1],0, n - 1);
        }

        public TreeNode BuildTreeNodeHelperNew(int[] pre, int[] pos, int[] preLeft, int posLeft, int posRight)
        {
            if(posLeft>posRight)
            {
                return null;
            }

            preLeft[0]++;
            TreeNode root = new TreeNode(pos[posRight]);

            if(posRight==posLeft)
            {
                return root;
            }

            int i = posLeft;

            while(i<=posRight&&pos[i]!=pre[preLeft[0]])
            {
                i++;
            }

            root.left = BuildTreeNodeHelperNew(pre, pos, preLeft, posLeft, i);
            root.right = BuildTreeNodeHelperNew(pre, pos, preLeft, i + 1, posRight-1);

            return root;
        }
        public TreeNode BuildTreeNodeHelper(int[] pre,int[] pos,int preLeft,int preRight,int posLeft,int posRight)
        {
            if(preLeft > preRight||posLeft>posRight)
            {
                return null;
            }

            TreeNode root = new TreeNode(pos[posRight]);
            preLeft++;
            posRight--;

            if(preLeft==preRight||posLeft==posRight)
            {
                return root;
            }

            int left = 0, right = 0;
            for (int i = preLeft; i <= preRight; i++)
            {
                if (pre[i] == pos[posRight])
                {
                    left = i - 1;
                    break;
                }
            }

            for(int i=posRight;i>=posLeft;i--)
            {
                if(pos[i]==pre[preLeft])
                {
                    right = i;
                    break;
                }
            }

            root.left = BuildTreeNodeHelper(pre, pos, preLeft, left, posLeft, right);
            root.right = BuildTreeNodeHelper(pre, pos, left + 1, preRight, right + 1, posRight);
            return root;
        }
    }
    public class TreeNode
    {
      public int val;
      public TreeNode left;
      public TreeNode right;
      public TreeNode(int x) { val = x; }
  }

    public class BSTIterator
    {
        Stack<TreeNode> s = new Stack<TreeNode>();
        public BSTIterator(TreeNode root)
        {
            PushAll(root);
        }

        /** @return whether we have a next smallest number */
        public bool HasNext()
        {
            return s.Count != 0;
        }

        /** @return the next smallest number */
        public int Next()
        {
            TreeNode node = s.Pop();
            PushAll(node.right);
            return node.val;
        }

        void PushAll(TreeNode root)
        {
            while(root!=null)
            {
                s.Push(root);
                root = root.left;
            }
        }
    }

    public class Node
    {
        public int val;
        public IList<Node> children;

        public Node() { }
        public Node(int _val, IList<Node> _children)
        {
            val = _val;
            children = _children;
        }

    }
        public class NaryTree
        {
            public int MaxDepth(Node root)
            {
                if(root==null)
                {
                    return 0;
                }
                
                int[] maxDepth=new int[1];
                
                MaxDepthHelper(root,0,maxDepth);

                return maxDepth[0];
            }

            public void MaxDepthHelper(Node root, int depth, int[] maxDepth)
            {
                if(root==null)
                {
                    return;
                }

                depth++;
                
                if(depth>maxDepth[0])
                {
                    maxDepth[0] = depth;
                }
                
                foreach(Node nd in root.children)
                {
                    MaxDepthHelper(nd, depth, maxDepth);
                }
            }
        }

    public class CBTInserter
    {
        List<int> ls = new List<int>();
        TreeNode root;
        Dictionary<int, TreeNode> dct = new Dictionary<int, TreeNode>();

        public CBTInserter(TreeNode root)
        {
            this.root = root;
            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(root);

            while(q.Count>0)
            {
                int size = q.Count;
                TreeNode cur = q.Dequeue();
                ls.Add(cur.val);
                dct.Add(ls.Count, cur);

                if(cur.left!=null)
                {
                    q.Enqueue(cur.left);
                }

                if (cur.right!= null)
                {
                    q.Enqueue(cur.right);
                }
            }
        }

        public int Insert(int v)
        {
            ls.Add(v);
            int index = ls.Count;

            TreeNode parent = dct[index / 2];

            if(index%2==0)
            {
                parent.left = new TreeNode(v);
                dct.Add(index, parent.left);
            }
            else
            {
                parent.right = new TreeNode(v);
                dct.Add(index, parent.right);
            }

            return parent.val;
        }

        public TreeNode Get_root()
        {
            return this.root;
        }
    }

    public class CBTInserterNew
    {
        TreeNode root;
        Queue<TreeNode> q = new Queue<TreeNode>();

        public CBTInserterNew(TreeNode root)
        {
            this.root = root;
            
            q.Enqueue(root);

            while(q.Peek().left!=null&&q.Peek().right!=null)
            {
                TreeNode node = q.Dequeue();
                q.Enqueue(node.left);
                q.Enqueue(node.right);
            }
        }

        public int Insert(int v)
        {
            TreeNode p = q.Peek();

            if (p.left==null)
            {
                p.left = new TreeNode(v);
            }
            else
            {
                p.right = new TreeNode(v);
                q.Enqueue(p.left);
                q.Enqueue(p.right);
                q.Dequeue();
            }

            return p.val;
        }

        public TreeNode Get_root()
        {
            return this.root;
        }
    }
}
