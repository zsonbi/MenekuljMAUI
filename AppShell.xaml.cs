#nullable enable
using MenekuljMAUI.View;
using Microsoft.Maui.Controls;

namespace MenekuljMAUI;

public partial class AppShell : Shell
{
    Menekulj.ViewModel.ViewModel viewModel;
    GamePage? gamePage;
    public AppShell(Menekulj.ViewModel.ViewModel viewModel)
    {


        this.viewModel = viewModel;
        viewModel.GameOver += GameOverEvent;
        viewModel.OnNewGame += NewGameEvent;
        viewModel.OnPaused += PausedEvent;
        viewModel.OnResumed += ResumedEvent;
        viewModel.OnLoading += LoadingEvent;
        viewModel.OnSaving+=SavingEvent;
        //  viewModel.NewGameCommand.Execute(this);
        //   viewModel.StartGameCommand.Execute(this);

        InitializeComponent();


    }

    private async void NewGameEvent(object? sender, EventArgs e)
    {
        //await this.Navigation.PopAsync();
        if (!Navigation.NavigationStack.Contains(gamePage) || gamePage is null)
        {
            gamePage = new GamePage { BindingContext = viewModel };
            await Navigation.PushAsync(gamePage);
            Shell.SetNavBarIsVisible(gamePage, false);
        }
       

        viewModel.StartGameCommand?.Execute(this);

    }

    private async void SavingEvent(object ?sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
    
    private async void LoadingEvent(object? sender, EventArgs e)
    {
       await Navigation.PopAsync();
        if (gamePage is null)
        {
            gamePage = new GamePage { BindingContext = viewModel };
        }
        await Navigation.PushAsync(gamePage);
        viewModel.StartGameCommand?.Execute(this);
    }


    private async void PausedEvent(object? sender , EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }

    private async void ResumedEvent(object? sender, EventArgs e)
    {
        if(gamePage is null)
        {
            gamePage = new GamePage { BindingContext = viewModel };
        }
        await Navigation.PushAsync(gamePage);
    }

    /// <summary>
    ///     Játék végének eseménykezelője.
    /// </summary>
    private async void GameOverEvent(object? sender, EventArgs e)
    {

        string message;
        if (viewModel!.PlayerWon)
        {
            message = "You won! Want to try again?";
        }
        else
        {
            message = "Game over! You died :C Want to try again?";

        }
        await this.Dispatcher.DispatchAsync(async () =>
              {

                  // győzelemtől függő üzenet megjelenítése
                  if (await DisplayAlert("Result", message, accept: "New Game", cancel: "Exit game"))
                  {
                    //  await Navigation.PopAsync();
                      viewModel.NewGameCommand.Execute(null);

                  }
                  else
                  {
                      Application.Current?.Quit();
                  }
              });
    }

}
