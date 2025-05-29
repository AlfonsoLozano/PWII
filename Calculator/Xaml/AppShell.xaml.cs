namespace Practical_Work_2
{

	public partial class AppShell : Shell
	{
		public AppShell()
		{
			InitializeComponent();
			Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));

			Routing.RegisterRoute(nameof(ConversorPage), typeof(ConversorPage));

			Routing.RegisterRoute(nameof(ForgotPasswordPage), typeof(ForgotPasswordPage));

			Routing.RegisterRoute(nameof(OperationsPage),typeof(OperationsPage));
		}
	}
}