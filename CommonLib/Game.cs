
using System.Text;

namespace CommonLib;

public class Game
{
    private readonly ComputerBoard _computerBoard;
    private readonly UserBoard _userBoard;

    public Game()
    {
        _computerBoard = new ComputerBoard("../compFleet.txt");
        _userBoard = new UserBoard("../userFleet.txt");
    }

    public (string, string?) MakeComputerMove()
    {
        return _userBoard.MakeComputerMove();
    }

    public string? MakePlayerMove(string move)
    {
        return _computerBoard.MakePlayerMove(new Move(move));
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