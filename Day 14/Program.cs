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

            var input = File.ReadAllLines("input.txt");
            var reactions = input.Select(line => new Reaction(line)).ToList();
            var inventory = new Dictionary<string, int> { { "ORE", int.MaxValue }, { "FUEL", 0 } };
            var targetInventory = new Dictionary<string, int> { { "ORE", 0 }, { "FUEL", 1 } };

            while (inventory["FUEL"] <= 0)
            {
                foreach (var target in targetInventory.ToDictionary(x => x.Key, x => x.Value))
                {
                    Reaction reaction = FindReactions(reactions, target.Key).FirstOrDefault();
                    if (reaction != null) targetInventory.AddMany(reaction.React(inventory));
                }
            }

            Console.WriteLine(int.MaxValue - inventory["ORE"]);

            sw.Stop();
            Debug.WriteLine(sw.Elapsed);
        }

        private static void Part2()
        {
            Stopwatch sw = Stopwatch.StartNew();

            var input = File.ReadAllLines("input.txt");


            sw.Stop();
            Debug.WriteLine(sw.Elapsed);
        }

        public static List<Reaction> FindReactions(List<Reaction> reactions, string target) => reactions.Where(r => r.Output.ContainsKey(target)).ToList();
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

        public static void AddMany(this Dictionary<string, int> dict, Dictionary<string, int> add)
        {
            foreach (var entry in add)
            {
                dict.AddOrUpdate(entry.Key, entry.Value);
            }
        }
    }
}
