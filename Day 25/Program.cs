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
        }

        private static void Part1()
        {
            var sw = Stopwatch.StartNew();

            var input = File.ReadAllText("input.txt").Split(',').Select(long.Parse);
            var computer = new IntcodeComputer(input);
            computer.AsciiInputMode = true;
            TAS(computer);

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

        private static void TAS(IntcodeComputer computer)
        {
            string tas = @"west
                           south
                           east
                           south
                           west
                           west
                           take astrolabe
                           east
                           east
                           north
                           take monolith
                           west
                           north
                           west
                           north
                           take tambourine
                           south
                           west
                           take dark matter
                           west
                           north
                           north";
            computer.AddAsciiInputBlock(tas);
        }
    }
}
