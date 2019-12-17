using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Day16
{
    internal class Program
    {
        private static readonly int[] sin = { 0, 1, 0, -1 };

        private static Dictionary<int, int[]> sequences = new Dictionary<int, int[]>();

        private static void Main()
        {
            Part1();
            Part2();
            Console.ReadLine();
        }

        private static void Part1()
        {
            Stopwatch sw = Stopwatch.StartNew();

            const string input = "59773419794631560412886746550049210714854107066028081032096591759575145680294995770741204955183395640103527371801225795364363411455113236683168088750631442993123053909358252440339859092431844641600092736006758954422097244486920945182483159023820538645717611051770509314159895220529097322723261391627686997403783043710213655074108451646685558064317469095295303320622883691266307865809481566214524686422834824930414730886697237161697731339757655485312568793531202988525963494119232351266908405705634244498096660057021101738706453735025060225814133166491989584616948876879383198021336484629381888934600383957019607807995278899293254143523702000576897358";
            var inputArray = input.Select(c => int.Parse(c.ToString())).ToArray();
            int[] outputArray = null;
            sequences = new Dictionary<int, int[]>();

            for (int phase = 0; phase < 100; phase++)
            {
                outputArray = FFT(inputArray);
                inputArray = outputArray;
            }

            if (outputArray != null) Console.WriteLine(string.Join("", outputArray).Substring(0,8));

            sw.Stop();
            Debug.WriteLine(sw.Elapsed);
        }

        private static void Part2()
        {
            //string input = "59773419794631560412886746550049210714854107066028081032096591759575145680294995770741204955183395640103527371801225795364363411455113236683168088750631442993123053909358252440339859092431844641600092736006758954422097244486920945182483159023820538645717611051770509314159895220529097322723261391627686997403783043710213655074108451646685558064317469095295303320622883691266307865809481566214524686422834824930414730886697237161697731339757655485312568793531202988525963494119232351266908405705634244498096660057021101738706453735025060225814133166491989584616948876879383198021336484629381888934600383957019607807995278899293254143523702000576897358";
            sequences = new Dictionary<int, int[]>();
        }

        private static int[] GetSequence(int pos, int len)
        {
            if (sequences.ContainsKey(pos)) return sequences[pos];
            var outList = new List<int>();
            for (int i = 0; i <= len; i++)
            {
                for (int j = 0; j <= pos; j++)
                {
                    outList.Add(sin[i % 4]);
                }
            }

            outList.RemoveAt(0);
            sequences.Add(pos, outList.ToArray());
            return sequences[pos];
        }

        private static int[] FFT(IReadOnlyList<int> inputArray)
        {
            var outputArray = new int[inputArray.Count];
            for (int i = 0; i < inputArray.Count; i++)
            {
                var seq = GetSequence(i, inputArray.Count);
                for (int j = 0; j < inputArray.Count; j++)
                {
                    outputArray[i] += inputArray[j] * seq[j];
                }

                outputArray[i] = Math.Abs(outputArray[i] % 10);
            }

            return outputArray;
        }
    }
}
