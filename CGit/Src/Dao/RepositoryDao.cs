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
    class RepositoryDao : BaseDao<Repository>
    {
        public static String dataBase = "CGit";
        /// <summary>
        /// 将数据库记录封装成User对象的方法
        /// </summary>
        /// <param name="reader">SqlDataReader类</param>
        /// <returns>封装好的一个User对象</returns>
        public Repository userHandler(SqlDataReader reader)
        {
            Repository repository = new Repository();
            repository.id = reader.GetString(0);
            repository.email = reader.GetString(1);
            repository.name = reader.GetString(2);
            repository.describe = reader.GetString(3);
            repository.language = reader.GetString(4);
            return repository;
        }
        /// <summary>
        /// 查询所有仓库
        /// </summary>
        /// <returns>根据email查找所有仓库，如果未找到Count为0</returns>
        public List<Repository> findAllRepositoryByEmail(string email)
        {
            String sql = "SELECT * from CGit.repository where email= @email";
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@email", email));
            List<Repository> list = query(dataBase, sql, sqlParameters, userHandler);
            if (list.Count > 0)
            {
                return list;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 根据id删除仓库
        /// </summary>
        /// <returns</returns>
        public int deleteRepositoryById(string id)
        {
            String sql = "delete from CGit.repository where repository_id= @id";
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@id", id));
            return update(dataBase, sql, sqlParameters);
        }
    }
}
