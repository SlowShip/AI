using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.TickTacToe.Players.AI
{
    public class TicTacToeState
    {
        public TicTacToeState(GridValue sideToAct, GameGrid grid)
        {
            SideToAct = sideToAct;
            Grid = grid;
        }

        public GridValue SideToAct { get; }
        public GameGrid Grid { get; }
    }
}
