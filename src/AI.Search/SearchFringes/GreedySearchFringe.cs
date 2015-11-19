using C5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Search.SearchFringes
{
    public class GreedySearchFringe<TState> : IFringeStrategy<TState>
    {
        private readonly IPriorityQueue<HuristicWrapper> items;
        private readonly IHuristic<TState> huristic;

        public GreedySearchFringe(IHuristic<TState> huristic)
        {
            this.huristic = huristic;
            this.items = new IntervalHeap<HuristicWrapper>(new HuristicWrapperComparer());
        }

        public void Add(TState node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }
            var huristicVal = huristic.GetHuristicValue(node);
            items.Add(new HuristicWrapper(node, huristicVal));
        }

        public TState GetNext()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("Fringe is empty");
            }

            var wrapper = items.DeleteMin();
            return wrapper.State;
        }

        public bool IsEmpty()
        {
            // Todo: Check Any is the right way with C5
            return !items.Any();
        }

        private class HuristicWrapper
        {
            public TState State { get; }
            public int HuristicValue { get; }

            public HuristicWrapper(TState state, int huristicValue)
            {
                State = state;
                HuristicValue = huristicValue;
            }
        }

        private class HuristicWrapperComparer : IComparer<HuristicWrapper>
        {
            public int Compare(HuristicWrapper x, HuristicWrapper y)
            {
                return x.HuristicValue.CompareTo(y.HuristicValue);
            }
        }
    }
}
