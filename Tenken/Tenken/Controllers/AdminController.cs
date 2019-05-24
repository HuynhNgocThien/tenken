using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Tenken.Models;
using TenkenWeb.Models;

namespace Tenken.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            if (Session["isLoginAdmin"] != null && bool.Parse(Session["isLoginAdmin"].ToString()) == true)
            {
                ViewBag.ProductList = ProductAPIController.GetTopProduct("New");
                ViewBag.Size = ViewBag.ProductList.Count;
                return View();
            }
            else
            {
                return Redirect("/Admin/Login");
            }
        }
        public ActionResult Login()
        {
            if (Session["isLoginAdmin"] != null && bool.Parse(Session["isLoginAdmin"].ToString()) == true)
            {
                return Redirect("/Admin/Index");
            }
            else
            {
                ViewBag.Error = false;
                ViewBag.Register = false;
                return View();
            }
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            string passwordAdmin = ConfigurationManager.AppSettings.Get("PasswordAdmin");
            bool result = username.Equals("Admin", StringComparison.InvariantCultureIgnoreCase) && password == passwordAdmin;
            if (result)
            {
                Session.Add("isLoginAdmin", true);
                return Redirect("/Admin/Index");
            }
            else
            {
                ViewBag.UserName = username;
                ViewBag.Password = "";
                ViewBag.Error = true;
                return View();
            }
        }

        public ActionResult LogOut()
        {
            if (Session["isLoginAdmin"] != null && bool.Parse(Session["isLoginAdmin"].ToString()) == true)
            {
                Session.Remove("isLoginAdmin");
            }
            return Redirect("/Admin/Login");

        }
        public ActionResult Menu(string TypeMenu)
        {
            if (Session["isLoginAdmin"] != null && bool.Parse(Session["isLoginAdmin"].ToString()) == true)
            {
                switch (TypeMenu)
                {
                    case "User":
                        ViewBag.DataList = UserAPIController.GetAllUser();
                        break;
                    case "Category":
                        ViewBag.DataList = CategoryAPIController.getAllCategoryAdmin();
                        break;
                    case "Product":
                        ViewBag.DataList = ProductAPIController.GetAllProduct();
                        break;
                }
                ViewBag.Type = TypeMenu;
                ViewBag.Size = ViewBag.DataList.Count;
                return View();
            }
            else
            {
                return Redirect("/Admin/Login");
            }

        }
        public ActionResult UserAdmin(int userID)
        {
            if (userID == 0)
            {
                ViewBag.User = new User();
            }
            else
            {
                ViewBag.User = UserAPIController.GetUserInfo(userID).user;
            }
            return View();
        }
        [HttpPost]
        public ActionResult UserAdmin(int userid, string username, string email)
        {
            HttpResult result = UserAPIController.EditUser(userid, username, email);
            if (result.Result)
            {
                ViewBag.Success = true;
            }
            else
            {
                ViewBag.Error = result.Message;
            }
            User user = new User()
            {
                ID = userid,
                UserName = username,
                Email = email
            };
            ViewBag.User = user;
            return View();
        }
        public ActionResult CategoryAdmin(int categoryID)
        {
            if (categoryID == 0)
            {
                List<Category> data = new List<Category>();
                data.Add(new Category());
                ViewBag.Category = data;
            }
            else
            {
                ViewBag.Category = CategoryAPIController.getCategory(categoryID, "");
            }
            return View();
        }
        [HttpPost]
        public ActionResult CategoryAdmin(int categoryID, string categoryName)
        {
            Category category = new Category()
            {
                CategoryID = categoryID,
                CategoryName = categoryName
            };
            HttpResult result = CategoryAPIController.CategoryMerge(category);
            if (result.Result)
            {
                ViewBag.Success = true;
            }
            else
            {
                ViewBag.Error = result.Message;
            }
            List<Category> data = new List<Category>();
            data.Add(category);
            ViewBag.Category = data;
            return View();
        }
        public ActionResult ProductAdmin(int productID)
        {
            if (productID == 0)
            {
                ViewBag.Product = new Product();
            }
            else
            {
                ViewBag.Product = ProductAPIController.GetProductByID(productID);
            }
            ViewBag.ImageURL = "~/images/" + ViewBag.Product.ImageName;
            ViewBag.CategoryList = CategoryAPIController.getAllCategoryAdmin();
            return View();
        }
        public void checkFileExist(string path)
        {
            // Check if our file exists
            if (System.IO.File.Exists(path))
            {
                // Delete our file
                System.IO.File.Delete(path);
            }
        }
        public string getExtension(string fileName)
        {
            string[] data = fileName.Split('.');
            return data[data.Length - 1];
        }
        [HttpPost]
        public ActionResult ProductAdmin(int productID, string productName, string description, int price, int categoryid, HttpPostedFileBase ImageURL)
        {
            if (ImageURL != null && ImageURL.ContentLength > 0)
            {
                // create file name mapping with product name
                var fileName = productName + "." + getExtension(Path.GetFileName(ImageURL.FileName));
                // store the file inside ~/App_Data/uploads folder
                var path = Path.Combine(Server.MapPath("~/images"), fileName);
                checkFileExist(path);
                ImageURL.SaveAs(path);
            }
            ViewBag.Product = new Product()
            {
                ProductID = productID,
                ProductName = productName,
                Description = description,
                Price = price,
                CategoryID = categoryid,
                ImageName = productName + "." + getExtension(Path.GetFileName(ImageURL.FileName))
            };
            HttpResult result = ProductAPIController.ProductMerge(ViewBag.Product);
            if (result.Result)
            {
                ViewBag.Success = true;
            }
            else
            {
                ViewBag.Error = result.Message;
            }
            ViewBag.CategoryList = CategoryAPIController.getAllCategoryAdmin();
            foreach (Category c in ViewBag.CategoryList)
            {
                if (c.CategoryID == ViewBag.Product.CategoryID)
                {
                    ViewBag.Product.CategoryName = c.CategoryName;
                }
            }
            return View();
        }
    }
}