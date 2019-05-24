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
    public class CartAPIController : Controller
    {
        static SqlConnection dbConnection = DBProvider.getDbConnection();

        [HttpGet]
        [Route("CartAPI/getCart")]
        public static Cart GetCart(int cartID)
        {
            return CartProvider.getCart(dbConnection, cartID);
        }

        [HttpGet]
        [Route("CartAPI/getCartValue")]
        public object GetCartValue(int cartID)
        {
            return JsonConvert.SerializeObject(CartProvider.GetCartValue(dbConnection, cartID));
        }

        [HttpPost]
        [Route("CartAPI/AddCart")]
        public object AddCart(int ProductID,int Quantity,int CartID)
        {
            return JsonConvert.SerializeObject(CartProvider.addCart(dbConnection, ProductID, Quantity, CartID));
        }

        [HttpPost]
        [Route("CartAPI/buyCart")]
        public static HttpResult BuyCart(int cartID, Order order)
        {
            return CartProvider.buy(dbConnection, cartID, order);
        }

        [HttpGet]
        [Route("CartAPI/getOrder")]
        public static Order GetOrder(int orderID)
        {
            return CartProvider.getOrder(dbConnection, orderID);
        }

        [HttpGet]
        [Route("CartAPI/getOrderItem")]
        public static List<OrderItem> GetOrderItem(int orderID)
        {
            return CartProvider.getOrderItem(dbConnection, orderID);
        }

        [HttpPost]
        [Route("CartAPI/Remove")]
        public object Remove(int ProductInfoID, int CartID)
        {
            return JsonConvert.SerializeObject(CartProvider.removeCartItem(dbConnection, ProductInfoID, CartID));
        }
    }
}