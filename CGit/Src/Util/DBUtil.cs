using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGit.Src.Util
{
    class DBUtil
    {
        public static readonly string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
        public static SqlConnection getConn()
        {
            return new SqlConnection(connStr); ;
        }
    }
}
