using DataAccess.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPFStylingTest;

namespace Dashboard_Admin
{
    /// <summary>
    /// Interaction logic for Loginpage.xaml
    /// </summary>
    public partial class Loginpage : Window
    {
        string filePath = "RememberMe.json";
        private readonly ManagerService managerService;
        public Loginpage()
        {
            managerService = App.GetService<ManagerService>();
            InitializeComponent();
        }
        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Check if the left mouse button was pressed
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                // Initiate the drag-and-drop operation
                this.DragMove();
            }
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Overlay.Visibility = Visibility.Visible;
            bool? isChecked = RememberMeCheckbox.IsChecked;
            if (await VerificationAsync())
            {
                MessageBox.Show("Login Successfully");
                if (isChecked == true)
                {
                    if (!File.Exists(filePath))
                    {
                        // Create a simple JSON object as an s
                        string jsonContent = "{\"RememberMe\": true}";

                        // Write the JSON content to the file
                        File.WriteAllText(filePath, jsonContent);
                    }
                    else
                    {
                        var settings = new Settings { RememberMe = true };
                        string jsonContent = JsonSerializer.Serialize(settings);

                        File.WriteAllText(filePath, jsonContent);
                    }
                }
                Overlay.Visibility = Visibility.Collapsed;
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
            } else
            {
                Overlay.Visibility = Visibility.Collapsed;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e) => this.Close();

        private async Task<bool> VerificationAsync()
        {
            bool Check = true;
            errorUsername.Text = "";
            errorPassword.Text = "";

            if (await managerService.CheckUsernameExistedAsync(txtUsername.Text))
            {
                if (!await managerService.CheckManagerExistedAsync(txtUsername.Text, txtPassword.Password))
                {
                    errorPassword.Text = "Username or Password does not exist";
                    Check = false;
                }
            }
            else
            {
                errorUsername.Text = "Username does not exist";
                Check = false;
            }

            return Check;
        }

        public class Settings
        {
            public bool RememberMe { get; set; }
        }

    }
}
