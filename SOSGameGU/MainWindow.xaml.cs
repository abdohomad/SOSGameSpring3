using SOSGameGU.GameManagers;
using SOSGameLogic.Implementation;
using SOSGameLogic.Interfaces;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace SOSGameGU
{
    public partial class MainWindow : Window
    {
        // Game-related variables
        public IGame game;
        public IPlayer player1;
        public IPlayer player2;
        public IPlayer currentPlayer;
        public string player1Name;
        public string player2Name;
        public bool IsReplayMode { get; set; }
        public string currentPlayerTurnName;
        public int boardSize;
        public bool player1SymbolSelected = true;
        public bool player2SymbolSelected = true;
        public bool gameHasStarted = false;
        public IGenericGameModeLogic _modeLogic;
        public char playerSymbol = ' ';
        public GameSetupManager gameSetupManager;
        public PlayerSelectionManager playerSelectionManager;
        public DrawSOSLineManager drawSOSLineManager;
        public ActiveGameManager activeGameManager;
        public GameRecordManager gameRecordManager;
        //public ReplayGameManager replayGameManager;
        public DispatcherTimer gameTimer;

        // Constructor
        public MainWindow()
        {
            InitializeComponent();
            InitializeGameComponents();
            SetupEventHandlers();
        }

        // Initialize game components
        private void InitializeGameComponents()
        {
            player1 = new HumanPlayer(playerSymbol);
            player2 = new HumanPlayer(playerSymbol);
            currentPlayer = player1;
            gameSetupManager = new GameSetupManager();
            playerSelectionManager = new PlayerSelectionManager(this);
            drawSOSLineManager = new DrawSOSLineManager();
            activeGameManager = new ActiveGameManager(drawSOSLineManager);
            _modeLogic = new SimpleGameMode();
            gameRecordManager = new GameRecordManager();
            //replayGameManager = new ReplayGameManager(this);
            IsReplayMode = false;
            game = new Game(boardSize, player1, player2, _modeLogic);
            gameTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(3) };
            gameTimer.Tick += GameTimer_Tick;
        }

        // Set up event handlers
        private void SetupEventHandlers()
        {
            GameBoardGrid.SizeChanged += (sender, e) => ResizeGameBoardGrid(e.NewSize.Width, e.NewSize.Height);
            gameTimer.Tick += GameTimer_Tick;
        }

        // Resize the game board grid based on the window size
        private void ResizeGameBoardGrid(double newWidth, double newHeight)
        {
            GameCanvas.Width = newWidth;
            GameCanvas.Height = newHeight;

            int numberOfColumns = GameBoardGrid.ColumnDefinitions.Count;
            int numberOfRows = GameBoardGrid.RowDefinitions.Count;
            double cellWidth = newWidth / numberOfColumns;
            double cellHeight = newHeight / numberOfRows;
        }

        // Event handler for the radio buttons representing game modes
        public void RadioButton_Checked_Game_Mode(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radioButton)
            {
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

            ComboBoxItem selectedItem = (ComboBoxItem)ddlBoardSize.SelectedItem;

            if (selectedItem != null)
            {
                string[] parts = selectedItem.Content.ToString().Split('x');
                int rows = int.Parse(parts[0]);
                int cols = int.Parse(parts[1]);

                GameBoardGrid.RowDefinitions.Clear();
                GameBoardGrid.ColumnDefinitions.Clear();

                player1Name = lblPlayer1.Content.ToString();
                player2Name = lblPlayer2.Content.ToString();
                currentPlayerTurnName = player1Name;
                txtCurrentPlayerTurn.Text = "Current Turn: " + currentPlayerTurnName;

                for (int i = 0; i < rows; i++)
                {
                    GameBoardGrid.RowDefinitions.Add(new RowDefinition());
                }

                for (int j = 0; j < cols; j++)
                {
                    GameBoardGrid.ColumnDefinitions.Add(new ColumnDefinition());
                }

                gameSetupManager.StartGame(GameBoardGrid, this, rows, GameCanvas);
                gameHasStarted = true;
                gameTimer.Start();
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
        public async  void Cell_Click(object sender, RoutedEventArgs e)
        {
            if (IsReplayMode)
            {
                return; // Skip cell clicks during replay
            }
            if (game.GetCurrentPlayer() is HumanPlayer humanPlayer)
            {
                activeGameManager.Cell_Click(sender, GameBoardGrid, this, GameCanvas, humanPlayer);
              
                await DelayAsync(3000);
            }
            else if (game.GetCurrentPlayer() is ComputerPlayer computerPlayer)
            {
                 activeGameManager.HandleComputerMoves(this, GameBoardGrid, GameCanvas, computerPlayer);
                activeGameManager.CheckGameState(this);
                

                await DelayAsync(3000);
            }
            if (RecordGameChecked)
            {
                // Save the records to JSON
                gameRecordManager.SaveRecordsToJson();

            }
        }

        // Event handler for the radio buttons representing player type
        private void RadioButton_PlayerType_Checked(object sender, RoutedEventArgs e)
        {
            playerSelectionManager.RadioButton_PlayerType_Checked(sender, e);
        }

        // Event handler for the game timer tick
        private async void GameTimer_Tick(object sender, EventArgs e)
        {

            if (game != null && !IsReplayMode) // Check if 'game' is not null and not in replay mode
            {
                await HandleAutomaticMoves();
            }


        }

        // Handle automatic moves for the computer player
        private async Task HandleAutomaticMoves()
        {
            if (IsReplayMode)
            {
                return; // Skip cell clicks during replay
            }
            while (!game.IsGameOver())
            {
                if (game.GetCurrentPlayer() is ComputerPlayer computerPlayer)
                {
                    
                    activeGameManager.HandleComputerMoves(this, GameBoardGrid, GameCanvas, computerPlayer);
                    activeGameManager.CheckGameState(this);
                    
                    await DelayAsync(5000);
                }
                else
                {
                    break;

                }
            }
            if (RecordGameChecked)
            {
                // Save the records to JSON
                gameRecordManager.SaveRecordsToJson();
                
            }
        }


        private async Task DelayAsync(int millisecondsDelay)
        {
            await Task.Delay(millisecondsDelay);
        }

        // Event handler for the game grid loaded
        private void GameGrid_Loaded(object sender, RoutedEventArgs e)
        {
            gameTimer.Start();
        }

        public bool RecordGameChecked
        {
            get { return chkRecordGame.IsChecked.GetValueOrDefault(); }
        }

        private void ReplayGame_Click(object sender, RoutedEventArgs e)
        {
            //IsReplayMode = true;
            //replayGameManager.ReplayGame_Click(sender, e);
        }


        public Button GetCellButtonFromPosition(int row, int col)
        {
            foreach (UIElement element in GameBoardGrid.Children)
            {
                if (element is Button cellButton)
                {
                    Tuple<int, int> cellPosition = (Tuple<int, int>)cellButton.Tag;
                    if (cellPosition.Item1 == row && cellPosition.Item2 == col)
                    {
                        return cellButton;
                    }
                }
            }

            return null;
        }

    }
}
