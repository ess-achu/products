using InWebApp.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace InWebApp.Services
{
    public class DbServices
    {
        public static string _connectionString = "Server=SYSLP1886;Database=test;Integrated Security=SSPI;Encrypt=True;TrustServerCertificate=True";
        public DbServices() { }
        public void AddProduct(ProductModel productModel)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("InsertProduct", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                cmd.Parameters.Add(new SqlParameter("@productname", productModel.productname));
                cmd.Parameters.Add(new SqlParameter("@category", productModel.category));
                cmd.Parameters.Add(new SqlParameter("@rate", productModel.rate));
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<ProductModel> GetProducts()
        {
            List<ProductModel> products = new List<ProductModel>();

            SqlConnection conn = new SqlConnection(_connectionString);

            try
            {
                SqlCommand cmd = new SqlCommand("GetAllProducts", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                         ProductModel model = new ProductModel
                         {
                             Id = reader.GetInt64("Id"),
                             productname = reader.GetString("productname"),
                             category = reader.GetString("category"),
                             rate = reader.GetDouble("rate")
                         };
                        products.Add(model);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return products;
        }

        public List<ProductModel> GetFilteredProducts(string search)
        {
            List<ProductModel> products = new List<ProductModel>();

            SqlConnection conn = new SqlConnection(_connectionString);

            try
            {
                SqlCommand cmd = new SqlCommand("GetFilteredProducts", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@filterString", search));
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ProductModel model = new ProductModel
                        {
                            Id = reader.GetInt64("Id"),
                            productname = reader.GetString("productname"),
                            category = reader.GetString("category"),
                            rate = reader.GetDouble("rate")
                        };
                        products.Add(model);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return products;
        }
    }
}
