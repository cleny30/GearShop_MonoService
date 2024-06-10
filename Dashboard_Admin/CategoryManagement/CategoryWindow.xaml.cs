using DataAccess.Service;
using System.Drawing;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using WPFStylingTest.CategoryManagement;

namespace WPFStylingTest
{
    /// <summary>
    /// Interaction logic for CategoryWindow.xaml
    /// </summary>
    public partial class CategoryWindow : Window
    {
        private readonly CategoryService categoryService;
        public CategoryWindow()
        {
            categoryService = new CategoryService();
            InitializeComponent();
            LoadCategory();
        }
        private void LoadCategory()
        {
            CategoryDataGrid.ItemsSource = categoryService.GetCategoryList();
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
            CategoryFunc func = new CategoryFunc();
            func.Show();
        }

        private void DisableButton_Click(object sender, RoutedEventArgs e)
        {
            // Handle trash can icon click event here
            MessageBox.Show("Trash can icon clicked");
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            CategoryFunc func = new CategoryFunc();
            func.Show();     
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e) => this.Close();
     
    } 
}
