using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Search
{
    public interface IProblem<TState>
    {
        TState GetStartingState();
        bool IsGoalState(TState state);
        IEnumerable<TState> GetSuccessors(TState state);
    }
}
