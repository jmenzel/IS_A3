using System;
using System.Collections.Generic;

namespace Fishing_for_Numbers.Player
{
    public class HumanPlayer : IPlayer
    {
        public HumanPlayer()
        {
            Name = AskForName();
        }

        private static string AskForName()
        {
            string input = null;
            while (String.IsNullOrEmpty(input) || String.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Enter the name of this Player: ");
                input = Console.ReadLine();
            }
            return input;
        }

        public string Name { get; private set; }

        public int ChooseNumber(int destinationNumber, int currentPlayerSum, IEnumerable<int> freeNumbers)
        {
            var choosenNumber = int.MinValue;
            
            try
            {
                Console.Write("Make your Choice " + Name + ": ");
                choosenNumber = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.Error.Write("Must be a number!");
            }

            return choosenNumber;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}