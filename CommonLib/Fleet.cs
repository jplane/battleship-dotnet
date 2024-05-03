
namespace CommonLib;

internal class Fleet
{
    private readonly AircraftCarrier _aircraftCarrier = new();
    private readonly Battleship _battleship = new();
    private readonly Cruiser _cruiser = new();
    private readonly Sub _sub = new();
    private readonly Destroyer _destroyer = new();

    public Fleet()
    {
    }

    public bool UpdateFleet(ShipType shipType)
    {
        return shipType switch
        {
            ShipType.AIRCRAFT_CARRIER => _aircraftCarrier.Hit(),
            ShipType.BATTLESHIP => _battleship.Hit(),
            ShipType.CRUISER => _cruiser.Hit(),
            ShipType.SUB => _sub.Hit(),
            ShipType.DESTROYER => _destroyer.Hit(),
            _ => false
        };
    }

    public bool IsGameOver()
    {
        return _aircraftCarrier.Sunk &&
               _battleship.Sunk &&
               _cruiser.Sunk &&
               _sub.Sunk &&
               _destroyer.Sunk;
    }
}
