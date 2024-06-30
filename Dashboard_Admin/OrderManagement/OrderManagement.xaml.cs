
using BusinessObject.Model.Entity;
using BusinessObject.Model.Page;
using DataAccess.Service;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;


namespace Dashboard_Admin.OrderManagement
{
    /// <summary>
    /// Interaction logic for OrderManagement.xaml
    /// </summary>
    public partial class OrderManagement : Page
    {
        //Initialize Service
        private readonly OrderDetailService orderDetailService;
        private readonly OrderService orderService;
        //------------------
        private ObservableCollection<OrderModel> orders;
        private ObservableCollection<OrderModel> filteredOrders;
        private int itemsPerPage = 7;
        private int currentPage = 1;
        private int StatusChange = 1;

        public OrderManagement()
        {
            orderDetailService = App.GetService<OrderDetailService>();
            orderService = App.GetService<OrderService>();
            DataContext = this;
            InitializeComponent();
            LoadOrder();
        }

        private void LoadOrder()
        {
            Title.Header = "PENDING ORDERS";
            ChangeOrderList(1);
            PageCount.Text = currentPage.ToString();
            ConfigDatePicker(false);
        }
        
        private void ConfigDatePicker(bool status)
        {
            datePickerTo.IsEnabled = status;
        }

        private void ChangeOrderList(int Status)
        {
            List<OrderModel> orderList = orderService.GetOrderList();
            orderList = orderList.Where(o => o.Status == Status).ToList();
            orders = new ObservableCollection<OrderModel>(orderList);
            filteredOrders = new ObservableCollection<OrderModel>(orders);
            UpdateDataGrid();
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
            Title.Header = "PENDING ORDERS";
            StatusChange = 1;
            ConfigDatePicker(false);
            ChangeOrderList(1);
        }
        private void AcceptedOrder_Click(object sender, RoutedEventArgs e)
        {
            Title.Header = "ACCEPTED ORDERS";
            StatusChange = 2;
            ConfigDatePicker(false);
            ChangeOrderList(2);
        }
        private void ShippingOrder_Click(object sender, RoutedEventArgs e)
        {
            Title.Header = "SHIPPING ORDERS";
            StatusChange = 3;
            ConfigDatePicker(false);
            ChangeOrderList(3);
        }
        private void CompletedOrder_Click(object sender, RoutedEventArgs e)
        {
            Title.Header = "COMPLETED ORDERS";
            StatusChange = 4;
            ConfigDatePicker(true);
            ChangeOrderList(4);
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            //Start Date
            DateTime? selectedStartDateTime = datePickerFrom.SelectedDate;
            DateOnly? selectedStartDate = selectedStartDateTime.HasValue ? DateOnly.FromDateTime(selectedStartDateTime.Value) : (DateOnly?)null;
            //---------

            //End Date
            DateTime? selectedEndDateTime = datePickerTo.SelectedDate;
            DateOnly? selectedEndDate = selectedEndDateTime.HasValue ? DateOnly.FromDateTime(selectedEndDateTime.Value) : (DateOnly?)null;
            //---------
            if (selectedStartDate.HasValue && selectedEndDate.HasValue)
            {
                filteredOrders = new ObservableCollection<OrderModel>(orders.Where(o => o.StartDate >= selectedStartDate && o.EndDate <= selectedEndDate));
            }
            else if (selectedStartDate.HasValue)
            {
                filteredOrders = new ObservableCollection<OrderModel>(orders.Where(o => o.StartDate >= selectedStartDate));
            }
            else if (selectedEndDate.HasValue)
            {
                filteredOrders = new ObservableCollection<OrderModel>(orders.Where(o => o.EndDate <= selectedEndDate));
            }                
            UpdateDataGrid();
        }
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            datePickerFrom.SelectedDate = null;
            datePickerTo.SelectedDate = null;
            ChangeOrderList(StatusChange);
        }
        private void ViewButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var dataContext = button.DataContext as OrderModel;
            if(dataContext != null)
            {
                var OrderID = dataContext.OrderId;
                OrderModel model = orderService.GetOrderByID(OrderID);
                List<OrderDetailModel> orderDetailModels = orderDetailService.GetOrderDetailList(model);
                OrderInfo info = new OrderInfo(model, orderDetailModels);
                info.OrderInfoClosed += OrderInfoWindow_Closed;
                info.ShowDialog();
                
            }
        }
        private void OrderInfoWindow_Closed(object sender, EventArgs e)
        {
            RoutedEventArgs _e = new RoutedEventArgs();
            PendingOrder_Click( sender, _e);
        }

    }
}
