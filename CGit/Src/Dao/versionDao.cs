using CGit.Dao;
using CGit.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGit.Src.Dao
{
    public class VersionDao: BaseDao<MVersion>
    {
        public static String dataBase = "CGit";

        /// <summary>
        /// 将数据库记录封装成MVersion对象的方法
        /// </summary>
        /// <param name="reader">SqlDataReader类</param>
        /// <returns>封装好的一个User对象</returns>
        public MVersion versionHandler(SqlDataReader reader)
        {
            MVersion version = new MVersion();
            version.version_id = reader.GetInt32(0);
            version.repository_id = reader.GetInt32(1);
            version.version_identity = reader.GetString(2);
            version.version_date = reader.GetString(3);
            return version;
        }

        /// <summary>
        /// 根据repositoryId查询所有版本
        /// </summary>
        /// <returns>根据repositoryId查找所有version，如果未找到返回null</returns>
        public List<MVersion> findAllVersionByRepositoryId(int repositoryId)
        {
            String sql = "SELECT * from CGit.version where repositoryId= @repositoryId";
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@repositoryId", repositoryId));
            List<MVersion> list = query(dataBase, sql, sqlParameters, versionHandler);
            return list;
        }

        /// <summary>
        /// 新建版本
        /// </summary>
        /// <param name="repository">需要保存的仓库</param>
        /// <returns>影响的行</returns>
        /// 
        public int save(MVersion version)
        {
            String sql = "INSERT INTO CGit.repository (repository_id, version_identity, version_date) VALUES(@repository_id, @version_identity, @version_date)";
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@repository_id", version.version_id));
            sqlParameters.Add(new SqlParameter("@version_identity", version.version_identity));
            sqlParameters.Add(new SqlParameter("@version_date", version.version_date));
            return update(dataBase, sql, sqlParameters);
        }

    }
}
