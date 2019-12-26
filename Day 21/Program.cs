using System;
using AdventUtils;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Day21
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
            IntcodeComputer computer = new IntcodeComputer(input) { AsciiInputMode = true };

            string program = @"NOT A J
                               NOT B T
                               OR T J
                               NOT C T
                               OR T J
                               AND D J
                               WALK";

            computer.AddAsciiInputBlock(program);

            computer.Run();
            while (computer.OutputQueue.Any())
            {
                long value = computer.OutputQueue.Dequeue();
                if (value > char.MaxValue || value < char.MinValue) Console.WriteLine($"Part 1: {value}");
                else Console.Write((char)value);
            }

            sw.Stop();
            Debug.WriteLine(sw.Elapsed);
        }

        private static void Part2()
        {
            Stopwatch sw = Stopwatch.StartNew();

            var input = File.ReadAllText("input.txt").Split(',').Select(long.Parse);
            IntcodeComputer computer = new IntcodeComputer(input) { AsciiInputMode = true };

            string program = @"NOT A J
                               NOT B T
                               OR T J
                               NOT C T
                               OR T J
                               AND D J
                               RUN";

            computer.AddAsciiInputBlock(program);

            computer.Run();
            while (computer.OutputQueue.Any())
            {
                long value = computer.OutputQueue.Dequeue();
                if (value > char.MaxValue || value < char.MinValue) Console.WriteLine($"Part 2: {value}");
                else Console.Write((char)value);
            }

            sw.Stop();
            Debug.WriteLine(sw.Elapsed);
        }
    }
}
