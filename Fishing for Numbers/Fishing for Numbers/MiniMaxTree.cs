using System.Collections.Generic;
using System.Linq;

namespace Fishing_for_Numbers
{
    public class MiniMaxTree: Tree<int>
    {
        public int[] NumbersLeft { get; set; }

        public int Diff { get; set; }
        public bool RegisterMove { get; set; }

        public MiniMaxTree(int number) : base(number)
        { }

        public IEnumerable<MiniMaxTree> GetChields()
        {
            return _children.Select(tree => (MiniMaxTree)tree).ToArray();
        }
    }
}