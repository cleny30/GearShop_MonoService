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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFStylingTest
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : UserControl
    {
        public Dashboard()
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
                new Student { ID = 3, Name = "Sam Brown", Description = "A mathematics student." }
            };

            StudentDataGrid.ItemsSource = students;
            StudentDataGrid2.ItemsSource = students;
            StudentDataGrid3.ItemsSource = students;
        }
    }
}
public class Student
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; } // New property
}
