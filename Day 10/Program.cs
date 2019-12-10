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
            Part1();
            Part2();
            Console.ReadLine();
        }

        private static void Part1()
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
        }

        private static void Part2()
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();

            var input = File.ReadAllLines("input.txt");

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
}
