
using System;

namespace SOSGameLogic.Interfaces
{
    public interface IPlayer
    {
        char GetPlayerSymbol();
        void IncreaseScore(int points);
        int GetScore();
        void SetPlayerSymbol(char symbol);
        void ResetScore();

        void MakeMove(IBoard board, int row, int col);


    }
}
