using System;
using System.Collections.Generic;
using System.Linq;

namespace AI.Search.SearchFringes
{
    public class DepthFirstSearchFringe<TState> : IFringeStrategy<TState>
    {
        private Stack<TState> items = new Stack<TState>();

        public void Add(TState state)
        {
            if (state == null)
            {
                throw new ArgumentNullException(nameof(state));
            }
            items.Push(state);
        }

        public TState GetNext()
        {
            if(IsEmpty())
            {
                throw new InvalidOperationException("Fringe is empty");
            }
            return items.Pop();            
        }

        public bool IsEmpty()
        {
            return !items.Any();
        }
    }
}
