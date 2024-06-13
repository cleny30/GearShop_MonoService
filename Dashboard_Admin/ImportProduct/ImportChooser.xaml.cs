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
        //-----------

        public ImportChooser(ProductModel product, ObservableCollection<ProductModel>? CartProducts)
        {
            _product = product;
            _CartProducts = CartProducts;
            InitializeComponent();
            InsertData();
        }


        private void Submit_Click(object sender, RoutedEventArgs e)
        {
           _CartProducts.Add(new ProductModel
           {
               ProId = _product.ProId,
               ProName = _product.ProName,
               ProPrice = double.Parse(txtProductPrice.Text),
               ProQuan = int.Parse(txtProductQuantity.Text)
              
           });
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

        }
    }
}
