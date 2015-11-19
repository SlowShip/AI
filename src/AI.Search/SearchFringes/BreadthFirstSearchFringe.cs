using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Search.SearchFringes
{
    public class BreadthFirstSearchFringe<TState> : IFringeStrategy<TState>
    {
        private Queue<TState> items = new Queue<TState>();

        public void Add(TState node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }
            items.Enqueue(node);
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
