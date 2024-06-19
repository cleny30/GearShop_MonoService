using BusinessObject.Model.Entity;
using BusinessObject.Model.Page;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.DirectoryServices.ActiveDirectory;
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
using WPFStylingTest;

namespace Dashboard_Admin.OrderManagement
{
    /// <summary>
    /// Interaction logic for OrderManagement.xaml
    /// </summary>
    public partial class OrderManagement : Page
    {
        private ObservableCollection<OrderModel> orders;
        private ObservableCollection<OrderModel> filteredOrders;
        private int itemsPerPage = 7;
        private int currentPage = 1;
        public OrderManagement()
        {
            InitializeComponent();
        }

        private void LoadOrder()
        {
            List<OrderModel> orderList = new List<OrderModel>();
            orders = new ObservableCollection<OrderModel>(orderList);
            filteredOrders = new ObservableCollection<OrderModel>(orders);
            UpdateDataGrid();
            PageCount.Text = currentPage.ToString();
        }
        private void UpdateDataGrid()
        {
            // Calculate the starting index and number of items for the current page
            int startIndex = (currentPage - 1) * itemsPerPage;
            int count = Math.Min(itemsPerPage, filteredOrders.Count - startIndex);

            // Update the data grid with the items for the current page
            OrderDataGrid.ItemsSource = filteredOrders.Skip(startIndex).Take(count);
        }

        private void PreviousPageButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if there are previous pages
            if (currentPage > 1)
            {
                currentPage--;
                PageCount.Text = currentPage.ToString();
                UpdateDataGrid();
            }
        }
        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if there are more pages
            if ((currentPage * itemsPerPage) < filteredOrders.Count)
            {
                currentPage++;
                PageCount.Text = currentPage.ToString();
                UpdateDataGrid();
            }
        }
        private void PendingOrder_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void AcceptedOrder_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void ShippingOrder_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void CompletedOrder_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void ViewButton_Click(object sender, RoutedEventArgs e)
        {

        }
       
    }
}
