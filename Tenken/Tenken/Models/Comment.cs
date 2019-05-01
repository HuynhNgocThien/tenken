using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tenken.Models
{
    public class Comment
    {
        public int CommentID { get; set; }
        public int ProductID { get; set; }
        public int UserID { get; set; }
        public string Content { get; set; }
        public int Reply { get; set; }
        public string UserName { get; set; }
        public int Rating { get; set; }
    }
}