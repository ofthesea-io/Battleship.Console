namespace Battleship.Core.Components.Ships
{
    public abstract class ShipBase : ComponentBase
    {
        private readonly int shipIndex;

        protected ShipBase(int shipIndex)
        {
            this.shipIndex = shipIndex;
        }

        public override string ToString()
        {
            return $"BaseShip {shipIndex.ToString()}";
        }
    }
}