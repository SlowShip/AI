using System;
using System.Collections.Generic;

namespace AI.TicTacToe
{
    //   a |b |c
    // 1|__|__|__
    // 2|__|__|__
    // 3|  |  |
    public class GameGrid
    {
        private const string _columns = "abc";
        private const string _rows = "123";

        private GridValue?[,] _grid = new GridValue?[3,3];

        // Todo, remove reliance on grid size = 3 or make overload private
        public GameGrid(GridValue?[,] grid = null)
        {
            _grid = grid ?? new GridValue?[3, 3];
        }

        public GridValue? this[char column, int row]
        {
            get { return _grid[GetColumnIndex(column), GetRowIndex(row)]; }
            private set { _grid[GetColumnIndex(column), GetRowIndex(row)] = value; }
        }

        public GridValue? this[int x, int y]
        {
            get { return _grid[x, y]; }
        }

        public void PlaceMove(Move move)
        {
            if(this[move.Position.Column, move.Position.Row] != null)
            {
                throw new ArgumentException($"Invalid move: cell is already occupied");
            }

            this[move.Position.Column, move.Position.Row] = move.Value;
        }

        public IEnumerable<GridPosition> GetEmptyCells()
        {
            foreach(var column in _columns)
            {
                foreach(var row in _rows)
                {
                    if(this[column, int.Parse(row.ToString())] == null)
                    {
                        yield return new GridPosition(column, int.Parse(row.ToString()));
                    }
                }
            }
        }

        public GameGrid Clone()
        {
            return new GameGrid((GridValue?[,])_grid.Clone());
        }

        private int GetColumnIndex(char col)
        {
            var ret = _columns.IndexOf(col.ToString(), StringComparison.OrdinalIgnoreCase);
            if(ret < 0)
            {
                throw new ArgumentOutOfRangeException($"Invalid column index: {col}", nameof(col));
            }
            return ret;
        }

        private int GetRowIndex(int row)
        {
            var ret = _rows.IndexOf(row.ToString());
            if (ret < 0)
            {
                throw new ArgumentOutOfRangeException($"Invalid row index: {row}", nameof(row));
            }
            return ret;
        }
    }
}
