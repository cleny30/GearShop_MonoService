using Dashboard_Admin;
using Dashboard_Admin.ImportProduct;
using Dashboard_Admin.OrderManagement;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFStylingTest
{
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();
            frMain.Content = new Dashboard();
        }

        private void DashboardButton_Click(object sender, RoutedEventArgs e)
        {
            frMain.Content = new Dashboard();
        }

        private void ProductButton_Click(object sender, RoutedEventArgs e)
        {
            frMain.Content = new ProductPage();
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            frMain.Content = new ImportProduct();
        }

        private void ImportReceipt_Click(object sender, RoutedEventArgs e)
        {
            frMain.Content = new ImportReceiptPage();
        }
        private void OrderManagement_Click(object sender, RoutedEventArgs e)
        {
            frMain.Content = new OrderManagement();
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            string FilePath = "RememberMe.json";
            var settings = new Settings { RememberMe = false };
            string jsonContent = JsonSerializer.Serialize(settings);

            File.WriteAllText(FilePath, jsonContent);
            Loginpage loginpage = new Loginpage();
            this.Close();
            loginpage.Show();  
        }
        public class Settings
        {
            public bool RememberMe { get; set; }
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
    }
}

