
namespace CommonLib;

internal class ComputerBoard : Board
{
    public ComputerBoard(ILayoutLoader loader) : base(loader)
    {
    }

    public string? MakePlayerMove(Move move)
    {
        return this.MakeMove(move);
    }

    protected override string GetCellStatusString(CellStatus status)
    {
        return CellStatusUtils.ToString(status)[..1];
    }
}
