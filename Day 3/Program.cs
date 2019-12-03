using System;
using System.Collections.Generic;
using System.IO;

namespace Day3
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Part1();
            Part2();
        }

        private static void Part1()
        {
            var wires = File.ReadAllLines(@"input.txt");

            var segments1 = GetSegments(wires[0]);
            var segments2 = GetSegments(wires[1]);

            int minDistance = int.MaxValue;

            foreach (Segment i in segments1)
            {
                foreach (Segment j in segments2)
                {
                    var intersection = GetIntersection(i, j);
                    if (intersection != null) minDistance = Math.Min(minDistance, GetDistance(intersection.Value));
                }
            }

            System.Diagnostics.Debug.WriteLine($"Distance: {minDistance}");
        }

        private static void Part2()
        {

        }

        private static List<Segment> GetSegments(string wire)
        {
            Point currentPoint = new Point(0,0);
            Point nextPoint = new Point(0,0);
            var returnList = new List<Segment>();
            string direction = "";

            foreach (string movement in wire.Split(','))
            {
                switch (movement[0])
                {
                    case 'L':
                        nextPoint.X -= int.Parse(movement.Substring(1));
                        direction = "H";
                        break;
                    case 'R':
                        nextPoint.X += int.Parse(movement.Substring(1));
                        direction = "H";
                        break;
                    case 'U':
                        nextPoint.Y += int.Parse(movement.Substring(1));
                        direction = "V";
                        break;
                    case 'D':
                        nextPoint.Y -= int.Parse(movement.Substring(1));
                        direction = "V";
                        break;
                }
                returnList.Add(new Segment(currentPoint, nextPoint, direction));
                currentPoint = nextPoint;
            }
            
            return returnList;
        }

        private static Point? GetIntersection(Segment segment1, Segment segment2)
        {
            Segment horizontal;
            Segment vertical;

            if (segment1.Direction == "H" && segment2.Direction == "V")
            {
                horizontal = segment1;
                vertical = segment2;
            }
            else if (segment2.Direction == "H" && segment1.Direction == "V")
            {
                horizontal = segment2;
                vertical = segment1;
            }
            else
            {
                return null;
            }

            if (horizontal.Point1.Y >= Math.Min(vertical.Point1.Y, vertical.Point2.Y) &&
                horizontal.Point1.Y <= Math.Max(vertical.Point1.Y, vertical.Point2.Y) &&
                vertical.Point1.X >= Math.Min(horizontal.Point1.X, horizontal.Point2.X) &&
                vertical.Point1.X <= Math.Max(horizontal.Point1.X, horizontal.Point2.X))
            {
                return new Point(vertical.Point1.X, horizontal.Point1.Y);
            }

            return null;
        }

        private static int GetDistance(Point point)
        {
            return Math.Abs(point.X) + Math.Abs(point.Y);
        }
    }

    internal struct Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
    }

    internal struct Segment
    {
        public Segment(Point point1, Point point2, string direction)
        {
            Point1 = point1;
            Point2 = point2;
            Direction = direction;
        }

        public Point Point1 { get; set; }
        public Point Point2 { get; set; }
        public string Direction { get; set; }
    }
}
