using BusinessObject.Model.Page;
using DataAccess.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Dashboard_Admin;

namespace WPFStylingTest.CategoryManagement
{
    /// <summary>
    /// Interaction logic for AddCategory.xaml
    /// </summary>
    public partial class CategoryFunc : Window
    {
        //Initialize Service
        private readonly CategoryService categoryService;
        //------------------

        public CategoryFunc()
        {
            categoryService = App.GetService<CategoryService>();
            InitializeComponent();
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

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (Validation())
            {
                CategoryModel _cate = new CategoryModel
                {
                    CateName = txtCategoryName.Text,
                    Keyword = txtCategoryKeyword.Text
                };

                if(categoryService.AddCategory(_cate))
                {
                    MessageBox.Show("Insert successful");
                } else
                {
                    MessageBox.Show("Something went wrong when inserting category!");
                }

            } else
            {
                MessageBox.Show("Something went wrong! Check the error for more information");
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e) =>this.Close();
        

        private Boolean Validation()
        {
            bool allCheck = true;
            bool IsKeywordExisted = categoryService.IsKeywordExisted(txtCategoryKeyword.Text);

            errorCateName.Text = "";
            errorCateKeyword.Text = "";


            if(txtCategoryName.Text == "")
            {
                allCheck = false;   
                errorCateName.Text = "Name is empty!";
            }

            if(txtCategoryKeyword.Text == "")
            {
                errorCateKeyword.Text = "Keyword is empty!";
                allCheck = false;
            } else if (!IsKeywordExisted)
            {
                allCheck = false;
                errorCateKeyword.Text = "Keyword Already Existed!";
            } else if (txtCategoryKeyword.Text.Length == 0 || txtCategoryKeyword.Text.Length > 2)
            {
                allCheck = false;
                errorCateKeyword.Text = "Keyword Length can't be longer than 2!";
            }

            return allCheck;
        }
    }
}
