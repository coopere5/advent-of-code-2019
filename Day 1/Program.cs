using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Day1
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

            var input = File.ReadAllLines("input.txt").Select(int.Parse).ToArray();
            var sum = 0;

            foreach (var line in input)
            {
                sum += line / 3 - 2;
            }

            Console.WriteLine($"Part 1: {sum}");

            sw.Stop();
            Debug.WriteLine(sw.Elapsed);
        }

        private static void Part2()
        {
            Stopwatch sw = Stopwatch.StartNew();

            var input = File.ReadAllLines("input.txt").Select(int.Parse).ToArray();
            var sum = 0;

            while (input.Any(i => i > 0))
            {
                for (int i = 0; i < input.Length; i++)
                {
                    int next = input[i] / 3 - 2;
                    if (next > 0) sum += next;
                    input[i] = next;
                }

            }

            Console.WriteLine($"Part 2: {sum}");

            sw.Stop();
            Debug.WriteLine(sw.Elapsed);
        }
    }
}
