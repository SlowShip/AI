using AI.TicTacToe.Players;
using System;

namespace AI.TicTacToe
{
    public class GameGridPrinter
    {
        public void PrintWinner(IPlayer player, GameGrid grid)
        {
            // Todo, highlight winning line
            Console.WriteLine();
            Console.WriteLine($"{player.PlayerName} Wins!");
            Print(grid);
            Console.WriteLine();
        }

        public void Print(GameGrid grid)
        {
            Console.WriteLine("   A   B   C");
            PrintRow("1", grid[0, 0], grid[1, 0], grid[2, 0]);
            PrintRowSeparator();
            PrintRow("2", grid[0, 1], grid[1, 1], grid[2, 1]);
            PrintRowSeparator();
            PrintRow("3", grid[0, 2], grid[1, 2], grid[2, 2]);
        }

        private void PrintRow(string rowLabel, GridValue? rowValue1, GridValue? rowValue2, GridValue? rowValue3)
        {
            Console.WriteLine("{0}  {1} | {2} | {3}", rowLabel, GetGridValue(rowValue1), GetGridValue(rowValue2), GetGridValue(rowValue3));
        }

        private void PrintRowSeparator()
        {
            Console.WriteLine("  -----------");
        }

        private string GetGridValue(GridValue? value)
        {
            switch (value)
            {
                case GridValue.O:
                    return "O";
                case GridValue.X:
                    return "X";
                default:
                    return " ";
            }
        }
    }
}
