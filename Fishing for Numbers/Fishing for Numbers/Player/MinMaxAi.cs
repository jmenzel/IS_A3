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

            var a = 0;
            var b = 0;

            var max = Max(_currentTree, ref a, ref b);
            var number = int.MinValue;

            _currentTree.Traverse(treeNode =>
            {
                var node = (MiniMaxTree) treeNode;
                if (node.EvaluatedValue == max)
                {
                    number = node.Data;
                }
            });

            return number;
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

        private int Max(MiniMaxTree node, ref int a, ref int b)
        {
            if (node.IsLeaf())
            {
                return EvaluateMove(node);
            }

            var best = int.MinValue;

            foreach (var child in node.GetChields())
            {
                if (best > a) a = best;
                var min = Min(child, ref a, ref b);
                if (min > best) best = min;
                if (best >= b) return best;
            }
            return best;
        }

        private int Min(MiniMaxTree node, ref int a, ref int b)
        {
            if (node.IsLeaf()) return EvaluateMove(node);
            
            var best = int.MaxValue;

            foreach (var child in node.GetChields())
            {
                if (best < b) b = best;
                var max = Max(child, ref a, ref b);
                if (max < best) best = max;
                if (a >= best) return best;
            }
            return best;
        }

        private int EvaluateMove(MiniMaxTree node)
        {
            int val;

            var maxNumber = node.NumbersLeft.Union(new[] { node.Data }).Max();
            var minNumber = node.NumbersLeft.Union(new[] { node.Data }).Min();

            //Sind wir noch unter der Zahl? Dann maximal
            if (CurrentPlayerSum < DestinationNumber)
            {
                //Je größer desto besser
                val = node.Data;
            }
            //sonst minimal
            else
            {
                if (node.Data == minNumber)
                {
                    //Kleinste ist gut
                    val = maxNumber;
                }
                else
                {
                    //je höher desto schlechter also kleinere zahlen
                    val = minNumber - node.Data;
                }
            }
            
            node.EvaluatedValue = val;
            return val;
        }
    }
}