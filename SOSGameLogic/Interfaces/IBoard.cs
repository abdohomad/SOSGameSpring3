using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOSGameLogic.Interfaces
{
    public interface IBoard
    {
        char[,] GetBoard();
        char GetSymbolAt(int row, int col);
        bool IsBoardFull();
        bool IsValidMove(int row, int col);
        void PlaceSymbol(int row, int col, char symbol);
    }
}
