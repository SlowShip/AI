using C5;
using System;
using System.Collections.Generic;

namespace AI.Search.SearchFringes
{
    public abstract class PriorityQueueDrivenFringeBase<TState> : IFringeStrategy<TState>
    {
        private readonly IPriorityQueue<PriorityWrapper> items = new IntervalHeap<PriorityWrapper>(new OrderWrapperComparer());

        public abstract int GetQueueOrder(TState state);

        public void Add(TState state)
        {
            if (state == null)
            {
                throw new ArgumentNullException(nameof(state));
            }
            var order = GetQueueOrder(state);
            items.Add(new PriorityWrapper(state, order));
        }

        public TState GetNext()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("Fringe is empty");
            }

            // Take lowest priority as we're defining priority to be it's order 
            var wrapper = items.DeleteMin(); 
            return wrapper.State;
        }

        public bool IsEmpty()
        {
            return items.IsEmpty;
        }

        private class PriorityWrapper
        {
            public TState State { get; }
            public long Priority { get; }

            public PriorityWrapper(TState state, long priority)
            {
                State = state;
                Priority = priority;
            }
        }

        private class OrderWrapperComparer : IComparer<PriorityWrapper>
        {
            public int Compare(PriorityWrapper x, PriorityWrapper y)
            {
                return x.Priority.CompareTo(y.Priority);
            }
        }
    }
}
