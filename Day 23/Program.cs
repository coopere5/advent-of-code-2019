using System;
using System.Collections.Generic;
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

            var NAT = new Tuple<long, long>(-1, -1);
            var NATHistory = new HashSet<long>();

            var input = File.ReadAllText("input.txt").Split(',').Select(long.Parse).ToArray();
            var computers = new IntcodeComputer[50];
            for (int i = 0; i < computers.Length; i++)
            {
                computers[i] = new IntcodeComputer(input);
                computers[i].InputQueue.Enqueue(i);
            }

            bool running = true;
            int idleCount = 0;
            const int maxIdle = 100;
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
                            NAT = new Tuple<long, long>(x, y);
                        }
                        else
                        {
                            computers[address].InputQueue.Enqueue(x);
                            computers[address].InputQueue.Enqueue(y);
                        }
                    }
                }

                if (NAT.Item1 != -1 && computers.All(c => !c.InputQueue.Any() && !c.OutputQueue.Any()) && idleCount++ >= maxIdle)
                {
                    computers[0].InputQueue.Enqueue(NAT.Item1);
                    computers[0].InputQueue.Enqueue(NAT.Item2);
                    idleCount = 0;
                    if (!NATHistory.Add(NAT.Item2))
                    {
                        Console.WriteLine($"Part 2: {NAT.Item2}");
                        running = false;
                    }
                }
            }

            sw.Stop();
            Debug.WriteLine(sw.Elapsed);
        }
    }
}
