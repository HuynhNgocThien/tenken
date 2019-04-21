using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenkenWeb.Models
{
    public class HttpResult
    {
        public int ID { get; set; }
        public bool Result { get; set; }
        public string Message { get; set; }
    }
}
