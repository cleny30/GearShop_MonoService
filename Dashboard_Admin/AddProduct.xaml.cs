using BusinessObject.Model.Page;
using DataAccess.Core.Cloudiary;
using DataAccess.Service;
using MaterialDesignThemes.Wpf;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;


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

        private readonly ProductService productService;
        private readonly CategoryService categoryService;
        private readonly BrandService brandService;

        public AddProduct()
        {
            productService = new ProductService();
            brandService = new BrandService();
            categoryService = new CategoryService();

            InitializeComponent();
            InitialCateAndBrand();
            DataContext = this;
            SelectedFiles = new ObservableCollection<string>();

        }

        //Select Files for image
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

        //Remove Files for image
        private void RemoveFile_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string fileName = button.Tag as string;
            if (fileName != null && SelectedFiles.Contains(fileName))
            {
                SelectedFiles.Remove(fileName);
            }
        }

        //Add Description
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
                Margin = new Thickness(5, 5, 20, 5)
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

        //Unuse Code
        private void cbBrand_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void cbCategory_Loaded(object sender, RoutedEventArgs e)
        {
            cbCategory.ItemsSource = categoryService.GetCategoryList();

        }
        private void cbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int categoryModel = (int)cbCategory.SelectedValue;
            if (categoryModel != null)
            {
                string ID = productService.GetNewProductID(categoryModel);
                txtProductID.Text = ID;
            }

        }

        //Window dragging
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
            // Find the index of the panel being deleted
            int index = TextBoxContainer.Children.IndexOf(panel);

            // Remove the panel from the TextBoxContainer
            TextBoxContainer.Children.Remove(panel);

            // Remove corresponding entries from attributesList and descriptionsList
            if (index >= 0 && index < attributesList.Count)
            {
                attributesList.RemoveAt(index);
                descriptionsList.RemoveAt(index);
            }
        }

        //Submit the form
        private async void Submit_Click(object sender, RoutedEventArgs e)
        {
            
            SaveAttributeAndDescription();
            if (Validation())
            {
                ProductData model = new ProductData
                {
                    ProId = txtProductID.Text,
                    ProName = txtProductName.Text,
                    ProPrice = double.Parse(txtProductPrice.Text),
                    CateId = (int)cbCategory.SelectedValue,
                    BrandId = (int)cbBrand.SelectedValue,
                    Discount = int.Parse(txtProductDiscount.Text),
                    ProDes = txtProductDiscount.Text,
                    IsAvailable = true
                };

                CloudinaryManagement cloud = new CloudinaryManagement();
                List<string> imageLink = new List<string>();
                foreach (string items in SelectedFiles)
                {
                    // Assuming Upload is an async method
                    string cloudinaryLink = await cloud.Upload(items, "Test");
                    imageLink.Add(cloudinaryLink);
                }

                productService.InsertProduct(model, imageLink, attributesList, descriptionsList);
                MessageBox.Show("Insert Successful");
            }
            else
            {
                MessageBox.Show("Something went wrong! Check the error for more information");
            }

        }

        private async Task UploadFilesAsync(ObservableCollection<string> selectedFiles, IProgress<int> progress)
        {
            CloudinaryManagement cloud = new CloudinaryManagement();
            int totalFiles = selectedFiles.Count;

            for (int i = 0; i < totalFiles; i++)
            {
                string item = selectedFiles[i];
                await Task.Run(() => cloud.Upload(item, "Test").ToString());
                int percentComplete = (i + 1) * 100 / totalFiles;
                progress.Report(percentComplete);
            }
        }

        //Save attribute and description
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

        }

        //Close the Window
        private void CloseButton_Click(object sender, RoutedEventArgs e) => this.Close();

        //Initialize the combobox
        private void InitialCateAndBrand()
        {
            cbBrand.ItemsSource = brandService.GetBrandList();
            cbBrand.DisplayMemberPath = "BrandName";
            cbBrand.SelectedValuePath = "BrandId";
        }

        private bool Validation()
        {
            bool allCheck = true;
            errorProAttribute.Text = "";

            //Product Name
            string Name = txtProductName.Text.Trim();
            if (Name == "")
            {
                errorProName.Text = "Name can't be empty";
                allCheck = false;
            }
            else
            {
                errorProName.Text = "";
            }

            //Product Price
            string Price = txtProductPrice.Text.Trim();
            string patternPrice = @"^\d+(\.\d+)?$";
            if (Regex.IsMatch(Price, patternPrice))
            {
                // Parse the input to a decimal
                decimal price = decimal.Parse(Price);

                // Check if the price is greater than zero
                if (price <= 0)
                {
                    errorProPrice.Text = "Price can't be 0 or under 0!";
                    allCheck = false;
                }
                else
                {
                    errorProPrice.Text = "";
                }

            }
            else
            {
                errorProPrice.Text = "Invalid price format. Please enter a valid number.";
                allCheck = false;
            }


            //Product Discount
            string Discount = txtProductDiscount.Text.Trim();
            if (Regex.IsMatch(Discount, patternPrice))
            {
                // Parse the input to a decimal
                decimal price = decimal.Parse(Discount);

                // Check if the price is greater than zero
                if (price <= 0 || price > 100)
                {
                    errorProDiscount.Text = "Discount can't be 0 or above 100%";
                    allCheck = false;
                }
                else
                {
                    errorProDiscount.Text = "";
                }

            }
            else
            {
                errorProDiscount.Text = "Invalid discount format. Please enter a valid number.";
                allCheck = false;
            }


            //Product Brand
            if (cbBrand.SelectedValue == null)
            {
                errorProBrand.Text = "Please choose a brand";
                allCheck = false;
            }
            else
            {
                errorProBrand.Text = "";
            }

            //Product Category
            if (cbCategory.SelectedValue == null)
            {
                errorProCategory.Text = "Please choose a category";
                allCheck = false;
            }
            else
            {
                errorProCategory.Text = "";
            }

            //Product Image
            bool containsInvalidExtension = false;

            if (SelectedFiles == null || SelectedFiles.Count == 0)
            {
                errorProImage.Text = "Please select images";
                allCheck = false;
            }
            else
            {
                foreach (string file in SelectedFiles)
                {
                    string extension = System.IO.Path.GetExtension(file);
                    if (extension != ".png" && extension != ".jpg")
                    {
                        containsInvalidExtension = true;
                        break;
                    }
                }

                if (containsInvalidExtension)
                {
                    errorProImage.Text = "Please select only .png or .jpg files";
                    allCheck = false;
                }
                else
                {
                    errorProImage.Text = "";
                }
            }


            //Product Attribute
            bool hasEmptyAttribute = attributesList.Any(string.IsNullOrEmpty);
            bool hasEmptyDescription = descriptionsList.Any(string.IsNullOrEmpty);
            if (attributesList.IsNullOrEmpty())
            {
                errorProAttribute.Text = "Please enter attribute";
                allCheck = false;
            }
            if (hasEmptyAttribute || hasEmptyDescription)
            {
                errorProAttribute.Text = "Some field are unfielded";
                allCheck = false;
            }

            //Product Description
            if (txtProductDescription.Text == "")
            {
                errorProDescription.Text = "Please enter Description";
                allCheck = false;
            }
            else
            {
                errorProDescription.Text = "";
            }
            return allCheck;
        }
    }
}
