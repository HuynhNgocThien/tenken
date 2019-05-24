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
    public class ProductAPIController : Controller
    {
        static SqlConnection dbConnection = DBProvider.getDbConnection();

        [HttpGet]
        [Route("ProductAPI/GetTopProduct")]
        public static IList<Product> GetTopProduct(string typeOfTop)
        {
            IList<Product> result = new List<Product>();
            switch (typeOfTop)
            {
                case "New":
                    result = ProductProvider.getTopProduct(dbConnection,0);
                    break;
            }
            return result;
        }
        [HttpGet]
        [Route("ProductAPI/getAllProduct")]
        public static IList<Product> GetAllProduct()
        {
            return ProductProvider.getProduct(dbConnection);
        }

        [HttpGet]
        [Route("ProductAPI/getProduct")]
        public static Product GetProductByID(int productID)
        {
            return ProductProvider.getProductByID(dbConnection, productID);
        }

        [HttpGet]
        [Route("ProductAPI/getProduct")]
        public static object GetProductByName(string productName)
        {
            return JsonConvert.SerializeObject(ProductProvider.getProduct(dbConnection, productName));
        }

        [HttpGet]
        [Route("ProductAPI/getProductByCategory")]
        public static IList<Product> GetProductByCategory(int categoryID)
        {
            return ProductProvider.getProductByCategory(dbConnection, categoryID);
        }

        [HttpPost]
        [HttpPut]
        [Route("ProductAPI/productMerge")]
        public static HttpResult ProductMerge(Product product)
        {
            return ProductProvider.productMerge(dbConnection,product);
        }

        [HttpPost]
        [Route("ProductAPI/productDelete")]
        public object ProductDelete(int productID)
        {
            return JsonConvert.SerializeObject(ProductProvider.productDelete(dbConnection, productID));
        }
    }
}