using System;
using System.Collections.Generic;
using System.Linq;

namespace AI.Search.Adversarial
{
    public class AdversarialSearch<TState, TAction>
    {
        private readonly IAdversarialSearchProblem<TState, TAction> _problem;
        private readonly IStateEvaluator<TState> _stateEvaluator;

        public AdversarialSearch(IAdversarialSearchProblem<TState, TAction> problem, IStateEvaluator<TState> stateEvaluator)
        {
            _problem = problem;
            _stateEvaluator = stateEvaluator;
        }

        // Todo, Alpha, Beta pruning
        public AdversarialSearchSolution<TState, TAction> RunMinimax(TState startingState, int maxDepth)
        {
            if(startingState == null)
            {
                throw new ArgumentNullException(nameof(startingState));
            }

            if(maxDepth < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxDepth));
            }

            var value = _stateEvaluator.GetValue(startingState);

            var result = RecursivleyRunSearch(startingState, new Stack<TAction>(), AgentType.Maximiser, 0, maxDepth);
            if(result == null)
            {
                return null;
            }

            return new AdversarialSearchSolution<TState, TAction>(result.State, result.Value, result.Actions);
        }

        private SearchNode RecursivleyRunSearch(TState currentState, IEnumerable<TAction> currentActions, AgentType currentAgent, int currentDepth, int maxDepth)
        {
            if(currentDepth >= maxDepth)
            {
                return PackCurrentState(currentState, currentActions);
            }

            var availableActions = _problem.GetAvaliableActions(currentState);
            if (!availableActions.Any())
            {
                return PackCurrentState(currentState, currentActions);
            }

            SearchNode bestOpt = null;
            var nextAgent = currentAgent == AgentType.Maximiser ? AgentType.Minimiser : AgentType.Maximiser;
            foreach(var nextAction in availableActions)
            {
                var nextState = _problem.GetResultOfAction(currentState, nextAction);
                var nextDepth = currentDepth + 1;
                var nextActions = new List<TAction>(currentActions);
                nextActions.Add(nextAction);

                var result = RecursivleyRunSearch(nextState, nextActions, nextAgent, nextDepth, maxDepth);

                bestOpt = PickBestOptionForAgent(currentAgent, bestOpt, result);
            }

            return bestOpt;
        }

        private SearchNode PackCurrentState(TState state, IEnumerable<TAction> actions)
        {
            var value = _stateEvaluator.GetValue(state);
            return new SearchNode(state, value, actions);
        }


        // Todo, move agent logic out of here
        private SearchNode PickBestOptionForAgent(AgentType agentType, SearchNode a, SearchNode b)
        {
            if (a == null)
            {
                return b;
            }

            if (b == null)
            {
                return a;
            }

            if (agentType == AgentType.Maximiser)
            {
                return a.Value < b.Value ? b : a;
            }

            if (agentType == AgentType.Minimiser)
            {
                return a.Value > b.Value ? b : a;
            }

            throw new NotSupportedException();
        }

        private class SearchNode
        {
            public TState State { get; }
            public decimal Value { get; }
            public IEnumerable<TAction> Actions { get; }

            public SearchNode(TState state, decimal value, IEnumerable<TAction> actions)
            {
                State = state;
                Value = value;
                Actions = actions;
            }
        }
    }
}
