using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TenkenWeb.Common;
using TenkenWeb.Models;

namespace TenkenWeb.Providers
{
    public class CartProvider
    {
        public static IList<Cart> getCart(SqlConnection connect, int userID)
        {
            IList<Cart> result = new List<Cart>();
            try
            {
                Cart cart = new Cart();
                string sql = "[tk].[cart_get]";
                SqlCommand cmd = new SqlCommand(sql, connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userID", userID);
                connect.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cart.cartID = int.Parse(reader["CartID"].ToString());
                    cart.BookID = int.Parse(reader["BookID"].ToString());
                    cart.BookName = reader["BookName"].ToString();
                    cart.quantity = int.Parse(reader["Quantity"].ToString());
                    cart.totalPrice = int.Parse(reader["Price"].ToString());
                    result.Add(cart);
                }
            }
            catch (Exception e)
            {
                connect.Close();
                return result;
            }
            connect.Close();
            return result;
        }

        public static HttpResult addCart(SqlConnection connect, Cart cart)
        {
            HttpResult result = new HttpResult();
            try
            {
                string sql = "[tk].[cart_insert_update]";
                SqlCommand cmd = new SqlCommand(sql, connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CartID", cart.cartID);
                cmd.Parameters.AddWithValue("@BookID", cart.BookID);
                cmd.Parameters.AddWithValue("@Quantity", cart.quantity);
                cmd.Parameters.Add("@cartIDOut", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@resultOut", SqlDbType.VarChar).Direction = ParameterDirection.Output;
                connect.Open();
                cmd.ExecuteNonQuery();
                result.ID = (int)cmd.Parameters["@cartIDOut"].Value;
                result.Message = cmd.Parameters["@resultOut"].Value.ToString();
                result.Result = result.ID > 0 ? true : false;
            }
            catch (Exception e)
            {
                result.ID = 1;
                result.Message = TkConstant.UnexpectedError;
                result.Result = false;
                connect.Close();
                return result;
            }
            connect.Close();
            return result;
        }

        public static HttpResult buy(SqlConnection connect, int userID)
        {
            HttpResult result = new HttpResult();
            try
            {
                string sql = "[tk].[buy_cart]";
                SqlCommand cmd = new SqlCommand(sql, connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.Parameters.Add("@cartIDOut", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@resultOut", SqlDbType.VarChar).Direction = ParameterDirection.Output;
                connect.Open();
                cmd.ExecuteNonQuery();
                result.ID = (int)cmd.Parameters["@cartIDOut"].Value;
                result.Message = cmd.Parameters["@resultOut"].Value.ToString();
                result.Result = result.ID > 0 ? true : false;
            }
            catch (Exception e)
            {
                result.ID = 1;
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
