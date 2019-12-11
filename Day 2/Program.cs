using System;
using System.Collections.ObjectModel;
using AdventUtils;

namespace Day2
{
    internal class Program
    {
        private static readonly ReadOnlyCollection<long> rom;

        static Program()
        {
            rom = Array.AsReadOnly(new long[]
            {
                1, 0, 0, 3, 1, 1, 2, 3, 1, 3, 4, 3, 1, 5, 0, 3, 2, 13, 1, 19, 1, 10, 19, 23, 1, 23, 9, 27, 1, 5, 27, 31,
                2, 31, 13, 35, 1, 35, 5, 39, 1, 39, 5, 43, 2, 13, 43, 47, 2, 47, 10, 51, 1, 51, 6, 55, 2, 55, 9, 59, 1,
                59, 5, 63, 1, 63, 13, 67, 2, 67, 6, 71, 1, 71, 5, 75, 1, 75, 5, 79, 1, 79, 9, 83, 1, 10, 83, 87, 1, 87,
                10, 91, 1, 91, 9, 95, 1, 10, 95, 99, 1, 10, 99, 103, 2, 103, 10, 107, 1, 107, 9, 111, 2, 6, 111, 115, 1,
                5, 115, 119, 2, 119, 13, 123, 1, 6, 123, 127, 2, 9, 127, 131, 1, 131, 5, 135, 1, 135, 13, 139, 1, 139,
                10, 143, 1, 2, 143, 147, 1, 147, 10, 0, 99, 2, 0, 14, 0
            });
        }

        private static void Main()
        {
            Part1();
            Part2();
        }

        private static void Part1()
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();
            var ram = new long[rom.Count];
            rom.CopyTo(ram, 0);
            ram[1] = 12;
            ram[2] = 2;
            IntcodeComputer computer = new IntcodeComputer(ram);
            long output = computer.Run();
            System.Diagnostics.Debug.WriteLine($"Part 1: {output}");
            sw.Stop();
            System.Diagnostics.Debug.WriteLine(sw.Elapsed);
        }

        private static void Part2()
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();
            var ram = new long[rom.Count];
            for (int i = 0; i < 99; i++)
            {
                for (int j = 0; j < 99; j++)
                {
                    rom.CopyTo(ram, 0);
                    ram[1] = i;
                    ram[2] = j;
                    IntcodeComputer computer = new IntcodeComputer(ram);
                    long output = computer.Run();
                    if (output == 19690720)
                    {
                        System.Diagnostics.Debug.WriteLine($"Part 2: {100 * i + j}");
                        sw.Stop();
                        System.Diagnostics.Debug.WriteLine(sw.Elapsed);
                        return;
                    }
                }
            }
        }
    }
}
