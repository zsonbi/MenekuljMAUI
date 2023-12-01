
using Microsoft.Maui.Controls;

namespace MenekuljMAUI;

public partial class AppShell : Shell
{
    Menekulj.ViewModel.ViewModel viewModel;

    public AppShell(Menekulj.ViewModel.ViewModel viewModel)
    {
        this.viewModel = viewModel;
        viewModel.GameOver += GameOverEvent;
        viewModel.NewGameCommand.Execute(this);
        viewModel.StartGameCommand.Execute(this);
        InitializeComponent();
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
       await this.Dispatcher.DispatchAsync(async ()=>
             { 
                // győzelemtől függő üzenet megjelenítése
                if(await DisplayAlert("Result", message, accept: "New Game", cancel: "Exit game"))
                 {
                     viewModel.NewGameCommand.Execute(null);
                     viewModel.StartGameCommand?.Execute(null);
                 }
                 else
                 {
                    Application.Current.Quit();
                 }
            });
    }

}
