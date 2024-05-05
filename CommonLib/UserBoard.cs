
namespace CommonLib;

internal class UserBoard : Board
{
    private readonly List<Move> _availableMoves = Move.AllMoves.ToList();
    private Random _random = new(Environment.TickCount);

    public UserBoard(ILayoutLoader loader) : base(loader)
    {
    }

    public (string, string?) MakeComputerMove()
    {
        var move = this.PickRandomMove();

        var message = this.MakeMove(move);

        return (move.ToString(), message);
    }

    protected override string GetCellStatusString(CellStatus status)
    {
        return CellStatusUtils.ToString(status)[1..];
    }

    private Move PickRandomMove()
    {
        var idx = _random.Next(_availableMoves.Count);

        var move = _availableMoves[idx];

        _availableMoves.RemoveAt(idx);

        return move;
    }
}
