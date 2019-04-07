namespace Battleship.Core.Tests
{
    using System;
    using System.Collections.Generic;

    using Battleship.Core.Components;
    using Battleship.Core.Components.Board;
    using Battleship.Core.Components.Ships;
    using Battleship.Core.Components.UserInterface;
    using Battleship.Core.Enums;
    using Battleship.Core.Models;
    using Battleship.Core.Utilities;

    using NUnit.Framework;

    [TestFixture]
    public class GridGeneratorTests : ComponentBase
    {
        private readonly GridGenerator gridGenerator;

        private readonly IShipRandomiser shipRandomiser;

        public GridGeneratorTests()
        {
            PlayerStats playerStats = new PlayerStats();
            ConsoleHelper consoleHelper = ConsoleHelper.Instance(playerStats);
     
            ISegmentation segmentation = Segmentation.Instance();
            shipRandomiser = ShipRandomiser.Instance();

            gridGenerator = new GridGenerator(segmentation, shipRandomiser, consoleHelper, new List<IShip>());
        }

        [Test]
        public void Board_WhenGridGenerated_ReturnOneHundredSegments()
        {
            // Arrange
            int totalSegments = base.GridDimension * base.GridDimension;
            int? result = 0;

            // Act
            try
            {
                this.gridGenerator.BuildGrid();
                result = gridGenerator.NumberOfSegments;
            }
            catch (IndexOutOfRangeException)
            {
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.Fail($"{e.Message}\n{e.StackTrace}");
            }

            // Assert
            Assert.AreEqual(totalSegments, result);
        }

        [Test]
        public void Board_WhenGridGenerated_ReturnThirteenOccupiedSegments()
        {
            // Arrange
            List<IShip> ships = new List<IShip> { new BattleShip(1), new Destroyer(2), new Destroyer(3), };
            int occupiedSegments = shipRandomiser.GetRandomisedShipCoordinates(ships).Count;

            // Act
            var result = gridGenerator.NumberOfOccupiedSegments;

            // Assert
            Assert.AreNotEqual(occupiedSegments, result);
        }
    }
}