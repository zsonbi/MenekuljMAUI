using System;
using System.Collections.ObjectModel;
using Menekulj.Model;
using ViewModel;

namespace Menekulj.ViewModel
{
    public class ViewModel : ViewModelBase
    {
        private GameModel? gameModel;

        public event EventHandler GameOver;
        public DelegateCommand LoadGameCommand { get; private set; }
        public DelegateCommand? SaveGameCommand { get; private set; }
        public DelegateCommand NewGameCommand { get; private set; }

        public DelegateCommand? PauseCommand { get; private set; }
        public DelegateCommand? ResumeCommand { get; private set; }

        public DelegateCommand? ChangeDirectionCommand { get; private set; }

        public DelegateCommand? StartGameCommand { get; private set; }

        public DelegateCommand? ChangeGameSizeCommand { get; private set; }

        public ObservableCollection<ViewModelCell> ViewModelCells { get; set; } = new ObservableCollection<ViewModelCell>();

        public byte MatrixSize { get => (GameIsCreated ? gameModel!.MatrixSize : (byte)0); }
        public bool GameIsCreated { get => gameModel is not null; }
        public bool Running { get => (GameIsCreated ? gameModel!.Running : false); }
        public bool PlayerWon { get => (GameIsCreated ? gameModel!.PlayerWon : false); }
        public bool ShowMenu { get => showMenu; private set { showMenu = value; OnPropertyChanged(nameof(ShowMenu)); } }


        /// <summary>
        /// Segédproperty a tábla méretezéséhez
        /// </summary>
        public RowDefinitionCollection GameTableRows
        {
            get => new RowDefinitionCollection(Enumerable.Repeat(new RowDefinition(GridLength.Star), MatrixSize).ToArray());
        }


        /// <summary>
        /// Segédproperty a tábla méretezéséhez
        /// </summary>
        public ColumnDefinitionCollection GameTableColumns
        {
            get => new ColumnDefinitionCollection(Enumerable.Repeat(new ColumnDefinition(GridLength.Star), MatrixSize).ToArray());
        }

        private byte newGameSize = 11;
        private uint newGameMineCount = 7;
        private bool showMenu = true;
        public ViewModel()
        {
            NewGameCommand = new DelegateCommand(new Action<object?>(NewGame));
            LoadGameCommand = new DelegateCommand(new Action<object?>(LoadGame));
            ChangeGameSizeCommand = new DelegateCommand(new Action<object?>(ChangeNewGameSize));
            PauseCommand = new DelegateCommand(new Action<object?>(Pause));
            ResumeCommand = new DelegateCommand(new Action<object?>(Resume));
            SaveGameCommand = new DelegateCommand(new Action<object?>(SaveGame));
            ChangeDirectionCommand = new DelegateCommand(new Action<object?>(ChangeDirection));
            StartGameCommand = new DelegateCommand(new Action<object?>(StartGame));

            this.ViewModelCells.Add(new ViewModelCell(0, 0, -1));
        }



        //****************************************************************************************
        //Actions
        private void Pause(object? obj)
        {
            gameModel?.Pause();
            ShowMenu = true;
        }

        private void Resume(object? obj)
        {
            gameModel?.Resume();
            ShowMenu = false;
        }

        private  void StartGame(object? obj)
        {
            //   await this.gameModel.StartGame();
            _ = Task.Run(() => this.gameModel!.StartGame());
            RefreshProperties();

        }

        private void ChangeDirection(object? obj)
        {
            if (obj is Direction)
            {
                gameModel?.ChangePlayerDirection((Direction)obj);
            }

        }

        private void ChangeNewGameSize(object? obj)
        {
            if (obj is not null)
            {
                uint size;

                if (uint.TryParse(obj!.ToString()!.Split(',')[0], out size) && uint.TryParse(obj!.ToString()!.Split(',')[1], out this.newGameMineCount))
                {
                    this.newGameSize = (byte)size;

                }
            }
        }

        private void NewGame(object? obj)
        {

            CreateNewGame(newGameSize, newGameMineCount);
        }

        private void SaveGame(object? obj)
        {
            if (obj is string && GameIsCreated)
            {
                gameModel!.SaveGame((string)obj).GetAwaiter();
            }

        }

        private void LoadGame(object? obj)
        {
            if (obj is GameModel)
            {
                CreateNewGame(gameModel: (GameModel)obj);
            }
        }


        //*********************************************************************************************
        //Actions end
        //********************************************************************************************

        //   Rectangle[,]? viewCells;

        private void RefreshProperties()
        {
            OnPropertyChanged(nameof(ShowMenu));
        }


        public Menekulj.Model.Cell GetCell(int row, int col)
        {


            return this.gameModel == null ? Menekulj.Model.Cell.Empty : this.gameModel.GetCell(row, col);
        }




        private void CreateNewGame(byte boardSize = 0, uint mineCount = 0, GameModel? gameModel = null)
        {
            if (gameModel != null)
            {
                this.gameModel = gameModel;

            }
            else
            {
                this.gameModel = new GameModel(boardSize, mineCount);
            }
            this.gameModel!.UpdateView += UpdateUnits;
            this.gameModel!.GameOver += GameOver;

            this.ViewModelCells.Clear();

            for (int i = 0; i < this.gameModel.MatrixSize; i++)
            {
                for (int j = 0; j < this.gameModel.MatrixSize; j++)
                {
                    ViewModelCell vMCell = new ViewModelCell(i, j, i * this.gameModel.MatrixSize + j);
                    vMCell.CellType = this.gameModel.GetCell(i, j);
                    this.ViewModelCells.Add(vMCell);
                }
            }
            ShowMenu = false;
            OnPropertyChanged(nameof(GameIsCreated));
        }



        private void UpdateUnits(object? sender, EventArgs args)
        {
            if (this.ViewModelCells == null)
            {
                throw new NullReferenceException("ViewModelCells");
            }

            if (gameModel == null)
            {
                throw new NoGameCreatedException();
            }

            foreach (var enemy in gameModel.Enemies)
            {

                this.ViewModelCells[enemy.PrevPosition.Row * gameModel.MatrixSize + enemy.PrevPosition.Col].CellType = Menekulj.Model.Cell.Empty;
                if (!enemy.Dead)
                {
                    this.ViewModelCells[enemy.Position.Row * gameModel.MatrixSize + enemy.Position.Col].CellType = Menekulj.Model.Cell.Enemy;
                }
            }

            this.ViewModelCells[gameModel.Player.PrevPosition.Row * gameModel.MatrixSize + gameModel.Player.PrevPosition.Col].CellType = Menekulj.Model.Cell.Empty;
            this.ViewModelCells[gameModel.Player.Position.Row * gameModel.MatrixSize + gameModel.Player.Position.Col].CellType = Menekulj.Model.Cell.Player;

            OnPropertyChanged(nameof(ViewModelCells));

        }









    }
}
