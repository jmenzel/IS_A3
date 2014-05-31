﻿using System.Collections.Generic;
using System.Linq;
using System.Net.Security;

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

        public string Name { get; private set; }


        public int ChooseNumber(int destinationNumber, int currentPlayerSum, IEnumerable<int> freeNumbers)
        {
            return 0;
        }

        public static MiniMaxTree BuildGameTree(int[] numbers, int depth)
        {
            var tree = new MiniMaxTree(int.MinValue);
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
    }
}