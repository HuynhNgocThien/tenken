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
    public class UserAPIController : Controller
    {
        static SqlConnection dbConnection = DBProvider.getDbConnection();

        [HttpGet]
        [Route("UserAPI/login")]
        public static bool Login(string email, string password, out User userOut)
        {
            return UserManagerProvider.login(dbConnection, email, password, out userOut);
        }

        [HttpPost]
        [Route("UserAPI/register")]
        public static HttpResult Register(User user, Address addess, string password)
        {
            return UserManagerProvider.register(dbConnection, user, addess, password);
        }

        [HttpGet]
        [Route("UserAPI/getUserInfo")]
        public static UserInfo GetUserInfo(int userID)
        {
            return UserManagerProvider.getUserInfo(dbConnection, userID);
        }

        [HttpGet]
        [Route("UserAPI/getAllUser")]
        public static IList<User> GetAllUser()
        {
            return UserManagerProvider.getAllUser(dbConnection);
        }
        public static HttpResult EditUser(int userid, string username, string email)
        {
            return UserManagerProvider.EditUser(dbConnection, userid, username, email);
        }
        [HttpPost]
        [Route("UserAPI/ResetPassword")]
        public object ResetPassword(int userID)
        {
            return JsonConvert.SerializeObject(UserManagerProvider.ResetPassword(dbConnection, userID));
        }

    }
}