using System;
using System.Collections.Generic;
using System.Linq;

namespace Fishing_for_Numbers
{
    public class Tree<T>
    {
        protected readonly LinkedList<Tree<T>> _children;
        public T Data { get; protected set; }

        protected Tree(T data)
        {
            Data = data;
            _children = new LinkedList<Tree<T>>();
        }

        public Tree<T> AddChild(T data)
        {
            var node = new Tree<T>(data);
            _children.AddFirst(node);
            return node;
        }

        public void AddChild(Tree<T> data)
        {
            _children.AddFirst(data);
        }


        public Tree<T> GetChild(int i)
        {
            return _children.FirstOrDefault(child => --i == 0);
        }

        public void Traverse(Action<Tree<T>> func)
        {
            TraverseRek(this, func);
        }

        private static void TraverseRek(Tree<T> node, Action<Tree<T>> func)
        {
            func(node);
            foreach (var child in node._children)
            {
                TraverseRek(child, func);
            }
        }

        public bool IsLeaf()
        {
            return !_children.Any();
        }


    }
}
