using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using AdventUtils;

namespace Day17
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

            var input = File.ReadAllText("input.txt").Split(',').Select(long.Parse);
            IntcodeComputer computer = new IntcodeComputer(input);

            var charList = new List<char>();
            computer.Run();
            while (computer.OutputQueue.Any())
            {
                charList.Add((char)computer.OutputQueue.Dequeue());
            }
            string s = new string(charList.ToArray());
            Console.Write(s);

            var scaffolding = new HashSet<Point>();

            int y = 0;
            foreach (string line in s.Split('\n'))
            {
                int x = 0;
                foreach (char c in line)
                {
                    if (c == '#') scaffolding.Add(new Point(x, y));
                    x++;
                }
                y++;
            }

            long sum = 0;

            foreach (Point scaffold in scaffolding)
            {
                if (scaffolding.Contains(new Point(scaffold.X - 1, scaffold.Y)) &&
                    scaffolding.Contains(new Point(scaffold.X + 1, scaffold.Y)) &&
                    scaffolding.Contains(new Point(scaffold.X, scaffold.Y - 1)) &&
                    scaffolding.Contains(new Point(scaffold.X, scaffold.Y + 1)))
                {
                    sum += scaffold.X * scaffold.Y;
                }
            }

            Console.WriteLine($"Part 1: {sum}");

            sw.Stop();
            Debug.WriteLine(sw.Elapsed);
        }

        private static void Part2()
        {
            Stopwatch sw = Stopwatch.StartNew();

            var input = File.ReadAllText("input.txt").Split(',').Select(long.Parse).ToArray();
            input[0] = 2;
            IntcodeComputer computer = new IntcodeComputer(input);

            string s = @"L,6,R,12, A
                         L,6,R,12, A
                         L,10,L,4,L,6, B
                         L,6,R,12, A
                         L,6,R,12, A
                         L,10,L,4,L,6 B
                         L,6,R,12 A
                         L,6,L,10,L,10,L,4, C?
                         L,6,R,12 A
                         L,10,L,4,L,6 B
                         L,10,L,10,L,4,L,6, ?
                         L,6,R,12,
                         L,6,L,10,L,10,L,4,
                         L,6";

            computer.Run();
            sw.Stop();
            Debug.WriteLine(sw.Elapsed);
        }
    }

    public struct Point
    {
        public Point(long x, long y)
        {
            X = x;
            Y = y;
        }

        public long X { get; set; }
        public long Y { get; set; }

        public override string ToString()
        {
            return $"({X},{Y})";
        }
    }
}
