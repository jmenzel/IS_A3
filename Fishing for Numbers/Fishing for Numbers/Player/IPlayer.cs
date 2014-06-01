using System.Collections.Generic;

namespace Fishing_for_Numbers.Player
{
    public interface IPlayer
    {
        string Name { get;}
        int DestinationNumber { get; set; }

        int ChooseNumber(int currentPlayerSum, IEnumerable<int> freeNumbers);
    }
}