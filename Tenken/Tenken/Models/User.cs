using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenkenWeb.Models
{
    public class User
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int CartID { get; set; }
    }
}
