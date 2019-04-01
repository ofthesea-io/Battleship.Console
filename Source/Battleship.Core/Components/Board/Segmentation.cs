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

        private readonly List<Segment> segmentations;

        protected Segmentation()
        {
            segmentations = new List<Segment>();
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

        public void AddSegment(Segment segment)
        {
            if (!BattleshipExtensions.IsSegmentWithInGridRange(segment.PositionX, segment.PositionY))
            {
                throw new IndexOutOfRangeException();
            }

            segmentations.Add(new Segment(segment.PositionX, segment.PositionY, segment.Character));
        }

        public void UpdateSegment(Segment segment)
        {
            if (!BattleshipExtensions.IsSegmentWithInGridRange(segment.PositionX, segment.PositionY))
            {
                throw new IndexOutOfRangeException();
            }
         
            Segment item = segmentations.FirstOrDefault(q => q.PositionX == segment.PositionX && q.PositionY == segment.PositionY);

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

        public void UpdateSegmentRange(IList<Segment> segments)
        {
            foreach (Segment segment in segments)
            {
                if (!BattleshipExtensions.IsSegmentWithInGridRange(segment.PositionX, segment.PositionY))
                {
                    throw new IndexOutOfRangeException();
                }

                Segment item = segmentations.FirstOrDefault(q => q.PositionX == segment.PositionX && q.PositionY == segment.PositionY);

                if (item != null)
                {
                    if (item.IsEmpty & segment.IsEmpty)
                    {
                        throw new ArgumentException();
                    }

                    item.IsEmpty = false;
                    if (segment.Ship != null)
                    {
                        item.Ship = segment.Ship;
                        item.ShipDirection = segment.ShipDirection;
                    }
                }
            }
        }

        public IList<Segment> GetSegments()
        {
            return segmentations;
        }

        public Segment GetSegment(int x, int y)
        {
            return segmentations.FirstOrDefault(q => q.PositionX == x && q.PositionY == y);
        }

        #endregion
    }
}