using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.TicTacToe.Players
{
    public interface IPlayer
    {
        string PlayerName { get; }
        GridValue PlayerSide { get; }
        Move GetMove(GameGrid grid);
    }
}
