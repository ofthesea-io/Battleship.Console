namespace Battleship.Core.Components.Ships
{
    public interface IShip
    {
        // The length of the ship 
        int ShipLength { get; }

        // The type if char
        char ShipChar { get; }

        // Ship hit counter
        int ShipHit { get; set; }

        // Ship Sunk
        bool IsShipSunk { get; set; }
    }
}