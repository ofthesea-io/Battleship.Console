namespace Battleship.Core.Models
{
    using Battleship.Core.Components.Ships;
    using Battleship.Core.Enums;

    public class Segment
    {
        public Segment(int positionX, int positionY, char character)
        {
            this.PositionX = positionX;
            this.PositionY = positionY;
            this.IsEmpty = true;
            this.Character = character;
        }

        public Segment(int positionX, int positionY, ShipDirection shipDirection, IShip ship)
        {
            this.PositionX = positionX;
            this.PositionY = positionY;
            this.ShipDirection = shipDirection;
            this.Ship = ship;
            this.IsEmpty = false;
        }

        public int PositionX { get; set; }

        public int PositionY { get; set; }

        public bool IsEmpty { get; set; }

        public char Character { get; set; }

        public ShipDirection ShipDirection { get; set; }

        public IShip Ship { get; set; }
    }
}