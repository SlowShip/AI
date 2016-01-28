namespace AI.Search
{
    public interface IHuristic<TState>
    {
        int GetHuristicValue(TState state);
    }
}
