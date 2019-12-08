using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;

namespace Day7
{
    class Program
    {
        private static readonly ReadOnlyCollection<int> rom;

        static Program()
        {
            rom = Array.AsReadOnly(new[] { 3, 8, 1001, 8, 10, 8, 105, 1, 0, 0, 21, 30, 51, 76, 101, 118, 199, 280, 361, 442, 99999, 3, 9, 102, 5, 9, 9, 4, 9, 99, 3, 9, 102, 4, 9, 9, 1001, 9, 3, 9, 102, 2, 9, 9, 101, 2, 9, 9, 4, 9, 99, 3, 9, 1002, 9, 3, 9, 1001, 9, 4, 9, 102, 5, 9, 9, 101, 3, 9, 9, 1002, 9, 3, 9, 4, 9, 99, 3, 9, 101, 5, 9, 9, 102, 4, 9, 9, 1001, 9, 3, 9, 1002, 9, 2, 9, 101, 4, 9, 9, 4, 9, 99, 3, 9, 1002, 9, 2, 9, 1001, 9, 3, 9, 102, 5, 9, 9, 4, 9, 99, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 99, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 99, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 99, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 99, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 99 });
        }

        private static void Main(string[] args)
        {
            Part1();
            Part2();

            Console.ReadLine();
        }

        private static void Part1()
        {
            var ram = new int[rom.Count];
            rom.CopyTo(ram, 0);
            int signal = 0;
            int max = int.MinValue;
            foreach (var settings in GetPermutations<int>(Enumerable.Range(0, 5), 5))
            {
                signal = 0;
                foreach (var phase in settings)
                {
                    Run(ram, out signal, phase, signal);
                }
                max = Math.Max(max, signal);
            }

            Console.WriteLine("MAX" + max);
        }

        private static void Part2()
        {
            // var ram = new int[rom.Count];
            // rom.CopyTo(ram, 0);
            // int signal = 0;
            // int max = int.MinValue;
            // foreach (var settings in GeneratePermutations<int>(Enumerable.Range(5, 5).ToList()))
            // {
            //     signal = 0;
            //     foreach (var phase in settings)
            //     {
            //         Run(ram, out signal, phase, signal);
            //     }
            //     while (true)
            //     {
            //         Run(ram, out signal, signal);
            //     }
            //     max = Math.Max(max, signal);
            // }

            // Console.WriteLine("MAX" + max);
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

        private static int Run(IList<int> intcode, out int output, params int[] inputParams)
        {
            output = -1;
            var inputList = inputParams.ToList();
            var sw = System.Diagnostics.Stopwatch.StartNew();

            int currentPos = 0;
            while (true)
            {
                string fullOpcode = intcode[currentPos].ToString().PadLeft(5, '0');

                int opcode = int.Parse(fullOpcode.Substring(3, 2));
                int param1Mode = int.Parse(fullOpcode.Substring(2, 1));
                int param2Mode = int.Parse(fullOpcode.Substring(1, 1));
                //int param3Mode = int.Parse(fullOpcode.Substring(0, 1));

                int param1 = int.MinValue;
                int param2 = int.MinValue;
                //int param3 = int.MinValue;

                if (new[] { 1, 2, 3, 4, 5, 6, 7, 8 }.Contains(opcode))
                {
                    param1 = param1Mode == 0 ? intcode[intcode[currentPos + 1]] : intcode[currentPos + 1];
                }

                if (new[] { 1, 2, 5, 6, 7, 8 }.Contains(opcode))
                {
                    param2 = param2Mode == 0 ? intcode[intcode[currentPos + 2]] : intcode[currentPos + 2];
                }

                //if (new[] {1, 2, 7, 8}.Contains(opcode))
                //{
                //    param3 = param3Mode == 0 ? intcode[intcode[currentPos + 3]] : intcode[currentPos + 3];
                //}

                switch (opcode)
                {
                    case 1:
                        intcode[intcode[currentPos + 3]] = param1 + param2;
                        currentPos += 4;
                        break;
                    case 2:
                        intcode[intcode[currentPos + 3]] = param1 * param2;
                        currentPos += 4;
                        break;
                    case 3:
                        Console.Write("Input: ");
                        string input = "";
                        if (inputList.Any())
                        {
                            input = inputList[0].ToString();
                            Console.WriteLine(input);
                            inputList.RemoveAt(0);
                        }
                        else
                        {
                            sw.Stop();
                            input = Console.ReadLine();
                            sw.Start();
                        }
                        intcode[intcode[currentPos + 1]] = int.Parse(input.Trim());
                        currentPos += 2;
                        break;
                    case 4:
                        Console.WriteLine($"Output: {param1}");
                        output = param1;
                        currentPos += 2;
                        break;
                    case 5:
                        if (param1 != 0) currentPos = param2;
                        else currentPos += 3;
                        break;
                    case 6:
                        if (param1 == 0) currentPos = param2;
                        else currentPos += 3;
                        break;
                    case 7:
                        intcode[intcode[currentPos + 3]] = param1 < param2 ? 1 : 0;
                        currentPos += 4;
                        break;
                    case 8:
                        intcode[intcode[currentPos + 3]] = param1 == param2 ? 1 : 0;
                        currentPos += 4;
                        break;
                    case 99:
                        sw.Stop();
                        System.Diagnostics.Debug.WriteLine(sw.Elapsed);
                        return intcode[0];
                    default:
                        throw new Exception($"Unknown opcode: {opcode} at {currentPos}");
                }
            }
        }
    }
}
