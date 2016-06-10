namespace AI.TicTacToe
{
    public class Move
    {
        public Move(GridPosition pos, GridValue value)
        {
            Position = pos;
            Value = value;
        }

        public GridPosition Position { get; }
        public GridValue Value { get; }
    }
}
