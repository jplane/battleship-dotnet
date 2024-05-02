
using BoardRow=System.Collections.Generic.List<CellStatus>;
using BoardLayout=System.Collections.Generic.List<System.Collections.Generic.List<CellStatus>>;
using System.Text;

internal abstract class Board
{
    private const int SIZE = 10;
    
    protected Board(string filename)
    {
        this.Fleet = new Fleet();

        this.Layout = [];
        this.Layout.AddRange(Enumerable.Range(0, SIZE).Select(_ => new BoardRow()));
        this.Layout.ForEach(row => row.AddRange(Enumerable.Range(0, SIZE)
                                                          .Select(_ => CellStatus.NOTHING)));

        this.LoadLayout(filename);
    }

    public BoardLayout Layout { get; }

    public Fleet Fleet { get; }

    public bool IsGameOver()
    {
        return this.Fleet.IsGameOver();
    }

    public override string ToString()
    {
        var sb = new StringBuilder();

        sb.AppendLine("  1 2 3 4 5 6 7 8 9 10");

        for (var row = 1; row <= 10; row++)
        {
            sb.Append(Move.GetRowName(row));
            sb.Append(" ");

            for (var col = 1; col <= 10; col++)
            {
                var status = this.Layout[row - 1][col - 1];

                sb.Append(GetCellStatusString(status));

                sb.Append(" ");
            }

            sb.AppendLine();
        }

        return sb.ToString();
    }

    protected abstract string GetCellStatusString(CellStatus status);

    protected string? MakeMove(Move move)
    {
        if (this.IsMoveAvailable(move))
        {
            var status = this.ApplyMoveToLayout(move);

            var sunk = false;
            var message = "Missed!";

            switch (status)
            {
                case CellStatus.AIRCRAFT_CARRIER:
                    sunk = this.Fleet.UpdateFleet(ShipType.AIRCRAFT_CARRIER);
                    message = sunk ? "Aircraft carrier is sunk!" : "Aircraft carrier is hit!";
                    break;
                case CellStatus.BATTLESHIP:
                    sunk = this.Fleet.UpdateFleet(ShipType.BATTLESHIP);
                    message = sunk ? "Battleship is sunk!" : "Battleship is hit!";
                    break;
                case CellStatus.CRUISER:
                    sunk = this.Fleet.UpdateFleet(ShipType.CRUISER);
                    message = sunk ? "Cruiser is sunk!" : "Cruiser is hit!";
                    break;
                case CellStatus.SUB:
                    sunk = this.Fleet.UpdateFleet(ShipType.SUB);
                    message = sunk ? "Sub is sunk!" : "Sub is hit!";
                    break;
                case CellStatus.DESTROYER:
                    sunk = this.Fleet.UpdateFleet(ShipType.DESTROYER);
                    message = sunk ? "Destroyer is sunk!" : "Destroyer is hit!";
                    break;
            }

            if (sunk)
            {
                this.MarkShipAsSunk(status);
            }

            return message;
        }
        else
        {
            return null;
        }
    }

    private CellStatus ApplyMoveToLayout(Move move)
    {
        var status = this.Layout[move.Row - 1][move.Col - 1];

        this.Layout[move.Row - 1][move.Col - 1] = status switch
        {
            CellStatus.AIRCRAFT_CARRIER => CellStatus.AIRCRAFT_CARRIER_HIT,
            CellStatus.BATTLESHIP => CellStatus.BATTLESHIP_HIT,
            CellStatus.CRUISER => CellStatus.CRUISER_HIT,
            CellStatus.SUB => CellStatus.SUB_HIT,
            CellStatus.DESTROYER => CellStatus.DESTROYER_HIT,
            _ => CellStatus.NOTHING_HIT
        };

        return status;
    }

    private bool IsMoveAvailable(Move move)
    {
        return this.Layout[move.Row - 1][move.Col - 1] switch
        {
            CellStatus.AIRCRAFT_CARRIER => true,
            CellStatus.BATTLESHIP => true,
            CellStatus.CRUISER => true,
            CellStatus.SUB => true,
            CellStatus.DESTROYER => true,
            CellStatus.NOTHING => true,
            _ => false
        };
    }

    private void MarkShipAsSunk(CellStatus status)
    {
        CellStatus hitStatus;
        CellStatus sunkStatus;

        switch (status)
        {
            case CellStatus.AIRCRAFT_CARRIER:
                hitStatus = CellStatus.AIRCRAFT_CARRIER_HIT;
                sunkStatus = CellStatus.AIRCRAFT_CARRIER_SUNK;
                break;
            case CellStatus.BATTLESHIP:
                hitStatus = CellStatus.BATTLESHIP_HIT;
                sunkStatus = CellStatus.BATTLESHIP_SUNK;
                break;
            case CellStatus.CRUISER:
                hitStatus = CellStatus.CRUISER_HIT;
                sunkStatus = CellStatus.CRUISER_SUNK;
                break;
            case CellStatus.SUB:
                hitStatus = CellStatus.SUB_HIT;
                sunkStatus = CellStatus.SUB_SUNK;
                break;
            case CellStatus.DESTROYER:
                hitStatus = CellStatus.DESTROYER_HIT;
                sunkStatus = CellStatus.DESTROYER_SUNK;
                break;
            default:
                throw new InvalidOperationException();
        }

        for (var row = 0; row < SIZE; row++)
        {
            for (var col = 0; col < SIZE; col++)
            {
                if (this.Layout[row][col] == hitStatus)
                {
                    this.Layout[row][col] = sunkStatus;
                }
            }
        }
    }

    private void LoadLayout(string filename)
    {
        foreach(var line in File.ReadAllLines(filename))
        {
            var parts = line.Split(' ');
            
            var status = parts[0] switch
            {
                "A" => CellStatus.AIRCRAFT_CARRIER,
                "B" => CellStatus.BATTLESHIP,
                "C" => CellStatus.CRUISER,
                "S" => CellStatus.SUB,
                "D" => CellStatus.DESTROYER,
                _ => CellStatus.NOTHING
            };

            var startrow = Move.GetRowIndex(parts[1][..1]);
            var startcol = int.Parse(parts[1][1..]) - 1;

            var endrow = Move.GetRowIndex(parts[2][..1]);
            var endcol = int.Parse(parts[2][1..]) - 1;

            if (startrow == endrow)
            {
                for (var col = startcol; col <= endcol; col++)
                {
                    this.Layout[startrow][col] = status;
                }
            }
            else if (startcol == endcol)
            {
                for (var row = startrow; row <= endrow; row++)
                {
                    this.Layout[row][startcol] = status;
                }
            }
        }
    }
}
