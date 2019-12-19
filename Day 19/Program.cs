using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using AdventUtils;

namespace Day19
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

            var input = File.ReadAllText("input.txt").Split(',').Select(long.Parse).ToArray();
            int count = 0;

            int firstX = 0;
            int lastX = 50;
            for (int y = 0; y < 50; y++)
            {
                bool found = false;
                int curX = lastX + 1;
                for (int x = firstX; x < lastX; x++)
                {
                    IntcodeComputer computer = new IntcodeComputer(input);
                    computer.Run(x, y);
                    if (computer.OutputQueue.Dequeue() == 1)
                    {
                        if (!found) firstX = x;
                        found = true;
                        count++;
                    }
                    else if (found)
                    {
                        curX = x + 1;
                        break;
                    }
                }
                lastX = curX;
            }

            Console.WriteLine($"Part 1: {count}");

            sw.Stop();
            Debug.WriteLine(sw.Elapsed);
        }

        private static void Part2()
        {
            Stopwatch sw = Stopwatch.StartNew();

            var input = File.ReadAllText("input.txt").Split(',').Select(long.Parse).ToArray();

            int firstX = 0;
            int lastX = 50;
            int y = 0;
            int columnCount = 0;

            var screen = new HashSet<Point>();
            bool running = true;
            while(running)
            {
                columnCount = 0;
                bool found = false;
                int curX = lastX + 1;
                for (int x = firstX; x < lastX; x++)
                {
                    IntcodeComputer computer = new IntcodeComputer(input);
                    computer.Run(x, y);
                    if (computer.OutputQueue.Dequeue() == 1)
                    {
                        if (!found)
                        {
                            firstX = x;
                            if (lastX - firstX < 100)
                            {
                                curX = lastX + 2;
                                break;
                            }

                            if (screen.Count(p => p.X == x) >= 100)
                            {
                                if (screen.Contains(new Point(x, y - 99)))
                                {
                                    Console.WriteLine($"Part 2: {x * 10000 + (y - 99)}");
                                    running = false;
                                }
                            }
                        }
                        found = true;
                        screen.Add(new Point(x, y));
                        columnCount++;
                    }
                    else if (found)
                    {
                        curX = x + 1;
                        break;
                    }
                }
                lastX = curX;
                y++;
            }

            sw.Stop();
            Debug.WriteLine(sw.Elapsed);
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

        public static Point[] Range(Point start, int count) => (from x in Enumerable.Range(start.X, count) from y in Enumerable.Range(start.Y, count) select new Point(x, y)).ToArray();

        public override string ToString()
        {
            return $"({X},{Y})";
        }
    }
}
