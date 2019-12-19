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

            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    IntcodeComputer computer = new IntcodeComputer(input);
                    computer.Run(x, y);
                    if (computer.OutputQueue.Dequeue() == 1) count++;
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
