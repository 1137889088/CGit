using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGit.Src.Constant
{
    public static class ConfigConstant
    {
        public static readonly string headImgPath = System.Configuration.ConfigurationManager.AppSettings["headImgPath"];
        public static readonly string repositoryPath = System.Configuration.ConfigurationManager.AppSettings["repositoryPath"];
    }
}
