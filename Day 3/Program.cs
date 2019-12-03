using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day3
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
            var wires = File.ReadAllLines("input.txt");

            var segments1 = GetSegments(wires[0]);
            var segments2 = GetSegments(wires[1]);

            int minDistance = int.MaxValue;

            foreach (Segment segment1 in segments1)
            {
                foreach (Segment segment2 in segments2)
                {
                    var intersection = GetIntersection(segment1, segment2);
                    if (intersection != null) minDistance = Math.Min(minDistance, GetDistance(intersection.Value));
                }
            }

            System.Diagnostics.Debug.WriteLine($"Part 1 Distance: {minDistance}");
        }

        private static void Part2()
        {
            var wires = File.ReadAllLines("input.txt");

            var segments1 = GetSegments(wires[0]);
            var segments2 = GetSegments(wires[1]);

            int minSteps = int.MaxValue;

            for (int i = 0; i < segments1.Count; i++)
            {
                Segment segment1 = segments1[i];
                for (int j = 0; j < segments2.Count; j++)
                {
                    Segment segment2 = segments2[j];

                    var intersection = GetIntersection(segment1, segment2);
                    if (intersection != null)
                    {
                        minSteps = Math.Min(minSteps, GetSteps(segments1.GetRange(0, i + 1), intersection.Value) + GetSteps(segments2.GetRange(0, j + 1), intersection.Value));
                    }
                }
            }

            System.Diagnostics.Debug.WriteLine($"Part 2 Steps: {minSteps}");
        }

        private static List<Segment> GetSegments(string wire)
        {
            Point currentPoint = new Point(0, 0);
            var returnList = new List<Segment>();
            foreach (string movement in wire.Split(','))
            {
                Segment nextSegment = GetNextSegment(currentPoint, movement);
                returnList.Add(nextSegment);
                currentPoint = nextSegment.Point2;
            }
            return returnList;
        }

        private static Segment GetNextSegment(Point? currentPoint, string movement)
        {
            if (currentPoint == null) currentPoint = new Point(0, 0);
            Point nextPoint = currentPoint.Value;
            string direction;
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
                default:
                    throw new Exception($"Unknown movement direction: {movement[0]}");
            }
            return new Segment(currentPoint.Value, nextPoint, direction);
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
            return GetDistance(point, new Point(0, 0));
        }

        private static int GetDistance(Point point1, Point point2)
        {
            return Math.Abs(point1.X - point2.X) + Math.Abs(point1.Y - point2.Y);
        }

        private static int GetSteps(IList<Segment> segments, Point intersection)
        {
            Segment last = segments.Last();
            segments[segments.Count - 1] = new Segment(last.Point1, intersection, last.Direction);

            return segments.Sum(segment => GetDistance(segment.Point1, segment.Point2));
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
