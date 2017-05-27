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
    public class CommentDao:BaseDao<Comment>
    {
        public static String dataBase = "CGit";
        /// <summary>
        /// 将数据库记录封装成User对象的方法
        /// </summary>
        /// <param name="reader">SqlDataReader类</param>
        /// <returns>封装好的一个User对象</returns>
        public Comment userHandler(SqlDataReader reader)
        {
            Comment comment = new Comment();
            comment.commentId = reader.GetInt32(0);
            comment.repositoryId = reader.GetInt32(1);
            comment.userEmail = reader.GetString(2);
            comment.content = reader.GetString(3);
            comment.data= reader.GetString(4);
            return comment;
        }

        /// <summary>
        /// 查询所有评论
        /// </summary>
        /// <returns>根据email查找所有评论，如果未找到返回null</returns>
        public List<Comment> findAllcommentByRepositoryId(string repositoryId)
        {
            String sql = "SELECT * from CGit.comment where repository_id= @repositoryId";
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@repositoryId", repositoryId));
            List<Comment> list = query(dataBase, sql, sqlParameters, userHandler);
            return list;
        }

        /// <summary>
        /// 保存用户
        /// </summary>
        /// <param name="comment">需要保存的仓库</param>
        /// <returns></returns>
        public int save(Comment comment)
        {
            String sql = "INSERT INTO CGit.comment (repository_id, user_email, content, data) VALUES(@repository_id, @user_email, @content, @data)";
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@repository_id", comment.repositoryId));
            sqlParameters.Add(new SqlParameter("@user_email", comment.userEmail));
            sqlParameters.Add(new SqlParameter("@content", comment.content));
            sqlParameters.Add(new SqlParameter("@data", comment.data));
            return update(dataBase, sql, sqlParameters);
        }

    }
}
