namespace Battleship.Core.Components.Ships
{
    public sealed class BattleShip : ShipBase, IShip
    {
        private readonly int shipLength = 5;

        private readonly char shipType = BattleShipCode;

        private int shipHit;

        public BattleShip(int shipIndex)
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