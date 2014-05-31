using System;
using System.Linq;
using Fishing_for_Numbers.Player;

namespace Fishing_for_Numbers
{
    class Program
    {
        public static readonly Tuple<string, Func<IPlayer>>[] AvailablePlayer =
        {
            new Tuple<string, Func<IPlayer>>("[0] - Human Player", () => new HumanPlayer()), 
            new Tuple<string, Func<IPlayer>>("[1] - AI Elli", () => new ElliAi()),
            new Tuple<string, Func<IPlayer>>("[2] - AI Randy", () => new RandyAi()),
            new Tuple<string, Func<IPlayer>>("[3] - AI MiniMax", () => new MinMaxAi(3))
        };


        static void Main()
        {
            var command = string.Empty;
            IPlayer playerA = null;
            IPlayer playerB = null;

            do
            {
                if (!String.IsNullOrEmpty(command) && command.ToLower() == "reset")
                {
                    playerA = null;
                    playerB = null;
                }

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

                Console.WriteLine("Type 'exit' to leave" + Environment.NewLine +
                                  "Type 'reset' for new player selection" + Environment.NewLine +
                                  "Hit Enter for new Game" + Environment.NewLine);
                command = Console.ReadLine();
            } 
            while (String.IsNullOrEmpty(command) || command.ToLower() != "exit");
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
