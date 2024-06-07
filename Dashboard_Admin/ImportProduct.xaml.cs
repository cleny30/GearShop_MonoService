using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFStylingTest.ViewModel;

namespace WPFStylingTest
{
    /// <summary>
    /// Interaction logic for ImportProduct.xaml
    /// </summary>
    public partial class ImportProduct : Page
    {
        public ImportProduct()
        {
            InitializeComponent();
            DataContext = new MainViewModel();

        }

        private void ToggleContentButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                if (button.Content.ToString() == "Show Normal Product")
                {
                    button.Content = "Show Out of Stocks";
                    titleChange.Text = "PRODUCTS";
                    OutOfStock.Visibility = Visibility.Collapsed;
                    OutOfStockControl.Visibility = Visibility.Collapsed;
                    NormalProduct.Visibility = Visibility.Visible;
                    NormalProductControl.Visibility = Visibility.Visible;
                }
                else
                {
                    button.Content = "Show Normal Product";
                    titleChange.Text = "OUT OF STOCKS";
                    NormalProduct.Visibility = Visibility.Collapsed;
                    NormalProductControl.Visibility = Visibility.Collapsed;
                    OutOfStock.Visibility = Visibility.Visible;
                    OutOfStockControl.Visibility = Visibility.Visible;

                }
            }
        }
    }
}
