using System.Windows;
using WPFStylingTest.CategoryManagement;

namespace WPFStylingTest
{
    /// <summary>
    /// Interaction logic for CategoryWindow.xaml
    /// </summary>
    public partial class CategoryWindow : Window
    {
        public CategoryWindow()
        {
            InitializeComponent();
            LoadStudents();
        }
        private void LoadStudents()
        {
            var students = new List<Student>
            {
                new Student { ID = 1, Name = "John Doe", Description = "A computer science major." },
                new Student { ID = 2, Name = "Jane Smith", Description = "An art student." },
                new Student { ID = 3, Name = "Sam Brown", Description = "A mathematics student." },
                 new Student { ID = 3, Name = "Sam Brown", Description = "A mathematics student." },
                  new Student { ID = 3, Name = "Sam Brown", Description = "A mathematics student." },
                   new Student { ID = 3, Name = "Sam Brown", Description = "A mathematics student." },
                    new Student { ID = 3, Name = "Sam Brown", Description = "A mathematics student." },
                     new Student { ID = 3, Name = "Sam Brown", Description = "A mathematics student." },
                     new Student { ID = 3, Name = "Sam Brown", Description = "A mathematics student." },
                     new Student { ID = 3, Name = "Sam Brown", Description = "A mathematics student." },
                     new Student { ID = 3, Name = "Sam Brown", Description = "A mathematics student." },
                     new Student { ID = 3, Name = "Sam Brown", Description = "A mathematics student." },
                     new Student { ID = 3, Name = "Sam Brown", Description = "A mathematics student." },
                     new Student { ID = 3, Name = "Sam Brown", Description = "A mathematics student." },
                     new Student { ID = 3, Name = "Sam Brown", Description = "A mathematics student." }

            };

            CategoryDataGrid.ItemsSource = students;

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
