using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tenken.Models;

namespace TenkenWeb.Models
{
    public class Cart
    {
        public List<CartItem> CartItem { get; set; }
        public double TotalPrice { get; set; }
    }
}
