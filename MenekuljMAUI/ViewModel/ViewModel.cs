
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Menekulj.Model;
using ViewModel;

namespace Menekulj.ViewModel
{
    public class ViewModel : ViewModelBase
    {

        private GameModel? gameModel;
        private string folderPath;

        public event EventHandler? GameOver;
        public event EventHandler? OnNewGame;
        public event EventHandler? OnPaused;
        public event EventHandler? OnResumed;
        public event EventHandler? OnSaving;
        public event EventHandler? OnLoading;

        public DelegateCommand? LoadGameCommand { get; private set; }
        public DelegateCommand? SaveGameCommand { get; private set; }
        public DelegateCommand? NewGameCommand { get; private set; }
        public DelegateCommand? PauseCommand { get; private set; }
        public DelegateCommand? ResumeCommand { get; private set; }
        public DelegateCommand? ChangeDirectionCommand { get; private set; }
        public DelegateCommand? StartGameCommand { get; private set; }
        public DelegateCommand? ChangeGameSizeCommand { get; private set; }


        public ObservableCollection<ViewModelCell> ViewModelCells { get; set; } = new ObservableCollection<ViewModelCell>();
        public ObservableCollection<StoredGameViewModel> StoredGames { get; private set; } = new ObservableCollection<StoredGameViewModel>();

        public byte MatrixSize { get => (GameIsCreated ? gameModel!.MatrixSize : (byte)0); }
        public bool GameIsCreated { get => gameModel is not null; }
        public bool Running { get => (GameIsCreated ? gameModel!.Running : false); }
        public bool PlayerWon { get => (GameIsCreated ? gameModel!.PlayerWon : false); }
        public bool ShowMenu { get => showMenu; private set { showMenu = value; OnPropertyChanged(nameof(ShowMenu)); } }
        public byte NewGameSize { get=>newGameSize;set { newGameSize=value; OnPropertyChanged(nameof(NewGameSize));}  }
        public Direction CurrentDir { get=>gameModel is null ? 0 : gameModel!.Player.LookingDirection; }

        public RowDefinitionCollection GameTableRows
        {
            get => new RowDefinitionCollection(Enumerable.Repeat(new RowDefinition(GridLength.Star), MatrixSize).ToArray());
        }
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
            folderPath = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "saves");
            foreach (var save in System.IO.Directory.GetFiles( folderPath))
            {
                StoredGameViewModel storedGameViewModel = new StoredGameViewModel(save.Substring(folderPath.Length).Trim(new char[] { '/', '\\' }).ToString(), System.IO.File.GetCreationTime(save));
                storedGameViewModel.LoadGameCommand = LoadGameCommand;
                storedGameViewModel.SaveGameCommand = SaveGameCommand;
                StoredGames.Add(storedGameViewModel);
            }
        }



        //****************************************************************************************
        //Actions
        private void Pause(object? obj)
        {
            gameModel?.Pause();
            ShowMenu = true;
            OnPaused?.Invoke(this, EventArgs.Empty);

        }

        private void Resume(object? obj)
        {
            gameModel?.Resume();
            ShowMenu = false;
            OnResumed?.Invoke(this, EventArgs.Empty);
        }

        private  void StartGame(object? obj)
        {
            //   await this.gameModel.StartGame();
            _ = Task.Run(() => this.gameModel!.StartGame());
            RefreshProperties();

        }

        private void ChangeDirection(object? obj)
        {
            byte num;
            if (byte.TryParse(obj?.ToString(), out num))
            {
                gameModel?.ChangePlayerDirection((Direction)num);
                OnPropertyChanged(nameof(CurrentDir));
            }

        }

        private void ChangeNewGameSize(object? obj)
        {
            if (obj is not null)
            {
                uint size;

                if (uint.TryParse(obj!.ToString()!.Split(',')[0], out size) && uint.TryParse(obj!.ToString()!.Split(',')[1], out this.newGameMineCount))
                {
                    this.NewGameSize = (byte)size;

                }
            }
        }

        private void NewGame(object? obj)
        {

            CreateNewGame(NewGameSize, newGameMineCount);
            OnNewGame?.Invoke(this, EventArgs.Empty);
        }

        private void SaveGame(object? obj)
        {
            if (obj is string && GameIsCreated)
            {
                string jsonified =  ((String)obj).Contains(".json") ? ((String)obj) : (string)obj + ".json";
                string filename = System.IO.Path.Combine(folderPath, jsonified);
                gameModel!.SaveGame(filename ).GetAwaiter();
                bool found = false;
                foreach (var item in StoredGames)
                {
                    if(item.Name ==  jsonified)
                    {
                        found = true;
                        item.Modified = DateTime.Now;
                        break;
                    }
                }

                if (!found) { 
                StoredGameViewModel storedGameViewModel = new StoredGameViewModel(jsonified, DateTime.Now);
                storedGameViewModel.LoadGameCommand = LoadGameCommand;
                storedGameViewModel.SaveGameCommand = SaveGameCommand;
                StoredGames.Add(storedGameViewModel);
                }
                OnSaving?.Invoke(this, EventArgs.Empty);
            }

        }

        private async void LoadGame(object? obj)
        {

            if (obj is string)
            {
                string jsonified = ((String)obj).Contains(".json") ? ((String)obj) : (string)obj + ".json";
                string filename = System.IO.Path.Combine(folderPath, jsonified);
                GameModel gameModel= await Persistance.Persistance.LoadStateAsync(filename);
                CreateNewGame(gameModel: gameModel);
                OnLoading?.Invoke(this, EventArgs.Empty);
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
