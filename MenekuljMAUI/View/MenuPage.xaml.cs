using Menekulj.ViewModel;

namespace MenekuljMAUI.View;

public partial class MenuPage : ContentPage
{
  //  private Menekulj.ViewModel.ViewModel viewModel;
    public MenuPage()
    {
  //      this.viewModel = viewModel;
//        BindingContext = viewModel;
        
        InitializeComponent();

    }


    private async void SaveGameBtn_Click(object sender, EventArgs e)
    {
        //if (!viewModel.GameIsCreated)
        //{
        //    await DisplayAlert("Error", "No game is running", "Ok");
        //    return;
        //}

        //SaveFileDialog saveFileDialog = new SaveFileDialog();
        //saveFileDialog.DefaultExt = "json";
        ////saveFileDialog.Filter ="(*.json)";
        //if (saveFileDialog.ShowDialog()!.Value)
        //{
        //    try
        //    {
        //        viewModel.SaveGameCommand?.Execute($"{saveFileDialog.FileName}");
        //    }
        //    catch (Exception)
        //    {
        //        MessageBox.Show("Error while saving the game");
        //    }


        //}
       await Navigation.PushAsync(new SavePage() { BindingContext=this.BindingContext});
    }

    private async void LoadGameBtn_Click(object sender, EventArgs e)
    {

        await Navigation.PushAsync(new LoadPage() { BindingContext = this.BindingContext });

    }

    private void ExitBtn_Click(object sender, EventArgs e)
    {
        Application.Current.Quit();
    }


}