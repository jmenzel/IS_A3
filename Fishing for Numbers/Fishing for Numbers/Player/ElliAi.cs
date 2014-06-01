using System;
using System.Collections.Generic;
using System.Linq;

namespace Fishing_for_Numbers.Player
{
    public class ElliAi : IPlayer
    {
        public ElliAi()
        {
            Name = "Elli-AI";
        }

        public string Name { get; private set; }
        public int DestinationNumber { get; set; }

        public int ChooseNumber(int currentPlayerSum, IEnumerable<int> freeNumbers)
        {
            var numArray = freeNumbers as int[] ?? freeNumbers.ToArray();
            var pos = new Random((int)DateTime.Now.Ticks).Next(0, numArray.Length - 1);
            return numArray[pos];
        }

        public override string ToString()
        {
            return Name;
        }
    }
}