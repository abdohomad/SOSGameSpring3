using SOSGameLogic.Implementation;
using System;
using System.Windows.Controls;
using System.Windows;

namespace SOSGameGU.GameManagers
{
    public class ActiveGameManager
    {
        private DrawSOSLineManager drawSOSLineManager;

        public ActiveGameManager(DrawSOSLineManager drawSOSLineManager)
        {
            this.drawSOSLineManager = drawSOSLineManager;
        }

        // Method for handling the click event on a game board cell.
        public void Cell_Click(object sender, Grid GameBoardGrid, MainWindow mainWindow,Canvas GameCanvas)
        {
            drawSOSLineManager = new DrawSOSLineManager();
            Button cellButton = (Button)sender;
            Tuple<int, int> cellPosition = (Tuple<int, int>)cellButton.Tag;
            int row = cellPosition.Item1;
            int col = cellPosition.Item2;

            // Check if the game is not over and both players have selected symbols.
            if (!mainWindow.game.IsGameOver())
            {
                if (!mainWindow.player1SymbolSelected || !mainWindow.player2SymbolSelected)
                {
                    MessageBox.Show("Both players must choose valid player symbols");
                    return;
                }
                // Check if the selected cell is already occupied.
                else if (mainWindow.game.IsCellOccupied(row, col))
                {
                    MessageBox.Show("This cell is already occupied.\n" +
                        "Please choose an empty cell.");
                    return;
                }
                else
                {
                    char currentPlayerSymbol = mainWindow.game.GetCurrentPlayerSymbol();
                    mainWindow.game.MakeMove(row, col);

                    // Update the cell button's content with the current player's symbol.
                    cellButton.Content = currentPlayerSymbol.ToString();
                    cellButton.HorizontalAlignment = HorizontalAlignment.Center;
                    cellButton.VerticalAlignment = VerticalAlignment.Center;
                    cellButton.FontSize = 15;

                    // Draw lines on the canvas to highlight SOS sequences.
                    drawSOSLineManager.DrawLinesOnCanvas(mainWindow, GameBoardGrid, GameCanvas);

                    // Update player scores and current player's turn information.
                    mainWindow.txtPlayer1Score.Text = mainWindow.player1.GetScore().ToString();
                    mainWindow.txtPlayer2Score.Text = mainWindow.player2.GetScore().ToString();

                    if (mainWindow.game.GetCurrentPlayer() == mainWindow.player1)
                    {
                        mainWindow.currentPlayerTurnName = mainWindow.player1Name;
                    }
                    else if (mainWindow.game.GetCurrentPlayer() == mainWindow.player2)
                    {
                        mainWindow.currentPlayerTurnName = mainWindow.player2Name;
                    }

                    mainWindow.txtCurrentPlayerTurn.Text = "Current Turn: " + mainWindow.currentPlayerTurnName;
                }
            }

            // Check the game state to see if it's over.
            CheckGameState(mainWindow);
        }

        // Method to check the game state and determine the winner.
        private void CheckGameState(MainWindow mainWindow)
        {
            if (mainWindow.game.IsGameOver())
            {
                // Add logic to determine the winner or draw in the SimpleGameMode.
                if (mainWindow._modeLogic is SimpleGameMode)
                {
                    if (mainWindow.player1.GetScore() >= 3)
                    {
                        MessageBox.Show($"Game Over! {mainWindow.player1Name} wins!\n" +
                            $"with score {mainWindow.player1.GetScore()}");
                    }
                    else if (mainWindow.player2.GetScore() >= 3)
                    {
                        MessageBox.Show($"Game Over! {mainWindow.player2Name} wins!\n" +
                            $"with score {mainWindow.player2.GetScore()}");
                    }
                    else
                    {
                        MessageBox.Show("Game Over! It's a Draw!");
                    }
                }
                else if (mainWindow._modeLogic is GeneralGameMode)
                {
                    // Add logic to determine the winner or draw in the GeneralGameMode.
                    if (mainWindow.player1.GetScore() > mainWindow.player2.GetScore())
                    {
                        MessageBox.Show($"Game Over! {mainWindow.player1Name} wins!\n" +
                            $"with score {mainWindow.player1.GetScore()}");
                    }
                    else if (mainWindow.player2.GetScore() > mainWindow.player1.GetScore())
                    {
                        MessageBox.Show($"Game Over! {mainWindow.player2Name} wins!\n" +
                            $"with score {mainWindow.player2.GetScore()}");
                    }
                    else
                    {
                        MessageBox.Show("Game Over! It's a Draw!");
                    }
                }
            }
        }
    }
}
