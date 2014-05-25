using System;

namespace Fishing_for_Numbers
{
    class Program
    {
        static void Main()
        {
            var readLine = string.Empty;
            do
            {
                Console.Write("Name Player A: ");
                var nameA = Console.ReadLine();

                Console.Write("Name Player B: ");
                var nameB = Console.ReadLine();

                var playerA = new Player(nameA);
                var playerB = new Player(nameB);

                var game = new Game(playerA, playerB);

                while (!game.GameFinished())
                {
                    //Set next player
                    game.CurrentPlayer = game.CurrentPlayer == null || game.CurrentPlayer == playerB ?  playerA : playerB;

                    //Clear CLS
                    Console.Clear();

                    //Draw game board
                    game.DrawBoard();

                    var choosenNumber = int.MinValue;
                    do
                    {
                        try
                        {
                            Console.Write("Make your Choice " + game.CurrentPlayer + ": ");
                            choosenNumber = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception)
                        {
                            Console.Error.Write("Must be a number!");
                        }

                    } 
                    while (!game.IsValidFreeNumber(choosenNumber));

                    game.MakeMove(game.CurrentPlayer, choosenNumber);

                    if (!game.GameFinished()) continue;
                    

                    //Get Winner Player and Prints
                    var winner = game.GetWinningPlayer();

                    Console.Clear();
                    game.DrawBoard();

                    Console.WriteLine("Game Finished");

                    if (winner.Item2 != null)
                    {
                        Console.WriteLine("This round is drawn");
                    }
                    else
                    {
                        Console.WriteLine("Winner is " + winner.Item1);
                        Console.WriteLine("With the Number " + game.GetCurrentNumberOf(winner.Item1));
                    }
                }

                readLine = Console.ReadLine();
            } while (readLine != null && readLine.ToLower() != "exit");
        }
    }
}
