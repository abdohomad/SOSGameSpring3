using SOSGameGU.GameManagers;
using SOSGameLogic.Implementation;
using SOSGameLogic.Interfaces;
using System.Windows;
using System.Windows.Controls;

namespace SOSGameGU
{
    public partial class MainWindow : Window
    {
        public IGame game;              // Reference to the game logic
        public IPlayer player1;         // Player 1
        public IPlayer player2;         // Player 2
        public string player1Name;             // Name of Player 1
        public string player2Name;             // Name of Player 2
        public string currentPlayerTurnName;   // Name of the current player
        public int boardSize;
        public bool player1SymbolSelected = false;
        public bool player2SymbolSelected = false;
        public IGenericGameModeLogic _modeLogic;
        public char playerSymbol = ' ';
        public readonly IPlayer currentPlayer;
        public readonly GameSetupManager gameSetupManager;
        public readonly PlayerSelectionManager playerSelectionManager;
        public readonly DrawSOSLineManager drawSOSLineManager;
        public readonly ActiveGameManager activeGameManager;

        public MainWindow()
        {
            InitializeComponent();

            // Event handler for resizing the game board grid.
            GameBoardGrid.SizeChanged += (sender, e) =>
            {
                double newWidth = e.NewSize.Width;
                double newHeight = e.NewSize.Height;
                GameCanvas.Width = newWidth;
                GameCanvas.Height = newHeight;

                // Calculate the cell width and height based on the grid dimensions.
                int numberOfColumns = GameBoardGrid.ColumnDefinitions.Count;
                int numberOfRows = GameBoardGrid.RowDefinitions.Count;
                double cellWidth = newWidth / numberOfColumns;
                double cellHeight = newHeight / numberOfRows;
            };

            player1 = new Player(playerSymbol); // Initialize Player 1
            player2 = new Player(playerSymbol); // Initialize Player 2

            gameSetupManager = new GameSetupManager();
            drawSOSLineManager = new DrawSOSLineManager();
            playerSelectionManager = new PlayerSelectionManager(this);
            activeGameManager = new ActiveGameManager(drawSOSLineManager);
            _modeLogic = new SimpleGameMode();
            game = new Game(boardSize, player1, player1, _modeLogic); // Initialize the game
        }

        // Event handler for when a radio button for game mode is checked
        public void RadioButton_Checked_Game_Mode(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radioButton)
            {
                // Check which radio button was checked and update the selected game mode
                if (radioButton.Name == "rbSimpleMode")
                {
                    _modeLogic = new SimpleGameMode();
                }
                else if (radioButton.Name == "rbGeneralMode")
                {
                    _modeLogic = new GeneralGameMode();
                }
            }
        }

        // Event handler for the "Start Game" button click
        public void StartGame_Click(object sender, RoutedEventArgs e)
        {
           
            drawSOSLineManager.ClearLinesOnCanvas(GameCanvas);
            gameSetupManager.ClearGameVariables(this);
            gameSetupManager.ClearGameBoard(GameBoardGrid);
            // Get the selected board size from the ComboBox
            ComboBoxItem selectedItem = (ComboBoxItem)ddlBoardSize.SelectedItem;

            if (selectedItem != null)
            {

                // Parse the selected item's content to get the board size
                string[] parts = selectedItem.Content.ToString().Split('x');
                int rows = int.Parse(parts[0]);
                int cols = int.Parse(parts[1]);

                // Clear any existing rows and columns from the grid
                GameBoardGrid.RowDefinitions.Clear();
                GameBoardGrid.ColumnDefinitions.Clear();

                player1Name = lblPlayer1.Content.ToString();
                player2Name = lblPlayer2.Content.ToString();
                currentPlayerTurnName = player1Name;
                txtCurrentPlayerTurn.Text = "Current Turn: " + currentPlayerTurnName;

                // Add new rows and columns based on the selected board size
                for (int i = 0; i < rows; i++)
                {
                    GameBoardGrid.RowDefinitions.Add(new RowDefinition());
                }

                for (int j = 0; j < cols; j++)
                {
                    GameBoardGrid.ColumnDefinitions.Add(new ColumnDefinition());
                }

                // Start the game by setting up the game board.
                gameSetupManager.StartGame(GameBoardGrid, this, rows);
            }
            else
            {
                MessageBox.Show("Please select a board size before starting the game.");
            }
        }

        // Event handler for player symbol radio buttons
        public void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            playerSelectionManager.RadioButton_Checked(sender, e);
        }

        // Event handler for cell button click
        public void Cell_Click(object sender, RoutedEventArgs e)
        {
            activeGameManager.Cell_Click(sender, GameBoardGrid, this, GameCanvas);
        }
    }
}
