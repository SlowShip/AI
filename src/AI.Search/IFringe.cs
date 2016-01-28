namespace AI.Search
{
    public interface IFringeStrategy<TState>
    {
        bool IsEmpty();
        TState GetNext();
        void Add(TState state);
    }
}
