using System.Collections.Generic;

namespace AI.Search.Adversarial
{
    public interface IAdversarialSearchProblem<TState, TAction>
    {
        IEnumerable<TAction> GetAvaliableActions(TState state);
        TState GetResultOfAction(TState state, TAction action);
    }
}
