using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Day24
{
    internal class Program
    {
        private static void Main()
        {
            Part1();
            Part2();
            Console.ReadLine();
        }

        private static void Part1()
        {
            Stopwatch sw = Stopwatch.StartNew();

            Eris e = new Eris();
            int biodiversity = e.Run();

            Console.WriteLine($"Part 1: {biodiversity}");

            sw.Stop();
            Debug.WriteLine(sw.Elapsed);
        }

        private static void Part2()
        {
            Stopwatch sw = Stopwatch.StartNew();

            sw.Stop();
            Debug.WriteLine(sw.Elapsed);
        }
    }

    public class Eris
    {
        public IDictionary<Point, char> Scan;

        public Eris()
        {
            Scan = new Dictionary<Point, char>();
            int y = 0;
            foreach (string line in File.ReadAllLines("input.txt"))
            {
                int x = 0;
                foreach (char c in line)
                {
                    Scan.Add(new Point(x++, y), c);
                }
                y++;
            }
        }

        public int Run()
        {
            var scores = new HashSet<int>();
            while (true)
            {
                Tick();
                int biodiversity = GetBiodiversity();
                if (!scores.Add(biodiversity)) return biodiversity;
            }
        }

        public void Tick()
        {
            IDictionary<Point, char> next = Scan.ToDictionary(p => p.Key, p => p.Value);

            foreach (var space in Scan)
            {
                int adjacent = GetNeighbors(space.Key).Count(neighbor => Scan[neighbor] == '#');

                if (space.Value == '#')
                {
                    if (adjacent != 1) next[space.Key] = '.';
                }
                else
                {
                    if (adjacent == 1 || adjacent == 2) next[space.Key] = '#';
                }
            }

            Scan = next;
        }

        private IEnumerable<Point> GetNeighbors(Point p)
        {
            var points = new HashSet<Point>
            {
                new Point(p.X - 1, p.Y), new Point(p.X + 1, p.Y), new Point(p.X, p.Y - 1), new Point(p.X, p.Y + 1)
            };
            points.IntersectWith(Scan.Keys);
            return points;
        }

        public int GetBiodiversity()
        {
            int p = 0;
            int biodiversity = 0;
            foreach (var space in Scan)
            {
                if (space.Value == '#') biodiversity += (int)Math.Pow(2, p);
                p++;
            }

            return biodiversity;
        }
    }

    public struct Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public override string ToString()
        {
            return $"({X},{Y})";
        }
    }
}
