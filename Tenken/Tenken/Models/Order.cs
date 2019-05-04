using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tenken.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public Address Address { get; set; }
        public string DeliveryStatus { get; set; }
        public string PaymentStatus { get; set; }
    }
}