
using System.Collections.Generic;

namespace SOSGameLogic.Interfaces
{
    public interface IGame
    {

        IPlayer GetCurrentPlayer();
        char GetCurrentPlayerSymbol();
        bool IsCellOccupied(int row, int col);
        bool IsGameOver();
        void MakeMove(int row, int col);
        void SwitchPlayer();
        List<SOSLine> GetDetectedSOSLines();
    }
}
