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
    public class UserManagerProvider
    {
        public static bool login(SqlConnection connect, string email, string password)
        {
            bool result;
            password = DBProvider.EncodeSHA1(password);
            try
            {
                User user = new User();
                string sql = "[tk].[get_login_user]";
                SqlCommand cmd = new SqlCommand(sql, connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@password", password);
                connect.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    user.ID = int.Parse(reader.GetValue(0).ToString());
                    user.UserName = reader.GetValue(1).ToString();
                    user.Email = reader.GetValue(3).ToString();
                    user.CartID = int.Parse(reader.GetValue(4).ToString());
                }
                if (user.ID > 0)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception e)
            {
                result = false;
                connect.Close();
                return result;
            }
            connect.Close();
            return result;
        }

        public static HttpResult register(SqlConnection connect, User user,Address addess, string password)
        {
            HttpResult result = new HttpResult();
            password = DBProvider.EncodeSHA1(password);
            try
            {
                string sql = "[tk].[register_user]";
                SqlCommand cmd = new SqlCommand(sql, connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userName", user.UserName);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@email", user.Email);
                cmd.Parameters.AddWithValue("@address", addess.AddressName);
                cmd.Parameters.AddWithValue("@phoneNumber", addess.PhoneNumber);
                cmd.Parameters.Add("@userIdOut", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@resultOut", SqlDbType.VarChar).Direction = ParameterDirection.Output;
                connect.Open();
                cmd.ExecuteNonQuery();
                result.ID = (int)cmd.Parameters["@userIdOut"].Value;
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

        public static UserInfo getUserInfo(SqlConnection connect, int userID)
        {
            UserInfo result = new UserInfo();
            try
            {
                string sql = "[tk].[get_user_info]";
                SqlCommand cmd = new SqlCommand(sql, connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userID", userID);
                connect.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.user.ID = int.Parse(reader["UserID"].ToString());
                    result.user.UserName = reader["UserName"].ToString();
                    result.user.Email = reader["Email"].ToString();
                    result.address.ID = int.Parse(reader["AddressID"].ToString());
                    result.address.AddressName = reader["Address"].ToString();
                    result.address.PhoneNumber = reader["PhoneNumber"].ToString();
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

        public static IList<User> getAllUser(SqlConnection connect)
        {
            List<User> result = new List<User>();
            try
            {
                User item = new User();
                string sql = "[tk].[get_all_user]";
                SqlCommand cmd = new SqlCommand(sql, connect);
                cmd.CommandType = CommandType.StoredProcedure;
                connect.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    item.ID = int.Parse(reader["UserID"].ToString());
                    item.UserName = reader["UserName"].ToString();
                    item.Email = reader["Email"].ToString();
                    item.CartID = int.Parse(reader["CartID"].ToString());
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
    }
}
