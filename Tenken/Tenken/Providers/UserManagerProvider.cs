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
    public class UserManagerProvider
    {
        public static bool login(SqlConnection connect, string userName, string password)
        {
            bool result;
            password = DBProvider.EncodeSHA1(password);
            try
            {
                User user = new User();
                string sql = "select top 1* from tk_user where UserName=@userName and Password=@password";
                SqlCommand cmd = new SqlCommand(sql, connect);
                cmd.Parameters.AddWithValue("@userName", userName);
                cmd.Parameters.AddWithValue("@password", password);
                connect.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    user.ID = int.Parse(reader.GetValue(0).ToString());
                    user.UserName = reader.GetValue(1).ToString();
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

        public static HttpResult register(SqlConnection connect, string userName, string password)
        {
            HttpResult result = new HttpResult();
            password = DBProvider.EncodeSHA1(password);
            try
            {
                string sql = "[tk].[register_user]";
                SqlCommand cmd = new SqlCommand(sql, connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userName", userName);
                cmd.Parameters.AddWithValue("@password", password);
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
    }
}
