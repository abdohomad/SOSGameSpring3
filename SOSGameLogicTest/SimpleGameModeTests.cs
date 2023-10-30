using SOSGameLogic.Implementation;
using SOSGameLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace SOSGameLogicTest
{
    public class SimpleGameModeTests
    {
        [Fact]
        public static void IsGameOverShouldReturnTrueWhenPlayer1Wins()
        {
            // Arrange
            IBoard board = new Board(3);
            IPlayer player1 = new Player('S');
            IPlayer player2 = new Player('O');
            var gameMode = new SimpleGameMode();

            player1.IncreaseScore(3);
            bool isGameOver = gameMode.IsGameOver(board, player1, player2);

            // Assert
            Assert.True(isGameOver);
        }

        [Fact]
        public void IsGameOverShouldReturnTrueWhenPlayer2Wins()
        {
            // Arrange
            IBoard board = new Board(3);
            IPlayer player1 = new Player('S');
            IPlayer player2 = new Player('O');
            var gameMode = new SimpleGameMode();
            player2.IncreaseScore(3);
            bool isGameOver = gameMode.IsGameOver(board, player1, player2);

            // Assert
            Assert.True(isGameOver);
        }

        [Fact]
        public void IsGameOverShouldReturnFalseWhenGameIsIncomplete()
        {
            // Arrange
            IBoard board = new Board(3);
            IPlayer player1 = new Player('S');
            IPlayer player2 = new Player('O');
            var gameMode = new SimpleGameMode();
            bool isGameOver = gameMode.IsGameOver(board, player1, player2);
            Assert.False(isGameOver);
        }

        [Fact]
        public void PlayerHasWonShouldReturnFalseWhenNoPlayerWins()
        {
            IBoard board = new Board(3);
            IPlayer player1 = new Player('S');
            IPlayer player2 = new Player('O');
            var gameMode = new SimpleGameMode();
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    board.PlaceSymbol(row, col, 'S');
                }
            }

            bool player1HasWon = gameMode.PlayerHasWon(player1, player2);

            Assert.False(player1HasWon);
        }

        [Fact]
        public void IsDrawShouldReturnFalseWhenGameIsIncomplete()
        {
            // Arrange
            IBoard board = new Board(3);
            IPlayer player1 = new Player('S');
            IPlayer player2 = new Player('S');
            var gameMode = new SimpleGameMode();

            for (int row = 0; row < 2; row++)
            {
                for (int col = 0; col < 2; col++)
                {
                    board.PlaceSymbol(row, col, 'S');
                }
            }


            bool isDraw = gameMode.IsDraw(board, player1, player2);

            Assert.False(isDraw);
        }

    }
}
