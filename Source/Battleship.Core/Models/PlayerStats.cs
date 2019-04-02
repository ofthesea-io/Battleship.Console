namespace Battleship.Core.Models
{
    /// <summary>
    ///     Players game play stats
    /// </summary>
    public class PlayerStats
    {
        public int Hit { get; set; }

        public int Miss { get; set; }

        public int Sunk { get; set; }
    }
}