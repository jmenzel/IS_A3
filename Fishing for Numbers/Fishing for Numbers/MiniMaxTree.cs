using System.Collections.Generic;
using System.Linq;

namespace Fishing_for_Numbers
{
    public class MiniMaxTree: Tree<int>
    {
        public int Evaluation { get; set; }
        public int[] NumbersLeft { get; set; }

        public MiniMaxTree(int number) : base(number)
        { }

        public IEnumerable<MiniMaxTree> GetChields()
        {
            return _children.Select(tree => (MiniMaxTree)tree).ToArray();
        }
    }
}