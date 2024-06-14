using BusinessObject.Model.Page;
using Dashboard_Admin;
using DataAccess.Service;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DataAccess.Core.Cloudiary;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPFStylingTest.ViewModel;

namespace WPFStylingTest.BrandManagement
{
    /// <summary>
    /// Interaction logic for BrandFunc.xaml
    /// </summary>
    public partial class BrandFunc : Window
    {
        public ObservableCollection<string> SelectedBrandLogo { get; set; }

        //Initialize Service
        private readonly BrandService brandService;
        //------------------

        //Used for updating
        public Boolean _IsUpdate { get; set; }
        public BrandModel _BrandModel { get; set; }

        public string changedBrandLogo {  get; set; }
        //-----------------

        public event EventHandler BrandFuncClosed;
        public BrandFunc(bool IsUpdate, BrandModel brand)
        {
            brandService = App.GetService<BrandService>();  

            _IsUpdate = IsUpdate;
            _BrandModel = brand;

            SelectedBrandLogo = new ObservableCollection<string>();

            DataContext = this;

            InitializeComponent();
            if (IsUpdate)
            {
                InputDataForUpdating();
            }
            
            
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
        private async void Submit_Click(object sender, RoutedEventArgs e)
        {
            CloudinaryManagement cloud = new CloudinaryManagement();

            // Show the overlay
            Overlay.Visibility = Visibility.Visible;

            // Disable the main window
            this.IsEnabled = false;

            try
            {
                if (Validation())
                {
                    

                    if (!_IsUpdate)
                    {
                        string cloudinaryLink = await cloud.Upload(SelectedBrandLogo[0], "Brands");
                        string BrandImage = cloudinaryLink;
                        BrandModel _brand = new BrandModel
                        {
                            BrandName = txtBrandName.Text,
                            BrandLogo = BrandImage
                        };
                        if (brandService.AddBrand(_brand))
                        {
                            MessageBox.Show("Insert successful");
                            this.Close();
                            BrandFuncClosed?.Invoke(this, EventArgs.Empty);
                        } else
                        {
                            MessageBox.Show("Something went wrong when inserting brand!");
                        }
                    } else
                    {
                        /*
                         * 1. Get Model
                         * 2. Delete Image from old
                         * 3. Add New Image
                         */
                        string BrandImage = SelectedBrandLogo[0];
                        if (!changedBrandLogo.Equals(SelectedBrandLogo[0]))
                        {
                            bool delete = await cloud.Delete(changedBrandLogo);
                            string cloudinaryLink = await cloud.Upload(SelectedBrandLogo[0], "Brand");
                            BrandImage = cloudinaryLink;
                        }
                        BrandModel _brand = new BrandModel
                        {
                            BrandId = _BrandModel.BrandId,
                            BrandName = txtBrandName.Text,
                            BrandLogo = BrandImage
                        };
                        if (brandService.UpdateBrand(_brand))
                        {
                            MessageBox.Show("Update successful");
                            this.Close();
                            BrandFuncClosed?.Invoke(this, EventArgs.Empty);
                        }
                        else
                        {
                            MessageBox.Show("Something went wrong when updating brand!");
                        }

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
        private void CloseButton_Click(object sender, RoutedEventArgs e) => this.Close();

        private void InputDataForUpdating()
        {
            txtBrandName.Text = _BrandModel.BrandName;
            SelectedBrandLogo.Add(_BrandModel.BrandLogo);
            changedBrandLogo = _BrandModel.BrandLogo;
        }

        private bool Validation()
        {
            bool allCheck = true;
            errorBrandName.Text = "";
            errorBrandImage.Text = "";

            //Brand Name
            if(txtBrandName.Text == "")
            {
                errorBrandName.Text = "Please insert Name!";
            }

            //Brand Image
            bool containsInvalidExtension = false;
            if (SelectedBrandLogo == null || SelectedBrandLogo.Count == 0)
            {
                errorBrandImage.Text = "Please select images";
                allCheck = false;
            }
            else
            {
                foreach (string file in SelectedBrandLogo)
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
                    errorBrandImage.Text = "Please select only .png, .jpg and .webp files";
                    allCheck = false;
                }            
            }


            return allCheck;
        }
    }
}
