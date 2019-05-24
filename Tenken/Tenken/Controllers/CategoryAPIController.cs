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
    public class CategoryAPIController : Controller
    {
        static SqlConnection dbConnection = DBProvider.getDbConnection();

        [HttpGet]
        [Route("CategoryAPI/getAllCategory")]
        public object getAllCategory()
        {
            return JsonConvert.SerializeObject(CategoryProvider.getCategory(dbConnection, "", 0));
        }
        [HttpGet]
        [Route("CategoryAPI/getCategory")]
        public static IList<Category> getCategory(int categoryID, string categoryName)
        {
            return CategoryProvider.getCategory(dbConnection,categoryName,categoryID);
        }

        [HttpPost]
        [HttpPut]
        [Route("CategoryAPI/categoryMerge")]
        public static HttpResult CategoryMerge(Category category)
        {
            return CategoryProvider.categoryMerge(dbConnection,category);
        }

        public static IList<Category> getAllCategoryAdmin()
        {
            return CategoryProvider.getCategory(dbConnection, "", 0);
        }

        [HttpPost]
        [Route("CategoryAPI/categoryDelete")]
        public object CategoryDelete(int categoryID)
        {
            return JsonConvert.SerializeObject(CategoryProvider.CategoryDelete(dbConnection,categoryID));
        }
    }
}