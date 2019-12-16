using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using AdventUtils;

namespace Day15
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
            Point droidPos = new Point(0, 0);
            Point nextPos = new Point(0, 0);
            long currentDirection = 1;

            bool running = true;
            while (running)
            {
                running = computer.RunNext() == long.MinValue;
                if (computer.OutputQueue.Count == 1)
                {
                    long result = computer.OutputQueue.Dequeue();
                    screen[nextPos] = result;
                    switch (result)
                    {
                        case 0:
                            nextPos = droidPos;
                            currentDirection++;
                            if (currentDirection > 4) currentDirection = 1;
                            break;
                        case 1:
                            droidPos = nextPos;
                            break;
                        case 2:
                            droidPos = nextPos;
                            Console.WriteLine(droidPos);
                            break;
                    }

                    //if (result == 2) break;
                }
                if (computer.AwaitingInput())
                {
                    //computer.InputQueue.Enqueue(currentDirection);
                    //switch (currentDirection)
                    //{
                    //    case 1:
                    //        nextPos.Y++;
                    //        break;
                    //    case 2:
                    //        nextPos.Y--;
                    //        break;
                    //    case 3:
                    //        nextPos.X--;
                    //        break;
                    //    case 4:
                    //        nextPos.X++;
                    //        break;
                    //}
                    Console.Clear();
                    Console.WriteLine();
                    for (long y = screen.Keys.Max(p => p.Y); y >= screen.Keys.Min(p => p.Y); y--)
                    {
                        for (long x = screen.Keys.Min(p => p.X); x <= screen.Keys.Max(p => p.X); x++)
                        {
                            Point point = new Point(x, y);
                            
                            if (point.Equals(droidPos))
                            {
                                Console.Write("D");
                                continue;
                            }

                            if (point.Equals(new Point(0, 0)))
                            {
                                Console.Write("&");
                                continue;
                            }
                            if (!screen.ContainsKey(point))
                            {
                                Console.Write(" ");
                                continue;
                            }
                            switch (screen[point])
                            {
                                case 0:
                                    Console.Write("#");
                                    break;
                                case 1:
                                    Console.Write(".");
                                    break;
                                case 2:
                                    Console.Write("*");
                                    break;
                            }
                        }
                        Console.WriteLine();
                    }

                    while (computer.AwaitingInput())
                    {
                        ConsoleKeyInfo i = Console.ReadKey();

                        switch (i.Key)
                        {
                            case ConsoleKey.LeftArrow:
                                computer.InputQueue.Enqueue(3);
                                nextPos.X--;
                                break;
                            case ConsoleKey.RightArrow:
                                computer.InputQueue.Enqueue(4);
                                nextPos.X++;
                                break;
                            case ConsoleKey.UpArrow:
                                computer.InputQueue.Enqueue(1);
                                nextPos.Y++;
                                break;
                            case ConsoleKey.DownArrow:
                                computer.InputQueue.Enqueue(2);
                                nextPos.Y--;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            sw.Stop();
            Debug.WriteLine(sw.Elapsed);
        }

        private static void Part2()
        {
            Stopwatch sw = Stopwatch.StartNew();

            var input = File.ReadAllText("input.txt").Split(',').Select(long.Parse);
            IntcodeComputer computer = new IntcodeComputer(input);


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
