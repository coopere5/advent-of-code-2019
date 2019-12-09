using System;
using System.Linq;
using System.IO;

namespace Day8
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
            var sw = System.Diagnostics.Stopwatch.StartNew();
            string input = File.ReadAllText("input.txt");

            var image = Enumerable.Range(0, input.Length / (25 * 6)).Select(i => input.Substring(i * (25 * 6), 25 * 6));

            int minZeroes = int.MaxValue;
            int currentProduct = 0;
            foreach (string layer in image)
            {
                int numZeroes = layer.Count(c => c == '0');
                minZeroes = Math.Min(minZeroes, numZeroes);
                if (numZeroes == minZeroes)
                {
                    currentProduct = layer.Count(c => c == '1') * layer.Count(c => c == '2');
                }
            }
            Console.WriteLine($"Part 1: {currentProduct}");
            sw.Stop();
            System.Diagnostics.Debug.WriteLine(sw.Elapsed);
        }

        private static void Part2()
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();

            string input = File.ReadAllText("input.txt");

            var inputImage = Enumerable.Range(0, input.Length / (25 * 6)).Select(i => input.Substring(i * (25 * 6), 25 * 6));
            var outputArray = new string('2', 25*6).ToCharArray();
            foreach (string layer in inputImage)
            {
                for (int i = 0; i < layer.Length; i++)
                {
                    if (outputArray[i] == '2') outputArray[i] = layer[i];
                }
            }

            string outputString = new string(outputArray);
            outputString = outputString.Replace('0', ' ');
            var outputFormatted = Enumerable.Range(0, outputString.Length / 25).Select(i => outputString.Substring(i * 25, 25));
            Console.WriteLine("Part 2");
            foreach (string line in outputFormatted)
            {
                Console.WriteLine(line);
            }

            sw.Stop();
            System.Diagnostics.Debug.WriteLine(sw.Elapsed);
        }
    }
}
