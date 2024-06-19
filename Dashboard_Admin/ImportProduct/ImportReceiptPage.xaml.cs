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

namespace Dashboard_Admin.ImportProduct
{
    /// <summary>
    /// Interaction logic for ImportReceiptPage.xaml
    /// </summary>
    public partial class ImportReceiptPage : Page
    {
        //Initialize Service
        private readonly ImportReceiptService _importReceiptService;
        private readonly ReceiptProductService _receiptProductService;
        //------------------

        //Load Receipt
        private ObservableCollection<ImportProductModel> receipts { get; set; }
        //------------

        //Paging
        private int itemsPerPage = 9;
        private int currentPage = 1;
        //------
        public ImportReceiptPage()
        {
            _importReceiptService = App.GetService<ImportReceiptService>();
            _receiptProductService = App.GetService<ReceiptProductService>();
            DataContext = this;
            InitializeComponent();
            GetDataGrid();
        }

        private void GetDataGrid()
        {
            List<ImportProductModel> list = _importReceiptService.GetImportProductsList();
            receipts = new ObservableCollection<ImportProductModel>(list);
            // Calculate the starting index and number of items for the current page
            int startIndex = (currentPage - 1) * itemsPerPage;
            int count = Math.Min(itemsPerPage, receipts.Count - startIndex);
            ImportReceiptDataGrid.ItemsSource = receipts.Skip(startIndex).Take(count);
            PageCount.Text = currentPage.ToString();
        }

        //Add Button
        private void ViewButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            var dataContext = button.DataContext as ImportProductModel;
            if (dataContext != null)
            {
                var receiptID = dataContext.ReceiptId;
                ImportProductModel model = _importReceiptService.GetImportProduct(receiptID);    
                ImportProductID.Text = receiptID.ToString();
                ImportProductName.Text = model.PersonChange;
                ImportProductDate.Text = model.DateImport.ToString();
                TotalMoney.Text = model.Payment.ToString() + "$";
                List<ReceiptProductModel> receiptProductModels = _receiptProductService.GetReceiptProducts(receiptID);
                ReceiptDataGrid.ItemsSource = receiptProductModels;
            }
        }

        //Went to Next Page
        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if there are more pages
            if ((currentPage * itemsPerPage) < receipts.Count)
            {
                currentPage++;
                PageCount.Text = currentPage.ToString();
                GetDataGrid();
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
                GetDataGrid();
            }
        }
    }
}
