using System.Data.SqlClient;
using WebApp.Models;

namespace WebApp.Services
{
    public class ProductService : IProductService
    {
        private readonly IConfiguration _configuration;

        public ProductService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("SQLConnection"));
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
