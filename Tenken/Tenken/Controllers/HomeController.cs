using System;
using System.Web.Mvc;
using Tenken.Models;
using TenkenWeb.Models;

namespace Tenken.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.ProductList = ProductAPIController.GetTopProduct("New");
            ViewBag.Size = ViewBag.ProductList.Count;
            return View();
        }
        public ActionResult Login()
        {
            //ViewBag.Email = "";
            //ViewBag.Password = "";
            ViewBag.Error = false;
            ViewBag.Register = false;
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            User userOut = new User();
            bool result = UserAPIController.Login(email, password, out userOut);
            if (result)
            {
                Session.Add("UserName", userOut.UserName);
                Session.Add("UserID", userOut.ID);
                Session.Add("CartID", userOut.CartID);
                return Redirect("/");
            }
            else
            {
                ViewBag.Email = email;
                ViewBag.Password = "";
                ViewBag.Error = true;
                return View();
            }
        }
        public ActionResult Register()
        {
            ViewBag.SuccessMessage = null;
            ViewBag.UserInfo = new UserInfo();
            ViewBag.UserInfo.user = new User();
            ViewBag.UserInfo.address = new Address();
            return View();
        }
        [HttpPost]
        public ActionResult Register(string username, string email, string password, string address, string phonenumber)
        {
            User user = new User()
            {
                UserName = username,
                Email = email
            };
            Address useraddress = new Address()
            {
                AddressName = address,
                PhoneNumber = phonenumber
            };
            UserInfo userinfo = new UserInfo()
            {
                user = user,
                address = useraddress
            };
            HttpResult result = UserAPIController.Register(user, useraddress, password);
            if (result.Result)
            {
                ViewBag.SuccessRegister = true;
            }
            else
            {
                ViewBag.ErrorMessage = result.Message;
            }
            ViewBag.UserInfo = userinfo;
            ViewBag.Password = password;
            ViewBag.RePassword = password;
            return View();
        }
        public ActionResult LogOut()
        {
            Session.Remove("UserName");
            Session.Remove("UserID");
            Session.Remove("CartID");
            return Redirect("/");
        }
    }
}