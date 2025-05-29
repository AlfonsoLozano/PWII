using Microsoft.Maui.Controls;
using System.Linq;

namespace Practical_Work_2;

public partial class MainPage : ContentPage
{
    private readonly string UsersFile = Path.Combine("Practical_Work_2", "User.csv");

    public MainPage()
    {
        InitializeComponent();
    }

    private async void ExitClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Exit", "You want to Exit the app?", "Yes", "No");
        if (confirm)
        {
            Application.Current.Quit();
        }
    }

    private async void RegisterClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(RegisterPage));
    }

    private async void ForgotPasswordClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ForgotPasswordPage));
    }

    private async void SigninClicked(object sender, EventArgs e)
    {
        try
        {
            if (!File.Exists(UsersFile))
            {
                await DisplayAlert("Error", "No users registerd", "OK");
                return;
            }

            string[] users = File.ReadAllLines(UsersFile);

            string usuarioEncontrado = null;

            foreach (string linea in users)
            {
                string[] partes = linea.Split(',');
                if (partes.Length > 2)
                {
                    if (partes[1] == Username.Text && partes[2] == Password.Text)
                    {
                        usuarioEncontrado = partes[1];
                        break;
                    }
                }
            }

            if (usuarioEncontrado != null)
            {
                Preferences.Set("currentUser", Username.Text);
                await Shell.Current.GoToAsync(nameof(ConversorPage));
            }
            else
            {
                await DisplayAlert("Error", "Invalid Credentials", "OK");
            }
        }
        catch (IOException ex)
        {
            await DisplayAlert("Error", $"Error to acces: {ex.Message}", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Unexpected error: {ex.Message}", "OK");
        }
    }
}