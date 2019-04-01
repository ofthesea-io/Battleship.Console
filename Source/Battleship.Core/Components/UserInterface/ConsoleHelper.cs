namespace Battleship.Core.Components.UserInterface
{
    using System;

    using Battleship.Core.Components.Board;
    using Battleship.Core.Models;
    using Battleship.Core.Utilities;

    public class ConsoleHelper : ComponentBase, IConsoleHelper
    {
        private static volatile ConsoleHelper instance;

        private readonly Segmentation segmentation;

        private readonly PlayerStats playerStats;

        protected ConsoleHelper(PlayerStats playerStats)
        {
            this.playerStats = playerStats;
            segmentation = Segmentation.Instance();
        }

        #region Properties

        public string HitMessage => "You've hit one of the ship(s)!";

        public string InvalidMessage => "Invalid input, please enter e.g. A1 or a1.";

        public string MissMessage => "Sorry, you missed!";

        public string CompletedMessage => "You've won. Game Completed!";

        public string StartGameMessage => "Time for war!";

        public string SunkMessage => "Ship Sunk";

        public string CoordinateTriedMessage => "You have already tried that coordinate";

        public string ApplicationErrorMessage => "Application Error. Please restart the game.";

        #endregion

        public static ConsoleHelper Instance(PlayerStats playerStats)
        {
            if (instance == null)
            {
                lock (SyncObject)
                {
                    if (instance == null)
                    {
                        instance = new ConsoleHelper(playerStats);
                    }
                }
            }

            return instance;
        }

        public void ClearLine(int inputLine)
        {
            if (inputLine <= 0)
            {
                throw new ArgumentException();
            }

            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, inputLine);
        }

        public void Write(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new NullReferenceException();
            }

            Console.Write(input);
        }

        public void WriteLine(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new NullReferenceException();
            }

            Console.WriteLine(input);
        }

        public void ClearBufferToWriteLine(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new NullReferenceException();
            }

            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);

            Console.WriteLine(input);
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void SetColour(ConsoleColor colour)
        {
            Console.ForegroundColor = colour;
        }

        public string GetUserInput(string userInput)
        {
            string message = string.Empty;

            if (userInput.Length > 3 || userInput.Length == 1 || string.IsNullOrEmpty(userInput))
            {
                throw new ArgumentException();
            }

            string userInputX = userInput.Substring(0, Index).ToUpper();

            char.TryParse(userInputX, out char x);

            string userInputY = userInput.Substring(Index, userInput.Length - Index);

            int.TryParse(userInputY, out int y);

            if (!BattleshipExtensions.IsSegmentWithInGridRange(x, y))
            {
                throw new ArgumentException();
            }

            var segment = segmentation.GetSegment(x, y);

            if (segment.Character == Miss || segment.Character == Hit)
            {
                return CoordinateTriedMessage;
            }

            if (segment.Ship == null)
            {
                segment.IsEmpty = false;
                segment.Character = Miss;
                segmentation.UpdateSegment(segment);
                message = MissMessage;
                playerStats.Miss++;
            }

            if (segment.Ship != null)
            {
                segment.IsEmpty = false;
                segment.Character = Hit;
                segment.Ship.ShipHit++;
                segmentation.UpdateSegment(segment);
                message = HitMessage;
                playerStats.Hit++;
                if (segment.Ship.IsShipSunk)
                {
                    playerStats.Sunk++;
                    message = SunkMessage;
                }
            }

            return message;
        }
    }
}