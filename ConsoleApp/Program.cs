
using CommonLib;

internal static class Program
{
    private static void Main(string[] args)
    {
        var game = new Game();

        Console.WriteLine("Welcome to Battleship!");
        Console.WriteLine("You will be playing against the computer.");
        Console.WriteLine("The first player to sink all of the other player's ships wins.");
        Console.WriteLine("Good luck!");

        int? choice = null;

        while (choice == null)
        {
            Console.WriteLine("Let's flip a coin to see who goes first. Enter 1 for heads or 2 for tails:");

            var input = Console.ReadLine();

            if (int.TryParse(input, out var result))
            {
                if (result == 1)
                {
                    choice = result;
                }
                else if (result == 2)
                {
                    choice = result;
                }
            }
        }

        Console.WriteLine();

        var flip = new Random(Environment.TickCount).Next(1, 3);

        var userTurn = choice == flip;

        if (userTurn)
        {
            Console.WriteLine("You won the coin toss! You will go first.");
        }
        else
        {
            Console.WriteLine("You lost the coin toss! The computer will go first.");
        }

        while (!game.IsUserDefeated && !game.IsComputerDefeated)
        {
            Console.WriteLine();
            Console.Write(game.ToString());

            if (userTurn)
            {
                string? msg = null;

                while (msg == null)
                {
                    string? move = null;

                    while (move == null)
                    {
                        Console.WriteLine("Your turn! Enter your move:");
                        move = Console.ReadLine();
                    }

                    msg = game.MakePlayerMove(move);

                    if (msg == null)
                    {
                        Console.WriteLine("Invalid move! Please try again.");
                    }
                }

                Console.WriteLine(msg);
            }
            else
            {
                (string move, string? msg) = game.MakeComputerMove();

                Console.WriteLine($"Computer's turn! It chose move {move}.");

                Console.WriteLine(msg);
            }

            userTurn = !userTurn;

            Console.ReadLine();
            Console.Clear();
        }

        Console.WriteLine();
        Console.Write(game.ToString());

        if (game.IsUserDefeated)
        {
            Console.WriteLine("Game over... you lost!");
        }
        else
        {
            Console.WriteLine("Game over... you won!");
        }

        Console.WriteLine();
        Console.WriteLine("Press any key to exit...");
        Console.ReadLine();
    }
}