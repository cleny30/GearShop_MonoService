using BusinessObject.Model.Entity;
using BusinessObject.Model.Page;
using DataAccess.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WPFStylingTest.BrandManagement;

namespace WPFStylingTest
{
    /// <summary>
    /// Interaction logic for Product.xaml
    /// </summary>
    public partial class ProductPage : UserControl
    {
        private ObservableCollection<ProductModel> products; // Your data source
        private ObservableCollection<ProductModel> filteredProducts;
        private int itemsPerPage = 7; // Number of items per page
        private int currentPage = 1; // Current page number
        private readonly ProductService productService;
        public ProductPage()
        {
            productService = new ProductService();
            InitializeComponent();
            LoadStudents();
         
        }

        private void LoadStudents()
        {
            //ProductService productService = new ProductService();
            List<ProductModel> productList = productService.GetProductList();

            // Convert the List to an ObservableCollection
            products = new ObservableCollection<ProductModel>(productList);
            // Initially, filteredStudents is the same as students
            filteredProducts = new ObservableCollection<ProductModel>(products);
            // Display data for the current page
            UpdateDataGrid();
            PageCount.Text = currentPage.ToString();
            
        }

        private void UpdateDataGrid()
        {
            // Calculate the starting index and number of items for the current page
            int startIndex = (currentPage - 1) * itemsPerPage;
            int count = Math.Min(itemsPerPage, filteredProducts.Count - startIndex);

            // Update the data grid with the items for the current page
            ProductDataGrid.ItemsSource = filteredProducts.Skip(startIndex).Take(count);


        }

        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if there are more pages
            if ((currentPage * itemsPerPage) < products.Count)
            {
                currentPage++;
                PageCount.Text = currentPage.ToString();
                UpdateDataGrid();
            }
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

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            AddProduct addProduct = new AddProduct();
            addProduct.Show();
        }

        private void CategoryButton_Click(object sender, RoutedEventArgs e)
        {
            CategoryWindow cate = new CategoryWindow();
            cate.Show();
        }

        private void BrandButton_Click(object sender, RoutedEventArgs e)
        {
            BrandWindow brand = new BrandWindow();
            brand.Show();
        }

        private void StudentDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            // Filter the students based on the search text
            string searchText = SearchTextBox.Text.ToLower();
            filteredProducts = new ObservableCollection<ProductModel>(products.Where(s => s.ProName.ToLower().Contains(searchText)));

            // Reset to the first page after a search
            currentPage = 1;
            PageCount.Text = currentPage.ToString();
            UpdateDataGrid();
        }
    }
}
