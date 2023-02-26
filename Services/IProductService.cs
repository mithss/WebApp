using System.Data.SqlClient;
using WebApp.Models;

namespace WebApp.Services
{
    public interface IProductService
    {
        List<Product> GetProducts();
    }
}