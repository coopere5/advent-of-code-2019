using System;
using System.IO;
using System.Diagnostics;
using System.Linq;

namespace Day22
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

            var input = File.ReadAllLines("input.txt");
            var deck = new Deck(10007);
            foreach (var line in input)
            {
                deck.HandleInput(line);
            }
            int pos = Array.IndexOf(deck.Cards, 2019);
            Console.WriteLine($"Part 1: {pos}");

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

    public class Deck
    {
        public int[] Cards { get; private set; }

        public Deck(int capacity)
        {
            Cards = Enumerable.Range(0, capacity).ToArray();
        }

        public void HandleInput(string line)
        {
            if (line.Contains("deal into new stack"))
            {
                Deal();
            }
            else if (line.Contains("cut"))
            {
                var arg = int.Parse(line.Replace("cut ", ""));
                Cut(arg);
            }
            else if (line.Contains("deal with increment"))
            {
                var arg = int.Parse(line.Replace("deal with increment ", ""));
                DealWithIncrement(arg);
            }
            else
            {
                throw new InvalidOperationException($"Invalid operation {line}");
            }
        }

        public void Deal()
        {
            Array.Reverse(Cards);
        }

        public void Cut(int offset)
        {
            if (offset >= 0)
            {
                var cut = Cards.Take(offset);
                var remaining = Cards.Skip(offset);
                Cards = remaining.Concat(cut).ToArray();
            }
            else
            {
                offset = Math.Abs(offset);
                var cut = Cards.Skip(Cards.Length - offset);
                var remaining = Cards.Take(Cards.Length - offset);
                Cards = cut.Concat(remaining).ToArray();
            }
        }

        public void DealWithIncrement(int increment)
        {
            var table = new int[Cards.Length];
            int idx = 0;
            foreach (var card in Cards)
            {
                table[idx] = card;
                idx += increment;
                if (idx >= Cards.Length) idx -= Cards.Length;
            }
            Cards = table;
        }

        public override string ToString()
        {
            string s = "";
            foreach (var x in Cards)
            {
                var concat = $"{x} ";
                s = string.Concat(s, concat);
            }
            return s;
        }
    }
}
