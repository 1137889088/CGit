using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGit.Models
{
    public class Comment
    {
        public int commentId { get; set; }
        public int repositoryId { get; set; }
        public String userEmail { get; set; }
        public String content { get; set; }
        public String data { get; set; }
    }
}
