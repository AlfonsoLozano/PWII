using Microsoft.Maui.Controls;
using System.IO;

namespace Practical_Work_2;

public partial class RegisterPage : ContentPage
{
    private readonly string UsersFile = Path.Combine("Practical_Work_2", "User.csv");

    public RegisterPage()
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

    private async void SignInClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }

    private async void PrivacyPolicyTapped(object sender, EventArgs e)
    {
        await DisplayAlert("Privacy Policy", "Your data will be store safely", "OK");
    }

    private async void RegisterClicked(object sender, EventArgs e)
    {
        bool valid = ValidateInputs();
        if (!valid)
        {
            return;
        }

        var newUser = new User();
        newUser.Name = Name.Text;
        newUser.Username = Username.Text;
        newUser.Password = Password.Text;
        newUser.OperationsCount = 0;
        newUser.AcceptedPolicy = PolicyCheckBox.IsChecked;

        SaveUser(newUser);

        await DisplayAlert("Succes", "Registration Completed!", "OK");
        await Shell.Current.GoToAsync("//MainPage");
    }

    private bool ValidateInputs()
    {
        try
        {
            if (Password.Text != RepeatPassword.Text)
            {
                DisplayAlert("Error", "Different Passwords", "OK");
                return false;
            }

            var usuarios = File.ReadAllLines(UsersFile);
            foreach (var u in usuarios)
            {
                var partes = u.Split(',');
                if (partes[1] == Username.Text)
                {
                    DisplayAlert("Error", "User already exists", "OK");
                    return false;
                }
            }

            return true;
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", $"Fail Autotification: {ex.Message}", "OK");
            return false;
        }
    }

    private void SaveUser(User user)
    {
        try
        {
            using (StreamWriter sw = File.AppendText(UsersFile))
            {
                string linea = user.Name + "," + user.Username + "," + user.Password + "," + user.OperationsCount + "," + user.AcceptedPolicy;
                sw.WriteLine(linea);
            }
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", $"Error when saving: {ex.Message}", "OK");
        }
    }
}