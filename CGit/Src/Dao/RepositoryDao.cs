﻿using CGit.Dao;
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
            repository.id = reader.GetInt32(0);
            repository.email = reader.GetString(1);
            repository.name = reader.GetString(2);
            repository.describe = reader.GetString(3);
            repository.language = reader.GetString(4);
            return repository;
        }

        /// <summary>
        /// 查询所有仓库
        /// </summary>
        /// <returns>根据email查找所有仓库，如果未找到返回null</returns>
        public List<Repository> findAllRepositoryByEmail(string email)
        {
            String sql = "SELECT * from CGit.repository where email= @email";
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@email", email));
            List<Repository> list = query(dataBase, sql, sqlParameters, userHandler);
            return list;
        }
        /// <summary>
        /// 根据id查找仓库
        /// </summary>
        /// <returns>如果</returns>
        public Repository findRepositoryById(string id)
        {
            String sql = "SELECT * from CGit.repository where repository_id= @id";
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@id", id));
            List<Repository> list = query(dataBase, sql, sqlParameters, userHandler);
            if (list != null)
            {
                return list[0];
            }
            return null;
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
        /// <summary>
        /// 保存用户
        /// </summary>
        /// <param name="repository">需要保存的仓库</param>
        /// <returns></returns>
        public int save(Repository repository)
        {
            String sql = "INSERT INTO CGit.repository (email, repository_name, repository_describe, repository_language) VALUES(@email, @name, @describe, @language)";
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@email", repository.email));
            sqlParameters.Add(new SqlParameter("@name", repository.name));
            sqlParameters.Add(new SqlParameter("@describe", repository.describe));
            sqlParameters.Add(new SqlParameter("@language", repository.language));
            return update(dataBase, sql, sqlParameters);
        }
        /// <summary>
        /// 保存用户
        /// </summary>
        /// <param name="repository">需要保存的仓库</param>
        /// <returns></returns>
        public int saveAndReturnId(Repository repository)
        {
            String sql = "INSERT INTO CGit.repository (email, repository_name, repository_describe, repository_language) VALUES(@email, @name, @describe, @language) SELECT @@IDENTITY AS ID";
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@email", repository.email));
            sqlParameters.Add(new SqlParameter("@name", repository.name));
            sqlParameters.Add(new SqlParameter("@describe", repository.describe));
            sqlParameters.Add(new SqlParameter("@language", repository.language));
            return saveAndReturnID(dataBase, sql, sqlParameters);
        }
        /// <summary>
        /// 保存用户
        /// </summary>
        /// <param name="repository">需要保存的仓库</param>
        /// <returns></returns>
        public int updataName(string name, string id)
        {
           
            String sql = "update CGit.repository set repository_name =@name WHERE repository_id = @id";
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@id", id));
            sqlParameters.Add(new SqlParameter("@name", name));
            return update(dataBase, sql, sqlParameters);
        }

        public List<Repository> searchRepository(string keyWord)
        {
            String sql = "SELECT * FROM CGit.repository WHERE repository_name like '%" + keyWord + "%' OR repository_describe like '%" + keyWord + "%' OR repository_language like '%" + keyWord + "%'";
            return query(dataBase, sql, null, userHandler);
        }
    }
}
