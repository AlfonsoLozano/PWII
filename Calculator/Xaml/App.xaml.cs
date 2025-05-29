namespace Practical_Work_2;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		
        var usersPath = Path.Combine("Practical_Work_2", "User.csv");

		MainPage = new AppShell();
	}
}
