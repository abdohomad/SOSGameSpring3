using SOSGameLogic.Interfaces;
using System.Windows.Controls;
using System.Windows;

namespace SOSGameGU.GameManagers
{
    public class PlayerSelectionManager
    {
        private MainWindow mainWindow;

        public PlayerSelectionManager(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;

            // Initialize player selection UI components...
            InitializePlayerSelection();
        }

        private void InitializePlayerSelection()
        {
            mainWindow.rbPlayer1S.Checked += RadioButton_Checked;
            mainWindow.rbPlayer1O.Checked += RadioButton_Checked;
            mainWindow.rbPlayer2S.Checked += RadioButton_Checked;
            mainWindow.rbPlayer2O.Checked += RadioButton_Checked;
        }

        public void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            IPlayer currentPlayer = null;

            // Determine which player's radio button was checked...
            switch (radioButton.Name)
            {
                case "rbPlayer1S":
                case "rbPlayer1O":
                    currentPlayer = mainWindow.player1;
                    break;
                case "rbPlayer2S":
                case "rbPlayer2O":
                    currentPlayer = mainWindow.player2;
                    break;
                default:
                    break;
            }

            if (currentPlayer != null)
            {
                if (radioButton.Name.EndsWith("S"))
                {
                    currentPlayer.SetPlayerSymbol('S');
                    if (currentPlayer == mainWindow.player1)
                    {
                        mainWindow.player1SymbolSelected = true;
                    }
                    else if (currentPlayer == mainWindow.player2)
                    {
                        mainWindow.player2SymbolSelected = true;
                    }
                }
                else if (radioButton.Name.EndsWith("O"))
                {
                    currentPlayer.SetPlayerSymbol('O');
                    if (currentPlayer == mainWindow.player1)
                    {
                        mainWindow.player1SymbolSelected = true;
                    }
                    else if (currentPlayer == mainWindow.player2)
                    {
                        mainWindow.player2SymbolSelected = true;
                    }
                }
            }

            mainWindow.btnStartGame.IsEnabled = mainWindow.player1SymbolSelected || mainWindow.player2SymbolSelected;
        }
    }
}
