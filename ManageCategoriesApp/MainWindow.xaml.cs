using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ManageCategoriesApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    ManageCategories _categories = new ManageCategories();
    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        LoadCategories();
        if (lvCategories.Items.Count > 0)
        {
            lvCategories.SelectedIndex = 0;
        }
    }
    private void LoadCategories() => lvCategories.ItemsSource = _categories.GetCategories();
    private void btnInsert_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            Category category = new Category { CategoryName = txtCategoryName.Text };
            _categories.InsertCategory(category);
            LoadCategories();
        } catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Insert Category");
        }
     
    }

    private void btnUpdate_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (int.TryParse(txtCategoryID.Text, out int categoryId))
            {
                var category = new Category
                {
                    CategoryID = categoryId,
                    CategoryName = txtCategoryName.Text
                };
                _categories.UpdateCategory(category);
                LoadCategories();
            }
            else
            {
                MessageBox.Show("Invalid Category ID", "Update Category");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Update Category");
        }
    }

    private void btnDelete_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (int.TryParse(txtCategoryID.Text, out int categoryId))
            {
                var category = new Category
                {
                    CategoryID = categoryId,
                    CategoryName = txtCategoryName.Text
                };
                _categories.DeleteCategory(category);
                LoadCategories();
            }
            else
            {
                MessageBox.Show("Invalid Category ID", "Delete Category");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Delete Category");
        }
    }
    
    private void btnLoad_Click(object sender, RoutedEventArgs e)
    {
        LoadCategories();
    }

}