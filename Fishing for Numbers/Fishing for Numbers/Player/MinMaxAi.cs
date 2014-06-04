using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Security;

namespace Fishing_for_Numbers.Player
{
    public class MinMaxAi : IPlayer
    {
        private readonly int _treeDepth;

        private MiniMaxTree _currentTree;

        public string Name { get; private set; }
        public int DestinationNumber { get; set; }

        public int CurrentPlayerSum { get; set; }

        public MinMaxAi(int treeDepth)
        {
            _treeDepth = treeDepth;
            Name = "MinMax-AI";
        }

        public override string ToString()
        {
            return Name;
        }

        public int ChooseNumber(int currentPlayerSum, IEnumerable<int> freeNumbers)
        {
            CurrentPlayerSum = currentPlayerSum;
            _currentTree = BuildGameTree(freeNumbers.ToArray(), _treeDepth);

            var diff = NegaMax(_currentTree, CurrentPlayerSum);
            var node = Evaluate(_currentTree, diff);

            PrintPath(node, 0, diff);
            //Console.ReadLine();
            return node.Data;
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
            Console.WriteLine("Path @ Level [" + level + "] = '" + tree.Data + "' => " + tree.Diff);

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
                var node in
                    numbers.Select(
                        number => new MiniMaxTree(number) {NumbersLeft = numbers.Except(new[] {number}).ToArray()}))
            {
                parent.AddChild(node);
                BuildGameTreeRek(node.NumbersLeft, depthLeft - 1, node);
            }
        }

        private int NegaMax(MiniMaxTree node, int sumOfPath)
        {
            if (!node.IsLeaf())
            {
                node.Diff = node.GetChields()
                                .OrderByDescending(n => n.Data)
                                .Select(child => NegaMax(child, sumOfPath + node.Data))
                                .Min();
                return node.Diff;
            }

            node.Diff = Math.Abs(DestinationNumber - sumOfPath + node.Data);
            return node.Diff;
        }
    }
}