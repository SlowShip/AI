using AI.Search.Adversarial;
using AI.TickTacToe.Players.AI;
using System.Linq;

namespace AI.TickTacToe.Players
{
    public class MiniMaxPlayer : IPlayer
    {
        private AdversarialSearch<TicTacToeState, Move> _search;

        public string PlayerName { get; }
        public GridValue PlayerSide { get; }

        public MiniMaxPlayer(string playerName, GridValue playerSide, GameRules rules)
        {
            PlayerName = playerName;
            PlayerSide = playerSide;

            var problem = new TicTacToeAdversarialSearchProblem();
            var stateEvaluator = new TicTacToeStateEvaluator(PlayerSide, rules);
            _search = new AdversarialSearch<TicTacToeState, Move>(problem, stateEvaluator);
        }

        public Move GetMove(GameGrid grid)
        {
            var state = new TicTacToeState(PlayerSide, grid);
            return _search.RunMinimax(state, 10).ActionsToState.First();
        }
    }
}
