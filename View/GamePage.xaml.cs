namespace MenekuljMAUI.View;

public partial class GamePage : ContentPage
{
	public GamePage()
	{

        InitializeComponent();
    }
    //Needed so the user can't outplay the pause system (I mean they would get in problem, so I'm a nice guy)
    private void ContentPage_NavigatedFrom(object sender, NavigatedFromEventArgs e)
    {
        ((Menekulj.ViewModel.ViewModel)BindingContext).PauseCommand.Execute(this);
    }
}