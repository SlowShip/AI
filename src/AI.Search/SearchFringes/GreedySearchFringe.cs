namespace AI.Search.SearchFringes
{
    public class GreedySearchFringe<TState> : PriorityQueueDrivenFringeBase<TState>
    {
        private readonly IHuristic<TState> huristic;

        public GreedySearchFringe(IHuristic<TState> huristic)
        {
            this.huristic = huristic;
        }

        public override int GetQueueOrder(TState state)
        {
            return huristic.GetHuristicValue(state);
        }
    }
}
