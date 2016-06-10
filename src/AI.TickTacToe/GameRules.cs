namespace AI.TicTacToe
{
    public class GameRules
    {
        public GridValue? GetWinner(GameGrid grid)
        {
            // Diagonal
            if (HaveSameValue(grid[0, 0], grid[1, 1], grid[2, 2]) || HaveSameValue(grid[2, 0], grid[1, 1], grid[0, 2]))
            {
                return grid[1, 1];
            }

            // Lines
            for (var x = 0; x < 3; x++)
            {
                if (HaveSameValue(grid[x, 0], grid[x, 1], grid[x, 2]))
                {
                    return grid[x, 0];
                }

                if (HaveSameValue(grid[0, x], grid[1, x], grid[2, x]))
                {
                    return grid[0, x];
                }
            }
            return null;
        }

        private bool HaveSameValue(GridValue? first, GridValue? second, GridValue? third)
        {
            if (first == null)
            {
                return false;
            }
            return first == second && second == third;
        }
    }
}
