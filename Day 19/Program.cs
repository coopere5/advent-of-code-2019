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

            for (int y = 0; y < 50; y++)
            {
                bool found = false;
                for (int x = 0; x < 50; x++)
                {
                    IntcodeComputer computer = new IntcodeComputer(input);
                    computer.Run(x, y);
                    if (computer.OutputQueue.Dequeue() == 1)
                    {
                        found = true;
                        count++;
                    }
                    else if (found)
                    {
                        break;
                    }
                }
            }

            Console.WriteLine($"Part 1: {count}");

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
}
