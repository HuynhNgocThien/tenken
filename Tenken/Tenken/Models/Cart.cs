using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenkenWeb.Models
{
    public class Cart
    {
        public int cartID { get; set; }
        public int BookID { get; set; }
        public string BookName { get; set; }
        public int quantity { get; set; }
        public int totalPrice { get; set; }
    }
}
