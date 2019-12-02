using System;

namespace Day2
{
    class Program
    {
        private static int[] rom = {
            1, 0, 0, 3, 1, 1, 2, 3, 1, 3, 4, 3, 1, 5, 0, 3, 2, 13, 1, 19, 1, 10, 19, 23, 1, 23, 9, 27, 1, 5, 27, 31, 2,
            31, 13, 35, 1, 35, 5, 39, 1, 39, 5, 43, 2, 13, 43, 47, 2, 47, 10, 51, 1, 51, 6, 55, 2, 55, 9, 59, 1, 59, 5,
            63, 1, 63, 13, 67, 2, 67, 6, 71, 1, 71, 5, 75, 1, 75, 5, 79, 1, 79, 9, 83, 1, 10, 83, 87, 1, 87, 10, 91, 1,
            91, 9, 95, 1, 10, 95, 99, 1, 10, 99, 103, 2, 103, 10, 107, 1, 107, 9, 111, 2, 6, 111, 115, 1, 5, 115, 119,
            2, 119, 13, 123, 1, 6, 123, 127, 2, 9, 127, 131, 1, 131, 5, 135, 1, 135, 13, 139, 1, 139, 10, 143, 1, 2,
            143, 147, 1, 147, 10, 0, 99, 2, 0, 14, 0
        };

        static void Main(string[] args)
        {   
            //Day1();
            Day2();
        }

        private static void Day1()
        {
            int[] ram = new int[rom.Length];
            Array.Copy(rom, ram, rom.Length);

            int noun = 12;
            int verb = 2;

            int output = Run(ram, noun, verb);
            System.Diagnostics.Debug.WriteLine(output);
        }

        private static void Day2()
        {
            int[] ram = new int[rom.Length];

            for (int i = 0; i < 99; i++)
            {
                for (int j = 0; j < 99; j++)
                {
                    Array.Copy(rom, ram, rom.Length);
                    int output = Run(ram, i, j);
                    if (output == 19690720)
                    {
                        System.Diagnostics.Debug.WriteLine(100*i+j);
                        return;
                    }
                }
            }
        }

        private static int Run(int[] input, int noun, int verb)
        {
            input[1] = noun;
            input[2] = verb;
            int currentPos = 0;
            while (true)
            {
                int opcode = input[currentPos];
                int x1 = input[input[currentPos + 1]];
                int x2 = input[input[currentPos + 2]];
                switch (opcode)
                {
                    case 1:
                        input[input[currentPos + 3]] = x1 + x2;
                        currentPos += 4;
                        break;
                    case 2:
                        input[input[currentPos + 3]] = x1 * x2;
                        currentPos += 4;
                        break;
                    case 99:
                        return input[0];
                    default:
                        throw new Exception($"Unknown opcode: {opcode} at {currentPos}");
                }
            }
        }
    }
}
