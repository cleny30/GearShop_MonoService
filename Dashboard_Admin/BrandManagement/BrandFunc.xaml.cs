using Microsoft.Win32;
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
using System.Windows.Shapes;

namespace WPFStylingTest.BrandManagement
{
    /// <summary>
    /// Interaction logic for BrandFunc.xaml
    /// </summary>
    public partial class BrandFunc : Window
    {
        public ObservableCollection<string> SelectedBrandLogo { get; set; }
        public BrandFunc()
        {
            InitializeComponent();
            SelectedBrandLogo = new ObservableCollection<string>();
            DataContext = this;
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
        private void SelectFiles_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;  // Ensure only one file can be selected
            if (openFileDialog.ShowDialog() == true)
            {
                SelectedBrandLogo.Clear();  // Optionally clear previous selections
                SelectedBrandLogo.Add(openFileDialog.FileName);  // Add the selected file
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e) => this.Close();
    }
}
