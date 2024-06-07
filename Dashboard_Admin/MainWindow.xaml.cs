using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
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
    }
}

