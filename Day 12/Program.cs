using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day12
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

            var input = File.ReadAllLines("input.txt");
            var moons = input.Select(line => line.Split(','))
                             .Select(split => new Moon(int.Parse(split[0].Split('=')[1]), int.Parse(split[1].Split('=')[1]), int.Parse(split[2].Split('=')[1].Replace('>', ' '))))
                             .ToList();

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

            var input = File.ReadAllLines("input.txt");
            var moons = input.Select(line => line.Split(','))
                             .Select(split => new Moon(int.Parse(split[0].Split('=')[1]), int.Parse(split[1].Split('=')[1]), int.Parse(split[2].Split('=')[1].Replace('>', ' '))))
                             .ToList();

            long stepsX = 0;
            while (true)
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
                stepsX++;
                if (moons.All(moon => moon.Velocity.X == 0)) break;
            }
            stepsX *= 2;

            long stepsY = 0;
            while (true)
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
                stepsY++;
                if (moons.All(moon => moon.Velocity.Y == 0)) break;
            }
            stepsY *= 2;

            long stepsZ = 0;
            while (true)
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
                stepsZ++;
                if (moons.All(moon => moon.Velocity.Z == 0)) break;
            }
            stepsZ *= 2;

            long steps = MathUtils.lcm(MathUtils.lcm(stepsX, stepsY), stepsZ);

            Console.WriteLine($"Part 2: {steps * 2}");

            sw.Stop();
            System.Diagnostics.Debug.WriteLine(sw.Elapsed);
        }
    }

    public class MathUtils
    {
        public static long gcf(long x, long y)
        {
            return (y == 0) ? x : gcf(y, x % y);
        }

        public static long lcm(long x, long y)
        {
            return (x == 0 || y == 0) ? 0 : Math.Abs(x * y) / gcf(x, y);
        }
    }

    public class Moon
    {
        public Vector Position;
        public Vector Velocity;

        public readonly Vector InitialPosition;
        public readonly Vector InitialVelocity;

        public Moon() : this(0, 0, 0)
        {
        }

        public Moon(int posX, int posY, int posZ)
        {
            Position = new Vector(posX, posY, posZ);
            Velocity = new Vector(0, 0, 0);

            InitialPosition = Position;
            InitialVelocity = Velocity;
        }

        public void ApplyGravity(Moon interactingMoon)
        {
            if (interactingMoon.Position.X > Position.X) Velocity.X++;
            else if (interactingMoon.Position.X < Position.X) Velocity.X--;

            if (interactingMoon.Position.Y > Position.Y) Velocity.Y++;
            else if (interactingMoon.Position.Y < Position.Y) Velocity.Y--;

            if (interactingMoon.Position.Z > Position.Z) Velocity.Z++;
            else if (interactingMoon.Position.Z < Position.Z) Velocity.Z--;
        }

        public void ApplyVelocity()
        {
            Position.X += Velocity.X;
            Position.Y += Velocity.Y;
            Position.Z += Velocity.Z;
        }

        public int CalculateEnergy()
        {
            int potentialEnergy = Math.Abs(Position.X) + Math.Abs(Position.Y) + Math.Abs(Position.Z);
            int kineticEnergy = Math.Abs(Velocity.X) + Math.Abs(Velocity.Y) + Math.Abs(Velocity.Z);
            int totalEnergy = potentialEnergy * kineticEnergy;
            return totalEnergy;
        }

        public bool AtInitial() => InitialPosition.Equals(Position) && InitialVelocity.Equals(Velocity);
    }

    public struct Vector
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public Vector(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
