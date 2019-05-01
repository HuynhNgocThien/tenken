using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TenkenWeb.Models;

namespace Tenken.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int StockQuantity { get; set; }
        public int CategoryID { get; set; }
    }
}