using AI.Search.Adversarial;
using System.Collections.Generic;
using System.Linq;

namespace AI.TicTacToe.Players.AI
{
    public class TicTacToeAdversarialSearchProblem : IAdversarialSearchProblem<TicTacToeState, Move>
    {
        public IEnumerable<Move> GetAvaliableActions(TicTacToeState state)
        {
            var empty = state.Grid.GetEmptyCells();
            return empty.Select(pos => new Move(pos, state.SideToAct));
        }

        public TicTacToeState GetResultOfAction(TicTacToeState state, Move action)
        {
            var nextGrid = state.Grid.Clone();
            nextGrid.PlaceMove(action);

            var nextSide = state.SideToAct == GridValue.O ? GridValue.X : GridValue.O; 

            return new TicTacToeState(nextSide, nextGrid);
        }
    }
}
