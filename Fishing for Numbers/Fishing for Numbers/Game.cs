using System;
using System.Collections.Generic;
using System.Linq;

namespace Fishing_for_Numbers
{
    public class Game
    {
        private readonly IPlayer _playerA;
        private readonly IPlayer _playerB;
        private readonly Board _board;
        private readonly IDictionary<IPlayer, List<int>> _stats;
        private readonly int _destinationNumber;

        public Game(IPlayer playerA, IPlayer playerB)
        {
            _playerA = playerA;
            _playerB = playerB;

            _board = new Board();
            _board.GenerateNumbers();

            _stats = new Dictionary<IPlayer, List<int>>();
            _stats[_playerA] = new List<int>();
            _stats[_playerB] = new List<int>();

            _destinationNumber = GenerateDestinationNumber();
        }

        public IPlayer CurrentPlayer { get; set; }

        private int GenerateDestinationNumber()
        {
            return (_board.GetFreeNumbers().Sum() / 2) + new Random().Next(1, 10);
        }

        public bool GameFinished()
        {
            return !_board.GetFreeNumbers().Any();
        }

        public Tuple<IPlayer, IPlayer> GetWinningPlayer()
        {
            var playerADiff = Math.Abs((GetCurrentNumberOf(_playerA) - _destinationNumber));
            var playerBDiff = Math.Abs((GetCurrentNumberOf(_playerB) - _destinationNumber));

            if(playerADiff == playerBDiff) return new Tuple<IPlayer, IPlayer>(_playerA, _playerB);

            return playerADiff < playerBDiff ? new Tuple<IPlayer, IPlayer>(_playerA, null) : new Tuple<IPlayer, IPlayer>(_playerB, null);
        }

        public void MakeMove(IPlayer player)
        {
            var numberFromBoard = player.ChooseNumber(_board);

            if(!_board.IsValidFreeNumber(numberFromBoard)) 
                throw new ArgumentException("number must be a free number from the board");

            _board.SetNumberAsChoosen(numberFromBoard);
            _stats[player].Add(numberFromBoard);
        }


        public int GetCurrentNumberOf(IPlayer player)
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

        public void Start()
        {
            //Game Loop
            while (!GameFinished())
            {
                //Set next player
                CurrentPlayer = CurrentPlayer == null || CurrentPlayer == _playerB ?  _playerA : _playerB;

                //Clear CLS
                Console.Clear();

                //Draw game board
                DrawBoard();



                MakeMove(CurrentPlayer);

                if (!GameFinished()) continue;
                    

                //Get Winner HumanPlayer and Prints
                var winner = GetWinningPlayer();

                Console.Clear();
                DrawBoard();

                Console.WriteLine("Game Finished");

                if (winner.Item2 != null)
                {
                    Console.WriteLine("This round is drawn");
                }
                else
                {
                    Console.WriteLine("Winner is " + winner.Item1);
                    Console.WriteLine("With the Number " + GetCurrentNumberOf(winner.Item1));
                }
            }
        }
    }
}
