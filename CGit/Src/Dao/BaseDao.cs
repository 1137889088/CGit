 using CGit.Src.Util;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
/// <summary>
/// 数据库操作类简单封装
/// </summary>
namespace CGit.Dao
{
    public class BaseDao<T>
    {
        public delegate T queryDelegate(SqlDataReader reader);
    
        /// <summary>
        /// 对数据库进行更新操作
        /// </summary>
        /// <param name="sql">更新的SQL语句</param>
        /// <param name="dataBase">要更新的数据库</param>
        /// <param name="sqlParameters">传入的参数</param>
        /// <returns>改变的行数</returns>
        public int update(String dataBase, String sql, List<SqlParameter> sqlParameters)
        {
            int colum = 0;
            SqlConnection conn = DBUtil.getConn();
            conn.Open();
            conn.ChangeDatabase(dataBase);
            SqlTransaction sqlTransaction = conn.BeginTransaction();
            try
            {
                SqlCommand command = conn.CreateCommand();
                command.CommandText = sql;
                command.Transaction = sqlTransaction;
                if (sqlParameters != null)
                {
                    foreach (SqlParameter parameter in sqlParameters)
                    {
                        command.Parameters.Add(parameter);
                    }                
                }
                colum = command.ExecuteNonQuery();
                sqlTransaction.Commit();
            }
            catch (Exception e)
            {
                sqlTransaction.Rollback();
            }
            finally
            {
                conn.Close();
                sqlTransaction.Dispose();
                conn.Dispose();
            }
            return colum;
        }



        /// <summary>
        /// 对数据库进行更新操作
        /// </summary>
        /// <param name="sql">更新的SQL语句</param>
        /// <param name="dataBase">要更新的数据库</param>
        /// <param name="sqlParameters">传入的参数</param>
        /// <returns>插入的主键</returns>
        public int saveAndReturnID(String dataBase, String sql, List<SqlParameter> sqlParameters)
        {
            int colum = 0;
            SqlConnection conn = DBUtil.getConn();
            conn.Open();
            conn.ChangeDatabase(dataBase);
            SqlTransaction sqlTransaction = conn.BeginTransaction();
            try
            {
                SqlCommand command = conn.CreateCommand();
                command.CommandText = sql;
                command.Transaction = sqlTransaction;
                if (sqlParameters != null)
                {
                    foreach (SqlParameter parameter in sqlParameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                }
                colum = int.Parse(command.ExecuteScalar().ToString());
                sqlTransaction.Commit();    
            }
            catch (Exception e)
            {
                sqlTransaction.Rollback();
            }
            finally
            {
                conn.Close();
                sqlTransaction.Dispose();
                conn.Dispose();
            }
            return colum;
        }
        /// <summary>
        /// 对数据库进行查询操作
        /// 这里使用了委托
        /// 需要传入一个方法
        /// 这个方法的参数为单个SqlDataReader类型
        /// 返回值为封装好的单个实体类
        /// 例如
        /// public User userHandler(SqlDataReader reader){
        ///     User user = new User();
        ///     user.name = reader.GetString(0);
        ///     user.pwd = reader.GetString(1);
        ///     return user;
        /// }
        /// </summary>
        /// <param name="dataBase">要操作的数据库</param>
        /// <param name="sql">进行查询的SQL语句</param>
        /// <param name="sqlParameters">传入的参数</param>
        /// <param name="method">
        ///     将查询的数据封装成实体类的方法
        /// </param>
        /// <returns>封装好的List，如果没有查询到返回null</returns>
        public List<T> query(String dataBase, String sql, List<SqlParameter> sqlParameters,queryDelegate handler)
        {
            List<T> result = new List<T>();
            SqlConnection conn = DBUtil.getConn();
            conn.Open();
            conn.ChangeDatabase(dataBase);
            SqlTransaction sqlTransaction = conn.BeginTransaction();
            try
            {
                SqlCommand command = conn.CreateCommand();
                command.CommandText = sql;
                command.Transaction = sqlTransaction;
                if (sqlParameters != null)
                {
                    foreach (SqlParameter parameter in sqlParameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                }
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(handler(reader));
                }
                if (reader != null)
                    reader.Close();
            }
            catch (Exception e)
            {
                sqlTransaction.Rollback();
                throw new Exception(e.ToString());
            }
            finally
            {
                sqlTransaction.Commit();
                conn.Close();
                sqlTransaction.Dispose();
                conn.Dispose();
            }
            if(result.Count > 0)
            {
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}