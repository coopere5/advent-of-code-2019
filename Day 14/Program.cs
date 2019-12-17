using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Day14
{
    internal class Program
    {
        private static void Main()
        {
            Part1();
            Part2();
            Console.ReadKey();
        }

        private static void Part1()
        {
            Stopwatch sw = Stopwatch.StartNew();

            Refinery refinery = new Refinery();

            refinery.Unrefine(new KeyValuePair<string, int>("FUEL", 1));

            Console.WriteLine($"Part 1: {refinery.GetOre()}");

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

    public class Refinery
    {
        public List<Reaction> Reactions;
        public Dictionary<string, int> Inventory;
        public Dictionary<string, int> Target;

        private readonly string[] _basicRefinedMaterials;

        public Refinery()
        {
            Reactions = File.ReadAllLines("input.txt").Select(line => new Reaction(line)).ToList();
            Inventory = new Dictionary<string, int>();
            Target = new Dictionary<string, int> { { "FUEL", 1 } };
            _basicRefinedMaterials = Reactions.Where(r => r.Input.ContainsKey("ORE"))
                                             .Select(r => r.Output)
                                             .SelectMany(output => output.Keys)
                                             .ToArray();
        }

        public void Unrefine(KeyValuePair<string, int> targetMaterial)
        {
            Reaction rxn = Reactions.First(r => r.Output.ContainsKey(targetMaterial.Key));
            int trials = (int)Math.Ceiling(targetMaterial.Value * 1.0 / rxn.Output[targetMaterial.Key]);
            var inputs = rxn.Input;
            foreach (var input in inputs)
            {
                var multipliedInput = new KeyValuePair<string, int>(input.Key, input.Value * trials);
                if (_basicRefinedMaterials.Contains(multipliedInput.Key))
                {
                    Inventory.IncrementOrUpdate(multipliedInput.Key, multipliedInput.Value);
                }
                else
                {
                    Unrefine(multipliedInput);
                }
            }
        }

        public int GetOre()
        {
            int fuel = 0;
            foreach (var mat in Inventory)
            {
                Reaction rxn = Reactions.First(r => r.Output.ContainsKey(mat.Key));
                int trials = (int)Math.Ceiling(mat.Value * 1.0 / rxn.Output[mat.Key]);
                fuel += rxn.Input.First().Value * trials;
            }

            return fuel;
        }
    }

    public class Reaction
    {
        public Dictionary<string, int> Input;
        public Dictionary<string, int> Output;

        public Reaction()
        {
            Input = new Dictionary<string, int>();
            Output = new Dictionary<string, int>();
        }

        public Reaction(string line) : this()
        {
            line = line.Replace('>', ' ').Trim();
            var split = line.Split('=');
            var inputs = split[0].Split(',').Select(input => input.Trim());
            var outputs = split[1].Split(',').Select(output => output.Trim());

            foreach (string input in inputs)
            {
                var splitInput = input.Split(' ');
                Input.Add(splitInput[1], int.Parse(splitInput[0]));
            }

            foreach (string output in outputs)
            {
                var splitOutput = output.Split(' ');
                Output.Add(splitOutput[1], int.Parse(splitOutput[0]));
            }
        }

        public bool CanReact(Dictionary<string, int> inventory)
        {
            foreach (var input in Input)
            {
                if (!inventory.ContainsKey(input.Key) || inventory[input.Key] < input.Value) return false;
            }

            return true;
        }

        public Dictionary<string, int> React(Dictionary<string, int> inventory)
        {
            var rv = new Dictionary<string, int>();
            if (CanReact(inventory))
            {
                foreach (var input in Input)
                {
                    inventory[input.Key] -= input.Value;
                }

                foreach (var output in Output)
                {
                    if (!inventory.ContainsKey(output.Key)) inventory.Add(output.Key, 0);
                    inventory[output.Key] += output.Value;
                }
            }
            else
            {
                foreach (var input in Input)
                {
                    if (!inventory.ContainsKey(input.Key)) inventory.Add(input.Key, 0);
                    if (inventory[input.Key] < input.Value) rv.Add(input.Key, input.Value - inventory[input.Key]);
                }
            }

            return rv;
        }
    }

    public static class DictUtils
    {
        public static void AddOrUpdate(this Dictionary<string, int> dict, string key, int value)
        {
            if (!dict.ContainsKey(key)) dict.Add(key, 0);
            dict[key] = value;
        }

        public static void AddOrUpdateMany(this Dictionary<string, int> dict, Dictionary<string, int> add)
        {
            foreach (var entry in add)
            {
                dict.AddOrUpdate(entry.Key, entry.Value);
            }
        }

        public static void IncrementOrUpdate(this Dictionary<string, int> dict, string key, int value)
        {
            if (!dict.ContainsKey(key)) dict.Add(key, 0);
            dict[key] += value;
        }
    }
}
