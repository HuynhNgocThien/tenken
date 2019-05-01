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
    public class CommentProvider
    {
        public static IList<Comment> getComment(SqlConnection connect, int productID)
        {
            List<Comment> result = new List<Comment>();
            try
            {
                Comment item = new Comment();
                string sql = "[tk].[get_comment]";
                SqlCommand cmd = new SqlCommand(sql, connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@productID", productID);
                connect.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    item.CommentID = int.Parse(reader["CommentID"].ToString());
                    item.ProductID = int.Parse(reader["ProductID"].ToString());
                    item.UserID = int.Parse(reader["UserID"].ToString());
                    item.UserName = reader["UserName"].ToString();
                    item.Content = reader["Content"].ToString();
                    item.Rating = int.Parse(reader["Rating"].ToString());
                    item.Reply = int.Parse(reader["Reply"].ToString());
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
        public static IList<Comment> getReply(SqlConnection connect, int commentID)
        {
            List<Comment> result = new List<Comment>();
            try
            {
                Comment item = new Comment();
                string sql = "[tk].[get_reply]";
                SqlCommand cmd = new SqlCommand(sql, connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@reply", commentID);
                connect.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    item.CommentID = int.Parse(reader["CommentID"].ToString());
                    item.ProductID = int.Parse(reader["ProductID"].ToString());
                    item.UserID = int.Parse(reader["UserID"].ToString());
                    item.UserName = reader["UserName"].ToString();
                    item.Content = reader["Content"].ToString();
                    item.Rating = int.Parse(reader["Rating"].ToString());
                    item.Reply = int.Parse(reader["Reply"].ToString());
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
        public static HttpResult commentMerge(SqlConnection connect, Comment comment)
        {
            HttpResult result = new HttpResult();
            try
            {
                string sql = "[tk].[comment_merge]";
                SqlCommand cmd = new SqlCommand(sql, connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@commentID", comment.CommentID);
                cmd.Parameters.AddWithValue("@content", comment.Content);
                cmd.Parameters.AddWithValue("@userID", comment.UserID);
                cmd.Parameters.AddWithValue("@reply", comment.Reply);
                cmd.Parameters.AddWithValue("@productID", comment.ProductID);
                cmd.Parameters.AddWithValue("@rating", comment.Rating);
                cmd.Parameters.Add("@commentIdOut", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@resultOut", SqlDbType.VarChar).Direction = ParameterDirection.Output;
                connect.Open();
                cmd.ExecuteNonQuery();
                result.ID = (int)cmd.Parameters["@commentIdOut"].Value;
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
