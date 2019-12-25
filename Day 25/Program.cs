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
            computer.AddASCIIInput("west\n");
            computer.AddASCIIInput("south\n");
            computer.AddASCIIInput("east\n");
            computer.AddASCIIInput("south\n");
            computer.AddASCIIInput("west\n");
            computer.AddASCIIInput("west\n");
            computer.AddASCIIInput("take astrolabe\n");
            computer.AddASCIIInput("east\n");
            computer.AddASCIIInput("east\n");
            computer.AddASCIIInput("north\n");
            computer.AddASCIIInput("take monolith\n");
            computer.AddASCIIInput("west\n");
            computer.AddASCIIInput("north\n");
            computer.AddASCIIInput("west\n");
            computer.AddASCIIInput("north\n");
            computer.AddASCIIInput("take tambourine\n");
            computer.AddASCIIInput("south\n");
            computer.AddASCIIInput("west\n");
            computer.AddASCIIInput("take dark matter\n");
            computer.AddASCIIInput("west\n");
            computer.AddASCIIInput("north\n");
            computer.AddASCIIInput("north\n");
        }
    }
}
