using System;
using System.Collections.Generic;
using System.Linq;

namespace Fishing_for_Numbers.Player
{
    public class RandyAi : IPlayer
    {
        public RandyAi()
        {
            Name = "Randy-AI";
        }

        public string Name { get; private set; }
        public int DestinationNumber { get; set; }

        public int ChooseNumber(int currentPlayerSum, IEnumerable<int> freeNumbers)
        {
            return currentPlayerSum < DestinationNumber ? freeNumbers.Max() : freeNumbers.Min();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}