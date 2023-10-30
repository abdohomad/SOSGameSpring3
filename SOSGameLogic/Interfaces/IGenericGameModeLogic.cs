using System;
using System.Collections.Generic;

namespace SOSGameLogic.Interfaces
{
    /// <summary>
    /// Defines the contract for generic game mode logic in the SOS game.
    /// </summary>
    public interface IGenericGameModeLogic
    {
       
        /// <summary>
        /// Detect an SOS line for a given move on the game board.
        /// </summary>
        /// <param name="board">The current game board represented as a 2D character array.</param>
        /// <param name="playerMoves">A list of tuples containing the coordinates of all player moves.</param>
        /// <param name="row">The row of the current move.</param>
        /// <param name="col">The column of the current move.</param>
        /// <param name="currentPlayerSymbol">The symbol of the current player making the move.</param>
        /// <param name="currentPlayer">The current player object.</param>
        /// <returns>An SOSLine object representing the detected SOS line, or null if no SOS line is detected.</returns>
        SOSLine DetectSOSLine(char[,] board, int row, int col,  IPlayer currentPlayer);

        /// <summary>
        /// Check whether one of the players has won the game.
        /// </summary>
        /// <param name="_player1">The player object for player 1.</param>
        /// <param name="_player2">The player object for player 2.</param>
        /// <returns>True if one of the players has won the game; otherwise, false.</returns>
        bool PlayerHasWon(IPlayer _player1, IPlayer _player2);

        /// <summary>
        /// Check whether the game is over.
        /// </summary>
        /// <param name="board">The current game board object.</param>
        /// <param name="_player1">The player object for player 1.</param>
        /// <param name="_player2">The player object for player 2.</param>
        /// <returns>True if the game is over; otherwise, false.</returns>
        bool IsGameOver(IBoard board, IPlayer _player1, IPlayer _player2);

        /// <summary>
        /// Check whether the game is a draw.
        /// </summary>
        /// <param name="board">The current game board object.</param>
        /// <param name="_player1">The player object for player 1.</param>
        /// <param name="_player2">The player object for player 2.</param>
        /// <returns>True if the game is a draw; otherwise, false.</returns>
        bool IsDraw(IBoard board, IPlayer _player1, IPlayer _player2);
    }
}
