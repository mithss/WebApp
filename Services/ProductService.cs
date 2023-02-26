using System.Data.SqlClient;
using WebApp.Models;

namespace WebApp.Services
{
    public class ProductService
    {
        private static string db_source = "sqlexpress50001.database.windows.net";
        private static string db_user = "sqladmin";
        private static string db_password = "Welcome@123";
        private static string db_database = "appdb";

        public SqlConnection GetConnection()
        {
            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = db_source;
            builder.UserID = db_user;
            builder.Password = db_password;
            builder.InitialCatalog = db_database;
            return new SqlConnection(builder.ConnectionString);
        }

        public List<Product> GetProducts()
        {
            SqlConnection connection = GetConnection();
            List<Product> products = new List<Product>();

            string statement = "SELECT ProductID, ProductName, Quantity FROM Products";

            connection.Open();

            SqlCommand command = new SqlCommand(statement, connection);
            using (SqlDataReader sqlDataReader = command.ExecuteReader())
            {
                while (sqlDataReader.Read())
                {
                    Product product = new Product()
                    {
                        Id = sqlDataReader.GetInt32(0),
                        ProductName = sqlDataReader.GetString(1),
                        Quantity = sqlDataReader.GetInt32(2),
                    };
                    products.Add(product);
                }
            }
            connection.Close();
            return products;
        }
    }
}
