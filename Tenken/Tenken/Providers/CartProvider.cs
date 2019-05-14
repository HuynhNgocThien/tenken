using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Tenken.Models;
using TenkenWeb.Common;
using TenkenWeb.Models;

namespace TenkenWeb.Providers
{
    public class CartProvider
    {
        public static Cart getCart(SqlConnection connect, int cartID)
        {
            List<CartItem> cartItem = new List<CartItem>();
            try
            {
                string sql = "[tk].[get_cart_item]";
                SqlCommand cmd = new SqlCommand(sql, connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cartID", cartID);
                connect.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CartItem cart = new CartItem();
                    cart.ProductInfoID = int.Parse(reader["ProductInfoID"].ToString());
                    cart.ProductID = int.Parse(reader["ProductID"].ToString());
                    cart.ProductName = reader["ProductName"].ToString();
                    cart.Price = double.Parse(reader["Price"].ToString());
                    cart.Quantity = int.Parse(reader["Quantity"].ToString());
                    cart.PricePerProduct = double.Parse(reader["PricePerProduct"].ToString());
                    cartItem.Add(cart);
                }
            }
            catch (Exception e)
            {
                connect.Close();
                return null;
            }
            connect.Close();
            Cart result = new Cart();
            result.CartItem = cartItem;
            foreach (CartItem item in cartItem)
            {
                result.TotalPrice += item.PricePerProduct;
            }
            return result;
        }

        public static HttpResult addCart(SqlConnection connect, CartItem cart, int CartID)
        {
            HttpResult result = new HttpResult();
            try
            {
                string sql = "[tk].[cart_item_merge]";
                SqlCommand cmd = new SqlCommand(sql, connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@productInfoID", cart.ProductInfoID);
                cmd.Parameters.AddWithValue("@productID", cart.ProductID);
                cmd.Parameters.AddWithValue("@quantity", cart.Quantity);
                cmd.Parameters.AddWithValue("@cartID", CartID);
                cmd.Parameters.Add("@productInfoIdOut", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@resultOut", SqlDbType.VarChar).Direction = ParameterDirection.Output;
                connect.Open();
                cmd.ExecuteNonQuery();
                result.ID = (int)cmd.Parameters["@productInfoIdOut"].Value;
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

        public static HttpResult buy(SqlConnection connect, int cartID, Order order)
        {
            HttpResult result = new HttpResult();
            try
            {
                string sql = "[tk].[order_merge]";
                SqlCommand cmd = new SqlCommand(sql, connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cartID", cartID);
                cmd.Parameters.AddWithValue("@orderID", order.OrderID);
                cmd.Parameters.AddWithValue("@addressID", order.Address.ID);
                cmd.Parameters.AddWithValue("@address", order.Address.AddressName);
                cmd.Parameters.AddWithValue("@phoneNumber", order.Address.PhoneNumber);
                cmd.Parameters.AddWithValue("@deliveryStatus", "Waiting");
                cmd.Parameters.AddWithValue("@paymentStatus", "Waiting");
                cmd.Parameters.Add("@resultOut", SqlDbType.VarChar).Direction = ParameterDirection.Output;
                connect.Open();
                cmd.ExecuteNonQuery();
                result.Message = cmd.Parameters["@resultOut"].Value.ToString();
                result.Result = true;
            }
            catch (Exception e)
            {
                result.ID = 0;
                result.Message = TkConstant.UnexpectedError;
                result.Result = false;
                connect.Close();
                return result;
            }
            connect.Close();
            return result;
        }
        public static int GetCartValue(SqlConnection connect, int cartID)
        {
            int result = 0;
            try
            {
                string sql = "[tk].[get_cart_value]";
                SqlCommand cmd = new SqlCommand(sql, connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cartID", cartID);
                connect.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result = int.Parse(reader["Quantity"].ToString());
                }
            }
            catch (Exception e)
            {
                connect.Close();
                return 0;
            }
            connect.Close();
            return result;
        }
    }
}
