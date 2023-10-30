using SOSGameLogic.Interfaces;

/// <summary>
/// Represents the game board in the SOS game as a 2D character array.
/// </summary>
namespace SOSGameLogic.Implementation
{
    public class Board : IBoard
    {
        /// <summary>
        /// Gets or sets the 2D character array representing the game board.
        /// </summary>
        public char[,] board;

        /// <summary>
        /// Initializes a new instance of the <see cref="Board"/> class with the specified size.
        /// </summary>
        /// <param name="size">The size of the square game board.</param>
        public Board(int size)
        {
            board = new char[size, size]; // Initialize the game board with the specified size
            InitializeBoard(); // Initialize the game board with empty spaces
        }

        // Initializes the game board with empty spaces
        private void InitializeBoard()
        {
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    board[row, col] = ' '; // Initialize all cells with empty spaces
                }
            }
        }

        /// <summary>
        /// Gets the game board as a 2D character array.
        /// </summary>
        /// <returns>The game board represented as a 2D character array.</returns>
        public char[,] GetBoard()
        {
            return board;
        }

        /// <summary>
        /// Checks if a move to the specified row and column is valid.
        /// </summary>
        /// <param name="row">The row of the cell to be checked.</param>
        /// <param name="col">The column of the cell to be checked.</param>
        /// <returns><c>true</c> if the move is valid; otherwise, <c>false</c>.</returns>
        public bool IsValidMove(int row, int col)
        {
            return row >= 0 && row < board.GetLength(0) &&
                   col >= 0 && col < board.GetLength(1) &&
                   board[row, col] == ' ';
        }

        /// <summary>
        /// Places the specified symbol at the given row and column on the board.
        /// </summary>
        /// <param name="row">The row where the symbol will be placed.</param>
        /// <param name="col">The column where the symbol will be placed.</param>
        /// <param name="symbol">The symbol to be placed on the board.</param>
        public void PlaceSymbol(int row, int col, char symbol)
        {
            board[row, col] = symbol;
        }

        /// <summary>
        /// Retrieves the symbol at the specified row and column on the board.
        /// </summary>
        /// <param name="row">The row of the cell to retrieve the symbol from.</param>
        /// <param name="col">The column of the cell to retrieve the symbol from.</param>
        /// <returns>The symbol at the specified row and column on the board.</returns>
        public char GetSymbolAt(int row, int col)
        {
            return board[row, col];
        }

        /// <summary>
        /// Checks if the board is full (no empty spaces remain).
        /// </summary>
        /// <returns><c>true</c> if the board is full; otherwise, <c>false</c>.</returns>
        public bool IsBoardFull()
        {
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    if (board[row, col] == ' ')
                    {
                        return false; // If an empty cell is found, the board is not full
                    }
                }
            }
            return true; // If no empty cells are found, the board is full
        }
    }
}
