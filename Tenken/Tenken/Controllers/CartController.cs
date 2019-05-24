using System.Web.Mvc;
using Tenken.Controllers;
using Tenken.Models;
using TenkenWeb.Models;

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
        public ActionResult Order(int cartID)
        {
            ViewBag.Error = false;
            ViewBag.CartID = cartID;
            return View();
        }

        [HttpPost]
        public ActionResult Order(int cartID, string address, string phoneNumber)
        {
            Address addressModel = new Address()
            {
                AddressName = address,
                PhoneNumber = phoneNumber
            };
            Order order = new Order()
            {
                Address = addressModel,
                DeliveryStatus = "Waiting",
                PaymentStatus = "Delivery to receive"
            };
            ViewBag.Error = false;
            HttpResult result = CartAPIController.BuyCart(cartID, order);
            if (result.Result)
            {
                ViewBag.Success = true;
            }
            else
            {
                ViewBag.Error = result.Message;
            }
            ViewBag.CartID = cartID;
            ViewBag.OrderID = result.ID;

            return View();
        }
        public ActionResult OrderStatus(int orderID)
        {
            ViewBag.Order = CartAPIController.GetOrder(orderID);
            ViewBag.OrderItem = CartAPIController.GetOrderItem(orderID);
            double total = 0;
            foreach (OrderItem item in ViewBag.OrderItem)
            {
                total += item.PricePerProduct;
            }
            ViewBag.TotalPrice = total;
            return View();
        }
    }
}