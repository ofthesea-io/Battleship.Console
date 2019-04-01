namespace Battleship.Core.Tests
{
    using System;

    using Battleship.Core.Components;
    using Battleship.Core.Components.UserInterface;
    using Battleship.Core.Models;

    using NUnit.Framework;

    [TestFixture]
    public class ConsoleHelperTests : ComponentBase
    {
        private readonly PlayerStats playerStats;

        private readonly IConsoleHelper consoleHelper;

        public ConsoleHelperTests()
        {
            playerStats = new PlayerStats();
            consoleHelper = ConsoleHelper.Instance(playerStats);
        }

        [Test]
        public void ConsoleHelper_CreateMultipleInstance_ReturnsSameInstance()
        {
            // Arrange
            ConsoleHelper instanceOne = ConsoleHelper.Instance(playerStats);
            ConsoleHelper instanceTwo = ConsoleHelper.Instance(playerStats);

            // Act         
            // Nothing to act on, checking if we are getting back a singleton

            // Assert
            Assert.AreEqual(instanceOne, instanceTwo);
        }

        [Test]
        public void GetUserInput_IfGreaterThanTwoChars_ThrowsNoArgumentException()
        {
            // Arrange

            // Act and Assert    
            try
            {
                consoleHelper.GetUserInput("123");
                Assert.Fail();
            }
            catch (ArgumentException)
            {
                Assert.Pass();
            }
        }

        [Test]
        public void GetUserInput_IfOnlyOneChars_ThrowsNoArgumentException()
        {
            // Arrange

            // Act and Assert    
            try
            {
                consoleHelper.GetUserInput("1");
                Assert.Fail();
            }
            catch (ArgumentException)
            {
                Assert.Pass();
            }
        }

        [Test]
        public void GetUserInput_NoInput_ThrowsNoArgumentException()
        {
            // Arrange

            // Act and Assert    
            try
            {
                consoleHelper.GetUserInput(string.Empty);
                Assert.Fail();
            }
            catch (ArgumentException)
            {
                Assert.Pass();
            }
        }

        [Test]
        public void GetUserInput_SpecialCharX_ThrowsNoArgumentException()
        {
            // Arrange

            // Act and Assert    
            try
            {
                consoleHelper.GetUserInput("&7");
                Assert.Fail();
            }
            catch (ArgumentException)
            {
                Assert.Pass();
            }
        }

        [Test]
        public void GetUserInput_SpecialCharY_ThrowsNoArgumentException()
        {
            // Arrange

            // Act and Assert    
            try
            {
                consoleHelper.GetUserInput("A*");
                Assert.Fail();
            }
            catch (ArgumentException)
            {
                Assert.Pass();
            }
        }

        [Test]
        public void GetUserInput_TwoNumbers_ThrowsNoArgumentException()
        {
            // Arrange

            // Act and Assert    
            try
            {
                consoleHelper.GetUserInput("77");
                Assert.Fail();
            }
            catch (ArgumentException)
            {
                Assert.Pass();
            }
        }
    }
}