using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGit.Models
{
    public class MVersion
    {
        public int versionId { get; set; }
        public int repositoryId { get; set; }
        public String versionIdentity { get; set; }
        public String versionDate { get; set; }
    }
}
