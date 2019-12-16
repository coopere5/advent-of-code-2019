using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using AdventUtils;

namespace Day13
{
    internal class Program
    {
        private static void Main()
        {
            Part1();
            Part2();
            Console.ReadKey();
        }

        private static void Part1()
        {
            Stopwatch sw = Stopwatch.StartNew();

            var input = File.ReadAllText("input.txt").Split(',').Select(long.Parse);
            IntcodeComputer computer = new IntcodeComputer(input);
            var screen = new Dictionary<Point, long>();

            bool running = true;

            while (running)
            {
                running = computer.RunNext() == long.MinValue;
                if (computer.OutputQueue.Count == 3)
                {
                    long x = computer.OutputQueue.Dequeue();
                    long y = computer.OutputQueue.Dequeue();
                    long id = computer.OutputQueue.Dequeue();

                    Point currentPoint = new Point(x, y);
                    if (!screen.ContainsKey(currentPoint)) screen.Add(currentPoint, 0);
                    screen[currentPoint] = id;
                }
            }

            Console.WriteLine($"Part 1: {screen.Values.Count(v => v == 2)}");

            sw.Stop();
            Debug.WriteLine(sw.Elapsed);
        }

        private static void Part2()
        {
            Stopwatch sw = Stopwatch.StartNew();

            var input = File.ReadAllText("input.txt").Split(',').Select(long.Parse).ToArray();
            input[0] = 2;
            IntcodeComputer computer = new IntcodeComputer(input);

            var screen = new Dictionary<Point, long>();

            bool running = true;

            while (running)
            {
                running = computer.RunNext() == long.MinValue;
                if (computer.OutputQueue.Count == 3)
                {
                    long x = computer.OutputQueue.Dequeue();
                    long y = computer.OutputQueue.Dequeue();
                    long id = computer.OutputQueue.Dequeue();

                    Point currentPoint = new Point(x, y);
                    if (!screen.ContainsKey(currentPoint)) screen.Add(currentPoint, 0);
                    screen[currentPoint] = id;
                }

                if (computer.AwaitingInput())
                {
                    //code for the display
                    //for (long y = screen.Keys.Max(p => p.Y); y >= screen.Keys.Min(p => p.Y); y--)
                    //{
                    //    for (long x = screen.Keys.Min(p => p.X); x <= screen.Keys.Max(p => p.X); x++)
                    //    {
                    //        Point point = new Point(x,y);
                    //        if (!screen.ContainsKey(point)) continue;
                    //        switch (screen[point])
                    //        {
                    //            case 0:
                    //                Console.Write(" ");
                    //                break;
                    //            case 1:
                    //                Console.Write("#");
                    //                break;
                    //            case 2:
                    //                Console.Write("$");
                    //                break;
                    //            case 3:
                    //                Console.Write("_");
                    //                break;
                    //            case 4:
                    //                Console.Write("*");
                    //                break;
                    //            default:
                    //                break;
                    //        }
                    //    }
                    //    Console.WriteLine();
                    //}
                    Point ballPos = screen.First(v => v.Value == 4).Key;
                    Point paddlePos = screen.First(v => v.Value == 3).Key;
                    if (ballPos.X < paddlePos.X)
                    {
                        computer.InputQueue.Enqueue(-1);
                    }
                    else if (ballPos.X > paddlePos.X)
                    {
                        computer.InputQueue.Enqueue(1);
                    }
                    else
                    {
                        computer.InputQueue.Enqueue(0);
                    }
                }
            }

            Console.WriteLine($"Part 2: {screen[new Point(-1, 0)]}");

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
