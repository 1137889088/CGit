using CGit.Dao;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGit.Src.Dao
{
    public class FollowRepositoryDao:BaseDao<string>
    {
        public static String dataBase = "CGit";
        /// <summary>
        /// 将数据库记录封装成Repository对象的方法
        /// </summary>
        /// <param name="reader">SqlDataReader类</param>
        /// <returns>封装好的一个Repository对象</returns>
        public string followRepositoryHandler(SqlDataReader reader)
        {
            return reader.GetString(1);
        }
        public string followRepositoryCountHandler(SqlDataReader reader)
        {
            return reader.GetString(0);
        }
        /// <summary>
        /// 查询关注用户的编号
        /// </summary>
        /// <returns>根据email查找查询关注用户编号，如果未找到返回null</returns>
        public List<string> findAllFollowRepositoryByRepositoryEmail(string userEmail)
        {
            String sql = "SELECT * from CGit.followRepository where user_email= @user_email";
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@user_email", userEmail));
            List<string> list = query(dataBase, sql, sqlParameters, followRepositoryHandler);
            return list;
        }

        /// <summary>
        /// 保存关注
        /// </summary>
        /// <param name="comment">需要保存的仓库</param>
        /// <returns></returns>
        public int save(string userEmail, string followEmail)
        {
            String sql = "INSERT INTO CGit.followRepository (user_email,follow_email) VALUES(@user_email,@follow_email)";
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@user_email", userEmail));
            sqlParameters.Add(new SqlParameter("@follow_email", followEmail));
            return update(dataBase, sql, sqlParameters);
        }
        public int delete(string userEmail, string followEmail)
        {
            String sql = "delete from CGit.followRepository where user_email = @user_email and follow_email = @follow_email";
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@user_email", userEmail));
            sqlParameters.Add(new SqlParameter("@follow_email", followEmail));
            return update(dataBase, sql, sqlParameters);
        }
        /// <summary>
        /// 保存关注
        /// </summary>
        /// <param name="comment">需要保存的仓库</param>
        /// <returns></returns>
        public bool isFollow(string userEmail, string followEmail)
        {
            String sql = "SELECT * from CGit.followRepository where user_email= @user_email and follow_email=@follow_email";
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@user_email", userEmail));
            sqlParameters.Add(new SqlParameter("@follow_email", followEmail));
            List<string> list = query(dataBase, sql, sqlParameters, followRepositoryHandler);
            if (list != null && list.Count > 0)
            {
                return true;
            }
            return false;
        }
        public List<string> findRepositoryAndCount()
        {
            String sql = "SELECT TOP 30 CGit.followRepository.follow_email, Count(CGit.followRepository.follow_email) as num FROM CGit.followRepository GROUP BY CGit.followRepository.follow_email ORDER BY num DESC";
            return query(dataBase, sql, null, followRepositoryCountHandler);
        }
    }
}
