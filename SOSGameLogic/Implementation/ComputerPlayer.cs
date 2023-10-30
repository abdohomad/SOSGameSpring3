using SOSGameLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOSGameLogic.Implementation
{
    public class ComputerPlayer : IPlayer
    {
        private char playerSymbol;
        private int score;

        public char GetPlayerSymbol()
        {
            return playerSymbol;
        }

        public void IncreaseScore(int points)
        {
            score += points;
        }

        public int GetScore()
        {
            return score;
        }

        public void SetPlayerSymbol(char symbol)
        {
            playerSymbol = symbol;
        }

        public void ResetScore()
        {
            score = 0;
        }

        // Implement an AI strategy for making random moves
        public Tuple<int, int> MakeRandomMove(char[,] board)
        {
            var random = new Random();
            int numRows = board.GetLength(0);
            int numCols = board.GetLength(1);
            int randomRow, randomCol;

            do
            {
                randomRow = random.Next(numRows);
                randomCol = random.Next(numCols);
            } while (board[randomRow, randomCol] != ' ');

            return Tuple.Create(randomRow, randomCol);
        }
    }
}
