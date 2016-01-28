using System;
using System.Collections.Generic;
using System.Linq;

namespace AI.Search.SearchFringes
{
    public class BreadthFirstSearchFringe<TState> : IFringeStrategy<TState>
    {
        private Queue<TState> items = new Queue<TState>();

        public void Add(TState state)
        {
            if (state == null)
            {
                throw new ArgumentNullException(nameof(state));
            }
            items.Enqueue(state);
        }

        public TState GetNext()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("Fringe is empty");
            }
            return items.Dequeue();
        }

        public bool IsEmpty()
        {
            return !items.Any();
        }
    }
}
