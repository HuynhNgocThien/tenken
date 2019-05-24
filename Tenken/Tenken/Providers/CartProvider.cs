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

        public static Order getOrder(SqlConnection connect, int orderID)
        {
            Order result = new Order()
            {
                Address = new Address()
            };
            try
            {
                string sql = "[tk].[get_order]";
                SqlCommand cmd = new SqlCommand(sql, connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@orderID", orderID);
                connect.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.OrderID = int.Parse(reader["OrderID"].ToString());
                    result.DeliveryStatus = reader["DeliveryStatus"].ToString();
                    result.PaymentStatus = reader["PaymentStatus"].ToString();
                    result.Address.ID = int.Parse(reader["AddressID"].ToString());
                    result.Address.AddressName = reader["Address"].ToString();
                    result.Address.PhoneNumber = reader["PhoneNumber"].ToString();
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
        public static List<OrderItem> getOrderItem(SqlConnection connect, int orderID)
        {
            List<OrderItem> result = new List<OrderItem>();
            try
            {

                string sql = "[tk].[get_order_item]";
                SqlCommand cmd = new SqlCommand(sql, connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@orderID", orderID);
                connect.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    OrderItem item = new OrderItem();
                    item.ProductID = int.Parse(reader["ProductID"].ToString());
                    item.ProductName = reader["ProductName"].ToString();
                    item.Price = double.Parse(reader["Price"].ToString());
                    item.Quantity = int.Parse(reader["Quantity"].ToString());
                    item.PricePerProduct = double.Parse(reader["PricePerProduct"].ToString());
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

        public static HttpResult addCart(SqlConnection connect, int ProductID, int Quantity, int CartID)
        {
            HttpResult result = new HttpResult();
            try
            {
                string sql = "[tk].[cart_item_merge]";
                SqlCommand cmd = new SqlCommand(sql, connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@productID", ProductID);
                cmd.Parameters.AddWithValue("@quantity", Quantity);
                cmd.Parameters.AddWithValue("@cartID", CartID);
                cmd.Parameters.Add("@productInfoIdOut", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@resultOut", SqlDbType.VarChar,200).Direction = ParameterDirection.Output;
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
                cmd.Parameters.Add("@orderIdOut", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@resultOut", SqlDbType.VarChar, 200).Direction = ParameterDirection.Output;
                connect.Open();
                cmd.ExecuteNonQuery();
                result.Message = cmd.Parameters["@resultOut"].Value.ToString();
                result.Result = true;
                result.ID = (int)cmd.Parameters["@orderIdOut"].Value;
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
        public static HttpResult removeCartItem(SqlConnection connect, int ProductInfoID, int CartID)
        {
            HttpResult result = new HttpResult();
            try
            {
                string sql = "[tk].[cart_item_delete]";
                SqlCommand cmd = new SqlCommand(sql, connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@productInfoID", ProductInfoID);
                cmd.Parameters.AddWithValue("@cartID", CartID);
                cmd.Parameters.Add("@cartItemIDOut", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@resultOut", SqlDbType.VarChar, 200).Direction = ParameterDirection.Output;
                connect.Open();
                cmd.ExecuteNonQuery();
                result.ID = (int)cmd.Parameters["@cartItemIDOut"].Value;
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
