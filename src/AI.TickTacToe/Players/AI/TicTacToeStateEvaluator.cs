using AI.Search.Adversarial;

namespace AI.TicTacToe.Players.AI
{
    public class TicTacToeStateEvaluator : IStateEvaluator<TicTacToeState>
    {
        private readonly GameRules _rules;
        private readonly GridValue _side;

        public TicTacToeStateEvaluator(GridValue side, GameRules rules)
        {
            _side = side;
            _rules = rules;
        }

        public decimal GetValue(TicTacToeState state)
        {
            var winner = _rules.GetWinner(state.Grid);
            if(winner == null)
            {
                return 0.0m;
            }
            return winner == _side ? 1.0m : -1.0m;
        }
    }
}
