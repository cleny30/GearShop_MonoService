using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

namespace WPFStylingTest
{
    /// <summary>
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        public ObservableCollection<string> SelectedFiles { get; set; }
        private List<string> attributesList = new List<string>();
        private List<string> descriptionsList = new List<string>();

        private String ErrorMessage = "";
        public AddProduct()
        {
            InitializeComponent();
            DataContext = this;
            SelectedFiles = new ObservableCollection<string>();
            ErrorMessage = "";
        }

        private void SelectFiles_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string fileName in openFileDialog.FileNames)
                {
                    SelectedFiles.Add(fileName);
                }
            }
        }


        private void RemoveFile_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string fileName = button.Tag as string;
            if (fileName != null && SelectedFiles.Contains(fileName))
            {
                SelectedFiles.Remove(fileName);
            }
        }


        private void AddDescription_Click(object sender, RoutedEventArgs e)
        {
            // Create a new StackPanel to hold the two TextBoxes
            StackPanel newPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal
            };


            // Create the Attributes TextBox
            TextBox attributesTextBox = new TextBox
            {
                Name = "txtAttribute",
                Width = 173,
                VerticalAlignment = VerticalAlignment.Top,
                Height = 32,
                Margin = new Thickness(5,5,20,5)
            };
            attributesTextBox.SetResourceReference(Control.StyleProperty, "MaterialDesignTextBox");
            attributesTextBox.SetValue(HintAssist.HintProperty, "Attributes");


            // Create the Description TextBox
            TextBox descriptionTextBox = new TextBox
            {
                Name = "txtDescription",
                Width = 350,
                VerticalAlignment = VerticalAlignment.Top,
                Height = 32,
                Margin = new Thickness(5)
            };
            descriptionTextBox.SetResourceReference(Control.StyleProperty, "MaterialDesignTextBox");
            descriptionTextBox.SetValue(HintAssist.HintProperty, "Description");
            // Create the Delete Button
            Button deleteButton = new Button
            {
                Content = "Delete",
                Margin = new Thickness(5, 5, 0, 5),
                VerticalAlignment = VerticalAlignment.Top
            };
            // Attach the event handler to the Delete Button
            deleteButton.Click += (s, args) => DeleteRow(newPanel);

            // Add the TextBoxes and Delete Button to the StackPanel
            newPanel.Children.Add(attributesTextBox);
            newPanel.Children.Add(descriptionTextBox);
            newPanel.Children.Add(deleteButton);

            // Add the StackPanel to the TextBoxContainer StackPanel
            TextBoxContainer.Children.Add(newPanel);
        }

        private void cbBrand_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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

        // Method to delete a row (StackPanel)
        private void DeleteRow(StackPanel panel)
        {
            TextBoxContainer.Children.Remove(panel);
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            foreach(string items in SelectedFiles)
            {

            }
            SaveAttributeAndDescription();

        }


        public void SaveAttributeAndDescription()
        {
            bool hasAttributesTextBox = false;

            // Loop through each child in TextBoxContainer
            foreach (var child in TextBoxContainer.Children)
            {
                // Check if the child is a StackPanel
                if (child is StackPanel stackPanel)
                {
                    // Loop through each element in the StackPanel
                    foreach (var element in stackPanel.Children)
                    {
                        // Check if the element is a TextBox and has the hint "Attributes"
                        if (element is TextBox textBox &&
                            textBox.GetValue(HintAssist.HintProperty) as string == "Attributes")
                        {
                            hasAttributesTextBox = true;
                            break;
                        }
                    }
                }
            }

            // Perform action based on the presence of the "Attributes" TextBox
            if (hasAttributesTextBox)
            {
                attributesList.Clear();
                descriptionsList.Clear();

                // Iterate through each child in TextBoxContainer
                foreach (var child in TextBoxContainer.Children)
                {
                    if (child is StackPanel stackPanel)
                    {
                        // Find the TextBoxes within the StackPanel
                        var attributesTextBox = stackPanel.Children.OfType<TextBox>().FirstOrDefault(tb => tb.Name == "txtAttribute");
                        var descriptionTextBox = stackPanel.Children.OfType<TextBox>().FirstOrDefault(tb => tb.Name == "txtDescription");

                        if (attributesTextBox != null && descriptionTextBox != null)
                        {
                            // Add the text content to the lists
                            attributesList.Add(attributesTextBox.Text);
                            descriptionsList.Add(descriptionTextBox.Text);
                        }
                    }
                }
            }
            else
            {
                ErrorMessage += "Please enter description";
                
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e) => this.Close();
    }
}
