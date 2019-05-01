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
    public class CategoryProvider
    {
        public static IList<Category> getCategory(SqlConnection connect, string categoryName, int categoryID)
        {
            List<Category> result = new List<Category>();
            try
            {
                Category item = new Category();
                string sql = "[tk].[get_category]";
                SqlCommand cmd = new SqlCommand(sql, connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@categoryName", categoryName);
                cmd.Parameters.AddWithValue("@categoryID", categoryID);
                connect.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    item.CategoryID = int.Parse(reader["CategoryID"].ToString());
                    item.CategoryName = reader["CategoryName"].ToString();
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

        public static HttpResult categoryMerge(SqlConnection connect, Category category)
        {
            HttpResult result = new HttpResult();
            try
            {
                string sql = "[tk].[category_merge]";
                SqlCommand cmd = new SqlCommand(sql, connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@categoryID", category.CategoryID);
                cmd.Parameters.AddWithValue("@categoryName", category.CategoryName);
                cmd.Parameters.Add("@categoryIdOut", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@resultOut", SqlDbType.VarChar).Direction = ParameterDirection.Output;
                connect.Open();
                cmd.ExecuteNonQuery();
                result.ID = (int)cmd.Parameters["@categoryIdOut"].Value;
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
