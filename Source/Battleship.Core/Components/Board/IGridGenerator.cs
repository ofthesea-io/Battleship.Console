namespace Battleship.Core.Components.Board
{

    /// <summary>
    /// The gaming board for battle ships
    /// </summary>
    public interface IGridGenerator
    {
        /// <summary>
        ///     Lays out the initial grid, with the X and Y axis chars and numbers
        /// </summary>
        void BuildGrid();

        /// <summary>
        ///     Redraws just the 10 X 10 grid after user input. For this game, the
        ///     loops are small, but should the grid grow e.g. (50 X 50) it would
        ///     become costly and this method would need to change, where it just
        ///     searches for the char and updates it directly. KISS for the moment.
        /// </summary>
        void RedrawGrid();
    }
}