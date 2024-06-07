using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using WPFStylingTest.CategoryManagement;

namespace WPFStylingTest.BrandManagement
{
    /// <summary>
    /// Interaction logic for BrandWindow.xaml
    /// </summary>
    public partial class BrandWindow : Window
    {
        public BrandWindow()
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

            BrandDataGrid.ItemsSource = students;

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
            
        }

        private void DisableButton_Click(object sender, RoutedEventArgs e)
        {
            // Handle trash can icon click event here
            MessageBox.Show("Trash can icon clicked");
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            BrandFunc func = new BrandFunc();
            func.Show();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e) => this.Close();
    }
}
