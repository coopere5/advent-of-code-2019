using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day10
{
    internal class Program
    {
        private static void Main()
        {
            Point station = Part1();
            Part2(station);
            Console.ReadLine();
        }

        private static Point Part1()
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();

            var input = File.ReadAllLines("input.txt");
            var map = new HashSet<Point>();
            int y = 0;
            foreach (string line in input)
            {
                int x = 0;
                foreach (char c in line)
                {
                    if (c == '#') map.Add(new Point(x, y));
                    x++;
                }
                y++;
            }

            int maxDetected = int.MinValue;
            Point detectedAt = new Point(0, 0);
            foreach (Point possibleStation in map)
            {
                var angles = new HashSet<double>();
                foreach (Point asteroid in map.Except(new[] { possibleStation }))
                {
                    angles.Add(GeometryUtil.GetAngle(possibleStation, asteroid));
                }
                maxDetected = Math.Max(maxDetected, angles.Count);
                if (maxDetected == angles.Count) detectedAt = possibleStation;
            }
            Console.WriteLine($"Part 1: {maxDetected} at {detectedAt}");

            sw.Stop();
            System.Diagnostics.Debug.WriteLine(sw.Elapsed);

            return detectedAt;
        }

        private static void Part2(Point station)
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();

            var input = File.ReadAllLines("input.txt");

            var map = new HashSet<Point>();
            int y = 0;
            foreach (string line in input)
            {
                int x = 0;
                foreach (char c in line)
                {
                    if (c == '#') map.Add(new Point(x, y));
                    x++;
                }
                y++;
            }

            map.Remove(station);
            var polarMap = new HashSet<Asteroid>();
            var angles = new SortedList<double, int>();
            int idx = 0;
            foreach (Point p in map)
            {
                double angle = GeometryUtil.GetAngle(station, p);
                var asteroid = new Asteroid(GeometryUtil.GetPolarCoordinate(station, p), p);
                polarMap.Add(asteroid);
                if (!angles.ContainsKey(angle))
                {
                    angles.Add(angle, idx);
                    idx++;
                }
            }

            int numDestroyed = 0;
            int curAngle = angles.IndexOfKey(-Math.PI / 2);
            while (polarMap.Any())
            {
                Asteroid toRemove = polarMap.Where(p => Math.Abs(p.Polar.Theta - angles.Keys[curAngle]) < double.Epsilon).OrderBy(p => p.Polar.R).FirstOrDefault();
                if (polarMap.Remove(toRemove))
                {
                    numDestroyed++;
                    if (numDestroyed == 200)
                    {
                        Console.WriteLine($"Part 2: {toRemove.Rectangular}, {toRemove.Rectangular.X * 100 + toRemove.Rectangular.Y}");
                    }
                }

                curAngle++;
                if (curAngle >= angles.Count) curAngle = 0;
            }

            sw.Stop();
            System.Diagnostics.Debug.WriteLine(sw.Elapsed);
        }
    }

    public class GeometryUtil
    {
        public static double GetAngle(Point p1, Point p2)
        {
            double dx = p2.X - p1.X;
            double dy = p2.Y - p1.Y;
            double angle = Math.Atan2(dy, dx);
            return angle % (2 * Math.PI);
        }

        public static double GetRadius(Point p1, Point p2)
        {
            double dx = p2.X - p1.X;
            double dy = p2.Y - p1.Y;
            double radius = Math.Sqrt(dx * dx + dy * dy);
            return radius;
        }

        public static PolarCoordinate GetPolarCoordinate(Point p1, Point p2)
        {
            return new PolarCoordinate(GetRadius(p1, p2), GetAngle(p1, p2));
        }
    }

    public struct Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public override string ToString()
        {
            return $"({X},{Y})";
        }
    }

    public struct PolarCoordinate
    {
        public PolarCoordinate(double r, double theta)
        {
            R = r;
            Theta = theta;
        }

        public double R { get; set; }
        public double Theta { get; set; }

        public override string ToString()
        {
            return $"({R},{Theta})";
        }
    }

    public struct Asteroid
    {
        public Asteroid(PolarCoordinate polar, Point rectangular)
        {
            Polar = polar;
            Rectangular = rectangular;
        }

        public PolarCoordinate Polar { get; set; }
        public Point Rectangular { get; set; }
    }
}
