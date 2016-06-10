using System.Collections.Generic;

namespace AI.Search.Adversarial
{
    public class AdversarialSearchSolution<TState, TAction>
    {
        public TState State { get; }
        public decimal Value { get; }
        public IEnumerable<TAction> ActionsToState { get; }

        public AdversarialSearchSolution(TState state, decimal value, IEnumerable<TAction> actions)
        {
            State = state;
            Value = value;
            ActionsToState = actions;
        }
    }
}
