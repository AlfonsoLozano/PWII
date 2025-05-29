using Microsoft.Maui.Controls;
using System.IO;

namespace Practical_Work_2
{
    public partial class ConversorPage : ContentPage
    {
        private readonly string UsersFile = Path.Combine("Practical_Work_2", "User.csv");
        private Converter mainConverter;

        public ConversorPage()
        {
            InitializeComponent();
            mainConverter = new Converter();
        }

       
        private void AcClicked(object sender, EventArgs e)
        {
            DisplayLabel.Text = "";
        }

        
        private async void ExitClicked(object sender, EventArgs e)
        {
           
            bool confirm = await DisplayAlert("Exit", "You want to Exit the app?", "Yes", "No");
            if (confirm)
            {
                Application.Current.Quit();
            }
        }

       
        private async void LogoutClicked(object sender, EventArgs e)
        {
            Preferences.Remove("currentUser");
            await Shell.Current.GoToAsync("//MainPage");
        }

       
        private void NumberButtonClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string valor = button.CommandParameter.ToString();
            string actual = DisplayLabel.Text;
            DisplayLabel.Text = actual + valor;
        }

        
        private async void ConversionButtonClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string parametro = button.CommandParameter?.ToString();
            int opIndex = 0;
            bool esNumero = int.TryParse(parametro, out opIndex);

            if (!esNumero)
            {
                return;
            }

            try
            {
                var conversion = mainConverter.Operations[opIndex];
                conversion.validate(DisplayLabel.Text);

                if (conversion.NeedBitSize())
                {
                    int bits = await GetBitSize(conversion.GetName());
                    if (bits > 0)
                    {
                        string resultado = conversion.Change(DisplayLabel.Text, bits);
                        DisplayLabel.Text = resultado;
                    }
                }
                else
                {
                    string resultado = conversion.Change(DisplayLabel.Text);
                    DisplayLabel.Text = resultado;
                }

                UpdateOperationsCount();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

      
        private async Task<int> GetBitSize(string conversionName)
        {
            string bitsStr = await DisplayPromptAsync(
                "Configuración",
                $"Bits needed {conversionName}:",
                "Aceptar",
                "Cancelar",
                maxLength: 2,
                keyboard: Keyboard.Numeric
            );

            int bits = 0;
            bool esNumero = int.TryParse(bitsStr, out bits);
            if (esNumero && bits > 0)
            {
                return bits;
            }
            else
            {
                return 0;
            }
        }

        private async void OperationsClicked(object sender, EventArgs e)
        {
            try
            {
                string currentUser = Preferences.Get("currentUser", "");
                var lineas = File.ReadLines(UsersFile);
                string userLine = null;

                foreach (var linea in lineas)
                {
                    var partes = linea.Split(',');
                    if (partes[1] == currentUser)
                    {
                        userLine = linea;
                        break;
                    }
                }

                if (userLine != null)
                {
                    var userData = userLine.Split(',');
                    await Navigation.PushAsync(new OperationsPage(
                        name: userData[0],
                        username: userData[1],
                        password: userData[2],
                        operations: userData[3]
                    ));
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Error while reading data: {ex.Message}", "OK");
            }
        }

        
        private void UpdateOperationsCount()
        {
            try
            {
                string currentUser = Preferences.Get("currentUser", "");
                var lines = File.ReadAllLines(UsersFile).ToList();

                for (int i = 0; i < lines.Count; i++)
                {
                    var partes = lines[i].Split(',');
                    if (partes[1] == currentUser)
                    {
                        int cantidad = int.Parse(partes[3]);
                        cantidad = cantidad + 1;
                        partes[3] = cantidad.ToString();
                        lines[i] = string.Join(",", partes);
                        break;
                    }
                }

                File.WriteAllLines(UsersFile, lines);
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", $"Error when updating operations: {ex.Message}", "OK");
            }
        }
    }
}