using ViewModel;

namespace MenekuljMAUI;

public partial class App : Application
{
    public App()
	{
        string mainDir = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "saves");

        if (!Directory.Exists(mainDir))
        {
            Directory.CreateDirectory(mainDir);
        }


        InitializeComponent();

		MainPage = new AppShell(new Menekulj.ViewModel.ViewModel()) {
	
		};
	}


}
