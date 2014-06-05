using System;
using System.Collections.Generic;
using System.Linq;

namespace Fishing_for_Numbers.Player
{
    public class NegaMaxAi : IPlayer
    {
        private readonly int _treeDepth;
        private MiniMaxTree _currentTree;

        public NegaMaxAi(int treeDepth)
        {
            _treeDepth = treeDepth;
            Name = "NegaMax-AI";
        }

        public int CurrentPlayerSum { get; set; }

        public int OppPlayerSum { get; set; }
        public int FullSum { get; set; }
        public string Name { get; private set; }
        public int DestinationNumber { get; set; }


        public int ChooseNumber(int currentPlayerSum, IEnumerable<int> freeNumbers)
        {
            var aktSum = freeNumbers.Aggregate((sum, acc) => sum = sum + acc);

            if (currentPlayerSum == 0) FullSum = aktSum;

            CurrentPlayerSum = currentPlayerSum;
            OppPlayerSum = FullSum - (currentPlayerSum + aktSum);

            _currentTree = BuildGameTree(freeNumbers, _treeDepth);

            int diff = NegaMax(_currentTree, CurrentPlayerSum, OppPlayerSum, false);
            MiniMaxTree node = Evaluate(_currentTree, diff);

            PrintPath(node, 0, diff);
            Console.ReadLine();
            return node.Data;
        }


        public override string ToString()
        {
            return Name;
        }

        private MiniMaxTree Evaluate(MiniMaxTree tree, int diff)
        {
            if (CurrentPlayerSum < DestinationNumber)
            {
                return
                    tree.GetChields()
                        .Where(node => node.MyDiff == diff)
                        .Single(node => node.Data == tree.GetChields()
                            .Where(n => n.MyDiff == diff)
                            .Max(elem => elem.Data));
            }


            return
                tree.GetChields()
                    .Where(node => node.MyDiff == diff)
                    .Single(node => node.Data == tree.GetChields()
                        .Where(n => n.MyDiff == diff)
                        .Min(elem => elem.Data));
        }

        private void PrintPath(MiniMaxTree tree, int level, int diff)
        {
            Console.ForegroundColor = tree.RegisterMove ? ConsoleColor.Green : ConsoleColor.Red;

            Console.WriteLine("Path @ Level [" + level + "] = '" + tree.Data + "' => " + tree.MyDiff);

            Console.ResetColor();

            if (tree.GetChields().Any())
            {
                PrintPath(Evaluate(tree, diff), level + 1, diff);
            }
        }

        public static MiniMaxTree BuildGameTree(IEnumerable<int> numbers, int depth)
        {
            var tree = new MiniMaxTree(0);
            BuildGameTreeRek(numbers, depth, tree);
            return tree;
        }

        private static void BuildGameTreeRek(IEnumerable<int> numbers, int depthLeft, MiniMaxTree parent)
        {
            if (depthLeft == 0) return;
            foreach (
                MiniMaxTree node in
                    numbers.Select(
                        number => new MiniMaxTree(number) {NumbersLeft = numbers.Except(new[] {number}).ToArray()}))
            {
                parent.AddChild(node);
                BuildGameTreeRek(node.NumbersLeft, depthLeft - 1, node);
            }
        }

        private int NegaMax(MiniMaxTree node, int sumOfPath, int oppSumOfPath, bool myMove)
        {
            node.RegisterMove = myMove;

            if (!node.IsLeaf())
            {
                node.MyDiff = node.GetChields()
                    .Select(child => NegaMax(child,
                        ((myMove) ? sumOfPath + node.Data : sumOfPath),
                        ((!myMove) ? oppSumOfPath + node.Data : oppSumOfPath),
                        !myMove))
                    .Min();

                node.OppDiff = node.GetChields()
                    .Select(child => child.OppDiff)
                    .Min();

                return node.MyDiff;
            }

            node.MyDiff = (myMove)
                ? Math.Abs(DestinationNumber - sumOfPath + node.Data)
                : Math.Abs(DestinationNumber - sumOfPath);
            node.OppDiff = (!myMove)
                ? Math.Abs(DestinationNumber - oppSumOfPath + node.Data)
                : Math.Abs(DestinationNumber - oppSumOfPath);
            return node.MyDiff;
        }
    }
}