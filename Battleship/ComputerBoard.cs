
internal class ComputerBoard : Board
{
    public ComputerBoard(string filename) : base(filename)
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
