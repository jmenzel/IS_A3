using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Fishing_for_Numbers
{
    public class Board
    {
        private readonly List<int> _fieldNumbers;
        private readonly List<int> _choosenNumbers;

        public Board()
        {
            _choosenNumbers = new List<int>();
            _fieldNumbers = new List<int>();
        }

        public void SetNumberAsChoosen(int number)
        {
            _choosenNumbers.Add(number);
        }

        public IEnumerable<int> GetFreeNumbers()
        {
            return _fieldNumbers.Except(_choosenNumbers);
        }


        public void GenerateNumbers()
        {
            _fieldNumbers.Clear();
            var r = new Random((int) DateTime.Now.Ticks);

            for (var i = 0; i < 10; ++i)
            {
                _fieldNumbers.Add(r.Next(1, 100));
            }
        }


        public bool IsValidFreeNumber(int choosenNumber)
        {
            return GetFreeNumbers().Contains(choosenNumber);
        }


        public void Draw()
        {
            //Listen sortiern
            var freeNumAsList = GetFreeNumbers().ToList();
            freeNumAsList.Sort();

            var choosenNumAsList = _choosenNumbers;
            choosenNumAsList.Sort();

            //Strings bauen
            var freeNumbers = String.Join(", ", freeNumAsList);
            var choosenNumber = String.Join(", ", choosenNumAsList);

            //Ausgeben
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(freeNumbers);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" [" + choosenNumber + "]" + Environment.NewLine);

            Console.ResetColor();
        }
    }
}