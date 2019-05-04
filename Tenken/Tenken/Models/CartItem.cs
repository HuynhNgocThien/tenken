using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tenken.Models
{
    public class CartItem
    {
        public int ProductInfoID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double PricePerProduct { get; set; }
    }
}