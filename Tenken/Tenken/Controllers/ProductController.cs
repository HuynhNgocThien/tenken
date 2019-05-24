using System.Web.Mvc;
using Tenken.Models;

namespace Tenken.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult ProductDetail(int productID)
        {
            ViewBag.Product = ProductAPIController.GetProductByID(productID);
            ViewBag.Comment = CommentAPIController.GetComment(ViewBag.Product.ProductID);
            ViewBag.QuantityBuy = 1;
            return View();
        }
        public ActionResult Category(int categoryID)
        {
            ViewBag.ProductList = ProductAPIController.GetProductByCategory(categoryID);
            ViewBag.Size = ViewBag.ProductList.Count;
            if (ViewBag.Size > 0)
            {
                ViewBag.CategoryName = ViewBag.ProductList[0].CategoryName;
            }
            return View();
        }
    }
}