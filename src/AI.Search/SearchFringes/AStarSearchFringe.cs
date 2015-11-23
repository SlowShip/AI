using C5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Search.SearchFringes
{
    public class AStarSearchFringe<TState> : PriorityQueueDrivenFringeBase<TState> where TState : ICostTracked
    {
        private readonly IHuristic<TState> huristic;

        public AStarSearchFringe(IHuristic<TState> huristic)
        {
            this.huristic = huristic;
        }
        
        public override int GetQueueOrder(TState state)
        {
            return state.Cost + huristic.GetHuristicValue(state);
        }
    }
}
