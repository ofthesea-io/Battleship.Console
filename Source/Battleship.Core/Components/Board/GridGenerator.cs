namespace Battleship.Core.Components.Board
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Battleship.Core.Components.Ships;
    using Battleship.Core.Components.UserInterface;
    using Battleship.Core.Models;

    public class GridGenerator : ComponentBase, IGridGenerator
    {
        private readonly IConsoleHelper consoleHelper;

        private readonly ISegmentation segmentation;

        private readonly IShipRandomiser shipRandomiser;

        private readonly List<IShip> ships;

        private int boardLeft;

        private int boardTop;

        public GridGenerator(
            ISegmentation segmentation,
            IShipRandomiser shipRandomiser,
            IConsoleHelper consoleHelper,
            List<IShip> ships)
        {
            this.segmentation = segmentation;
            this.consoleHelper = consoleHelper;
            this.shipRandomiser = shipRandomiser;
            this.ships = ships;
        }


        #region Properties

        public int? NumberOfSegments { get; private set; }

        public int? NumberOfOccupiedSegments { get; private set; }

        #endregion

        #region Methods

        public void BuildGrid()
        {
            // set the cursors position 
            boardTop = Console.CursorTop + 1;

            consoleHelper.Write("    ");

            foreach (int row in this.GetAlphaColumnChars())
            {
                consoleHelper.Write($" {Convert.ToChar(row)} ");
            }

            consoleHelper.WriteLine("  ");

            foreach (int column in this.GetNumericRows())
            {
                consoleHelper.WriteLine($" {column} ");
            }

            // fill the grid with water
            this.CreateSegmentationGrid();
        }

        public void RedrawGrid()
        {
            boardLeft = 5;
            boardTop = 3;
            Console.SetCursorPosition(boardLeft, boardTop);

            boardLeft = 4;
            Console.SetCursorPosition(boardLeft, boardTop);

            int yCounter = 1;
            while (yCounter <= GridDimension)
            {
                for (int xCounter = 0; xCounter <= GridDimension - Index; xCounter++)
                {
                    var segment = segmentation.GetSegment(XInitialPoint + xCounter, yCounter);
                    if (segment != null)
                    {
                        switch (segment.Character)
                        {
                            case Miss:
                                consoleHelper.SetColour(ConsoleColor.White);
                                consoleHelper.Write($" {Miss} ");
                                break;
                            case Hit:
                                consoleHelper.SetColour(ConsoleColor.Red);
                                consoleHelper.Write($" {Hit} ");
                                break;
                            default:
                                consoleHelper.SetColour(ConsoleColor.Blue);
                                consoleHelper.Write($" {Water} ");
                                break;
                        }
                    }
                }

                boardTop++;
                yCounter++;

                Console.SetCursorPosition(boardLeft, boardTop);
            }

            consoleHelper.SetColour(ConsoleColor.White);
        }

        private void CreateSegmentationGrid()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            boardLeft = 4;
            Console.SetCursorPosition(boardLeft, boardTop);

            int yCounter = 1;
            while (yCounter <= GridDimension)
            {
                for (int xCounter = 0; xCounter <= GridDimension - Index; xCounter++)
                {
                    consoleHelper.Write($" {Water} ");
                    Segment segment = new Segment(XInitialPoint + xCounter, yCounter, Water);
                    segmentation.AddSegment(segment);
                }

                boardTop++;
                yCounter++;

                Console.SetCursorPosition(boardLeft, boardTop);
            }

            this.NumberOfSegments = segmentation.GetSegments().Count();

            // update the board with randomly generated ship coordinates
            this.UpdateSegmentationGridWithShips();
        }

        private void UpdateSegmentationGridWithShips()
        {
            List<Segment> segments = shipRandomiser.GetRandomisedShipCoordinates(ships);

            segmentation.UpdateSegmentRange(segments);

            this.NumberOfOccupiedSegments = segmentation.GetSegments().Count(q => !q.IsEmpty);
        }

        private IEnumerable<int> GetNumericRows()
        {
            for (int i = 1; i <= GridDimension; i++)
            {
                yield return i;
            }
        }

        private IEnumerable<int> GetAlphaColumnChars()
        {
            for (int i = XInitialPoint; i < XInitialPoint + GridDimension; i++)
            {
                yield return i;
            }
        }

        #endregion
    }
}