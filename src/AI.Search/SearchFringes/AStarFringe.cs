using C5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Search.SearchFringes
{
    //public class AStarFringe<TState, TAction> : IFringeStrategy<Node<TState, TAction>>
    //{
    //    private readonly IHuristic<TState, TAction> _huristic;
        
    //    // Priority Queue
    //    private IntervalHeap<AStarNode> _internalPriorityQueue = new IntervalHeap<AStarNode>(new AStarNodeComparer());

    //    private class AStarNode
    //    {
    //        public Node<TState, TAction> Node { get; set; }
    //        public decimal EstimatedCost { get; set; }
    //    }

    //    private class AStarNodeComparer : IComparer<AStarNode>
    //    {
    //        public int Compare(AStarNode x, AStarNode y)
    //        {
    //            return decimal.Compare(x.EstimatedCost, y.EstimatedCost);
    //        }
    //    }

    //    public AStarFringe(IHuristic<TState, TAction> huristic)
    //    {
    //        _huristic = huristic;
    //    }

    //    public void Add(Node<TState, TAction> node)
    //    {
    //        var estimatedCost = node.Cost + _huristic.Calculate(node);
    //        _internalPriorityQueue.Add(new AStarNode() { Node = node, EstimatedCost = estimatedCost });
    //    }

    //    public Node<TState, TAction> GetNext()
    //    {
    //        var max = _internalPriorityQueue.DeleteMin();
    //        return max.Node;
    //    }

    //    public bool IsEmpty()
    //    {
    //        return _internalPriorityQueue.IsEmpty;
    //    }
    //}

    //public interface IHuristic<TState, TAction>
    //{
    //    decimal Calculate(Node<TState, TAction> node);
    //}
}
