using BusinessObject.Model.Page;
using Dashboard_Admin;
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


        private List<string> attributesList = new List<string>();
        private List<string> descriptionsList = new List<string>();
        public ObservableCollection<string> SelectedFiles { get; set; }
        public event EventHandler AddProductWindowClosed;

        //Initialize Service
        private readonly ProductService productService;
        private readonly CategoryService categoryService;
        private readonly BrandService brandService;
        //------------------

        //Used for Updating Product
        public Boolean _IsUpdate { get; set; }
        public ProductModel _Product { get; set; }
        public List<ProductImageModel> _ProductImage { get; set; }
        public List<ProductAttributeModel> _ProductAttribute { get; set; }

        private ObservableCollection<string> SelectedFilesUpdate { get; set; }

        private ObservableCollection<string> SelectedFilesDelete { get; set; }
        //-------------------------



        public AddProduct(Boolean IsUpdate, ProductModel Product, List<ProductImageModel> ProductImage, List<ProductAttributeModel> ProductAttribute)
        {
            productService = App.GetService<ProductService>();
            brandService = App.GetService<BrandService>();
            categoryService = App.GetService<CategoryService>();
            DataContext = this;
            SelectedFiles = new ObservableCollection<string>();
            SelectedFilesUpdate = new ObservableCollection<string>();
            SelectedFilesDelete = new ObservableCollection<string>();

            _IsUpdate = IsUpdate;
            _Product = Product;
            _ProductImage = ProductImage;
            _ProductAttribute = ProductAttribute;

            InitializeComponent();
            InitializeBrand();
            InputDataForUpdating();
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
                    if (_IsUpdate)
                    {
                        SelectedFilesUpdate.Add(fileName);
                    }
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
                if (_IsUpdate)
                {
                    SelectedFilesDelete.Add(fileName);
                }
                SelectedFiles.Remove(fileName);
            }
        }

        //Add Description
        private void AddDescription_Click(object sender, RoutedEventArgs e)
        {
            TextBox attribute = CreateAttribute();
            TextBox des = CreateDescription();

            CreateStackPanel(attribute, des);
        }

        //Create Stack Panel for feature & description
        public void CreateStackPanel(TextBox attribute, TextBox description)
        {
            // Create a new StackPanel to hold the two TextBoxes
            StackPanel newPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal
            };

            // Create the Attributes TextBox
            TextBox attributesTextBox = attribute;

            // Create the Description TextBox
            TextBox descriptionTextBox = description;

            // Create the Delete Button
            Button deleteButton = new Button
            {
                Content = "Delete",
                Margin = new Thickness(5, 5, 0, 5),
                VerticalAlignment = VerticalAlignment.Top,
                Name = "Delete",
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

        //Create Attribute Textbox
        public TextBox CreateAttribute()
        {
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

            return attributesTextBox;
        }

        //Create Description Textbox
        public TextBox CreateDescription()
        {
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
            return descriptionTextBox;
        }

        //Unuse Code
        private void cbBrand_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        //Loaded Category with data
        private void cbCategory_Loaded(object sender, RoutedEventArgs e)
        {
            cbCategory.ItemsSource = categoryService.GetCategoryList().Where(c => c.IsAvailable == true);
            if (_IsUpdate == true)
            {
                cbCategory.SelectedValue = _Product.CateId;
            }

        }

        //Action when change category
        private void cbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_IsUpdate == false)
            {
                int categoryModel = (int)cbCategory.SelectedValue;
                if (categoryModel != null)
                {
                    string ID = productService.GetNewProductID(categoryModel);
                    txtProductID.Text = ID;
                }
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
            CloudinaryManagement cloud = new CloudinaryManagement();

            // Show the overlay
            Overlay.Visibility = Visibility.Visible;

            // Disable the main window
            this.IsEnabled = false;

            try
            {
                SaveAttributeAndDescription();

                if (Validation())
                {
                    if (_IsUpdate == false)
                    {
                        ProductData model = new ProductData
                        {
                            ProId = txtProductID.Text,
                            ProName = txtProductName.Text,
                            ProPrice = double.Parse(txtProductPrice.Text),
                            CateId = (int)cbCategory.SelectedValue,
                            BrandId = (int)cbBrand.SelectedValue,
                            Discount = int.Parse(txtProductDiscount.Text),
                            ProDes = txtProductDescription.Text,
                            IsAvailable = true
                        };


                        List<string> imageLink = new List<string>();
                        foreach (string items in SelectedFiles)
                        {
                            // Assuming Upload is an async method
                            string cloudinaryLink = await cloud.Upload(items, "Products");
                            imageLink.Add(cloudinaryLink);
                        }

                        // Assuming InsertProduct is an asynchronous method
                        productService.InsertProduct(model, imageLink, attributesList, descriptionsList);
                        MessageBox.Show("Insert Successful");
                        this.Close();
                        AddProductWindowClosed?.Invoke(this, EventArgs.Empty);
                    }
                    else
                    {
                        /*
                         * 1. Update Product
                         * 2. Delete Product Image
                         * 3. Add New Product Image
                         * 4. Delete All feature
                         * 5. Add New Feature
                         */

                        ProductData model = new ProductData
                        {
                            ProId = txtProductID.Text,
                            ProName = txtProductName.Text,
                            ProPrice = double.Parse(txtProductPrice.Text),
                            CateId = (int)cbCategory.SelectedValue,
                            BrandId = (int)cbBrand.SelectedValue,
                            Discount = int.Parse(txtProductDiscount.Text),
                            ProDes = txtProductDescription.Text,
                            IsAvailable = true
                        };

                        List<string> deleteList = new List<string>();
                        foreach (string items in SelectedFilesDelete)
                        {
                            bool delete = await cloud.Delete(items);
                            deleteList.Add(items);
                        }

                        List<string> imageLink = new List<string>();
                        foreach (string items in SelectedFilesUpdate)
                        {
                            // Assuming Upload is an async method
                            string cloudinaryLink = await cloud.Upload(items, "Products");
                            imageLink.Add(cloudinaryLink);
                        }

                        productService.UpdateProduct(model, deleteList, imageLink, attributesList, descriptionsList);
                        MessageBox.Show("Update Successful");
                        this.Close();
                        AddProductWindowClosed?.Invoke(this, EventArgs.Empty);


                    }
                }
                else
                {
                    MessageBox.Show("Something went wrong! Check the error for more information");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
            finally
            {
                // Hide the overlay and enable the window again
                Overlay.Visibility = Visibility.Collapsed;
                this.IsEnabled = true;
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
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            //AddProductWindowClosed?.Invoke(this, EventArgs.Empty);
        }

        //Initialize the combobox
        private void InitializeBrand()
        {
            cbBrand.ItemsSource = brandService.GetBrandList().Where(b => b.IsAvailable == true);
            cbBrand.DisplayMemberPath = "BrandName";
            cbBrand.SelectedValuePath = "BrandId";
        }

        //Validation
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
                if (price < 0 || price > 100)
                {
                    errorProDiscount.Text = "Discount can't be under 0 or above 100%";
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
                    if (extension != ".png" && extension != ".jpg" && extension != ".webp")
                    {
                        containsInvalidExtension = true;
                        break;
                    }
                }

                if (containsInvalidExtension)
                {
                    errorProImage.Text = "Please select only .png, .jpg and .webp files";
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
            bool allUnique = attributesList.Count == attributesList.Distinct().Count();
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
            if (!allUnique)
            {
                errorProAttribute.Text = "There are duplicated Attribute!";
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

        //Input Data For Updating
        private void InputDataForUpdating()
        {
            if (_IsUpdate == true)
            {
                Title.Text = "UPDATE " + _Product.ProId;
                //Add Into Product
                txtProductID.Text = _Product.ProId;
                txtProductName.Text = _Product.ProName;
                txtProductPrice.Text = _Product.ProPrice.ToString();
                txtProductDiscount.Text = _Product.Discount.ToString();
                txtProductDescription.Text = _Product.ProDes;
                cbBrand.SelectedValue = _Product.BrandId;
                cbCategory.IsEnabled = false;

                //Disable for readonly
                txtProductID.IsEnabled = false;
                txtProductName.IsEnabled = false;
                txtProductPrice.IsEnabled = false;
                txtProductDiscount.IsEnabled = false;
                txtProductDescription.IsEnabled = false;
                cbBrand.IsEnabled = false;
                SelectedFileButton.IsEnabled = false;
                AddDescriptionButton.IsEnabled = false;
                SubmitButton.IsEnabled = false;
                OverlayUpdate.Visibility = Visibility.Visible;
                OverlayAttribute.Visibility = Visibility.Visible;
                OverlayImage.Visibility = Visibility.Visible;

                if (!_Product.IsAvailable)
                {
                    DisableButton.Content = "Enable";
                }
                DisableButton.Visibility = Visibility.Visible;
                DisableButton.IsEnabled = false;

                //Add Into Product Image
                foreach (ProductImageModel fileName in _ProductImage)
                {
                    SelectedFiles.Add(fileName.ProImg);
                }

                foreach (ProductAttributeModel attribute in _ProductAttribute)
                {
                    //Create attribute textbox
                    TextBox att = CreateAttribute();
                    att.Text = attribute.Feature;

                    //Create description Textbox
                    TextBox des = CreateDescription();
                    des.Text = attribute.Description;

                    //Create Stackpanel
                    CreateStackPanel(att, des);
                }
            }
        }

        private void Enable_Click(object sender, RoutedEventArgs e)
        {
            txtProductID.IsEnabled = true;
            txtProductName.IsEnabled = true;
            txtProductPrice.IsEnabled = true;
            txtProductDiscount.IsEnabled = true;
            txtProductDescription.IsEnabled = true;
            cbBrand.IsEnabled = true;
            SelectedFileButton.IsEnabled = true;
            AddDescriptionButton.IsEnabled = true;
            SubmitButton.IsEnabled = true;
            DisableButton.IsEnabled = true;
            OverlayUpdate.Visibility = Visibility.Collapsed;
            OverlayAttribute.Visibility = Visibility.Collapsed;
            OverlayImage.Visibility = Visibility.Collapsed;
        }

        private async void Disable_Click(object sender, RoutedEventArgs e)
        {
            if (_Product.IsAvailable == true)
            {
                // Show a message box to confirm disabling
                MessageBoxResult result = MessageBox.Show("Are you sure you want to disable this product?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    Overlay.Visibility = Visibility.Collapsed;
                    if(await productService.ChangeProductStatus(_Product, false))
                    {
                        MessageBox.Show("Disable Product Successfully!");
                        this.Close();
                        AddProductWindowClosed?.Invoke(this, EventArgs.Empty);
                    }
                }
               
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to enable this product?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    Overlay.Visibility = Visibility.Collapsed;
                    if (await productService.ChangeProductStatus(_Product, true))
                    {
                        MessageBox.Show("Enable Product Successfully!");
                        this.Close();
                        AddProductWindowClosed?.Invoke(this, EventArgs.Empty);
                    }
                }
                
            }
        }
    }
}
