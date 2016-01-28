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
