using ViewModel;

namespace MenekuljMAUI;

public partial class App : Application
{
    private Menekulj.ViewModel.ViewModel viewModel;
    public App()
	{
        string mainDir = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "saves");

        if (!Directory.Exists(mainDir))
        {
            Directory.CreateDirectory(mainDir);
        }


        InitializeComponent();
		viewModel = new Menekulj.ViewModel.ViewModel();
		MainPage = new AppShell(viewModel) {
			BindingContext = viewModel
		};
	}


}
