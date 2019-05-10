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
        public IList<Comment> GetComment(int productID)
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
        [HttpPut]
        [Route("CommentAPI/commentMerge")]
        public HttpResult CommentMerge(Comment comment)
        {
            return CommentProvider.commentMerge(dbConnection,comment);
        }
    }
}