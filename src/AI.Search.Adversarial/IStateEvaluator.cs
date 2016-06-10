namespace AI.Search.Adversarial
{
    public interface IStateEvaluator<TState>
    {
        decimal GetValue(TState state);
    }
}
