using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;
using Tenken.Models;
using TenkenWeb.Common;
using TenkenWeb.Models;
using TenkenWeb.Providers;

namespace Tenken.Controllers
{
    public class CommentAPIController : Controller
    {
        static SqlConnection dbConnection = DBProvider.getDbConnection();

        [HttpGet]
        [Route("CommentAPI/getComment")]
        public static IList<Comment> GetComment(int productID)
        {
            return CommentProvider.getComment(dbConnection,productID);
        }

        [HttpGet]
        [Route("CommentAPI/getReply")]
        public IList<Comment> GetReply(int commentID)
        {
            return CommentProvider.getReply(dbConnection, commentID);
        }

        [HttpPost]
        [Route("CommentAPI/commentMerge")]
        public object CommentMerge(string content, int productID,int userID,int rating, int reply)
        {
            Comment comment = new Comment()
            {
                ProductID = productID,
                UserID = userID,
                Content = content,
                Rating = rating,
                Reply = reply
            };
            return JsonConvert.SerializeObject(CommentProvider.commentMerge(dbConnection,comment));
        }
    }
}