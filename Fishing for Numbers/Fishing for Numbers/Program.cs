using System;
using System.Linq;

namespace Fishing_for_Numbers
{
    class Program
    {
        public static readonly Tuple<string, Func<IPlayer>>[] AvailablePlayer =
        {
            new Tuple<string, Func<IPlayer>>("[0] - Human Player", () => new HumanPlayer()), 
            new Tuple<string, Func<IPlayer>>("[1] - AI Elli", () => new ElliAi())
        };


        static void Main()
        {
            string readLine;
            IPlayer playerA = null;
            IPlayer playerB = null;

            do
            {
                if (playerA == null)
                {
                    Console.WriteLine("Select first player");
                    playerA = ShowPlayerChoiceAndGetPlayer();
                }

                if (playerB == null)
                {
                    Console.WriteLine("Select second player");
                    playerB = ShowPlayerChoiceAndGetPlayer();
                }

                var game = new Game(playerA, playerB);
                game.Start();

                Console.WriteLine("Type exit to leave, hit Enter for new Game: ");
                readLine = Console.ReadLine();
            } 
            while (String.IsNullOrEmpty(readLine) || readLine.ToLower() != "exit");
        }

        private static IPlayer ShowPlayerChoiceAndGetPlayer()
        {
            foreach (var player in AvailablePlayer)
            {
                Console.WriteLine(player.Item1);
            }
            var playerChoice = int.MinValue;

            do
            {
                try
                {
                    playerChoice = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.Error.WriteLine("Must be a Number between 0 and " + (AvailablePlayer.Length - 1));
                }

            } while (playerChoice < 0 && playerChoice >= AvailablePlayer.Length);

            return AvailablePlayer[playerChoice].Item2();
        }
    }
}
