using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Search.SearchFringes
{
    public class DepthFirstSearchFringe<TState> : IFringeStrategy<TState>
    {
        private Stack<TState> items = new Stack<TState>();

        public void Add(TState node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }
            items.Push(node);
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
