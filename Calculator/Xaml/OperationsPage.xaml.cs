using Microsoft.Maui.Controls;

namespace Practical_Work_2;

public partial class OperationsPage : ContentPage
{
    public OperationsPage(string name, string username, string password, string operations)
    {
        InitializeComponent();
        MostrarDatosUsuario(name, username, password, operations);
    }

    private void MostrarDatosUsuario(string nombre, string usuario, string clave, string ops)
    {
        lblName.Text = "Name: " + nombre;
        lblUsername.Text = "Ussername: " + usuario;
        lblPassword.Text = "Password: " + clave;
        lblOperations.Text = "Operations: " + ops;
    }

    private async void ExitButtonClicked(object sender, EventArgs e)
    {
        bool salir = await DisplayAlert("Exit", "You want to Exit the app?", "Yes", "No");
        if (salir)
        {
            Application.Current.Quit();
        }
    }

    private async void BackButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}