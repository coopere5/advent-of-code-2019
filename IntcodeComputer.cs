using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventUtils
{
    public class IntcodeComputer
    {
        private readonly IDictionary<long, long> Memory;
        private long RelativeBase;
        private long InstructionPtr;

        public bool PrintOutput = false;

        public Queue<long> InputQueue { get; set; }
        public Queue<long> OutputQueue { get; set; }

        public IntcodeComputer()
        {
            RelativeBase = 0;
            InstructionPtr = 0;

            InputQueue = new Queue<long>();
            OutputQueue = new Queue<long>();
        }

        public IntcodeComputer(IEnumerable<long> intcode) : this()
        {
            long idx = 0;
            Memory = intcode.ToDictionary(key => idx++, value => value);
        }

        public IntcodeComputer(IDictionary<long, long> intcode) : this()
        {
            Memory = intcode;
        }

        public long Run()
        {
            return Run(out long output);
        }

        public long Run(out long output, params long[] inputParams)
        {
            long rv = 0;
            RelativeBase = 0;
            InstructionPtr = 0;
            output = 0;
            Array.ForEach(inputParams, e => InputQueue.Enqueue(e));

            bool running = true;
            while (running)
            {
                rv = RunNext();
                running = rv == long.MinValue;
            }

            if (OutputQueue.Any()) output = OutputQueue.Peek();

            return rv;
        }

        public long RunNext()
        {
            string fullOpcode = Memory[InstructionPtr].ToString().PadLeft(5, '0');
            long opcode = long.Parse(fullOpcode.Substring(3, 2));
            long param1Mode = long.Parse(fullOpcode.Substring(2, 1));
            long param2Mode = long.Parse(fullOpcode.Substring(1, 1));
            long param3Mode = long.Parse(fullOpcode.Substring(0, 1));

            long param1 = 0;
            long param2 = 0;
            long param3 = 0;

            long param1Key = 0;
            long param2Key = 0;
            long param3Key = 0;

            if (new long[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains(opcode))
            {
                if (!Memory.ContainsKey(InstructionPtr + 1)) Memory.Add(InstructionPtr + 1, 0);
                switch (param1Mode)
                {
                    case 0:
                        param1Key = Memory[InstructionPtr + 1];
                        if (!Memory.ContainsKey(param1Key)) Memory.Add(param1Key, 0);
                        param1 = Memory[param1Key];
                        break;
                    case 1:
                        param1 = Memory[InstructionPtr + 1];
                        break;
                    case 2:
                        param1Key = RelativeBase + Memory[InstructionPtr + 1];
                        if (!Memory.ContainsKey(param1Key)) Memory.Add(param1Key, 0);
                        param1 = Memory[param1Key];
                        break;
                }
            }

            if (new long[] { 1, 2, 5, 6, 7, 8 }.Contains(opcode))
            {
                if (!Memory.ContainsKey(InstructionPtr + 2)) Memory.Add(InstructionPtr + 2, 0);
                switch (param2Mode)
                {
                    case 0:
                        param2Key = Memory[InstructionPtr + 2];
                        if (!Memory.ContainsKey(param2Key)) Memory.Add(param2Key, 0);
                        param2 = Memory[param2Key];
                        break;
                    case 1:
                        param2 = Memory[InstructionPtr + 2];
                        break;
                    case 2:
                        param2Key = RelativeBase + Memory[InstructionPtr + 2];
                        if (!Memory.ContainsKey(param2Key)) Memory.Add(param2Key, 0);
                        param2 = Memory[param2Key];
                        break;
                }
            }

            if (new long[] { 1, 2, 7, 8 }.Contains(opcode))
            {
                if (!Memory.ContainsKey(InstructionPtr + 3)) Memory.Add(InstructionPtr + 3, 0);
                switch (param3Mode)
                {
                    case 0:
                        param3Key = Memory[InstructionPtr + 3];
                        if (!Memory.ContainsKey(param3Key)) Memory.Add(param3Key, 0);
                        param3 = Memory[param3Key];
                        break;
                    case 1:
                        param3 = Memory[InstructionPtr + 3];
                        break;
                    case 2:
                        param3Key = RelativeBase + Memory[InstructionPtr + 3];
                        if (!Memory.ContainsKey(param3Key)) Memory.Add(param3Key, 0);
                        param3 = Memory[param3Key];
                        break;
                }
            }

            switch (opcode)
            {
                case 1:
                    Memory[param3Key] = param1 + param2;
                    InstructionPtr += 4;
                    break;
                case 2:
                    Memory[param3Key] = param1 * param2;
                    InstructionPtr += 4;
                    break;
                case 3:
                    string input;
                    if (InputQueue.Any())
                    {
                        input = InputQueue.Dequeue().ToString();
                        if (PrintOutput) Console.WriteLine($"Input: {input}");
                    }
                    else
                    {
                        Console.Write("Input: ");
                        input = Console.ReadLine();
                    }
                    Memory[param1Key] = int.Parse(input.Trim());
                    InstructionPtr += 2;
                    break;
                case 4:
                    if (PrintOutput) Console.WriteLine($"Output: {param1}");
                    OutputQueue.Enqueue(param1);
                    InstructionPtr += 2;
                    break;
                case 5:
                    if (param1 != 0) InstructionPtr = param2;
                    else InstructionPtr += 3;
                    break;
                case 6:
                    if (param1 == 0) InstructionPtr = param2;
                    else InstructionPtr += 3;
                    break;
                case 7:
                    Memory[param3Key] = param1 < param2 ? 1 : 0;
                    InstructionPtr += 4;
                    break;
                case 8:
                    Memory[param3Key] = param1 == param2 ? 1 : 0;
                    InstructionPtr += 4;
                    break;
                case 9:
                    RelativeBase += param1;
                    InstructionPtr += 2;
                    break;
                case 99:
                    return Memory[0];
                default:
                    throw new Exception($"Unknown opcode: {opcode} at {InstructionPtr}");
            }

            return long.MinValue;
        }

        public long PeekNext()
        {
            string fullOpcode = Memory[InstructionPtr].ToString().PadLeft(5, '0');
            long opcode = long.Parse(fullOpcode.Substring(3, 2));
            return opcode;
        }
    }
}
