using System.Web.Mvc;
using Tenken.Models;

namespace Tenken.Controllers
{
    public class CartController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.ProductList = ProductAPIController.GetTopProduct("New");
            ViewBag.Size = ViewBag.ProductList.Count;
            return View();
        }
    }
}