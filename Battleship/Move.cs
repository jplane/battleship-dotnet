
internal class Move
{
    private static readonly string[] _rows = ["A", "B", "C", "D", "E", "F", "G", "H", "I", "J"];

    public static int GetRowIndex(string row) => Array.IndexOf(_rows, row);

    public static string GetRowName(int row)
    {
        try
        {
            return _rows[row - 1];
        }
        catch (IndexOutOfRangeException)
        {
            return string.Empty;
        }
    }

    public static Move[] AllMoves
    {
        get
        {
            var moves = new List<Move>();

            for (var row = 1; row <= 10; row++)
            {
                for (var col = 1; col <= 10; col++)
                {
                    moves.Add(new Move(row, col));
                }
            }

            return moves.ToArray();
        }
    }

    public Move(int row, int col)
    {
        this.Row = row;
        this.Col = col;
    }

    public Move(string move)
    {
        this.Row = GetRowIndex(move[..1]) + 1;
        this.Col = int.Parse(move[1..]);
    }

    public int Row { get; }
    public int Col { get; }

    public override string ToString()
    {
        return $"{GetRowName(this.Row)}{this.Col}";
    }
}
