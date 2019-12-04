using System.Linq;

namespace Day4
{
    internal class Program
    {
        private static void Main()
        {
            Part1();
            Part2();
        }

        private static void Part1()
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();

            int passwordCount = Enumerable.Range(183564, 657474 - 183564).Count(password => CheckPassword_Part1(password.ToString()));
            System.Diagnostics.Debug.WriteLine($"Part 1 count: {passwordCount}");

            sw.Stop();
            System.Diagnostics.Debug.WriteLine(sw.Elapsed);
        }

        private static void Part2()
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();

            int passwordCount = Enumerable.Range(183564, 657474 - 183564).Count(password => CheckPassword_Part2(password.ToString()));
            System.Diagnostics.Debug.WriteLine($"Part 2 count: {passwordCount}");

            sw.Stop();
            System.Diagnostics.Debug.WriteLine(sw.Elapsed);
        }

        private static bool CheckPassword_Part1(string password)
        {
            int highest = -1;
            bool hasDouble = false;
            foreach (char character in password)
            {
                int digit = int.Parse(character.ToString());
                if (highest > digit) return false;
                highest = digit;
                if (!hasDouble) hasDouble = password.Count(c => c == character) > 1;    //this works because we have established that it is nondecreasing
            }

            return hasDouble;
        }

        private static bool CheckPassword_Part2(string password)
        {
            int highest = -1;
            bool hasDouble = false;
            foreach (char character in password)
            {
                int digit = int.Parse(character.ToString());
                if (highest > digit) return false;
                highest = digit;
                if (!hasDouble) hasDouble = password.Count(c => c == character) == 2;    //this works because we have established that it is nondecreasing
            }

            return hasDouble;
        }
    }
}
