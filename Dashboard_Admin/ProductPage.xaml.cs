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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WPFStylingTest
{
    /// <summary>
    /// Interaction logic for Product.xaml
    /// </summary>
    public partial class ProductPage : UserControl
    {
        private ObservableCollection<ProductModel> products; 
        private ObservableCollection<ProductModel> filteredProducts;
        private int itemsPerPage = 7; 
        private int currentPage = 1;
        
        private AddProduct addProductWindow;
        //Service Initialization
        private readonly ProductService productService;
        private readonly CategoryService categoryService;
        private readonly BrandService brandService;
        private readonly ProductAttributeService productAttributeService;
        private readonly ProductImageService productImageService;
        //----------------------

        public ProductPage()
        {
            productService = new ProductService();
            brandService = new BrandService();
            categoryService = new CategoryService();
            productImageService = new ProductImageService();
            productAttributeService = new ProductAttributeService();
            DataContext = this;
            InitializeComponent();
            LoadStudents();
            InitialSearch();
        }

        //Load The List
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

        //Update datagrid when using search
        private void UpdateDataGrid()
        {
            // Calculate the starting index and number of items for the current page
            int startIndex = (currentPage - 1) * itemsPerPage;
            int count = Math.Min(itemsPerPage, filteredProducts.Count - startIndex);

            // Update the data grid with the items for the current page
            ProductDataGrid.ItemsSource = filteredProducts.Skip(startIndex).Take(count);


        }

        //Went to Next Page
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

        //Go back to the previous page
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

        //Open the addProduct window
        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            Boolean IsUpdate;
            ProductModel product = null;
            List<ProductImageModel> productImages = null;
            List<ProductAttributeModel> productAttributes = null;
            AddProduct addProductWindow = new AddProduct(IsUpdate = false, product, productImages, productAttributes) ;
            addProductWindow.AddProductWindowClosed += AddProductWindow_AddProductWindowClosed;
            addProductWindow.Show();
        }

        //Check AddProduct page closed
        private void AddProductWindow_AddProductWindowClosed(object sender, EventArgs e)
        {
            Reset();
            LoadStudents();
        }

        //Open The Category Page
        private void CategoryButton_Click(object sender, RoutedEventArgs e)
        {
            CategoryWindow cate = new CategoryWindow();
            cate.Show();
        }

        //Open the Brand Page
        private void BrandButton_Click(object sender, RoutedEventArgs e)
        {
            BrandWindow brand = new BrandWindow();
            brand.Show();
        }

        //Unused
        private void StudentDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        //Search The grid
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            // Filter the students based on the search text
            string searchText = SearchTextBox.Text.ToLower();
            int? BrandID = cbBrand.SelectedValue as int?;
            int? CateID = cbCategory.SelectedValue as int?;
            filteredProducts = new ObservableCollection<ProductModel>(products.Where(s => s.ProName.ToLower().Contains(searchText) && (s.BrandId == BrandID || !BrandID.HasValue || BrandID == -1)
                                                                      && (s.CateId == CateID || !CateID.HasValue || CateID == -1)));

            // Reset to the first page after a search
            currentPage = 1;
            PageCount.Text = currentPage.ToString();
            UpdateDataGrid();
        }

        //Reset the search and grid
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        //Reset
        public void Reset()
        {
            filteredProducts = new ObservableCollection<ProductModel>(products);
            cbBrand.SelectedValue = -1;
            cbCategory.SelectedValue = -1;
            SearchTextBox.Clear();
            currentPage = 1;
            UpdateDataGrid();
        }

        //Initialize the search function
        private void InitialSearch()
        {
            List<BrandModel> brands = brandService.GetBrandList();
            brands.Insert(0, new BrandModel { BrandId = -1, BrandName = "All" });
            cbBrand.ItemsSource = brands;
            cbBrand.DisplayMemberPath = "BrandName";
            cbBrand.SelectedValuePath = "BrandId";
            cbBrand.SelectedValue = -1;

            List<CategoryModel> categorys = categoryService.GetCategoryList();
            categorys.Insert(0, new CategoryModel { CateId = -1, CateName = "All" });
            cbCategory.ItemsSource = categorys;
            cbCategory.DisplayMemberPath = "CateName";
            cbCategory.SelectedValuePath = "CateId";
            cbCategory.SelectedValue = -1;
        }

        private void ViewButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the current row's data context (which is the bound data item)
            var button = sender as Button;
            var dataContext = button.DataContext as ProductModel; // Assuming your data item class is named Product

            if (dataContext != null)
            {
                // Retrieve the ID of the product
                var productId = dataContext.ProId;
                Boolean IsUpdate;
                var product = productService.GetProduct(productId);
                var productImages = productImageService.GetProductImageList()
                                                       .Where(p => p.ProId.Equals(productId))
                                                       .ToList();
                var productAttributes = productAttributeService.GetProductAttributeList()
                                                               .Where(p => p.ProId.Equals(productId))
                                                               .ToList();
                // Do something with the product ID, for example, display it in a message box
                AddProduct addProductWindowUpdate = new AddProduct(IsUpdate = true, product, productImages, productAttributes);

                addProductWindowUpdate.AddProductWindowClosed += AddProductWindow_AddProductWindowClosed;
                addProductWindowUpdate.Show();

            }
        }
    }
}
