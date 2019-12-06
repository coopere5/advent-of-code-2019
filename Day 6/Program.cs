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

            var nodes = BuildTree();

            int count = nodes.Select(node => node.Parent).Select(CountOrbits).Sum();

            System.Diagnostics.Debug.WriteLine($"Part 1: {count}");

            sw.Stop();
            System.Diagnostics.Debug.WriteLine(sw.Elapsed);
        }

        private static void Part2()
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();

            var nodes = BuildTree();

            var youParents = new List<TreeNode>();
            var sanParents = new List<TreeNode>();

            TreeNode curParent = nodes.First(n => n.ID == "YOU").Parent;
            while (curParent != null)
            {
                youParents.Add(curParent);
                curParent = curParent.Parent;
            }

            curParent = nodes.First(n => n.ID == "SAN").Parent;
            while (curParent != null)
            {
                sanParents.Add(curParent);
                curParent = curParent.Parent;
            }

            foreach (TreeNode node in youParents)
            {
                if (!sanParents.Contains(node)) continue;
                node.Parent = null; //break tree at first common ancestor
                break;
            }

            int count = 0;
            count += CountOrbits(youParents.First().Parent);
            count += CountOrbits(sanParents.First().Parent);

            System.Diagnostics.Debug.WriteLine($"Part 2: {count}");

            sw.Stop();
            System.Diagnostics.Debug.WriteLine(sw.Elapsed);
        }

        private static IList<TreeNode> BuildTree()
        {
            var input = File.ReadAllLines("input.txt");
            var nodes = new List<TreeNode> { new TreeNode(null, "COM") };
            nodes.AddRange(input.Select(line => line.Split(')')).Select(splitLine => new TreeNode(splitLine[0], splitLine[1])));

            foreach (TreeNode node in nodes)
            {
                TreeNode parent = nodes.FirstOrDefault(n => n.ID == node.ParentID);
                node.Parent = parent;
                parent?.Children.Add(node);
            }

            return nodes;
        }

        private static int CountOrbits(TreeNode node)
        {
            int count = 0;

            while (node != null)
            {
                count++;
                node = node.Parent;
            }

            return count;
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
