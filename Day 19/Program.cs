using System;
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
                int curX = lastX * 2;
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


            sw.Stop();
            Debug.WriteLine(sw.Elapsed);
        }
    }
}
