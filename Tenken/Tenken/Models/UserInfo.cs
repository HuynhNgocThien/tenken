using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TenkenWeb.Models;

namespace Tenken.Models
{
    public class UserInfo
    {
        public User user { get; set; }
        public Address address { get; set; }
    }
}