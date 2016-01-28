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
        // Todo, can this be made more efficient or cleaner using recursion?
        public TState Execute(IProblem<TState> problem, IFringeStrategy<TState> strategy, IEqualityComparer<TState> stateEqualityComparer = default(IEqualityComparer<TState>))
        {
            if (problem == null)
            {
                throw new ArgumentNullException(nameof(problem));
            }

            if (strategy == null)
            {
                throw new ArgumentNullException(nameof(strategy));
            }

            var evaluatedStates = new HashSet<TState>(stateEqualityComparer);
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
