using Microsoft.Maui.Controls;
using System.IO;

namespace Practical_Work_2
{
    public partial class ForgotPasswordPage : ContentPage
    {
        private readonly string UsersFile = Path.Combine("Practical_Work_2", "User.csv");

        public ForgotPasswordPage()
        {
            InitializeComponent();
        }

        private async void ExitClicked(object sender, EventArgs e)
        {
            var salir = await DisplayAlert("Exit", "You want to Exit the app?", "Yes", "No");
            if (salir)
            {
                Application.Current.Quit();
            }
        }

        private async void SigninClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//MainPage");
        }

        private async void ChangeButton(object sender, EventArgs e)
        {
            if (NewPassword.IsVisible == false)
            {
                var usuario = BuscarUsuario(Username.Text);

                if (usuario == null)
                {
                    await DisplayAlert("Error", "User not found", "OK");
                    return;
                }

                NewPassword.IsVisible = true;
                ConfirmNewPassword.IsVisible = true;
                SubmitButton.Text = "Change password";
            }
            else
            {
                if (NewPassword.Text != ConfirmNewPassword.Text)
                {
                    await DisplayAlert("Error", "Passwords are different", "OK");
                    return;
                }

                var actualizado = CambiarPassword(Username.Text, NewPassword.Text);

                if (actualizado)
                {
                    await DisplayAlert("Succes", "Password updated", "OK");
                    await Navigation.PopAsync();
                }
            }
        }

        private User BuscarUsuario(string username)
        {
            if (!File.Exists(UsersFile))
            {
                return null;
            }

            var lineas = File.ReadAllLines(UsersFile);

            foreach (var linea in lineas)
            {
                var datos = linea.Split(',');
                if (datos.Length >= 2)
                {
                    if (datos[1] == username)
                    {
                        var user = new User();
                        user.Name = datos[0];
                        user.Username = datos[1];
                        user.Password = datos[2];
                        user.OperationsCount = int.Parse(datos[3]);
                        user.AcceptedPolicy = bool.Parse(datos[4]);
                        return user;
                    }
                }
            }
            return null;
        }

        private bool CambiarPassword(string username, string nuevaPassword)
        {
            try
            {
                var lineas = File.ReadAllLines(UsersFile);
                for (int i = 0; i < lineas.Length; i++)
                {
                    var datos = lineas[i].Split(',');
                    if (datos[1] == username)
                    {
                        datos[2] = nuevaPassword;
                        lineas[i] = string.Join(",", datos);
                        File.WriteAllLines(UsersFile, lineas);
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }

    public static class PasswordValidator
    {
        public static bool IsValid(string password)
        {
            bool largo = password.Length >= 8;
            bool mayus = password.Any(char.IsUpper);
            bool minus = password.Any(char.IsLower);
            bool digito = password.Any(char.IsDigit);
            bool especial = password.Any(c => !char.IsLetterOrDigit(c));
            if (largo && mayus && minus && digito && especial)
            {
                return true;
            }
            return false;
        }
    }
}