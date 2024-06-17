using BusinessObject.Model.Page;
using DataAccess.Service;
using System.Collections.ObjectModel;
using System.Windows;
using Dashboard_Admin;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using System.Windows.Media;
namespace WPFStylingTest.BrandManagement
{
    /// <summary>
    /// Interaction logic for BrandWindow.xaml
    /// </summary>
    public partial class BrandWindow : Window
    {
        private readonly BrandService brandService;
        public ObservableCollection<BrandModel> MyItems { get; set; 
        public BrandWindow()
        {
            brandService = App.GetService<BrandService>();
            this.DataContext = this;
            InitializeComponent();
            LoadBrands();
        }
        private void LoadBrands()
        {
            var brands = brandService.GetBrandList();

            foreach(var brand in brands)
            {
                var stackPanel = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(10, 0, 0, 0) };
                var editButton = new System.Windows.Controls.Button
                {
                    Margin = new Thickness(0, 0, 5, 0),
                    Background = System.Windows.Media.Brushes.Transparent,
                    BorderBrush = System.Windows.Media.Brushes.Transparent,
                    Padding = new Thickness(0)
                };
                var editIcon = new PackIcon
                {
                    Kind = PackIconKind.Pencil,
                    Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 254, 32))
                };
                editButton.Content = editIcon;
                editButton.Click += EditButton_Click;
                stackPanel.Children.Add(editButton);
                if (brand.IsAvailable)
                {
                    var disableButton = new System.Windows.Controls.Button
                    {
                        Background = System.Windows.Media.Brushes.Transparent,
                        BorderBrush = System.Windows.Media.Brushes.Transparent,
                        Padding = new Thickness(0)
                    };
                    var disableIcon = new PackIcon
                    {
                        Kind = PackIconKind.TrashCan,
                        Foreground = System.Windows.Media.Brushes.Red
                    };
                    disableButton.Content = disableIcon;
                    disableButton.Click += DisableButton_Click;

                    stackPanel.Children.Add(disableButton);
                } else
                {
                    var disableButton = new System.Windows.Controls.Button
                    {
                        Background = System.Windows.Media.Brushes.Transparent,
                        BorderBrush = System.Windows.Media.Brushes.Transparent,
                        Padding = new Thickness(0)
                    };
                    var disableIcon = new PackIcon
                    {
                        Kind = PackIconKind.LockOpen,
                        Foreground = System.Windows.Media.Brushes.Green
                    };
                    disableButton.Content = disableIcon;
                    disableButton.Click += EnableButton_Click;

                    stackPanel.Children.Add(disableButton);
                }
                brand.FunctionContent = stackPanel;
            }
            BrandDataGrid.ItemsSource = brands;
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
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            var dataContext = button.DataContext as BrandModel;


            if (dataContext != null)
            {
                var BrandID = dataContext.BrandId;
                Boolean IsUpdate = true;
                var brand = brandService.GetBrandList().SingleOrDefault(b => b.BrandId.Equals(BrandID));

                BrandFunc func = new BrandFunc(IsUpdate, brand);
                func.BrandFuncClosed += AddBrandWindow_Closed;
                func.ShowDialog();
            }
        }
        private void DisableButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            var dataContext = button.DataContext as BrandModel;

            if (dataContext != null)
            {
                var BrandID = dataContext.BrandId;
                MessageBoxResult result = MessageBox.Show(
        "Do you want to disable the product?",
        "Confirm Disable",
        MessageBoxButton.YesNo,
        MessageBoxImage.Warning);

                // Check the result and act accordingly
                if (result == MessageBoxResult.Yes)
                {
                    if (brandService.ChangeBrandStatus(BrandID, false))
                    {
                        // Code to delete the product goes here
                        MessageBox.Show("Brand Disabled.");
                        LoadBrands();
                    }
                }
            }
        }
        private void EnableButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            var dataContext = button.DataContext as BrandModel;

            if (dataContext != null)
            {
                var BrandID = dataContext.BrandId;
                MessageBoxResult result = MessageBox.Show(
        "Do you want to enable the product?",
        "Confirm Disable",
        MessageBoxButton.YesNo,
        MessageBoxImage.Warning);

                // Check the result and act accordingly
                if (result == MessageBoxResult.Yes)
                {
                    if (brandService.ChangeBrandStatus(BrandID, true))
                    {
                        // Code to delete the product goes here
                        MessageBox.Show("Brand Enable.");
                        LoadBrands();
                    }
                }
            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            BrandFunc func = new BrandFunc(false, null);
            func.BrandFuncClosed += AddBrandWindow_Closed;
            func.ShowDialog();
        }
        private void AddBrandWindow_Closed(object sender, EventArgs e)
        {
            LoadBrands();
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e) => this.Close();
    }
}
