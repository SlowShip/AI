using System.Collections.Generic;

namespace AI.Search
{
    public interface IProblem<TState>
    {
        TState GetStartingState();
        bool IsGoalState(TState state);
        IEnumerable<TState> GetSuccessors(TState state);
    }
}
