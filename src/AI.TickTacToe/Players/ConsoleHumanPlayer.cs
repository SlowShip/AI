using System;
using System.Text.RegularExpressions;

namespace AI.TicTacToe.Players
{
    // Todo, create abstraction over console?
    public class ConsoleHumanPlayer : IPlayer
    {
        public string PlayerName { get; }
        public GridValue PlayerSide { get; }

        public ConsoleHumanPlayer(string playerName, GridValue playerSide)
        {
            PlayerName = playerName;
            PlayerSide = playerSide;
        }

        public Move GetMove(GameGrid grid)
        {
            string input = null;
            do
            {
                Console.WriteLine($"{PlayerName}, please enter your move (column, row)");
                input = Console.ReadLine();
            }
            while (!IsValidInput(input));

            var pos = new GridPosition(input[0], int.Parse(input.Substring(1)));
            return new Move(pos, PlayerSide);
        }

        public bool IsValidInput(string input)
        {
            // Todo
            //var regex = new Regex("/^[a-zA-Z][1-9]*$/");
            // Also need to ensure cell is empty
            return true;
        }
    }
}
