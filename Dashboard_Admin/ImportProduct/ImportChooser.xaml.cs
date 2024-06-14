using BusinessObject.Model.Entity;
using BusinessObject.Model.Page;
using DataAccess.Service;
using System.Collections.ObjectModel;
using System.Windows;
using WPFStylingTest;


namespace Dashboard_Admin.ImportProduct
{
    /// <summary>
    /// Interaction logic for ImportChooser.xaml
    /// </summary>
    public partial class ImportChooser : Window
    {
        //Add To Cart
        public ProductModel _product { get; set; }
        public ObservableCollection<ProductModel>? _CartProducts { get; private set; }

        public bool _IsUpdate { get; set; }
        //-----------

        public ImportChooser(ProductModel product, ObservableCollection<ProductModel>? CartProducts, bool isUpdate)
        {
            _product = product;
            _CartProducts = CartProducts;
            _IsUpdate = isUpdate;

            InitializeComponent();
            InsertData();
            _IsUpdate = isUpdate;
        }


        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            // Check if the product should be updated
            if (_IsUpdate)
            {
                // Find the product in the cart by its ProId
                var existingProduct = _CartProducts.FirstOrDefault(p => p.ProId == _product.ProId);

                if (existingProduct != null)
                {
                    // Parse the price and quantity from the text inputs with error handling
                    double price;
                    int quantity;
                    bool isPriceValid = double.TryParse(txtProductPrice.Text, out price);
                    bool isQuantityValid = int.TryParse(txtProductQuantity.Text, out quantity);

                    if (isPriceValid && isQuantityValid)
                    {
                        // Update the existing product's properties
                        existingProduct.ProName = _product.ProName; // Assuming the name might also change
                        existingProduct.ProPrice = price;
                        existingProduct.ProQuan = quantity;
                    }
                    else
                    {
                        // Handle the error for invalid inputs
                        MessageBox.Show("Please enter valid price and quantity.");
                    }
                }
                else
                {
                    // Handle the error if the product is not found in the cart
                    MessageBox.Show("Product not found in the cart.");
                }
            }
            else
            {
                // Parse the price and quantity from the text inputs with error handling
                double price;
                int quantity;
                bool isPriceValid = double.TryParse(txtProductPrice.Text, out price);
                bool isQuantityValid = int.TryParse(txtProductQuantity.Text, out quantity);

                if (isPriceValid && isQuantityValid)
                {
                    // Add the new product to the cart
                    _CartProducts.Add(new ProductModel
                    {
                        ProId = _product.ProId,
                        ProName = _product.ProName,
                        ProPrice = price,
                        ProQuan = quantity
                    });
                }
                else
                {
                    // Handle the error for invalid inputs
                    MessageBox.Show("Please enter valid price and quantity.");
                }
            }

            this.Close();

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

        private void CloseButton_Click(object sender, RoutedEventArgs e) => this.Close();

        private void InsertData()
        {
            txtProductName.Text = _product.ProName;
            if (_IsUpdate)
            {
                txtProductPrice.Text = _product.ProPrice.ToString();
                txtProductQuantity.Text = _product.ProQuan.ToString();
            }

        }
    }
}
