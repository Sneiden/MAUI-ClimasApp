using Climas.Views;

namespace Climas;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new NavigationPage(new MainTabbedPage());
	}
}
