using Newtonsoft.Json;
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
        public Cart GetCart(int cartID)
        {
            return CartProvider.getCart(dbConnection,cartID);
        }

        [HttpGet]
        [Route("CartAPI/getCartValue")]
        public object GetCartValue(int cartID)
        {
            return JsonConvert.SerializeObject(CartProvider.GetCartValue(dbConnection, cartID));
        }

        [HttpPost]
        [HttpPut]
        [Route("CartAPI/addCart")]
        public HttpResult AddCart(CartItem cart, int cartID)
        {
            return CartProvider.addCart(dbConnection,cart,cartID);
        }

        [HttpPost]
        [Route("CartAPI/buyCart")]
        public HttpResult BuyCart(int cartID, Order order)
        {
            return CartProvider.buy(dbConnection, cartID, order);
        }
    }
}