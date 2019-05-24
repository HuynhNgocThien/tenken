using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Tenken.Models;
using TenkenWeb.Common;
using TenkenWeb.Models;

namespace TenkenWeb.Providers
{
    public class ProductProvider
    {
        public static IList<Product> getTopProduct(SqlConnection connect, int typeOfTop)
        {
            List<Product> result = new List<Product>();
            try
            {
                string sql = "[tk].[get_top_product]";
                SqlCommand cmd = new SqlCommand(sql, connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@typeOfTop", typeOfTop);
                connect.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Product item = new Product();
                    item.ProductID = int.Parse(reader["ProductID"].ToString());
                    item.ProductName = reader["ProductName"].ToString();
                    item.Description = reader["Description"].ToString();
                    item.Price = double.Parse(reader["Price"].ToString());
                    item.StockQuantity = int.Parse(reader["StockQuantity"].ToString());
                    item.CategoryID = int.Parse(reader["CategoryID"].ToString());
                    item.CategoryName = reader["CategoryName"].ToString();
                    item.ImageName = reader["ImageName"].ToString();
                    result.Add(item);
                }
            }
            catch (Exception e)
            {
                connect.Close();
                return null;
            }
            connect.Close();
            return result;
        }
        public static Product getProductByID(SqlConnection connect, int productID)
        {
            Product result = new Product();
            try
            {
                string sql = "[tk].[get_product]";
                SqlCommand cmd = new SqlCommand(sql, connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@productID", productID);
                connect.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.ProductID = int.Parse(reader["ProductID"].ToString());
                    result.ProductName = reader["ProductName"].ToString();
                    result.Description = reader["Description"].ToString();
                    result.Price = double.Parse(reader["Price"].ToString());
                    result.StockQuantity = int.Parse(reader["StockQuantity"].ToString());
                    result.CategoryID = int.Parse(reader["CategoryID"].ToString());
                    result.CategoryName = reader["CategoryName"].ToString();
                    result.ImageName = reader["ImageName"].ToString();
                }
            }
            catch (Exception e)
            {
                connect.Close();
                return null;
            }
            connect.Close();
            return result;
        }
        public static IList<Product> getProduct(SqlConnection connect, string productName = "", int productID = 0)
        {
            List<Product> result = new List<Product>();
            try
            {
                string sql = "[tk].[get_product]";
                SqlCommand cmd = new SqlCommand(sql, connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@productName", productName);
                cmd.Parameters.AddWithValue("@productID", productID);
                connect.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Product item = new Product();
                    item.ProductID = int.Parse(reader["ProductID"].ToString());
                    item.ProductName = reader["ProductName"].ToString();
                    item.Description = reader["Description"].ToString();
                    item.Price = double.Parse(reader["Price"].ToString());
                    item.StockQuantity = int.Parse(reader["StockQuantity"].ToString());
                    item.CategoryID = int.Parse(reader["CategoryID"].ToString());
                    item.CategoryName = reader["CategoryName"].ToString();
                    item.ImageName = reader["ImageName"].ToString();
                    result.Add(item);
                }
            }
            catch (Exception e)
            {
                connect.Close();
                return null;
            }
            connect.Close();
            return result;
        }
        public static IList<Product> getProductByCategory(SqlConnection connect, int categoryID)
        {
            List<Product> result = new List<Product>();
            try
            {
                string sql = "[tk].[get_product_by_category]";
                SqlCommand cmd = new SqlCommand(sql, connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@categoryID", categoryID);
                connect.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Product item = new Product();
                    item.ProductID = int.Parse(reader["ProductID"].ToString());
                    item.ProductName = reader["ProductName"].ToString();
                    item.Description = reader["Description"].ToString();
                    item.Price = double.Parse(reader["Price"].ToString());
                    item.StockQuantity = int.Parse(reader["StockQuantity"].ToString());
                    item.CategoryID = int.Parse(reader["CategoryID"].ToString());
                    item.CategoryName = reader["CategoryName"].ToString();
                    item.ImageName = reader["ImageName"].ToString();
                    result.Add(item);
                }
            }
            catch (Exception e)
            {
                connect.Close();
                return null;
            }
            connect.Close();
            return result;
        }
        public static HttpResult productMerge(SqlConnection connect, Product product)
        {
            HttpResult result = new HttpResult();
            try
            {
                string sql = "[tk].[product_merge]";
                SqlCommand cmd = new SqlCommand(sql, connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@productID", product.ProductID);
                cmd.Parameters.AddWithValue("@productName", product.ProductName);
                cmd.Parameters.AddWithValue("@description", product.Description);
                cmd.Parameters.AddWithValue("@price", product.Price);
                cmd.Parameters.AddWithValue("@stockQuantity", product.StockQuantity);
                cmd.Parameters.AddWithValue("@categoryID", product.CategoryID);
                cmd.Parameters.AddWithValue("@imageName", product.ImageName);
                cmd.Parameters.Add("@productIdOut", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@resultOut", SqlDbType.VarChar,200).Direction = ParameterDirection.Output;
                connect.Open();
                cmd.ExecuteNonQuery();
                result.ID = (int)cmd.Parameters["@productIdOut"].Value;
                result.Message = cmd.Parameters["@resultOut"].Value.ToString();
                result.Result = result.ID > 0 ? true : false;
            }
            catch (Exception e)
            {
                result.ID = -1;
                result.Message = TkConstant.UnexpectedError;
                result.Result = false;
                connect.Close();
                return result;
            }
            connect.Close();
            return result;
        }
        public static HttpResult productDelete(SqlConnection connect, int productID)
        {
            HttpResult result = new HttpResult();
            try
            {
                string sql = "[tk].[product_delete]";
                SqlCommand cmd = new SqlCommand(sql, connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@productID", productID);
                cmd.Parameters.Add("@productIDOut", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@resultOut", SqlDbType.VarChar, 200).Direction = ParameterDirection.Output;
                connect.Open();
                cmd.ExecuteNonQuery();
                result.ID = (int)cmd.Parameters["@productIDOut"].Value;
                result.Message = cmd.Parameters["@resultOut"].Value.ToString();
                result.Result = result.ID > 0 ? true : false;
            }
            catch (Exception e)
            {
                result.ID = -1;
                result.Message = TkConstant.UnexpectedError;
                result.Result = false;
                connect.Close();
                return result;
            }
            connect.Close();
            return result;
        }
    }
}
