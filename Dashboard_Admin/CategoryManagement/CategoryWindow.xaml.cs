using DataAccess.Service;
using System.Drawing;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using WPFStylingTest.CategoryManagement;
using Dashboard_Admin;
using System;
using BusinessObject.Model.Page;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using static MaterialDesignThemes.Wpf.Theme;
using MaterialDesignThemes.Wpf;
using System.Windows.Media;
namespace WPFStylingTest
{
    /// <summary>
    /// Interaction logic for CategoryWindow.xaml
    /// </summary>
    public partial class CategoryWindow : Window
    {
        private readonly CategoryService categoryService;
        public ObservableCollection<CategoryModel> MyItems { get; set; }

        public CategoryWindow()
        {
            categoryService = App.GetService<CategoryService>();
            this.DataContext = this;
            InitializeComponent();
            LoadCategory();
          

        }
        private void LoadCategory()
        {
            var categories = categoryService.GetCategoryList();

            foreach (var category in categories)
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

                if (category.IsAvailable)
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


                category.FunctionContent = stackPanel;
            }

            CategoryDataGrid.ItemsSource = categories;

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
            var dataContext = button.DataContext as CategoryModel;


            if (dataContext != null)
            { 
                var CateID = dataContext.CateId;
                Boolean IsUpdate = true;
                var category = categoryService.GetCategoryList().SingleOrDefault(c => c.CateId.Equals(CateID));

                CategoryFunc func = new CategoryFunc(IsUpdate, category);
                func.CategoryFuncClosed += AddCategoryWindow_Closed;
                func.ShowDialog();
            }
         }

        private void DisableButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            var dataContext = button.DataContext as CategoryModel;

            if (dataContext != null)
            {
                var CateID = dataContext.CateId;
                MessageBoxResult result = MessageBox.Show(
        "Do you want to disable the product?",
        "Confirm Disable",
        MessageBoxButton.YesNo,
        MessageBoxImage.Warning);

                // Check the result and act accordingly
                if (result == MessageBoxResult.Yes)
                {
                    if (categoryService.ChangeCategoryStatus(CateID, false))
                    {
                        // Code to delete the product goes here
                        MessageBox.Show("Category Disabled.");
                        LoadCategory();
                    }
                }               
            }
        }


        private void EnableButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            var dataContext = button.DataContext as CategoryModel;

            if (dataContext != null)
            {
                var CateID = dataContext.CateId;
                MessageBoxResult result = MessageBox.Show(
        "Do you want to enable the product?",
        "Confirm Disable",
        MessageBoxButton.YesNo,
        MessageBoxImage.Warning);

                // Check the result and act accordingly
                if (result == MessageBoxResult.Yes)
                {
                    if (categoryService.ChangeCategoryStatus(CateID, true))
                    {
                        // Code to delete the product goes here
                        MessageBox.Show("Category Enable.");
                        LoadCategory();
                    }
                }
            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            CategoryFunc func = new CategoryFunc(false, null);
            func.CategoryFuncClosed += AddCategoryWindow_Closed;
            func.ShowDialog();
        }

        private void AddCategoryWindow_Closed(object sender, EventArgs e)
        {
            LoadCategory();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e) => this.Close();
    } 
}
