using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Search.SearchFringes
{
    public class UniformCostSearchFringe<TState> : PriorityQueueDrivenFringeBase<TState> where TState : ICostTracked
    {
        public override int GetQueueOrder(TState state)
        {
            return state.Cost; 
        }
    }
}
