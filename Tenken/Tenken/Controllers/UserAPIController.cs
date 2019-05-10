using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;
using Tenken.Models;
using TenkenWeb.Common;
using TenkenWeb.Models;
using TenkenWeb.Providers;

namespace Tenken.Controllers
{
    public class UserAPIController : Controller
    {
        static SqlConnection dbConnection = DBProvider.getDbConnection();

        [HttpGet]
        [Route("UserAPI/login")]
        public bool Login(string email, string password)
        {
            password = DBProvider.EncodeSHA1(password);
            return UserManagerProvider.login(dbConnection,email, password);
        }

        [HttpPost]
        [Route("UserAPI/register")]
        public HttpResult Register(User user, Address addess, string password)
        {
            return UserManagerProvider.register(dbConnection,user,addess,password);
        }

        [HttpGet]
        [Route("UserAPI/getUserInfo")]
        public UserInfo GetUserInfo(int userID)
        {
            return UserManagerProvider.getUserInfo(dbConnection, userID);
        }

        [HttpGet]
        [Route("UserAPI/getAllUser")]
        public IList<User> GetAllUser()
        {
            return UserManagerProvider.getAllUser(dbConnection);
        }

    }
}