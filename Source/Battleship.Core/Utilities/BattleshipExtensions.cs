namespace Battleship.Core.Utilities
{
    using System.Collections.Generic;
    using System.Linq;

    using Battleship.Core.Models;

    public static class BattleshipExtensions 
    {
        private static readonly int Index = 1;

        private static readonly int XInitialPoint = 65;

        private static readonly int GridDimension = 26;

        public static bool IsSegmentAvailable<TSource>(this IEnumerable<TSource> source, int x, int y)
        {
            bool result = true;

            if (source.Any())
            {
                SortedDictionary<Coordinate, Segment> segments = (SortedDictionary<Coordinate, Segment>)source;
                bool isTaken = segments.Any(q => q.Key.X == x && q.Key.Y == y && !q.Value.IsEmpty);
                if (isTaken)
                {
                    result = false;
                }
            }

            return result;
        }

        public static bool AddRange<TSource>(this IEnumerable<TSource> source, SortedDictionary<Coordinate, Segment> range)
        {
            bool result = true;

            if (range.Any())
            {
                SortedDictionary<Coordinate, Segment> segments = (SortedDictionary<Coordinate, Segment>)source;

                if (segments != null)
                {
                    foreach (KeyValuePair<Coordinate, Segment> pair in range)
                    {
                        segments.Add(pair.Key, pair.Value);
                    }
                }
              
            }

            return result;
        }

        public static bool IsSegmentWithInGridRange(int x, int y)
        {
            bool result = false;

            int maxXLength = XInitialPoint + GridDimension - Index;

            // Test the X and Y Axis coordinates
            if (x >= XInitialPoint && x <= maxXLength && y >= Index && y <= GridDimension)
            {
                result = true;
            }

            return result;
        }
    }
}