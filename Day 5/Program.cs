using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Day5
{
    internal class Program
    {
        private static readonly ReadOnlyCollection<int> rom;

        static Program()
        {
            rom = Array.AsReadOnly(new[]
            {
                3, 225, 1, 225, 6, 6, 1100, 1, 238, 225, 104, 0, 1101, 48, 82, 225, 102, 59, 84, 224, 1001, 224, -944,
                224, 4, 224, 102, 8, 223, 223, 101, 6, 224, 224, 1, 223, 224, 223, 1101, 92, 58, 224, 101, -150, 224,
                224, 4, 224, 102, 8, 223, 223, 1001, 224, 3, 224, 1, 224, 223, 223, 1102, 10, 89, 224, 101, -890, 224,
                224, 4, 224, 1002, 223, 8, 223, 1001, 224, 5, 224, 1, 224, 223, 223, 1101, 29, 16, 225, 101, 23, 110,
                224, 1001, 224, -95, 224, 4, 224, 102, 8, 223, 223, 1001, 224, 3, 224, 1, 223, 224, 223, 1102, 75, 72,
                225, 1102, 51, 8, 225, 1102, 26, 16, 225, 1102, 8, 49, 225, 1001, 122, 64, 224, 1001, 224, -113, 224, 4,
                224, 102, 8, 223, 223, 1001, 224, 3, 224, 1, 224, 223, 223, 1102, 55, 72, 225, 1002, 174, 28, 224, 101,
                -896, 224, 224, 4, 224, 1002, 223, 8, 223, 101, 4, 224, 224, 1, 224, 223, 223, 1102, 57, 32, 225, 2,
                113, 117, 224, 101, -1326, 224, 224, 4, 224, 102, 8, 223, 223, 101, 5, 224, 224, 1, 223, 224, 223, 1,
                148, 13, 224, 101, -120, 224, 224, 4, 224, 1002, 223, 8, 223, 101, 7, 224, 224, 1, 223, 224, 223, 4,
                223, 99, 0, 0, 0, 677, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1105, 0, 99999, 1105, 227, 247, 1105, 1, 99999,
                1005, 227, 99999, 1005, 0, 256, 1105, 1, 99999, 1106, 227, 99999, 1106, 0, 265, 1105, 1, 99999, 1006, 0,
                99999, 1006, 227, 274, 1105, 1, 99999, 1105, 1, 280, 1105, 1, 99999, 1, 225, 225, 225, 1101, 294, 0, 0,
                105, 1, 0, 1105, 1, 99999, 1106, 0, 300, 1105, 1, 99999, 1, 225, 225, 225, 1101, 314, 0, 0, 106, 0, 0,
                1105, 1, 99999, 8, 677, 226, 224, 102, 2, 223, 223, 1006, 224, 329, 101, 1, 223, 223, 107, 677, 677,
                224, 1002, 223, 2, 223, 1006, 224, 344, 101, 1, 223, 223, 8, 226, 677, 224, 102, 2, 223, 223, 1006, 224,
                359, 101, 1, 223, 223, 107, 226, 226, 224, 102, 2, 223, 223, 1005, 224, 374, 1001, 223, 1, 223, 1108,
                677, 226, 224, 1002, 223, 2, 223, 1006, 224, 389, 101, 1, 223, 223, 107, 677, 226, 224, 102, 2, 223,
                223, 1006, 224, 404, 1001, 223, 1, 223, 1107, 226, 677, 224, 1002, 223, 2, 223, 1006, 224, 419, 1001,
                223, 1, 223, 108, 677, 677, 224, 102, 2, 223, 223, 1005, 224, 434, 1001, 223, 1, 223, 1008, 677, 226,
                224, 1002, 223, 2, 223, 1006, 224, 449, 1001, 223, 1, 223, 7, 226, 677, 224, 1002, 223, 2, 223, 1006,
                224, 464, 1001, 223, 1, 223, 1007, 677, 677, 224, 102, 2, 223, 223, 1005, 224, 479, 1001, 223, 1, 223,
                1007, 226, 226, 224, 1002, 223, 2, 223, 1005, 224, 494, 1001, 223, 1, 223, 108, 226, 226, 224, 1002,
                223, 2, 223, 1005, 224, 509, 1001, 223, 1, 223, 1007, 226, 677, 224, 1002, 223, 2, 223, 1006, 224, 524,
                101, 1, 223, 223, 1107, 677, 677, 224, 102, 2, 223, 223, 1005, 224, 539, 101, 1, 223, 223, 1107, 677,
                226, 224, 102, 2, 223, 223, 1005, 224, 554, 1001, 223, 1, 223, 108, 677, 226, 224, 1002, 223, 2, 223,
                1006, 224, 569, 1001, 223, 1, 223, 1108, 226, 677, 224, 1002, 223, 2, 223, 1006, 224, 584, 101, 1, 223,
                223, 8, 677, 677, 224, 1002, 223, 2, 223, 1006, 224, 599, 1001, 223, 1, 223, 1008, 226, 226, 224, 102,
                2, 223, 223, 1006, 224, 614, 101, 1, 223, 223, 7, 677, 677, 224, 1002, 223, 2, 223, 1006, 224, 629, 101,
                1, 223, 223, 1008, 677, 677, 224, 102, 2, 223, 223, 1005, 224, 644, 101, 1, 223, 223, 7, 677, 226, 224,
                1002, 223, 2, 223, 1005, 224, 659, 101, 1, 223, 223, 1108, 226, 226, 224, 102, 2, 223, 223, 1006, 224,
                674, 1001, 223, 1, 223, 4, 223, 99, 226
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
            Console.WriteLine("Input 1 for Part 1");
            
            var ram = new int[rom.Count];
            rom.CopyTo(ram, 0);
            Run(ram);
        }

        private static void Part2()
        {
            Console.WriteLine("Input 5 for Part 2");

            var ram = new int[rom.Count];
            rom.CopyTo(ram, 0);
            Run(ram);
        }

        private static int Run(IList<int> intcode)
        {
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
                
                if (new[]{1,2,3,4,5,6,7,8}.Contains(opcode))
                {
                    param1 = param1Mode == 0 ? intcode[intcode[currentPos + 1]] : intcode[currentPos + 1];
                }

                if (new[] {1, 2, 5, 6, 7, 8}.Contains(opcode))
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
                        sw.Stop();
                        string input = Console.ReadLine();
                        sw.Start();
                        intcode[intcode[currentPos + 1]] = int.Parse(input.Trim());
                        currentPos += 2;
                        break;
                    case 4:
                        Console.WriteLine($"Output: {param1}");
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
