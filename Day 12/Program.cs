using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day12
{
    internal class Program
    {
        //input:
        // <x=-13, y=-13, z=-13>
        // <x=5, y=-8, z=3>
        // <x=-6, y=-10, z=-3>
        // <x=0, y=5, z=-5>

        private static void Main()
        {
            Part1();
            Part2();
            Console.ReadKey();
        }

        private static void Part1()
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();

            var moons = new List<Moon>();
            var input = File.ReadAllLines("input.txt");
            foreach (var line in input)
            {
                var split = line.Split(',');
                Moon moon = new Moon
                {
                    PositionX = int.Parse(split[0].Split('=')[1]),
                    PositionY = int.Parse(split[1].Split('=')[1]),
                    PositionZ = int.Parse(split[2].Split('=')[1].Replace('>', ' '))
                };
                moons.Add(moon);
            }

            for (int steps = 0; steps < 1000; steps++)
            {
                foreach (Moon moon in moons)
                {
                    foreach (Moon interactingMoon in moons)
                    {
                        moon.ApplyGravity(interactingMoon);
                    }
                }

                foreach (Moon moon in moons)
                {
                    moon.ApplyVelocity();
                }
            }

            int energy = 0;
            foreach (Moon moon in moons)
            {
                energy += moon.CalculateEnergy();
            }

            Console.WriteLine($"Part 1: {energy}");

            sw.Stop();
            System.Diagnostics.Debug.WriteLine(sw.Elapsed);
        }

        private static void Part2()
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();

            sw.Stop();
            System.Diagnostics.Debug.WriteLine(sw.Elapsed);
        }
    }

    public class Moon
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int PositionZ { get; set; }

        public int VelocityX { get; set; }
        public int VelocityY { get; set; }
        public int VelocityZ { get; set; }

        public void ApplyGravity(Moon interactingMoon)
        {
            if (interactingMoon.PositionX > PositionX) VelocityX++;
            else if (interactingMoon.PositionX < PositionX) VelocityX--;

            if (interactingMoon.PositionY > PositionY) VelocityY++;
            else if (interactingMoon.PositionY < PositionY) VelocityY--;

            if (interactingMoon.PositionZ > PositionZ) VelocityZ++;
            else if (interactingMoon.PositionZ < PositionZ) VelocityZ--;
        }

        public void ApplyVelocity()
        {
            PositionX += VelocityX;
            PositionY += VelocityY;
            PositionZ += VelocityZ;
        }

        public int CalculateEnergy()
        {
            int potentialEnergy = Math.Abs(PositionX) + Math.Abs(PositionY) + Math.Abs(PositionZ);
            int kineticEnergy = Math.Abs(VelocityX) + Math.Abs(VelocityY) + Math.Abs(VelocityZ);
            int totalEnergy = potentialEnergy * kineticEnergy;
            return totalEnergy;
        }
    }
}
