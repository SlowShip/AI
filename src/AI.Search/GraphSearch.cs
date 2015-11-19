using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Search
{
    public class NoSolutionPossibleException : Exception
    {
        public NoSolutionPossibleException() : base() { }
        public NoSolutionPossibleException(string message) : base(message) { }
    }

    public class GraphSearch<TState>
    {
        // Todo, can this be made more efficient using recursion?
        public TState Execute(IProblem<TState> problem, IFringeStrategy<TState> strategy)
        {
            if (problem == null) { throw new ArgumentNullException(nameof(problem)); }
            if (strategy == null) { throw new ArgumentNullException(nameof(strategy)); }

            // Todo can a custom equality comparer make this better so graph search acutally works?
            var evaluatedStates = new HashSet<TState>();
            var startState = problem.GetStartingState();
            strategy.Add(startState);

            while (true)
            {
                if (strategy.IsEmpty())
                {
                    throw new NoSolutionPossibleException("Problem does not have a solution!");
                }

                var next = strategy.GetNext();

                if (evaluatedStates.Contains(next))
                {
                    continue;
                }

                if (problem.IsGoalState(next))
                {
                    return next;
                }

                var successors = problem.GetSuccessors(next);
                foreach (var successor in successors)
                {
                    strategy.Add(successor);
                }
                evaluatedStates.Add(next);
            }
        }
    }
}
