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
    public class VersionDao : BaseDao<MVersion>
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
            version.versionId = reader.GetInt32(0);
            version.repositoryId = reader.GetInt32(1);
            version.versionIdentity = reader.GetString(2);
            version.versionDate = reader.GetString(3);
            return version;
        }

        /// <summary>
        /// 根据repositoryId查询所有版本
        /// </summary>
        /// <returns>根据repositoryId查找所有version，如果未找到返回null</returns>
        public List<MVersion> findVersionByRepositoryId(string repositoryId)
        {
            String sql = "SELECT * from CGit.version where repository_id= @repositoryId ORDER BY version_date DESC";
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@repositoryId", repositoryId));
            List<MVersion> list = query(dataBase, sql, sqlParameters, versionHandler);
            return list;
        }

        /// <summary>
        /// 新建版本
        /// </summary>
        /// <param name="repository">需要保存版本</param>
        /// <returns>插入生成的的主键</returns>
        /// 
        public int saveAndReturn(MVersion version)
        {
            String sql = "INSERT INTO CGit.repository (repository_id, version_identity, version_date) VALUES(@repository_id, @version_identity, @version_date)SELECT @@IDENTITY AS ID";
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@repository_id", version.repositoryId));
            sqlParameters.Add(new SqlParameter("@version_identity", version.versionIdentity));
            sqlParameters.Add(new SqlParameter("@version_date", version.versionDate));
            return saveAndReturnID(dataBase, sql, sqlParameters);
        }

    }
}
