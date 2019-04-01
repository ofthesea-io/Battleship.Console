namespace Battleship.Core.Components.Ships
{
    public sealed class Destroyer : ShipBase, IShip
    {
        private readonly int shipLength = 4;

        private readonly char shipType = DestroyerCode;

        private int shipHit;

        public Destroyer(int shipIndex)
            : base(shipIndex)
        {
            this.ShipLength = shipLength;
            this.ShipChar = shipType;
        }

        #region IShip Members

        public int ShipLength { get; }

        public char ShipChar { get; }

        public int ShipHit
        {
            get => shipHit;
            set
            {
                if (shipHit == this.ShipLength - Index)
                {
                    this.IsShipSunk = true;
                }
                else
                {
                    shipHit++;
                }
            }
        }

        public bool IsShipSunk { get; set; }

        #endregion
    }
}