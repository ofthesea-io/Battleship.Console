namespace Battleship.Game
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    using Battleship.Core.Components.Board;
    using Battleship.Core.Components.Ships;
    using Battleship.Core.Components.UserInterface;
    using Battleship.Core.Models;

    using Newtonsoft.Json;

    public class Program
    {
        private readonly IConsoleHelper consoleHelper;

        private readonly IGridGenerator gridGenerator;

        private readonly PlayerStats playerStats;

        private readonly int shipCounter;

        private string message;

        public Program()
        {
            // Game play objects
            List<IShip> ships = new List<IShip> { new BattleShip(1), new Destroyer(2), new Destroyer(3) };
            playerStats = new PlayerStats();

            // Not using a IoC framework (manual singletons), so it will just get new up a instance here,
            // and injected directly. Once we go out of scope here, allow the GC to clean up behind us  
            ISegmentation segmentation = Segmentation.Instance();
            IShipRandomiser shipRandomiser = ShipRandomiser.Instance();
            consoleHelper = ConsoleHelper.Instance(playerStats);

            gridGenerator = new GridGenerator(segmentation, shipRandomiser, consoleHelper, ships);

            shipCounter = ships.Count;
            message = consoleHelper.StartGameMessage;
        }

        private static void Main(string[] args)
        {
            Program p = new Program();

            if (args.Length == 0)
            {
                p.Run();
            }
            else
            {
                p.SimulateGamePlay(args[0]);
                p.Run();
            }
        }

        private void Run()
        {
            this.BuildUserInterface();
            this.StartGame();
        }

        private void StartGame()
        {
            while (true)
            {
                this.Execute();
            }
        }

        private void SimulateGamePlay(string input)
        {
            try
            {
                if (!string.IsNullOrEmpty(input))
                {
                    var definition = new { SimulationTimer = 0, X = new List<char>(), Y = new List<int>() };
                    var json = JsonConvert.DeserializeAnonymousType(input, definition);

                    this.BuildUserInterface();

                    // we running on a single thread, so we don't have to
                    // create Tasks to block the UI thread
                    for (int x = 0; x < json.X.Count; x++)
                    {
                        for (int y = 0; y < json.X.Count; y++)
                        {
                            Thread.Sleep(TimeSpan.FromMilliseconds(json.SimulationTimer));
                            string automatedInput = $"{json.X[x]}{json.Y[y]}";
                            this.Execute(automatedInput);
                            if (CheckShipHitCount()) break;
                        }
                        if (CheckShipHitCount()) break;
                    }

                    this.Execute(); // execute again, to complete process
                    while (true)
                    {
                        this.Exit(Console.ReadLine());
                    }
                }
            }
            catch (Exception)
            {
                // Json parsing failed. 
                // Fall through catch, start manual
            }
        }

        private void BuildUserInterface()
        {
            Console.Clear();
            consoleHelper.WriteLine("Welcome to the Battleship Game 101");
            consoleHelper.WriteLine("**********************************");

            gridGenerator.BuildGrid();

            consoleHelper.SetColour(ConsoleColor.White);
            consoleHelper.WriteLine("\nEnter coordinate's e.g. A1 or j3 (Press X to exit)");
        }

        private void Execute(string input = null)
        {
            int inputLine = 0;
            try
            {
                int resetTop = Console.CursorTop;

                if (CheckShipHitCount())
                {
                    message = consoleHelper.CompletedMessage;
                }

                consoleHelper.WriteLine($"[Hit: {playerStats.Hit}] [Miss: {playerStats.Miss}] [Ship(s) Sunk : {playerStats.Sunk}]");
                consoleHelper.ClearBufferToWriteLine($"Message : {message}");

                inputLine = Console.CursorTop - 2;

                if (string.IsNullOrEmpty(input))
                {
                    input = consoleHelper.ReadLine();
                }

                this.Exit(input);

                message = consoleHelper.GetUserInput(input);
                consoleHelper.ClearLine(inputLine);
                gridGenerator.RedrawGrid();
                Console.SetCursorPosition(0, resetTop);
            }
            catch (ArgumentException)
            {
                message = consoleHelper.InvalidMessage;
                consoleHelper.ClearLine(inputLine);
            }
            catch (Exception)
            {
                message = consoleHelper.ApplicationErrorMessage;
                consoleHelper.ClearLine(inputLine);
            }
        }

        private void Exit(string input)
        {
            if (!string.IsNullOrEmpty(input) && input.Length == 1)
            {
                input = input.ToUpper();
                bool isValid = char.TryParse(input, out char result);

                if (isValid && result == 'X')
                {
                    Environment.Exit(0);
                }
            }
        }

        private bool CheckShipHitCount()
        {
            if (this.shipCounter == playerStats.Sunk)
            {
                return true;
            }

            return false;
        }
    }
}