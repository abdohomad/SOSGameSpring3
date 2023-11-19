using SOSGameLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SOSGameLogic.Implementation
{
    public class Game : IGame
    {
        private readonly IBoard board;
        private IPlayer currentPlayer;
        private readonly IPlayer player1;
        private readonly IPlayer player2;
        private readonly List<Tuple<int, int>> playerMoves;
        private readonly List<SOSLine> detectedSOSLines;
        internal readonly IGenericGameModeLogic modeLogic;
       
       
     
        public Game(int size, IPlayer player1, IPlayer player2, IGenericGameModeLogic modeLogic)
        {
            board = new Board(size);
            this.player1 = player1;
            this.player2 = player2;
            currentPlayer = player1;
            playerMoves = new List<Tuple<int, int>>();
            detectedSOSLines = new List<SOSLine>();
            this.modeLogic = modeLogic;
            
     
        }

        public bool IsCellOccupied(int row, int col)
        {
            return board.GetSymbolAt(row, col) != ' ';
        }
        public bool IsGameOver()
        {
            return modeLogic.IsGameOver(board, player1, player2);
        }

        public void SwitchPlayer()
        {
            currentPlayer = (currentPlayer == player1) ? player2 : player1;
        }

        public IPlayer GetCurrentPlayer()
        {
            return currentPlayer;
        }

        public void MakeMove(int row, int col)
        {
            if (!IsGameOver() && board.IsValidMove(row, col))
            {

                // currentPlayer is a reference to the IPlayer interface, and it's used to call the MakeMove method.
                // Both ComputerPlayer and HumanPlayer, which implement IPlayer, can be substituted for currentPlayer without breaking
                // the expected behavior of making a move on the game board.

                currentPlayer.MakeMove(board, row, col);

                // The MakeMove method is a part of the IPlayer interface, and it is open for extension
                // by allowing different player types to implement their own logic for making moves.
                // Adding a new type of player (e.g., creating a new class that implements IPlayer) does
                // not require modifying the existing code. Instead, it extends the behavior through
                // polymorphism and adherence to the IPlayer interface.


                Tuple<int, int> move = Tuple.Create(row, col);
                playerMoves.Add(move);
                SOSLine sosLine = modeLogic.DetectSOSLine(board.GetBoard(), row, col, currentPlayer);
                if (sosLine != null)
                {
                    detectedSOSLines.Add(sosLine);
                    currentPlayer.IncreaseScore(3);
                }
                else
                { 
                    
                    SwitchPlayer();
                }
                
            }
        }


        public List<SOSLine> GetDetectedSOSLines()
        {
            return detectedSOSLines;
        }

       
        public char GetCurrentPlayerSymbol()
        {
            return currentPlayer.GetPlayerSymbol();
        }

        public IBoard GetBoard()
        {
            return board;
        }
    }
}
