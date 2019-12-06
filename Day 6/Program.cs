using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day6
{
    internal class Program
    {
        private static void Main()
        {
            Part1();
            Part2();
        }

        private static void Part1()
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();

            var input = File.ReadAllLines("input.txt");
            var nodes = new List<TreeNode> {new TreeNode(null, "COM")};
            nodes.AddRange(input.Select(line => line.Split(')')).Select(splitLine => new TreeNode(splitLine[0], splitLine[1])));
            
            TreeNode parent;
            foreach (TreeNode node in nodes)
            {
                parent = nodes.FirstOrDefault(n => n.ID == node.ParentID);
                node.Parent = parent;
                parent?.Children.Add(node);
            }

            int count = 0;
            foreach (TreeNode node in nodes)
            {
                parent = node.Parent;
                while (parent != null)
                {
                    count++;
                    parent = parent.Parent;
                }
            }

            System.Diagnostics.Debug.WriteLine(count);

            sw.Stop();
            System.Diagnostics.Debug.WriteLine(sw.Elapsed);
        }

        private static void Part2()
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();

            var input = File.ReadAllLines("input.txt");
            var nodes = new List<TreeNode> {new TreeNode(null, "COM")};
            nodes.AddRange(input.Select(line => line.Split(')')).Select(splitLine => new TreeNode(splitLine[0], splitLine[1])));
            
            TreeNode parent;
            foreach (TreeNode node in nodes)
            {
                parent = nodes.FirstOrDefault(n => n.ID == node.ParentID);
                node.Parent = parent;
                parent?.Children.Add(node);
            }

            var youParents = new List<TreeNode>();
            parent = nodes.First(n => n.ID == "YOU").Parent;
            while (parent != null)
            {
                youParents.Add(parent);
                parent = parent.Parent;
            }

            var sanParents = new List<TreeNode>();
            parent = nodes.First(n => n.ID == "SAN").Parent;
            while (parent != null)
            {
                sanParents.Add(parent);
                parent = parent.Parent;
            }

            foreach (TreeNode node in youParents)
            {
                if (!sanParents.Contains(node)) continue;
                node.Parent = null; //break tree at first common ancestor
                break;
            }

            int count = 0;
            parent = youParents.First().Parent;
            while (parent != null)
            {
                count++;
                parent = parent.Parent;
            }

            parent = sanParents.First().Parent;
            while (parent != null)
            {
                count++;
                parent = parent.Parent;
            }

            System.Diagnostics.Debug.WriteLine(count);

            sw.Stop();
            System.Diagnostics.Debug.WriteLine(sw.Elapsed);
        }
    }

    public class TreeNode
    {
        public string ID { get; set; }
        public string ParentID { get; set; }
        public TreeNode Parent { get; set; }
        public List<TreeNode> Children { get; set; }

        public TreeNode()
        {
            Children = new List<TreeNode>();
        }

        public TreeNode(string parent, string id) : this()
        {
            ID = id;
            ParentID = parent;
        }
    }
}
