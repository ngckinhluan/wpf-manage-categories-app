using System.Data;
using Microsoft.Data.SqlClient;

namespace ManageCategoriesApp;

public record Category
{
    public int CategoryID { get; set; }
    public string CategoryName { get; set; }
}

public class ManageCategories
{
    private SqlConnection _connection;
    private SqlCommand _command;
    private string ConnectionString = "Server=(local);uid=sa;pwd=12345;database=MyStore;TrustServerCertificate=True";

    public List<Category> GetCategories()
    {
        List<Category> categories = new List<Category>();
        _connection = new SqlConnection(ConnectionString);
        string query = "SELECT * FROM Categories";
        _command = new SqlCommand(query, _connection);
        try
        {
            _connection.Open();
            SqlDataReader reader = _command.ExecuteReader(CommandBehavior.CloseConnection);
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    categories.Add(new Category
                    {
                        CategoryID = (int)reader["CategoryID"],
                        CategoryName = reader.GetString("CategoryName")
                    });
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            _connection.Close();
        }

        return categories;
    }

    public void InsertCategory(Category category)
    {
        _connection = new SqlConnection(ConnectionString);
        _command = new SqlCommand("INSERT INTO Categories VALUES(@CategoryName)", _connection);
        _command.Parameters.AddWithValue("@CategoryName", category.CategoryName); // Add this line
        try
        {
            _connection.Open();
            _command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            _connection.Close();
        }
    }

    public void UpdateCategory(Category category)
    {
        _connection = new SqlConnection(ConnectionString);
        string query = "UPDATE Categories SET CategoryName = @CategoryName WHERE CategoryID = @CategoryID";
        _command = new SqlCommand(query, _connection);
        _command.Parameters.AddWithValue("@CategoryID", category.CategoryID);
        _command.Parameters.AddWithValue("@CategoryName", category.CategoryName);
        try
        {
            _connection.Open();
            _command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            _connection.Close();
        }
    }

    public void DeleteCategory(Category category)
    {
        _connection = new SqlConnection(ConnectionString);
        string query = "DELETE Categories WHERE CategoryID = @CategoryID";
        _command = new SqlCommand(query, _connection);
        _command.Parameters.AddWithValue("@CategoryID", category.CategoryID);
        try
        {
            _connection.Open();
            _command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            _connection.Close();
        }
    }
   
}