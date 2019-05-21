using System.Web.Mvc;
using Tenken.Models;

namespace Tenken.Controllers
{
    public class CartController : Controller
    {
        public ActionResult Cart(int CartID)
        {
            ViewBag.Cart = CartAPIController.GetCart(CartID);
            ViewBag.Size = ViewBag.Cart.CartItem.Count;
            return View();
        }
    }
}