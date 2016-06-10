using AI.TicTacToe.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            var gameGrid = new GameGrid();
            var printer = new GameGridPrinter();
            var rules = new GameRules();

            // Todo, get the number of human players from console
            // 3d TicTacToe using some web graphics thing???
            IPlayer player1 = new ConsoleHumanPlayer("Player 1", GridValue.X);
            IPlayer player2 = new MiniMaxPlayer("Player 2", GridValue.O, rules);
            var playerQueue = new Queue<IPlayer>(new[] { player1, player2 });

            while (true)
            {
                printer.Print(gameGrid);

                var player = playerQueue.Dequeue();
                playerQueue.Enqueue(player);

                var move = player.GetMove(gameGrid);
                gameGrid.PlaceMove(move);
                var winner = rules.GetWinner(gameGrid);
                if(winner == null)
                {
                    continue;
                }

                printer.PrintWinner(playerQueue.First(x => x.PlayerSide == winner), gameGrid);
                break;
            }

            // Todo, abstract
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
