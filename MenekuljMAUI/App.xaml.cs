using ViewModel;

namespace MenekuljMAUI;

public partial class App : Application
{
    private Menekulj.ViewModel.ViewModel viewModel;
    public App()
	{


		InitializeComponent();
		viewModel = new Menekulj.ViewModel.ViewModel();
		MainPage = new AppShell(viewModel) {
			BindingContext = viewModel
		};
	}


}
