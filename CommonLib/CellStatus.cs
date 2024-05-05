
namespace CommonLib;

public enum CellStatus
{
    AIRCRAFT_CARRIER,
    AIRCRAFT_CARRIER_HIT,
    AIRCRAFT_CARRIER_SUNK,
    BATTLESHIP,
    BATTLESHIP_HIT,
    BATTLESHIP_SUNK,
    CRUISER,
    CRUISER_HIT,
    CRUISER_SUNK,
    DESTROYER,
    DESTROYER_HIT,
    DESTROYER_SUNK,
    SUB,
    SUB_HIT,
    SUB_SUNK,
    NOTHING,
    NOTHING_HIT
}

internal static class CellStatusUtils
{
    public static string ToString(CellStatus status)
    {
        return status switch
        {
            CellStatus.AIRCRAFT_CARRIER => "oA",
            CellStatus.AIRCRAFT_CARRIER_HIT => "HX",
            CellStatus.AIRCRAFT_CARRIER_SUNK => "AX",
            CellStatus.BATTLESHIP => "oB",
            CellStatus.BATTLESHIP_HIT => "HX",
            CellStatus.BATTLESHIP_SUNK => "BX",
            CellStatus.CRUISER => "oC",
            CellStatus.CRUISER_HIT => "HX",
            CellStatus.CRUISER_SUNK => "CX",
            CellStatus.DESTROYER => "oD",
            CellStatus.DESTROYER_HIT => "HX",
            CellStatus.DESTROYER_SUNK => "DX",
            CellStatus.SUB => "oS",
            CellStatus.SUB_HIT => "HX",
            CellStatus.SUB_SUNK => "SX",
            CellStatus.NOTHING => "oo",
            CellStatus.NOTHING_HIT => "xx",
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}