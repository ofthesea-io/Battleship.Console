namespace Battleship.Core.Tests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using Battleship.Core.Components;
    using Battleship.Core.Components.Board;
    using Battleship.Core.Components.Ships;
    using Battleship.Core.Enums;
    using Battleship.Core.Models;
    using Battleship.Core.Utilities;

    using NUnit.Framework;

    [TestFixture]
    public class SegmentationTests : ComponentBase
    {
        private readonly IShipRandomiser shipRandomiser;

        private readonly ISegmentation segmentation;

        public SegmentationTests()
        {
            shipRandomiser = ShipRandomiser.Instance();
            segmentation = Segmentation.Instance();
        }

        [Test]
        public void Add_AddSegmentYAxisNotInGridDimension_ThrowIndexOutOfRangeException()
        {
            // Arrange 
            int x = XInitialPoint;
            int y = GridDimension + Index;
            Segment segment = new Segment(x, y, Water);

            // Act and Assert
            try
            {
                segmentation.AddSegment(segment);
                Assert.Fail();
            }
            catch (IndexOutOfRangeException)
            {
                Assert.Pass();
            }
        }

        [Test]
        public void UpdateSegment_SegmentOutOfRange_ThrowIndexOutOfRangeException()
        {
            // Arrange 
            int x = XInitialPoint;
            int y = GridDimension + Index;

            // Act and Assert
            try
            {
                var segment = new Segment(x, y, ShipDirection.Vertical, new Destroyer(1));
                segmentation.UpdateSegment(segment);
                Assert.Fail();
            }
            catch (IndexOutOfRangeException)
            {
                Assert.Pass();
            }
        }

        [Test]
        public void UpdateSegmentRange_CantUpdateAEmptySegmentWithAnotherEmptySegment_TrowArgumentException()
        {
            // Arrange 
            int x = XInitialPoint;
            int y = GridDimension;

            // Act and Assert
            try
            {
                segmentation.AddSegment(new Segment(x, y, Water));
                IList<Segment> range = new List<Segment>()
                   {
                       new Segment(x, y, Water)
                   };
                segmentation.UpdateSegmentRange(range);
                Assert.Fail();
            }
            catch (ArgumentException)
            {
                Assert.Pass();
            }
        }

        [Test]
        public void UpdateSegmentRange_CantUpdateFilledSegmentWithEmptySegment_TrowArgumentException()
        {
            // Arrange 
            int x = XInitialPoint;
            int y = GridDimension;

            // Act and Assert
            try
            {
                segmentation.AddSegment(new Segment(x, y, ShipDirection.Horizontal, new BattleShip(1)));
                IList<Segment> range = new List<Segment>()
                                       {
                                           new Segment(x, y, Water)
                                       };
                segmentation.UpdateSegmentRange(range);
                Assert.Fail();
            }
            catch (ArgumentException)
            {
                Assert.Pass();
            }
        }

        [Test]
        public void UpdateSegmentRange_CanUpdateSegmentWithFilledSegment_ReturnsVoid()
        {
            // Arrange 
            int x = XInitialPoint;
            int y = GridDimension;

            // Act and Assert
            try
            {
                segmentation.AddSegment(new Segment(x, y, Water));
                IList<Segment> range = new List<Segment>()
                                       {
                                           new Segment(x, y, ShipDirection.Horizontal, new BattleShip(1))
                                       };
                segmentation.UpdateSegmentRange(range);
                Assert.Pass();
            }
            catch (ArgumentException)
            {
               Assert.Fail();
            }
        }
    }
}