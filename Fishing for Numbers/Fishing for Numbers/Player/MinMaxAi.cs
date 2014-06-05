using System;
using System.Collections.Generic;
using System.Linq;

namespace Fishing_for_Numbers.Player
{
    public class MinMaxAi : IPlayer
    {
        private readonly int _treeDepth;

        private MiniMaxTree _currentTree;

        public MinMaxAi(int treeDepth)
        {
            _treeDepth = treeDepth;
            Name = "MinMax-AI";
        }

        public int CurrentPlayerSum { get; set; }

        public string Name { get; private set; }
        public int DestinationNumber { get; set; }

        public int ChooseNumber(int currentPlayerSum, IEnumerable<int> freeNumbers)
        {
            CurrentPlayerSum = currentPlayerSum;
            var numbers = freeNumbers as int[] ?? freeNumbers.ToArray();

            _currentTree = BuildGameTree(numbers, _treeDepth);

            int diff = NegaMax(_currentTree, CurrentPlayerSum, false);
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
                        .Where(node => node.Diff == diff)
                        .Single(node => node.Data == tree.GetChields()
                            .Where(n => n.Diff == diff)
                            .Max(elem => elem.Data));
            }
            return
                tree.GetChields()
                    .Where(node => node.Diff == diff)
                    .Single(node => node.Data == tree.GetChields()
                        .Where(n => n.Diff == diff)
                        .Min(elem => elem.Data));
        }

        private void PrintPath(MiniMaxTree tree, int level, int diff)
        {
            Console.ForegroundColor = tree.RegisterMove ? ConsoleColor.Green : ConsoleColor.Red;

            Console.WriteLine("Path @ Level [" + level + "] = '" + tree.Data + "' => " + tree.Diff);

            Console.ResetColor();

            if (tree.GetChields().Any())
            {
                PrintPath(Evaluate(tree, diff), level + 1, diff);
            }

        }

        public static MiniMaxTree BuildGameTree(int[] numbers, int depth)
        {
            var tree = new MiniMaxTree(0);
            BuildGameTreeRek(numbers, depth, tree);
            return tree;
        }

        private static void BuildGameTreeRek(int[] numbers, int depthLeft, MiniMaxTree parent)
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

        private int NegaMax(MiniMaxTree node, int sumOfPath, bool myMove)
        {
            node.RegisterMove = myMove;

            if (!node.IsLeaf())
            {
                node.Diff = node.GetChields()
                    .Select(child => NegaMax(child, ((myMove)
                        ? sumOfPath + node.Data
                        : sumOfPath),
                        !myMove))
                    .Min();


                return node.Diff;
            }

            node.Diff = (myMove) 
                ? Math.Abs(DestinationNumber - sumOfPath + node.Data)
                : Math.Abs(DestinationNumber - sumOfPath);
            return node.Diff;
        }
    }
}