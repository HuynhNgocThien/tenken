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
        [Route("ProductAPI/getAllProduct")]
        public IList<Product> GetAllProduct()
        {
            return ProductProvider.getProduct(dbConnection, "", 0);
        }

        [HttpGet]
        [Route("ProductAPI/getProduct")]
        public IList<Product> GetProduct(int productID, string productName)
        {
            return ProductProvider.getProduct(dbConnection, productName, productID);
        }

        [HttpGet]
        [Route("ProductAPI/getProductByCategory")]
        public IList<Product> GetProductByCategory(int categoryID)
        {
            return ProductProvider.getProductByCategory(dbConnection, categoryID);
        }

        [HttpPost]
        [HttpPut]
        [Route("ProductAPI/productMerge")]
        public HttpResult ProductMerge(Product product)
        {
            return ProductProvider.productMerge(dbConnection,product);
        }
    }
}