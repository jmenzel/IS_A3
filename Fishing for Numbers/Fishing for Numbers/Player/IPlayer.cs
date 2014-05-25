using System.Collections.Generic;

namespace Fishing_for_Numbers.Player
{
    public interface IPlayer
    {
        string Name { get;}

        int ChooseNumber(int destinationNumber, int currentPlayerSum, IEnumerable<int> freeNumbers);
    }
}