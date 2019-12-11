using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AdventUtils;

namespace Day7
{
    internal class Program
    {
        private static readonly ReadOnlyCollection<long> rom;

        static Program()
        {
            rom = Array.AsReadOnly(new long[]
            {
                3, 8, 1001, 8, 10, 8, 105, 1, 0, 0, 21, 30, 51, 76, 101, 118, 199, 280, 361, 442, 99999, 3, 9, 102, 5,
                9, 9, 4, 9, 99, 3, 9, 102, 4, 9, 9, 1001, 9, 3, 9, 102, 2, 9, 9, 101, 2, 9, 9, 4, 9, 99, 3, 9, 1002, 9,
                3, 9, 1001, 9, 4, 9, 102, 5, 9, 9, 101, 3, 9, 9, 1002, 9, 3, 9, 4, 9, 99, 3, 9, 101, 5, 9, 9, 102, 4, 9,
                9, 1001, 9, 3, 9, 1002, 9, 2, 9, 101, 4, 9, 9, 4, 9, 99, 3, 9, 1002, 9, 2, 9, 1001, 9, 3, 9, 102, 5, 9,
                9, 4, 9, 99, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9,
                1001, 9, 2, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3,
                9, 101, 2, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 99, 3, 9, 1001, 9, 1, 9, 4,
                9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9,
                4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 101, 1, 9, 9,
                4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 99, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 101, 1,
                9, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 101,
                1, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 99, 3, 9,
                1001, 9, 1, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3,
                9, 101, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9,
                3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 99, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 2, 9,
                4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 1, 9,
                4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 2,
                9, 4, 9, 99
            });
        }

        private static void Main()
        {
            Part1();
            Part2();
            Console.ReadLine();
        }

        private static void Part1()
        {
            IntcodeComputer computer = new IntcodeComputer(rom);
            long max = int.MinValue;
            foreach (var settings in GetPermutations(Enumerable.Range(0, 5), 5))
            {
                long signal = 0;
                foreach (var phase in settings)
                {
                    computer.Run(phase, signal);
                    if (computer.OutputQueue.Any()) signal = computer.OutputQueue.Dequeue();
                }
                max = Math.Max(max, signal);
            }
            Console.WriteLine($"Part 1: {max}");
        }

        private static void Part2()
        {
            long max = long.MinValue;
            foreach (var settings in GetPermutations(Enumerable.Range(5, 5), 5))
            {
                IntcodeComputer computerA = new IntcodeComputer(rom);
                IntcodeComputer computerB = new IntcodeComputer(rom);
                IntcodeComputer computerC = new IntcodeComputer(rom);
                IntcodeComputer computerD = new IntcodeComputer(rom);
                IntcodeComputer computerE = new IntcodeComputer(rom);

                computerA.InputQueue = computerE.OutputQueue;
                computerB.InputQueue = computerA.OutputQueue;
                computerC.InputQueue = computerB.OutputQueue;
                computerD.InputQueue = computerC.OutputQueue;
                computerE.InputQueue = computerD.OutputQueue;

                var settingsArray = settings as int[] ?? settings.ToArray();
                computerA.InputQueue.Enqueue(settingsArray[0]);
                computerB.InputQueue.Enqueue(settingsArray[1]);
                computerC.InputQueue.Enqueue(settingsArray[2]);
                computerD.InputQueue.Enqueue(settingsArray[3]);
                computerE.InputQueue.Enqueue(settingsArray[4]);

                computerA.InputQueue.Enqueue(0);

                bool running = true;
                while (running)
                {
                    if (!computerA.AwaitingInput()) computerA.RunNext();
                    if (!computerB.AwaitingInput()) computerB.RunNext();
                    if (!computerC.AwaitingInput()) computerC.RunNext();
                    if (!computerD.AwaitingInput()) computerD.RunNext();
                    if (!computerE.AwaitingInput()) running = computerE.RunNext() == long.MinValue;
                }

                long signal = computerE.OutputQueue.Dequeue();
                max = Math.Max(max, signal);
            }

            Console.WriteLine($"Part 2: {max}");
        }

        //thanks to SO user Pengyang
        //https://stackoverflow.com/a/10630026/1038840
        //used under CC-BY-SA 4.0
        private static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }
    }
}
