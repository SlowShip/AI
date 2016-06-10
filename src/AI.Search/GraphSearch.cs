using System;
using System.Collections.Generic;

namespace AI.Search
{
    public class GraphSearch<TState>
    {
        private readonly IEqualityComparer<TState> stateEqualityComparer;
        private readonly IFringeStrategy<TState> strategy;

        public GraphSearch(IFringeStrategy<TState> strategy, IEqualityComparer<TState> stateEqualityComparer = null)
        {
            if (strategy == null)
            {
                throw new ArgumentNullException(nameof(strategy));
            }
            
            this.strategy = strategy;
            this.stateEqualityComparer = stateEqualityComparer ?? default(IEqualityComparer<TState>); // May still be null, but thats ok
        }

        public TState Execute(IProblem<TState> problem)
        {
            if (problem == null)
            {
                throw new ArgumentNullException(nameof(problem));
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
