
using System.Text;

namespace CommonLib;

public class Game
{
    private readonly ComputerBoard _computerBoard;
    private readonly UserBoard _userBoard;

    public Game(ILayoutLoader computerLoader, ILayoutLoader userLoader)
    {
        _computerBoard = new ComputerBoard(computerLoader);
        _userBoard = new UserBoard(userLoader);
    }

    public (string, string?) MakeComputerMove()
    {
        return _userBoard.MakeComputerMove();
    }

    public string? MakePlayerMove(string move)
    {
        try
        {
            return _computerBoard.MakePlayerMove(new Move(move));
        }
        catch (IndexOutOfRangeException)
        {
            return null;
        }
    }

    public bool IsUserDefeated => _userBoard.IsGameOver();

    public bool IsComputerDefeated => _computerBoard.IsGameOver();

    override public string ToString()
    {
        var sb = new StringBuilder();

        sb.AppendLine("Computer Board:");
        sb.AppendLine(_computerBoard.ToString());
        sb.AppendLine("User Board:");
        sb.AppendLine(_userBoard.ToString());

        return sb.ToString();
    }
}