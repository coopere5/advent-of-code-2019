using System;
using System.Collections.Generic;
using System.IO;

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

            var moons = new List<Moon>();
            var input = File.ReadAllLines("input.txt");
            foreach (var line in input)
            {
                var split = line.Split(',');
                Moon moon = new Moon
                {
                    Position =
                    {
                        X = int.Parse(split[0].Split('=')[1]),
                        Y = int.Parse(split[1].Split('=')[1]),
                        Z = int.Parse(split[2].Split('=')[1].Replace('>', ' '))
                    }
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

            var moons = new List<Moon>();
            var input = File.ReadAllLines("input.txt");
            foreach (var line in input)
            {
                var split = line.Split(',');
                Moon moon = new Moon
                {
                    Position =
                    {
                        X = int.Parse(split[0].Split('=')[1]),
                        Y = int.Parse(split[1].Split('=')[1]),
                        Z = int.Parse(split[2].Split('=')[1].Replace('>', ' '))
                    }
                };
                moons.Add(moon);
            }

            var snapshots = new HashSet<Snapshot>();
            bool running = true;
            long steps = 0;
            while (running) //this absolutely will not work efficiently, ran for 7 minutes before giving up last time
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

                steps++;
                running = snapshots.Add(new Snapshot(moons[0], moons[1], moons[2], moons[3]));

                if (steps % 10000 == 0) System.Diagnostics.Debug.WriteLine($"{steps}: {sw.Elapsed}");
            }
            Console.WriteLine($"Part 2: {steps - 1}");

            sw.Stop();
            System.Diagnostics.Debug.WriteLine(sw.Elapsed);
        }
    }

    public class Moon
    {
        public Vector Position;
        public Vector Velocity;

        public Moon() : this(0, 0, 0)
        {
        }

        public Moon(int posX, int posY, int posZ)
        {
            Position = new Vector(posX, posY, posZ);
            Velocity = new Vector(0, 0, 0);
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

    public struct Snapshot
    {
        private Vector position1;
        private Vector velocity1;

        private Vector position2;
        private Vector velocity2;

        private Vector position3;
        private Vector velocity3;

        private Vector position4;
        private Vector velocity4;

        public Snapshot(Vector p1, Vector v1, Vector p2, Vector v2, Vector p3, Vector v3, Vector p4, Vector v4)
        {
            position1 = p1;
            velocity1 = v1;
            position2 = p2;
            velocity2 = v2;
            position3 = p3;
            velocity3 = v3;
            position4 = p4;
            velocity4 = v4;
        }

        public Snapshot(Moon moon1, Moon moon2, Moon moon3, Moon moon4) : this(moon1.Position, moon1.Velocity,
            moon2.Position, moon2.Velocity, moon3.Position, moon3.Velocity, moon4.Position, moon4.Velocity)
        {
        }
    }
}
