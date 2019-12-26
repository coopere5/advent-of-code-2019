using System;
using AdventUtils;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Day23
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
            var computers = new IntcodeComputer[50];
            for (int i = 0; i < computers.Length; i++)
            {
                computers[i] = new IntcodeComputer(input);
                computers[i].InputQueue.Enqueue(i);
            }

            bool running = true;
            while (running)
            {
                foreach (IntcodeComputer computer in computers)
                {
                    if (computer.AwaitingInput()) computer.InputQueue.Enqueue(-1);
                    computer.RunNext();
                    if (computer.OutputQueue.Count == 3)
                    {
                        long address = computer.OutputQueue.Dequeue();
                        long x = computer.OutputQueue.Dequeue();
                        long y = computer.OutputQueue.Dequeue();

                        if (address == 255)
                        {
                            Console.WriteLine($"Part 1: {y}");
                            running = false;
                            break;
                        }

                        computers[address].InputQueue.Enqueue(x);
                        computers[address].InputQueue.Enqueue(y);
                    }
                }
            }

            sw.Stop();
            Debug.WriteLine(sw.Elapsed);
        }

        private static void Part2()
        {
            Stopwatch sw = Stopwatch.StartNew();

            sw.Stop();
            Debug.WriteLine(sw.Elapsed);
        }
    }
}
