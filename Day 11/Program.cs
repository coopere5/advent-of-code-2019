using System;
using System.Collections.Generic;
using System.Linq;
using AdventUtils;

namespace Day11
{
    internal class Program
    {
        private static readonly long[] memory = new long[]
        {
            3, 8, 1005, 8, 328, 1106, 0, 11, 0, 0, 0, 104, 1, 104, 0, 3, 8, 102, -1, 8, 10, 1001, 10, 1, 10, 4, 10, 108,
            0, 8, 10, 4, 10, 1002, 8, 1, 28, 1, 1003, 10, 10, 3, 8, 1002, 8, -1, 10, 101, 1, 10, 10, 4, 10, 108, 1, 8,
            10, 4, 10, 102, 1, 8, 54, 2, 1103, 6, 10, 3, 8, 1002, 8, -1, 10, 101, 1, 10, 10, 4, 10, 108, 0, 8, 10, 4,
            10, 101, 0, 8, 80, 3, 8, 1002, 8, -1, 10, 1001, 10, 1, 10, 4, 10, 108, 1, 8, 10, 4, 10, 1002, 8, 1, 102, 3,
            8, 102, -1, 8, 10, 1001, 10, 1, 10, 4, 10, 108, 0, 8, 10, 4, 10, 1001, 8, 0, 124, 3, 8, 102, -1, 8, 10, 101,
            1, 10, 10, 4, 10, 1008, 8, 1, 10, 4, 10, 1001, 8, 0, 147, 1006, 0, 35, 1, 7, 3, 10, 2, 106, 13, 10, 2, 1104,
            9, 10, 3, 8, 102, -1, 8, 10, 1001, 10, 1, 10, 4, 10, 108, 0, 8, 10, 4, 10, 1002, 8, 1, 183, 2, 7, 16, 10, 2,
            105, 14, 10, 1, 1002, 12, 10, 1006, 0, 13, 3, 8, 102, -1, 8, 10, 1001, 10, 1, 10, 4, 10, 108, 0, 8, 10, 4,
            10, 1002, 8, 1, 220, 1006, 0, 78, 2, 5, 3, 10, 1006, 0, 92, 1006, 0, 92, 3, 8, 1002, 8, -1, 10, 101, 1, 10,
            10, 4, 10, 108, 1, 8, 10, 4, 10, 1001, 8, 0, 255, 1006, 0, 57, 2, 1001, 11, 10, 1006, 0, 34, 2, 1007, 18,
            10, 3, 8, 1002, 8, -1, 10, 101, 1, 10, 10, 4, 10, 1008, 8, 1, 10, 4, 10, 1002, 8, 1, 292, 2, 109, 3, 10, 1,
            1103, 14, 10, 2, 2, 5, 10, 2, 1006, 3, 10, 101, 1, 9, 9, 1007, 9, 997, 10, 1005, 10, 15, 99, 109, 650, 104,
            0, 104, 1, 21101, 932700762920, 0, 1, 21101, 0, 345, 0, 1105, 1, 449, 21102, 1, 386577306516, 1, 21102, 356,
            1, 0, 1106, 0, 449, 3, 10, 104, 0, 104, 1, 3, 10, 104, 0, 104, 0, 3, 10, 104, 0, 104, 1, 3, 10, 104, 0, 104,
            1, 3, 10, 104, 0, 104, 0, 3, 10, 104, 0, 104, 1, 21101, 179355975827, 0, 1, 21101, 403, 0, 0, 1106, 0, 449,
            21102, 1, 46413220903, 1, 21102, 1, 414, 0, 1106, 0, 449, 3, 10, 104, 0, 104, 0, 3, 10, 104, 0, 104, 0,
            21101, 988224959252, 0, 1, 21102, 1, 437, 0, 1106, 0, 449, 21101, 717637968660, 0, 1, 21101, 0, 448, 0,
            1106, 0, 449, 99, 109, 2, 22101, 0, -1, 1, 21102, 40, 1, 2, 21101, 480, 0, 3, 21101, 470, 0, 0, 1106, 0,
            513, 109, -2, 2105, 1, 0, 0, 1, 0, 0, 1, 109, 2, 3, 10, 204, -1, 1001, 475, 476, 491, 4, 0, 1001, 475, 1,
            475, 108, 4, 475, 10, 1006, 10, 507, 1102, 1, 0, 475, 109, -2, 2105, 1, 0, 0, 109, 4, 2102, 1, -1, 512,
            1207, -3, 0, 10, 1006, 10, 530, 21102, 1, 0, -3, 22102, 1, -3, 1, 22101, 0, -2, 2, 21102, 1, 1, 3, 21101, 0,
            549, 0, 1105, 1, 554, 109, -4, 2105, 1, 0, 109, 5, 1207, -3, 1, 10, 1006, 10, 577, 2207, -4, -2, 10, 1006,
            10, 577, 21202, -4, 1, -4, 1106, 0, 645, 21202, -4, 1, 1, 21201, -3, -1, 2, 21202, -2, 2, 3, 21102, 1, 596,
            0, 1106, 0, 554, 21201, 1, 0, -4, 21101, 1, 0, -1, 2207, -4, -2, 10, 1006, 10, 615, 21101, 0, 0, -1, 22202,
            -2, -1, -2, 2107, 0, -3, 10, 1006, 10, 637, 21201, -1, 0, 1, 21101, 0, 637, 0, 105, 1, 512, 21202, -2, -1,
            -2, 22201, -4, -2, -4, 109, -5, 2105, 1, 0
        };

        private static void Main()
        {
            Part1();
            Part2();
            Console.ReadLine();
        }

        private static void Part1()
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();

            var points = new Dictionary<Point, long>();
            IntcodeComputer computer = new IntcodeComputer(memory);
            computer.InputQueue.Enqueue(0);

            bool running = true;
            long output = 0;
            Point currentPoint = new Point(0, 0);
            points.Add(currentPoint, 0);
            int currentDirection = 0;

            while (running)
            {
                running = computer.RunNext(ref output) == long.MinValue;
                if (computer.OutputQueue.Count == 2)
                {
                    points[currentPoint] = computer.OutputQueue.Dequeue();
                    if (computer.OutputQueue.Dequeue() == 0) currentDirection--;
                    else currentDirection++;
                    if (currentDirection == -1) currentDirection = 3;
                    else if (currentDirection == 4) currentDirection = 0;
                    switch (currentDirection)
                    {
                        case 0:
                            currentPoint.Y++;
                            break;
                        case 1:
                            currentPoint.X++;
                            break;
                        case 2:
                            currentPoint.Y--;
                            break;
                        case 3:
                            currentPoint.X--;
                            break;
                    }
                    if (!points.ContainsKey(currentPoint)) points.Add(currentPoint, 0);
                    computer.InputQueue.Enqueue(points[currentPoint]);
                }
            }
            Console.WriteLine($"Part 1: {points.Count}");

            sw.Stop();
            System.Diagnostics.Debug.WriteLine(sw.Elapsed);
        }

        private static void Part2()
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();

            var points = new Dictionary<Point, long>();
            IntcodeComputer computer = new IntcodeComputer(memory);
            computer.InputQueue.Enqueue(1);

            bool running = true;
            long output = 0;
            Point currentPoint = new Point(0, 0);
            points.Add(currentPoint, 1);
            int currentDirection = 0;

            while (running)
            {
                running = computer.RunNext(ref output) == long.MinValue;
                if (computer.OutputQueue.Count == 2)
                {
                    points[currentPoint] = computer.OutputQueue.Dequeue();
                    if (computer.OutputQueue.Dequeue() == 0) currentDirection--;
                    else currentDirection++;
                    if (currentDirection == -1) currentDirection = 3;
                    else if (currentDirection == 4) currentDirection = 0;
                    switch (currentDirection)
                    {
                        case 0:
                            currentPoint.Y++;
                            break;
                        case 1:
                            currentPoint.X++;
                            break;
                        case 2:
                            currentPoint.Y--;
                            break;
                        case 3:
                            currentPoint.X--;
                            break;
                    }
                    if (!points.ContainsKey(currentPoint)) points.Add(currentPoint, 0);
                    computer.InputQueue.Enqueue(points[currentPoint]);
                }
            }
            Console.WriteLine("Part 2:");
            for (int y = points.Keys.Max(p => p.Y); y >= points.Keys.Min(p => p.Y); y--)
            {
                for (int x = points.Keys.Min(p => p.X); x <= points.Keys.Max(p => p.X); x++)
                {
                    Point point = new Point(x,y);
                    if (points.ContainsKey(point) && points[point]==1)
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }

            sw.Stop();
            System.Diagnostics.Debug.WriteLine(sw.Elapsed);
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
