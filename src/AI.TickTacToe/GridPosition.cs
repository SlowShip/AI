namespace AI.TicTacToe
{
    public class GridPosition
    {
        public GridPosition(char column, int row)
        {
            Column = column;
            Row = row;
        }

        public char Column { get; }
        public int Row { get; }
    }
}
