namespace Battleship.Core.Components.Board
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Battleship.Core.Models;
    using Battleship.Core.Utilities;

    public class Segmentation : ComponentBase, ISegmentation
    {
        private static volatile Segmentation instance;

        private readonly SortedList<Coordinate, Segment> segmentations;

        protected Segmentation()
        {
            segmentations = new SortedList<Coordinate, Segment>(new CoordinateComparer());
        }

        public static Segmentation Instance()
        {
            if (instance == null)
            {
                lock (SyncObject)
                {
                    if (instance == null)
                    {
                        instance = new Segmentation();
                    }
                }
            }

            return instance;
        }

        #region ISegmentation Members

        public void AddSegment(Coordinate coordinate, Segment segment)
        {
            if (!BattleshipExtensions.IsSegmentWithInGridRange(coordinate.X, coordinate.Y))
            {
                throw new IndexOutOfRangeException();
            }


            segmentations.Add(coordinate, new Segment(segment.Character));
        }

        public void UpdateSegment(Coordinate coordinate, Segment segment)
        {
            if (!BattleshipExtensions.IsSegmentWithInGridRange(coordinate.X, coordinate.Y))
            {
                throw new IndexOutOfRangeException();
            }
         
            Segment item = segmentations.FirstOrDefault(q => q.Key.X == coordinate.X && q.Key.Y == coordinate.Y).Value;

            if (item != null)
            {
                item.IsEmpty = false;
                item.Character = segment.Character;

                if (segment.Ship != null)
                {
                    item.Ship = segment.Ship;
                    item.ShipDirection = segment.ShipDirection;
                }
            }
        }

        public void UpdateSegmentRange(SortedList<Coordinate, Segment> segments)
        {
            foreach (var segment in segments)
            {
                if (!BattleshipExtensions.IsSegmentWithInGridRange(segment.Key.X, segment.Key.Y))
                {
                    throw new IndexOutOfRangeException();
                }

                Segment item = segmentations.FirstOrDefault(q => q.Key.X == segment.Key.X && q.Key.Y == segment.Key.Y).Value;

                if (item != null)
                {
                    if (item.IsEmpty & segment.Value.IsEmpty)
                    {
                        throw new ArgumentException();
                    }

                    item.IsEmpty = false;
                    if (segment.Value.Ship != null)
                    {
                        item.Ship = segment.Value.Ship;
                        item.ShipDirection = segment.Value.ShipDirection;
                    }
                }
            }
        }

        public SortedList<Coordinate, Segment> GetSegments()
        {
            return segmentations;
        }

        public Segment GetSegment(int x, int y)
        {
            return segmentations.FirstOrDefault(q => q.Key.X == x && q.Key.Y == y).Value;
        }

        #endregion
    }
}