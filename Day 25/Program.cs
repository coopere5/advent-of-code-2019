using System;
using AdventUtils;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Day25
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
            var sw = Stopwatch.StartNew();

            var input = File.ReadAllText("input.txt").Split(',').Select(long.Parse);
            var computer = new IntcodeComputer(input);
            computer.AsciiInputMode = true;

            var running = true;
            while (running)
            {
                running = computer.RunNext() == long.MinValue;
                while (computer.OutputQueue.Any())
                {
                    Console.Write((char)computer.OutputQueue.Dequeue());
                }
            }

            sw.Stop();
            Debug.WriteLine(sw.Elapsed);
        }

        private static void Part2()
        {
            Stopwatch sw = Stopwatch.StartNew();

            var input = File.ReadAllText("input.txt").Split(',').Select(long.Parse);
            var computer = new IntcodeComputer(input);

            sw.Stop();
            Debug.WriteLine(sw.Elapsed);
        }
    }
}
