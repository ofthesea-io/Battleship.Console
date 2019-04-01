namespace Battleship.Core.Tests
{
    using System.Collections.Generic;

    using Battleship.Core.Components;
    using Battleship.Core.Components.Ships;
    using Battleship.Core.Enums;
    using Battleship.Core.Models;
    using Battleship.Core.Utilities;

    using NUnit.Framework;

    [TestFixture]
    public class BattleshipExtensionTests : ComponentBase
    {
        [Test]
        public void IsSegmentAvailable_WhenShipInterceptsHorizontally_ReturnFalse()
        {
            // Arrange
            IShip firstDestroyer = new Destroyer(1);
            IShip secondDestroyer = new Destroyer(2);

            List<Segment> segments = new List<Segment>
             {
                 new Segment(69, 1, ShipDirection.Vertical, firstDestroyer),
                 new Segment(69, 2, ShipDirection.Vertical, firstDestroyer),
                 new Segment(69, 3, ShipDirection.Vertical, firstDestroyer),
                 new Segment(69, 4, ShipDirection.Vertical, firstDestroyer)
             }; // list of current segments that is not available 

            // Horizontal Intercepting ship
            Segment segment = new Segment(69, 2, ShipDirection.Horizontal, secondDestroyer); 

            // Act
            bool result = segments.IsSegmentAvailable(segment.PositionX, segment.PositionY);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsSegmentAvailable_WhenShipDoesNotInterceptHorizontally_ReturnTrue()
        {
            // Arrange
            IShip firstDestroyer = new Destroyer(1);
            IShip secondDestroyer = new Destroyer(2);

            List<Segment> segments = new List<Segment>
                                     {
                                         new Segment(69, 1, ShipDirection.Vertical, firstDestroyer),
                                         new Segment(69, 2, ShipDirection.Vertical, firstDestroyer),
                                         new Segment(69, 3, ShipDirection.Vertical, firstDestroyer),
                                         new Segment(69, 4, ShipDirection.Vertical, firstDestroyer)
                                     }; // list of current segments that is not available

            Segment segment = new Segment(69, 5, ShipDirection.Horizontal, secondDestroyer); // pass point

            // Act
            bool result = segments.IsSegmentAvailable(segment.PositionX, segment.PositionY);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsSegmentAvailable_WhenShipInterceptsVertically_ReturnFalse()
        {
            // Arrange
            IShip firstDestroyer = new Destroyer(1);
            IShip secondDestroyer = new Destroyer(2);

            List<Segment> segments = new List<Segment>
             {
                 new Segment(68, 3, ShipDirection.Horizontal, firstDestroyer),
                 new Segment(69, 3, ShipDirection.Horizontal, firstDestroyer),
                 new Segment(70, 3, ShipDirection.Horizontal, firstDestroyer),
                 new Segment(71, 3, ShipDirection.Horizontal, firstDestroyer)
             }; // list of current segments that is not available

            Segment segment = new Segment(69, 3, ShipDirection.Vertical, secondDestroyer); // fail point

            // Act
            bool result = segments.IsSegmentAvailable(segment.PositionX, segment.PositionY);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsSegmentAvailable_WhenShipDoesNotInterceptVertically_ReturnTrue()
        {
            // Arrange
            IShip firstDestroyer = new Destroyer(1);
            IShip secondDestroyer = new Destroyer(2);

            List<Segment> segments = new List<Segment>
                                     {
                                         new Segment(68, 3, ShipDirection.Horizontal, firstDestroyer),
                                         new Segment(69, 3, ShipDirection.Horizontal, firstDestroyer),
                                         new Segment(70, 3, ShipDirection.Horizontal, firstDestroyer),
                                         new Segment(71, 3, ShipDirection.Horizontal, firstDestroyer)
                                     }; // list of current segments that is not available

            Segment segment = new Segment(73, 3, ShipDirection.Vertical, secondDestroyer); // pass point

            // Act
            bool result = segments.IsSegmentAvailable(segment.PositionX, segment.PositionY);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsSegmentAvailable_WhenTwoHorizontalShipsIntercept_ReturnFalse()
        {
            // Arrange
            IShip firstDestroyer = new Destroyer(1);
            IShip secondDestroyer = new Destroyer(2);

            List<Segment> segments = new List<Segment>
             {
                 new Segment(68, 2, ShipDirection.Vertical, firstDestroyer),
                 new Segment(69, 2, ShipDirection.Vertical, firstDestroyer),
                 new Segment(70, 2, ShipDirection.Vertical, firstDestroyer),
                 new Segment(71, 2, ShipDirection.Vertical, firstDestroyer)
             }; // list of current segments that is not available 

            // Horizontal Intercepting ship
            Segment segment = new Segment(71, 2, ShipDirection.Horizontal, secondDestroyer);

            // Act
            bool result = segments.IsSegmentAvailable(segment.PositionX, segment.PositionY);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsSegmentAvailable_WhenTwoHorizontalShipsDoesNotIntercept_ReturnTrue()
        {
            // Arrange
            IShip firstDestroyer = new Destroyer(1);
            IShip secondDestroyer = new Destroyer(2);

            List<Segment> segments = new List<Segment>
                                     {
                                         new Segment(68, 2, ShipDirection.Vertical, firstDestroyer),
                                         new Segment(69, 2, ShipDirection.Vertical, firstDestroyer),
                                         new Segment(70, 2, ShipDirection.Vertical, firstDestroyer),
                                         new Segment(71, 2, ShipDirection.Vertical, firstDestroyer)
                                     }; // list of current segments that is not available 

            Segment segment = new Segment(67, 2, ShipDirection.Horizontal, secondDestroyer); // pass point

            // Act
            bool result = segments.IsSegmentAvailable(segment.PositionX, segment.PositionY);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsSegmentWithInGridRange_WhenXAxisIsGreaterThanDimension_ReturnFalse()
        {
            // Arrange
            IShip firstDestroyer = new Destroyer(1);

            int x = XInitialPoint + GridDimension + Index;
            int y = Index;

            Segment segment = new Segment(x, y, ShipDirection.Horizontal, firstDestroyer);
            
            // Act
            bool result = BattleshipExtensions.IsSegmentWithInGridRange(segment.PositionX, segment.PositionY);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsSegmentWithInGridRange_WhenXAxisIsLessThanXIndex_ReturnFalse()
        {
            // Arrange
            IShip firstDestroyer = new Destroyer(1);
            int x = XInitialPoint - Index;
            int y = Index;

            Segment segment = new Segment(x, y, ShipDirection.Horizontal, firstDestroyer);

            // Act
            bool result = BattleshipExtensions.IsSegmentWithInGridRange(segment.PositionX, segment.PositionY);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsSegmentWithInGridRange_WhenYAxisIsGreaterThanDimension_ReturnFalse()
        {
            // Arrange
            IShip firstDestroyer = new Destroyer(1);
            int x = XInitialPoint;
            int y = Index + GridDimension;

            Segment segment = new Segment(x, y, ShipDirection.Horizontal, firstDestroyer);

            // Act
            bool result = BattleshipExtensions.IsSegmentWithInGridRange(segment.PositionX, segment.PositionY);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsSegmentWithInGridRange_WhenYAxisIsLessThanYAxisIndex_ReturnFalse()
        {
            // Arrange
            IShip firstDestroyer = new Destroyer(1);
            int x = XInitialPoint;
            int y = 0;

            Segment segment = new Segment(x, y, ShipDirection.Horizontal, firstDestroyer);

            // Act
            bool result = BattleshipExtensions.IsSegmentWithInGridRange(segment.PositionX, segment.PositionY);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsSegmentWithInGridRange_WhenXAxisIsWithinDimension_ReturnTrue()
        {
            // Arrange
            IShip firstDestroyer = new Destroyer(1);
            int x = XInitialPoint;
            int y = GridDimension;
            Segment segment = new Segment(x, y, ShipDirection.Horizontal, firstDestroyer);

            // Act
            bool result = BattleshipExtensions.IsSegmentWithInGridRange(segment.PositionX, segment.PositionY);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsSegmentWithInGridRange_WhenYAxisIsWithinDimension_ReturnTrue()
        {
            // Arrange
            IShip firstDestroyer = new Destroyer(1);
            int x = XInitialPoint;
            int y = GridDimension;
            Segment segment = new Segment(x, y, ShipDirection.Horizontal, firstDestroyer);

            // Act
            bool result = BattleshipExtensions.IsSegmentWithInGridRange(segment.PositionX, segment.PositionY);
            // Assert
            Assert.IsTrue(result);
        }
    }
}