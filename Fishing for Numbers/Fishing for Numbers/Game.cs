using System;
using System.Collections.Generic;
using System.Linq;

namespace Fishing_for_Numbers
{
    public class Game
    {
        private readonly Player _playerA;
        private readonly Player _playerB;
        private readonly Board _board;
        private readonly IDictionary<Player, List<int>> _stats;
        private readonly int _destinationNumber;

        public Game(Player playerA, Player playerB)
        {
            _playerA = playerA;
            _playerB = playerB;

            _board = new Board();
            _board.GenerateNumbers();

            _stats = new Dictionary<Player, List<int>>();
            _stats[_playerA] = new List<int>();
            _stats[_playerB] = new List<int>();

            _destinationNumber = GenerateDestinationNumber();
        }

        public Player CurrentPlayer { get; set; }

        private int GenerateDestinationNumber()
        {
            return (_board.GetFreeNumbers().Sum() / 2) + new Random().Next(1, 10);
        }

        public bool GameFinished()
        {
            return !_board.GetFreeNumbers().Any();
        }

        public Tuple<Player, Player> GetWinningPlayer()
        {
            var playerADiff = Math.Abs((GetCurrentNumberOf(_playerA) - _destinationNumber));
            var playerBDiff = Math.Abs((GetCurrentNumberOf(_playerB) - _destinationNumber));

            if(playerADiff == playerBDiff) return new Tuple<Player, Player>(_playerA, _playerB);

            return playerADiff < playerBDiff ? new Tuple<Player, Player>(_playerA, null) : new Tuple<Player, Player>(_playerB, null);
        }

        public void MakeMove(Player player, int numberFromBoard)
        {
            if(!IsValidFreeNumber(numberFromBoard)) 
                throw new ArgumentException("number must be a free number from the board");

            _board.SetNumberAsChoosen(numberFromBoard);
            _stats[player].Add(numberFromBoard);
        }

        public bool IsValidFreeNumber(int choosenNumber)
        {
            return _board.GetFreeNumbers().Contains(choosenNumber);
        }

        public int GetCurrentNumberOf(Player player)
        {
            return _stats[player].Sum();
        }

        public void DrawBoard()
        {
            var left = _playerA + " [" + GetCurrentNumberOf(_playerA) + "]";
            var right = _playerB + " [" + GetCurrentNumberOf(_playerB) + "]";

            Console.WriteLine("Destination Number: " + _destinationNumber);
            Console.Write(left + " | " + right + Environment.NewLine);
            _board.Draw();
        }
    }
}
