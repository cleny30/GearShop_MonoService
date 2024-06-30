using BusinessObject.Model.Entity;
using Dashboard_Admin;
using DataAccess.Service;
using MaterialDesignColors.Recommended;
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

namespace WPFStylingTest
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : UserControl
    {
        private readonly OrderService orderService;
        private readonly ProductService productService;
        private readonly OrderDetailService orderDetailService;
        public Dashboard()
        {
            orderService = App.GetService<OrderService>();
            productService = App.GetService<ProductService>();
            orderDetailService = App.GetService<OrderDetailService>();
            DataContext = this;
            InitializeComponent();
            GetCompletedOrder();
            GetIncome();
            GetRevenue();
            GetOutOfStock();
            GetTopProduct();
            GetTopCustomer();
        }

        private void GetCompletedOrder()
        {
            txtCompletedOrder.Text = orderService.GetCompletedOrder().ToString();
        }

        private void GetIncome()
        {
            txtIncome.Text = orderService.GetIncome().ToString() + "$";
        }

        private void GetRevenue()
        {

            double Revenue = orderService.GetRevenue();
            if(Revenue > 0)
            {
                txtRevenue.Foreground = Brushes.Green;
            }
            else
            {
                txtRevenue.Foreground = Brushes.Red;
            }
            txtRevenue.Text = Revenue.ToString() + "$";
        }

        private void GetOutOfStock()
        {
            var OutOfStock = productService.GetProductList().Where(p => p.ProQuan <= 1).ToList();

            OutOfStockDataGrid.ItemsSource = OutOfStock;
        }

        private void GetTopProduct()
        {
            var consolidatedProducts = orderDetailService.GetAllOrderDetailList()
                .GroupBy(detail => detail.ProId)
                  .Select(group => new
                  {
                      productID = group.Key,
                      productName = group.First().ProName, // Take the product name from the first item in the group
                      quantity = group.Sum(detail => detail.Quantity)
                  })
               .OrderByDescending(product => product.quantity) // Sort by quantity in descending order
               .Take(10) // Select the top 10 products
                .ToList();

            TopProductDataGrid.ItemsSource = consolidatedProducts;
        }

        private void GetTopCustomer()
        {
            var Top10Customer = orderService.GetTop10Customer();

            TopCustomerDataGrid.ItemsSource = Top10Customer;
        }
    }
}

