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

        public int ChooseNumber(int currentPlayerSum, IEnumerable<int> freeNumbers)
        {
            CurrentPlayerSum = currentPlayerSum;
            _currentTree = BuildGameTree(freeNumbers.ToArray(), _treeDepth);

            var bestDiff = NegaMax(_currentTree, CurrentPlayerSum);

            var retNode = _currentTree.GetChields().First(node => node.Diff == bestDiff);

            //_currentTree.Traverse(tree => Console.WriteLine(tree.Data + " -> " + ((MiniMaxTree)tree).Diff));

            return retNode.Data;
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

        public int NegaMax(MiniMaxTree node, int sumOfPath)
        {
            if (!node.IsLeaf())
            {
                node.Diff = node.GetChields().Select(child => NegaMax(child, sumOfPath + node.Data)).Min();
                return node.Diff;
            }

            node.Diff = Math.Abs(DestinationNumber - sumOfPath + node.Data);
            return node.Diff;
        }
    }
}