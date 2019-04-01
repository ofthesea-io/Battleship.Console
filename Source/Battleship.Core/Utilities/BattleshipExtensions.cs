namespace Battleship.Core.Utilities
{
    using System.Collections.Generic;
    using System.Linq;

    using Battleship.Core.Models;

    public static class BattleshipExtensions
    {
        private static readonly int Index = 1;

        private static readonly int XInitialPoint = 65;

        private static readonly int GridDimension = 10;

        public static bool IsSegmentAvailable<TSource>(this IList<TSource> source, int x, int y)
        {
            bool result = true;

            if (source.Count != 0)
            {
                List<Segment> segments = (List<Segment>)source;
                bool isTaken = segments.Any(q => q.PositionX == x && q.PositionY == y && !q.IsEmpty);
                if (isTaken)
                {
                    result = false;
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